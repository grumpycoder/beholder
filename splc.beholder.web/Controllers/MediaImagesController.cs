using Caseiro.Mvc.PagedList.Extensions;
using splc.beholder.web.Utility;
using splc.data.repository;
using splc.domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class MediaImagesController : BaseController
    {
        private readonly ILookupRepository _lookupRepo;
        private readonly IMediaImageRepository _mediaImageRepo;

        public MediaImagesController(
            ILookupRepository lookupRepo,
            IMediaImageRepository mediaImageRepo)
        {
            _lookupRepo = lookupRepo;
            _mediaImageRepo = mediaImageRepo;

            ViewBag.PossibleMediaTypes = _lookupRepo.GetMediaTypes();
            ViewBag.PossibleImageTypes = _lookupRepo.GetImageTypes();
            ViewBag.PossibleMovementClasses = _lookupRepo.GetMovementClasses();
            ViewBag.PossibleStates = _lookupRepo.GetStates();
            ViewBag.PossibleRemovalStatuses = _lookupRepo.GetRemovalStatus();
            ViewBag.PossibleMimeTypes = _lookupRepo.GetMimeTypes();
            ViewBag.PossibleApprovalStatus = _lookupRepo.GetApprovalStatuses();
            ViewBag.PossiblePrimaryStatuses = _lookupRepo.GetPrimaryStatuses();
            ViewBag.PossibleRenewalPermissionTypes = _lookupRepo.RenewalPermissionTypes();
            ViewBag.PossibleNewsSources = _lookupRepo.GetNewsSources();
        }

        public ViewResult Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim();
            var pred = PredicateBuilder.True<MediaImage>().And(p => p.PhotographerArtist.Contains(searchTerm) || p.ImageTitle.Contains(searchTerm));

            IQueryable<MediaImage> list = _mediaImageRepo.GetMediaImages(currentUser, pred);

            return View("Index", list);
        }

        //
        // GET: /Organizations/
        //public ViewResult Index()
        public ActionResult Index(int? activeyear, string imagetitle = "", string comment = "", string location = "", string artist = "", List<int> movementclassid = null, string movementclassid_string = "", List<int> stateid = null, string stateid_string = "", int? page = 1, int? pageSize = 15)
        {
            if (!string.IsNullOrWhiteSpace(movementclassid_string)) { movementclassid = movementclassid_string.Split(',').Select(int.Parse).ToList(); }
            if (!string.IsNullOrWhiteSpace(stateid_string)) { stateid = stateid_string.Split(',').Select(int.Parse).ToList(); }

            imagetitle = imagetitle.Trim();
            comment = comment.Trim();
            location = location.Trim();
            artist = artist.Trim();

            Session["stateid"] = stateid;
            Session["movementclassid"] = movementclassid;
            Session["imagetitle"] = imagetitle;
            Session["activeyear"] = activeyear;
            Session["comment"] = comment;
            Session["location"] = location;
            Session["artist"] = artist;
            Session["page"] = page;
            Session["pageSize"] = pageSize;

            var pred = PredicateBuilder.True<MediaImage>();
            if (movementclassid != null) pred = pred.And(p => movementclassid.Contains((int)p.MovementClassId));
            if (!string.IsNullOrWhiteSpace(imagetitle)) pred = pred.And(p => p.ImageTitle.Contains(imagetitle));
            if (!string.IsNullOrWhiteSpace(location)) pred = pred.And(p => p.City.Contains(location));
            if (stateid != null) pred = pred.And(p => stateid.Contains((int)p.StateId));
            if (!string.IsNullOrWhiteSpace(comment)) pred = pred.And(p => p.MediaImageComments.Any(c => c.Comment.Contains(comment)));
            if (!string.IsNullOrWhiteSpace(artist)) pred = pred.And(p => p.PhotographerArtist.Contains(artist));

            var list = _mediaImageRepo.GetMediaImages(currentUser, pred).OrderByDescending(m => m.DateCreated).ToPagedList(page ?? 1, pageSize ?? 15);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaImageList", list);
            }

            return View("Index", list);

        }

        // GET: Search for BeholderPersons 
        public JsonResult GetMediaImageList(string term)
        {
            term = term.Trim();
            var list = _mediaImageRepo.GetMediaImages(currentUser, p => p.ImageTitle.Contains(term)).ToArray().Select(
                e => new
                {
                    Id = e.Id,
                    label = e.ImageTitle
                });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MediaImages/Details/5
        public ViewResult Details(int id)
        {
            ViewBag.OrganizationId = -1;
            ViewBag.ChapterId = -1;
            ViewBag.BeholderPersonId = -1;
            ViewBag.PersonId = -1;
            ViewBag.EventId = -1;
            ViewBag.VehicleId = -1;
            ViewBag.MediaImageId = id;
            ViewBag.MediaAudioVideoId = -1;
            ViewBag.MediaWebsiteEGroupId = -1;
            ViewBag.MediaCorrespondenceId = -1;
            ViewBag.SubscriptionId = -1;
            ViewBag.Controller = "MediaImages";

            var image = _mediaImageRepo.GetMediaImage(currentUser, id);
            if (image != null)
            {
                return View(_mediaImageRepo.GetMediaImage(currentUser, id));
            }
            return View("MediaImage404");

        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            var filePath = Path.Combine(Server.MapPath("~/Content/upload/"), file.FileName);
            System.IO.File.WriteAllBytes(Uri.UnescapeDataString(filePath), ReadData(file.InputStream));

            return Json("All files have been successfully stored.");
        }

        private byte[] ReadData(Stream stream)
        {
            var buffer = new byte[16 * 1024];

            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }


        private bool FileExists(string fileName)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/Content/upload/")))
            {
                return false;
            }

            return System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/upload/"), fileName ?? ""));
        }

        private byte[] CompressFile(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Open);
            byte[] tmp;

            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                stream.Close();
                tmp = ImageHelper.Compress(ms.ToArray(), fileName);
            }
            return tmp;

            ////var filePath = Path.Combine(Server.MapPath("~/Content/upload/"), fileName);
            //if (!System.IO.File.Exists(fileName))
            //{
            //    return null;
            //}
            //FileStream stream = new FileStream(fileName, FileMode.Open);

            //byte[] buffer = new byte[16 * 1024];
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    int read;
            //    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            //    {
            //        ms.Write(buffer, 0, read);
            //    }
            //    var tmp = ImageHelper.Compress(ms.ToArray(), fileName);
            //    return tmp;
            //}
        }

        public ActionResult Create()
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            var image = new Image();
            var mediaImage = new MediaImage
            {
                MediaTypeId = _lookupRepo.GetMediaTypes().SingleOrDefault(p => p.Name.Equals("Image")).Id,
                Image = image
            };
            return View(mediaImage);
        }

        [HttpPost]
        public ActionResult Create(MediaImage mediaimage, HttpPostedFileBase attachment)
        {
            if (ModelState.IsValid)
            {

                if (attachment != null)
                {
                    var fileName = Path.GetFileName(attachment.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    attachment.SaveAs(physicalPath);

                    var mimetypeid = _lookupRepo.GetMimeTypes().SingleOrDefault(p => p.Name.Equals(attachment.ContentType.ToLower())).Id;
                    mediaimage.Image.MimeTypeId = mimetypeid;
                    mediaimage.ImageFileName = fileName;
                    mediaimage.Image.IMAGE1 = CompressFile(physicalPath);
                    mediaimage.Image.MimeType = null;

                }
                _mediaImageRepo.InsertOrUpdate(mediaimage);
                try
                {
                    _mediaImageRepo.Save();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        var temp2 = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            var temp = string.Format("- Property: {0}, Error: {1}", ve.PropertyName, ve.ErrorMessage);
                            //Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            //    ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                return RedirectToAction("Details", new { id = mediaimage.Id });
            }
            var img = new Image()
            {
                ConfidentialityTypeId = _lookupRepo.GetConfidentialityTypes().SingleOrDefault(p => p.Name.Equals("Identity")).Id,
            };
            var entity = new MediaImage
            {
                MediaTypeId = _lookupRepo.GetMediaTypes().SingleOrDefault(p => p.Name.Equals("Image")).Id,
                Image = img,
            };
            return View(entity);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            var mediaImage = _mediaImageRepo.GetMediaImage(currentUser, id);
            if (mediaImage != null)
            {
                //allow add image on edit if not already there.
                if (mediaImage.ImageId == null)
                {
                    mediaImage.Image = new Image();
                }
                return View(mediaImage);
            }

            return View("MediaImage404");
        }

        //
        // POST: /MediaImages/Edit/5
        [HttpPost]
        public ActionResult Edit(MediaImage mediaimage, HttpPostedFileBase attachment)
        {
            if (ModelState.IsValid)
            {
                if (attachment != null)
                {
                    var fileName = Path.GetFileName(attachment.FileName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    attachment.SaveAs(physicalPath);

                    var mimetypeid = _lookupRepo.GetMimeTypes().SingleOrDefault(p => p.Name.Equals(attachment.ContentType.ToLower())).Id;
                    mediaimage.Image.MimeTypeId = mimetypeid;
                    mediaimage.ImageFileName = fileName;
                    mediaimage.Image.IMAGE1 = CompressFile(physicalPath);
                }
                mediaimage.Image.MimeType = null;
                _mediaImageRepo.InsertOrUpdate(mediaimage);
                _mediaImageRepo.Save();
                return RedirectToAction("Details", new { id = mediaimage.Id });
            }
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            return View(mediaimage);
        }

        public ActionResult RemoveImage(int id)
        {
            var img = _mediaImageRepo.GetMediaImage(currentUser, id);
            return View(img);
        }

        [HttpPost]
        public ActionResult RemoveImage(int id, string removalreason)
        {
            var img = _mediaImageRepo.GetMediaImage(currentUser, id);
            img.RemovalReason = removalreason;

            img.RemovalStatusId = 1;
            _mediaImageRepo.InsertOrUpdate(img);
            _mediaImageRepo.Save();

            return RedirectToAction("Index");

        }

        //
        // GET: /MediaImages/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_mediaImageRepo.GetMediaImage(id));

        }

        //
        // POST: /MediaImages/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _mediaImageRepo.Delete(id);
            _mediaImageRepo.Save();

            return RedirectToAction("Index");
        }

        public ImageResult ShowImage(int id)
        {
            var image = _mediaImageRepo.GetImage(id);

            byte[] img = ImageHelper.Decompress(image.IMAGE1);
            string contentType = image.MimeType.Name;

            // Here we are calling the extension method and returning    
            // the ImageResult.
            //return splc.beholder.web.Utility.ControllerExtensions.Image(this, image, contentType);
            return this.Image(img, contentType);
        }

        #region MediaAudioVideos

        public ActionResult GetMediaImageMediaAudioVideos(int mediaImageId, int mediaAudioVideoId)
        {
            IQueryable<MediaImageMediaAudioVideoRel> mediaImageMediaAudioVideos;
            if (mediaAudioVideoId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.Controller = "MediaImages";
                mediaImageMediaAudioVideos = _mediaImageRepo.GetMediaImageMediaAudioVideos(p => p.MediaImage.Id.Equals(mediaImageId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.Controller = "MediaAudioVideos";
                mediaImageMediaAudioVideos = _mediaImageRepo.GetMediaImageMediaAudioVideos(p => p.MediaAudioVideo.Id.Equals(mediaAudioVideoId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaImageMediaAudioVideoList", mediaImageMediaAudioVideos);
            }
            return View(mediaImageMediaAudioVideos);
        }

        //
        // GET: /MediaAudioVideos/CreatePersonMediaAudioVideo
        public ActionResult CreateMediaImageMediaAudioVideo(int mediaImageId, int mediaAudioVideoId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaImageMediaAudioVideoRel = new MediaImageMediaAudioVideoRel
            {
                MediaAudioVideoId = mediaAudioVideoId,
                MediaImageId = mediaImageId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaAudioVideoId == -1)
            {
                mediaImageMediaAudioVideoRel.MediaAudioVideo = new MediaAudioVideo();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaImages";
                ViewBag.MediaImageId = mediaImageId;
            }
            else
            {
                mediaImageMediaAudioVideoRel.MediaImage = new MediaImage();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaAudioVideos";
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaImageMediaAudioVideo", mediaImageMediaAudioVideoRel);
            }

            return View();

        }

        //
        // POST: /PersonMediaAudioVideoRels/CreatePersonMediaAudioVideo
        [HttpPost]
        public ActionResult CreateMediaImageMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaImageId,MediaAudioVideoId")] MediaImageMediaAudioVideoRel mediaImagemediaAudioVideorel)
        {
            if (ModelState.IsValid)
            {
                if (mediaImagemediaAudioVideorel.MediaImage == null)
                {
                    mediaImagemediaAudioVideorel.MediaAudioVideo = null;
                    _mediaImageRepo.InsertOrUpdateMediaImageMediaAudioVideo(mediaImagemediaAudioVideorel);
                    _mediaImageRepo.Save();
                    //return RedirectToAction("Details", "MediaImages", new { id = mediaImagemediaAudioVideorel.MediaImageId });
                    return null;
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaImageOrganizationRel.
                    mediaImagemediaAudioVideorel.MediaImage = null;
                    _mediaImageRepo.InsertOrUpdateMediaImageMediaAudioVideo(mediaImagemediaAudioVideorel);
                    _mediaImageRepo.Save();
                    //return RedirectToAction("Details", "MediaAudioVideos", new { id = mediaImagemediaAudioVideorel.MediaAudioVideoId });
                    return null;
                }
            }
            return View();
        }

        //
        // GET: /MediaAudioVideos/EditPersonMediaAudioVideo/5
        public ActionResult EditMediaImageMediaAudioVideo(int id, string source)
        {
            var mediaImageMediaAudioVideoRel = _mediaImageRepo.GetMediaImageMediaAudioVideo(id);
            if (source == "MediaImages")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaImageMediaAudioVideoRel.MediaImageId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaAudioVideoId = mediaImageMediaAudioVideoRel.MediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaImageMediaAudioVideo", mediaImageMediaAudioVideoRel);
            }
            return View(mediaImageMediaAudioVideoRel);

        }

        //
        // POST: /MediaAudioVideos/EditPersonMediaAudioVideo/5
        [HttpPost]
        public ActionResult EditMediaImageMediaAudioVideo(MediaImageMediaAudioVideoRel mediaImagemediaAudioVideorel)
        {
            if (ModelState.IsValid)
            {
                if (mediaImagemediaAudioVideorel.MediaImage == null)
                {
                    //reset the mediaImage object.  This is only added from Organization, not MediaImageOrganizationRel.
                    mediaImagemediaAudioVideorel.MediaAudioVideo = null;
                    _mediaImageRepo.InsertOrUpdateMediaImageMediaAudioVideo(mediaImagemediaAudioVideorel);
                    _mediaImageRepo.Save();
                    return RedirectToAction("Details", "MediaImages", new { id = mediaImagemediaAudioVideorel.MediaImageId });
                }
                //reset the organization object.  This is only added from Organization, not MediaImageOrganizationRel.
                mediaImagemediaAudioVideorel.MediaImage = null;
                _mediaImageRepo.InsertOrUpdateMediaImageMediaAudioVideo(mediaImagemediaAudioVideorel);
                _mediaImageRepo.Save();
                return RedirectToAction("Details", "MediaAudioVideos", new { id = mediaImagemediaAudioVideorel.MediaAudioVideoId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaImageMediaAudioVideo(int id)
        {
            _mediaImageRepo.DeleteMediaImageMediaAudioVideo(id);
            _mediaImageRepo.Save();
        }

        #endregion


        #region MediaWebsiteEGroups

        public ActionResult GetMediaImageMediaWebsiteEGroups(int mediaImageId, int mediaWebsiteEGroupId)
        {
            IQueryable<MediaImageMediaWebsiteEGroupRel> mediaImageMediaWebsiteEGroups;
            if (mediaWebsiteEGroupId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.Controller = "MediaImages";
                mediaImageMediaWebsiteEGroups = _mediaImageRepo.GetMediaImageMediaWebsiteEGroups(p => p.MediaImage.Id.Equals(mediaImageId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.Controller = "MediaWebsiteEGroups";
                mediaImageMediaWebsiteEGroups = _mediaImageRepo.GetMediaImageMediaWebsiteEGroups(p => p.MediaWebsiteEGroup.Id.Equals(mediaWebsiteEGroupId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaImageMediaWebsiteEGroupList", mediaImageMediaWebsiteEGroups);
            }
            return View(mediaImageMediaWebsiteEGroups);
        }

        //
        // GET: /MediaWebsiteEGroups/CreatePersonMediaWebsiteEGroup
        public ActionResult CreateMediaImageMediaWebsiteEGroup(int mediaImageId, int mediaWebsiteEGroupId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaImageMediaWebsiteEGroupRel = new MediaImageMediaWebsiteEGroupRel
            {
                MediaWebsiteEGroupId = mediaWebsiteEGroupId,
                MediaImageId = mediaImageId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaWebsiteEGroupId == -1)
            {
                mediaImageMediaWebsiteEGroupRel.MediaWebsiteEGroup = new MediaWebsiteEGroup();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaImages";
                ViewBag.MediaImageId = mediaImageId;
            }
            else
            {
                mediaImageMediaWebsiteEGroupRel.MediaImage = new MediaImage();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaWebsiteEGroups";
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaImageMediaWebsiteEGroup", mediaImageMediaWebsiteEGroupRel);
            }

            return View();

        }

        //
        // POST: /PersonMediaWebsiteEGroupRels/CreatePersonMediaWebsiteEGroup
        [HttpPost]
        public ActionResult CreateMediaImageMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaImageId,MediaWebsiteEGroupId")] MediaImageMediaWebsiteEGroupRel mediaImagemediaWebsiteEGrouprel)
        {
            if (ModelState.IsValid)
            {
                if (mediaImagemediaWebsiteEGrouprel.MediaImage == null)
                {
                    mediaImagemediaWebsiteEGrouprel.MediaWebsiteEGroup = null;
                    _mediaImageRepo.InsertOrUpdateMediaImageMediaWebsiteEGroup(mediaImagemediaWebsiteEGrouprel);
                    _mediaImageRepo.Save();
                    //return RedirectToAction("Details", "MediaImages", new { id = mediaImagemediaWebsiteEGrouprel.MediaImageId });
                    return null;
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaImageOrganizationRel.
                    mediaImagemediaWebsiteEGrouprel.MediaImage = null;
                    _mediaImageRepo.InsertOrUpdateMediaImageMediaWebsiteEGroup(mediaImagemediaWebsiteEGrouprel);
                    _mediaImageRepo.Save();
                    //return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = mediaImagemediaWebsiteEGrouprel.MediaWebsiteEGroupId });
                    return null;
                }
            }
            return View();
        }

        //
        // GET: /MediaWebsiteEGroups/EditPersonMediaWebsiteEGroup/5
        public ActionResult EditMediaImageMediaWebsiteEGroup(int id, string source)
        {
            var mediaImageMediaWebsiteEGroupRel = _mediaImageRepo.GetMediaImageMediaWebsiteEGroup(id);
            if (source == "MediaImages")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaImageMediaWebsiteEGroupRel.MediaImageId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaWebsiteEGroupId = mediaImageMediaWebsiteEGroupRel.MediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaImageMediaWebsiteEGroup", mediaImageMediaWebsiteEGroupRel);
            }
            return View(mediaImageMediaWebsiteEGroupRel);

        }

        //
        // POST: /MediaWebsiteEGroups/EditPersonMediaWebsiteEGroup/5
        [HttpPost]
        public ActionResult EditMediaImageMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,MediaWebsiteEGroupId,MediaImageId")] MediaImageMediaWebsiteEGroupRel mediaImagemediaWebsiteEGrouprel)
        {
            if (ModelState.IsValid)
            {
                if (mediaImagemediaWebsiteEGrouprel.MediaImage == null)
                {
                    //reset the mediaImage object.  This is only added from Organization, not MediaImageOrganizationRel.
                    mediaImagemediaWebsiteEGrouprel.MediaWebsiteEGroup = null;
                    _mediaImageRepo.InsertOrUpdateMediaImageMediaWebsiteEGroup(mediaImagemediaWebsiteEGrouprel);
                    _mediaImageRepo.Save();
                    return RedirectToAction("Details", "MediaImages", new { id = mediaImagemediaWebsiteEGrouprel.MediaImageId });
                }
                //reset the organization object.  This is only added from Organization, not MediaImageOrganizationRel.
                mediaImagemediaWebsiteEGrouprel.MediaImage = null;
                _mediaImageRepo.InsertOrUpdateMediaImageMediaWebsiteEGroup(mediaImagemediaWebsiteEGrouprel);
                _mediaImageRepo.Save();
                return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = mediaImagemediaWebsiteEGrouprel.MediaWebsiteEGroupId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaImageMediaWebsiteEGroup(int id)
        {
            _mediaImageRepo.DeleteMediaImageMediaWebsiteEGroup(id);
            _mediaImageRepo.Save();
        }

        #endregion


        #region Vehicles

        public ActionResult GetMediaImageVehicles(int mediaImageId, int vehicleId)
        {
            IQueryable<MediaImageVehicleRel> mediaImageVehicles;
            if (vehicleId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.Controller = "MediaImages";
                mediaImageVehicles = _mediaImageRepo.GetMediaImageVehicles(p => p.MediaImage.Id.Equals(mediaImageId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.Controller = "Vehicles";
                mediaImageVehicles = _mediaImageRepo.GetMediaImageVehicles(p => p.Vehicle.Id.Equals(vehicleId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaImageVehicleList", mediaImageVehicles);
            }
            return View(mediaImageVehicles);
        }

        //
        // GET: /Vehicles/CreatePersonVehicle
        public ActionResult CreateMediaImageVehicle(int vehicleId, int mediaImageId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaImageVehicleRel = new MediaImageVehicleRel
            {
                VehicleId = vehicleId,
                MediaImageId = mediaImageId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (vehicleId == -1)
            {
                mediaImageVehicleRel.Vehicle = new Vehicle();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaImages";
                ViewBag.MediaImageId = mediaImageId;
            }
            else
            {
                mediaImageVehicleRel.MediaImage = new MediaImage();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Vehicles";
                ViewBag.VehicleId = vehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaImageVehicle", mediaImageVehicleRel);
            }

            return View();

        }

        //
        // POST: /PersonVehicleRels/CreatePersonVehicle
        [HttpPost]
        public ActionResult CreateMediaImageVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaImageId,VehicleId")] MediaImageVehicleRel mediaImagevehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (mediaImagevehiclerel.MediaImage == null)
                {
                    mediaImagevehiclerel.Vehicle = null;
                    _mediaImageRepo.InsertOrUpdateMediaImageVehicle(mediaImagevehiclerel);
                    _mediaImageRepo.Save();
                    return null;
                    //return RedirectToAction("Details", "MediaImages", new { id = mediaImagevehiclerel.MediaImageId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaImageOrganizationRel.
                    mediaImagevehiclerel.MediaImage = null;
                    _mediaImageRepo.InsertOrUpdateMediaImageVehicle(mediaImagevehiclerel);
                    _mediaImageRepo.Save();
                    //return RedirectToAction("Details", "Vehicles", new { id = mediaImagevehiclerel.VehicleId });
                    return null;
                }
            }
            return View();
        }

        //
        // GET: /Vehicles/EditPersonVehicle/5
        public ActionResult EditMediaImageVehicle(int id, string source)
        {
            var mediaImageVehicleRel = _mediaImageRepo.GetMediaImageVehicle(id);
            if (source == "MediaImages")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaImageVehicleRel.MediaImageId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.VehicleId = mediaImageVehicleRel.VehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaImageVehicle", mediaImageVehicleRel);
            }
            return View(mediaImageVehicleRel);

        }

        //
        // POST: /Vehicles/EditPersonVehicle/5
        [HttpPost]
        public ActionResult EditMediaImageVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,VehicleId,MediaImageId")] MediaImageVehicleRel mediaImagevehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (mediaImagevehiclerel.MediaImage == null)
                {
                    //reset the mediaImage object.  This is only added from Organization, not MediaImageOrganizationRel.
                    mediaImagevehiclerel.Vehicle = null;
                    _mediaImageRepo.InsertOrUpdateMediaImageVehicle(mediaImagevehiclerel);
                    _mediaImageRepo.Save();
                    return RedirectToAction("Details", "MediaImages", new { id = mediaImagevehiclerel.MediaImageId });
                }
                //reset the organization object.  This is only added from Organization, not MediaImageOrganizationRel.
                mediaImagevehiclerel.MediaImage = null;
                _mediaImageRepo.InsertOrUpdateMediaImageVehicle(mediaImagevehiclerel);
                _mediaImageRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = mediaImagevehiclerel.VehicleId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaImageVehicle(int id)
        {
            _mediaImageRepo.DeleteMediaImageVehicle(id);
            _mediaImageRepo.Save();
        }

        #endregion


        #region Subscriptions

        public ActionResult GetMediaImageSubscriptions(int mediaImageId, int subscriptionId)
        {
            IQueryable<MediaImageSubscriptionRel> mediaImageSubscriptions;
            if (subscriptionId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.Controller = "MediaImages";
                mediaImageSubscriptions = _mediaImageRepo.GetMediaImageSubscriptions(p => p.MediaImage.Id.Equals(mediaImageId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.Controller = "Subscriptions";
                mediaImageSubscriptions = _mediaImageRepo.GetMediaImageSubscriptions(p => p.Subscription.Id.Equals(subscriptionId));
            }

            //if (Request.IsAjaxRequest())
            //{
            return PartialView("_MediaImageSubscriptionList", mediaImageSubscriptions);
            //}
            //return View(mediaImageSubscriptions);
        }

        //
        // GET: /Subscriptions/CreatePersonSubscription
        public ActionResult CreateMediaImageSubscription(int subscriptionId, int mediaImageId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaImageSubscriptionRel = new MediaImageSubscriptionRel
            {
                SubscriptionId = subscriptionId,
                MediaImageId = mediaImageId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (subscriptionId == -1)
            {
                mediaImageSubscriptionRel.Subscription = new Subscription();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaImages";
                ViewBag.MediaImageId = mediaImageId;
            }
            else
            {
                mediaImageSubscriptionRel.MediaImage = new MediaImage();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Subscriptions";
                ViewBag.SubscriptionId = subscriptionId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaImageSubscription", mediaImageSubscriptionRel);
            }

            return View();

        }

        //
        // POST: /PersonSubscriptionRels/CreatePersonSubscription
        [HttpPost]
        public ActionResult CreateMediaImageSubscription([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaImageId,SubscriptionId")] MediaImageSubscriptionRel mediaImagesubscriptionrel)
        {
            if (ModelState.IsValid)
            {
                if (mediaImagesubscriptionrel.MediaImage == null)
                {
                    mediaImagesubscriptionrel.Subscription = null;
                    _mediaImageRepo.InsertOrUpdateMediaImageSubscription(mediaImagesubscriptionrel);
                    _mediaImageRepo.Save();
                    //return RedirectToAction("Details", "MediaImages", new { id = mediaImagesubscriptionrel.MediaImageId });
                    return null;
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaImageOrganizationRel.
                    mediaImagesubscriptionrel.MediaImage = null;
                    _mediaImageRepo.InsertOrUpdateMediaImageSubscription(mediaImagesubscriptionrel);
                    _mediaImageRepo.Save();
                    //return RedirectToAction("Details", "Subscriptions", new { id = mediaImagesubscriptionrel.SubscriptionId });
                    return null;
                }
            }
            return View();
        }

        //
        // GET: /Subscriptions/EditPersonSubscription/5
        public ActionResult EditMediaImageSubscription(int id, string source)
        {
            var mediaImageSubscriptionRel = _mediaImageRepo.GetMediaImageSubscription(id);
            if (source == "MediaImages")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaImageSubscriptionRel.MediaImageId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.SubscriptionId = mediaImageSubscriptionRel.SubscriptionId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaImageSubscription", mediaImageSubscriptionRel);
            }
            return View(mediaImageSubscriptionRel);

        }

        //
        // POST: /Subscriptions/EditPersonSubscription/5
        [HttpPost]
        public ActionResult EditMediaImageSubscription(MediaImageSubscriptionRel mediaImagesubscriptionrel)
        {
            if (ModelState.IsValid)
            {
                if (mediaImagesubscriptionrel.MediaImage == null)
                {
                    //reset the mediaImage object.  This is only added from Organization, not MediaImageOrganizationRel.
                    mediaImagesubscriptionrel.Subscription = null;
                    _mediaImageRepo.InsertOrUpdateMediaImageSubscription(mediaImagesubscriptionrel);
                    _mediaImageRepo.Save();
                    return RedirectToAction("Details", "MediaImages", new { id = mediaImagesubscriptionrel.MediaImageId });
                }
                //reset the organization object.  This is only added from Organization, not MediaImageOrganizationRel.
                mediaImagesubscriptionrel.MediaImage = null;
                _mediaImageRepo.InsertOrUpdateMediaImageSubscription(mediaImagesubscriptionrel);
                _mediaImageRepo.Save();
                return RedirectToAction("Details", "Subscriptions", new { id = mediaImagesubscriptionrel.SubscriptionId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaImageSubscription(int id)
        {
            _mediaImageRepo.DeleteMediaImageSubscription(id);
            _mediaImageRepo.Save();
        }

        #endregion


        #region MediaImages

        public ActionResult GetMediaImageMediaImages(int mediaImageId)
        {
            //MediaImage mediaImages;
            //if (eventId == -1)
            //{
            //ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
            ViewBag.MediaImageId = mediaImageId;
            ViewBag.MediaImage2Id = mediaImageId;
            ViewBag.Controller = "MediaImages";
            //try
            //{
            var mediaImages = _mediaImageRepo.GetMediaImageMediaImages(currentUser, mediaImageId);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}

            //if (Request.IsAjaxRequest())
            //{
            return PartialView("_MediaImageMediaImageList", mediaImages);
            //}
            //return View(mediaImages);
        }

        public ActionResult CreateMediaImageMediaImage(int mediaimageId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaImageMediaImageRel = new MediaImageMediaImageRel
            {
                MediaImageId = mediaimageId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
                MediaImage2 = new MediaImage()
            };

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
            ViewBag.MediaImageId = mediaimageId;
            ViewBag.MediaImage2Id = -1;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaImageMediaImage", mediaImageMediaImageRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateMediaImageMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaImageId,MediaImage2Id")] MediaImageMediaImageRel mediaImageMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                mediaImageMediaImageRel.MediaImage2 = null;
                _mediaImageRepo.InsertOrUpdateMediaImageMediaImage(mediaImageMediaImageRel);
                _mediaImageRepo.Save();
                //return RedirectToAction("Details", "MediaImages", new { id = mediaImageMediaImageRel.MediaImageId });
                return null;
            }
            return View();

        }

        public ActionResult EditMediaImageMediaImage(int id)
        {
            var mediaImageMediaImageRel = _mediaImageRepo.GetMediaImageMediaImage(id);
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaImageMediaImage", mediaImageMediaImageRel);
            }
            return View(mediaImageMediaImageRel);

        }

        //
        // POST: /EventPersonRels/EditEventPerson
        [HttpPost]
        public ActionResult EditMediaImageMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,MediaImage2Id,MediaImageId")] MediaImageMediaImageRel mediaImageMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                _mediaImageRepo.InsertOrUpdateMediaImageMediaImage(mediaImageMediaImageRel);
                _mediaImageRepo.Save();
                return RedirectToAction("Details", "MediaImages", new { id = mediaImageMediaImageRel.MediaImageId });
            }

            return View();
        }

        [HttpPost]
        public void DeleteMediaImageMediaImage(int id)
        {
            _mediaImageRepo.DeleteMediaImageMediaImage(id);
            _mediaImageRepo.Save();
        }

        #endregion

    }
}

