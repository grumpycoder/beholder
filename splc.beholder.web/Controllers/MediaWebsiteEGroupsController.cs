using Caseiro.Mvc.PagedList.Extensions;
using splc.beholder.web.Models;
using splc.beholder.web.Utility;
using splc.data;
using splc.data.repository;
using splc.data.Utility;
using splc.domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class MediaWebsiteEGroupsController : BaseController
    {
        private readonly ILookupRepository _lookupRepo;
        private readonly IMediaWebsiteEGroupRepository _mediaWebsiteEGroupRepo;
        private readonly ACDBContext db;

        public MediaWebsiteEGroupsController(
            ILookupRepository lookupRepo,
            IMediaWebsiteEGroupRepository mediaWebsiteEGroupRepo,
            ACDBContext _db)
        {
            _lookupRepo = lookupRepo;
            _mediaWebsiteEGroupRepo = mediaWebsiteEGroupRepo;
            db = _db;

            ViewBag.PossibleApprovalStatus = _lookupRepo.GetApprovalStatuses();
            ViewBag.PossibleMediaTypes = _lookupRepo.GetMediaTypes();
            ViewBag.PossibleMimeTypes = _lookupRepo.GetMimeTypes();
            ViewBag.PossibleWebsiteGroupTypes = _lookupRepo.GetWebsiteGroupTypes();
            ViewBag.PossibleNewsSources = _lookupRepo.GetNewsSources();
            ViewBag.PossibleMovementClasses = _lookupRepo.GetMovementClasses();
            ViewBag.PossibleRenewalPermissionTypes = _lookupRepo.RenewalPermissionTypes();
            ViewBag.PossiblePrimaryStatuses = _lookupRepo.GetPrimaryStatuses();
            ViewBag.PossibleActiveStatus = _lookupRepo.GetActiveStatuses().OrderBy(x => x.SortOrder);
            ViewBag.PossibleStates = _lookupRepo.GetStates();
            ViewBag.PossibleRemovalStatuses = _lookupRepo.GetRemovalStatus();
        }

        #region Base

        public ActionResult GetContextList(int websiteEgroupId)
        {
            var list = db.MediaWebsiteEGroupContexts.Where(x => x.MediaWebsiteEGroupId == websiteEgroupId).Select(s =>
            new WebsiteContextViewModel()
            {
                Id = s.Id,
                FileName = s.FileName,
                MediaWebsiteEGroupId = s.MediaWebsiteEGroupId
            }).ToList();
            return View("_DocumentList", list);
        }



        [HttpPost]
        public JsonResult SaveTextAsContent(int websiteEgroupId, string filename, string content)
        {
            var context = new MediaWebsiteEGroupContext()
            {
                MediaWebsiteEGroupId = websiteEgroupId,
                FileName = filename,
                ContextText = Encoding.ASCII.GetBytes(content),
                DocumentExtension = "txt",
                MimeTypeId = 22
            };

            try
            {
                db.MediaWebsiteEGroupContexts.Add(context);
                db.SaveChanges();
            }
            catch (ValidationException e)
            {
                var message = e.Message;
            }
            return Json(context, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaveAttachments(IEnumerable<HttpPostedFileBase> attachments, int websiteEgroupId)
        {
            if (attachments != null)
            {
                foreach (var file in attachments)
                {
                    try
                    {

                        var fileName = Path.GetFileName(file.FileName);
                        var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                        file.SaveAs(physicalPath);

                        var mimetypeid = _lookupRepo.GetMimeTypes().SingleOrDefault(p => p.Name.Equals(file.ContentType.ToLower())).Id;
                        var context = new MediaWebsiteEGroupContext()
                        {
                            MediaWebsiteEGroupId = websiteEgroupId,
                            FileName = fileName,
                            ContextText = GetFile(physicalPath),
                            DocumentExtension = Path.GetExtension(physicalPath),
                            MimeTypeId = mimetypeid
                        };
                        try
                        {
                            db.MediaWebsiteEGroupContexts.Add(context);
                            db.SaveChanges();
                            System.IO.File.Delete(physicalPath);
                        }
                        catch (ValidationException e)
                        {
                            return Content(e.Message);
                        }
                    }
                    catch (ValidationException aeException)
                    {
                        return Content(string.Format(@"File contains invalid characters \/:*?""<>|"));
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public JsonResult GetWebsiteSelectList(string term)
        {
            if (term == null)
            {
                term = Request.Params["filter[filters][0][value]"];
            }
            if (string.IsNullOrWhiteSpace(term) || term == "-1")
            {

                var list = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroups(CurrentUser).OrderBy(x => x.Name).Take(50).Select(c => new
                {
                    c.Name,
                    c.Id,
                    c.City,
                    State = c.State != null ? c.State.StateCode : "",
                    Movement = c.MovementClass.Name
                });

                var items = list.ToList().Select(x => new
                {
                    x.Name,
                    x.Id,
                    x.City,
                    x.State,
                    x.Movement,
                    Location = string.Format("{0}{1} {2}", x.City, (!string.IsNullOrWhiteSpace(x.City) && !string.IsNullOrWhiteSpace(x.State)) ? "," : "", x.State)
                });

                return Json(items, JsonRequestBehavior.AllowGet);
            }
            if (term.IsInt())
            {
                var id = Convert.ToInt32(term);

                var list = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroups(CurrentUser, x => x.Id == id).Select(c => new
                {
                    c.Name,
                    c.Id,
                    c.City,
                    State = c.State != null ? c.State.StateCode : "",
                    Movement = c.MovementClass.Name
                });

                var items = list.ToList().Select(x => new
                {
                    x.Name,
                    x.Id,
                    x.City,
                    x.State,
                    x.Movement,
                    Location = string.Format("{0}{1} {2}", x.City, (!string.IsNullOrWhiteSpace(x.City) && !string.IsNullOrWhiteSpace(x.State)) ? "," : "", x.State)
                });

                return Json(items, JsonRequestBehavior.AllowGet);
            }

            var list2 = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroups(CurrentUser, x => x.Name.Contains(term)).OrderBy(x => x.Name).Select(c => new
            {
                c.Name,
                c.Id,
                c.City,
                State = c.State != null ? c.State.StateCode : "",
                Movement = c.MovementClass.Name
            });

            var items2 = list2.ToList().Select(x => new
            {
                x.Name,
                x.Id,
                x.City,
                x.State,
                x.Movement,
                Location = string.Format("{0}{1} {2}", x.City, (!string.IsNullOrWhiteSpace(x.City) && !string.IsNullOrWhiteSpace(x.State)) ? "," : "", x.State)
            });

            return Json(items2, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteAttachment(int id)
        {
            var context = db.MediaWebsiteEGroupContexts.Find(id);
            db.MediaWebsiteEGroupContexts.Remove(context);
            db.SaveChanges();
            return null;
        }

        public ViewResult Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim();
            IQueryable<MediaWebsiteEGroup> list = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroups(p => p.Name.Contains(searchTerm) || p.URL.Contains(searchTerm));

            return View("Index", list);
        }

        public ActionResult Index(int? activeyear, int? activestatusid, List<int> movementclassid = null, string movementclassid_string = "", string name = "", string mediaurl = "", string comment = "", string docsearch = "", int? page = 1, int? pageSize = 15)
        {
            if (!string.IsNullOrWhiteSpace(movementclassid_string))
            {
                movementclassid = movementclassid_string.Split(',').Select(int.Parse).ToList();
            }

            name = name.Trim();
            mediaurl = mediaurl.Trim();
            comment = comment.Trim();
            docsearch = docsearch.Trim();

            Session["movementclassid"] = movementclassid;
            Session["name"] = name;
            Session["activeyear"] = activeyear;
            Session["activestatusid"] = activestatusid;
            Session["mediaurl"] = mediaurl;
            Session["comment"] = comment;
            Session["page"] = page;
            Session["pageSize"] = pageSize;
            Session["docsearch"] = docsearch;

            //TODO:  need to handle multiple search expressions.
            //this is to prepare the search term for full text index search.  s variable is used for full text index search.
            string s = null;
            if (!string.IsNullOrEmpty(docsearch)) { s = string.Format("\"{0}\"", FtsInterceptor.Fts(docsearch.Replace("\"", ""))); }

            //TODO: WebsiteEGroupContext errors

            var pred = PredicateBuilder.True<MediaWebsiteEGroup>();
            if (movementclassid != null) pred = pred.And(p => movementclassid.Contains((int)p.MovementClassId));
            if (!string.IsNullOrWhiteSpace(name)) pred = pred.And(p => p.Name.Contains(name));
            if (!string.IsNullOrWhiteSpace(mediaurl)) pred = pred.And(p => p.URL.Contains(mediaurl));
            if (activeyear != null) pred = pred.And(p => p.ActiveYear == activeyear);
            if (activestatusid != null) pred = pred.And(p => p.ActiveStatusId == activestatusid);
            if (!string.IsNullOrWhiteSpace(comment)) pred = pred.And(p => p.MediaWebsiteEGroupComments.Any(c => c.Comment.Contains(comment)));
            if (!string.IsNullOrWhiteSpace(s)) pred = pred.And(p => p.MediaWebsiteEGroupContext_Indexes.Any(m => m.ContextText.Contains(s)));

            var list = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroups(CurrentUser, pred).OrderBy(m => m.Name).ToPagedList(page ?? 1, pageSize ?? 15);

            //slj had to move filter of deleted records here because having the filter build here for the full context search and in the repository for the date deleted filter was messing up the full context search and causing an error.

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaWebsiteEGroupList", list);
            }

            return View("Index", list);

        }

        public JsonResult GetMediaWebsiteEGroupList(string term)
        {
            term = term.Trim();
            var list = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroups(p => p.Name.Contains(term) || p.URL.Contains(term)).ToArray().Select(
                e => new { e.Id, label = e.Name });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Details(int id)
        {
            ViewBag.OrganizationId = -1;
            ViewBag.ChapterId = -1;
            ViewBag.ContactId = -1;
            ViewBag.BeholderPersonId = -1;
            ViewBag.PersonId = -1;
            ViewBag.EventId = -1;
            ViewBag.SubscriptionId = -1;
            ViewBag.VehicleId = -1;
            ViewBag.MediaImageId = -1;
            ViewBag.MediaAudioVideoId = -1;
            ViewBag.MediaCorrespondenceId = -1;
            ViewBag.SubscriptionId = -1;
            ViewBag.MediaWebsiteEGroupId = id;
            ViewBag.Controller = "MediaWebsiteEGroups";
            var websiteEGroup = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroup(CurrentUser, id);

            if (websiteEGroup != null)
            {
                return View(websiteEGroup);
            }
            return View("WebsiteEGroup404");
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

        private byte[] GetFile(string fileName)
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
                tmp = ms.ToArray();
            }
            return tmp;
        }

        private bool FileExists(string fileName)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/Content/upload/")))
            {
                return false;
            }

            return System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/upload/"), fileName ?? ""));
        }

        public ActionResult Download(int id)
        {
            var document = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroupContext(id);
            var cd = new System.Net.Mime.ContentDisposition
            {
                // for example foo.bak
                FileName = document.FileName,

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(document.ContextText, document.MimeType.Name);
        }

        public byte[] CreateFile(string fileText)
        {
            return Encoding.ASCII.GetBytes(fileText);

        }

        public ActionResult Create()
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(CurrentUser);
            var mediaTypeId = _lookupRepo.GetMediaTypes().SingleOrDefault(p => p.Name.Equals("Website/eGroup")).Id;
            var context = new MediaWebsiteEGroupContext();

            var mediaWebsiteEGroup = new MediaWebsiteEGroup
            {
                DateDiscovered = DateTime.Now,
                MediaTypeId = mediaTypeId,
                //MediaWebsiteEGroupContext = context
            };

            return View(mediaWebsiteEGroup);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(MediaWebsiteEGroup mediawebsiteegroup, FormCollection form, IEnumerable<HttpPostedFileBase> attachments)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _mediaWebsiteEGroupRepo.InsertOrUpdate(mediawebsiteegroup);
                    _mediaWebsiteEGroupRepo.Save();

                    var content = form["fileContent"];
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        var filename = form["filename"];
                        var context = new MediaWebsiteEGroupContext()
                        {
                            MediaWebsiteEGroupId = mediawebsiteegroup.Id,
                            FileName = filename,
                            ContextText = Encoding.ASCII.GetBytes(content),
                            DocumentExtension = "txt",
                            MimeTypeId = 22
                        };

                        try
                        {
                            db.MediaWebsiteEGroupContexts.Add(context);
                            db.SaveChanges();
                        }
                        catch (ValidationException e)
                        {
                            var message = e.Message;
                        }
                    }


                    if (attachments != null)
                    {
                        foreach (var file in attachments)
                        {
                            if (file == null) break;
                            var filename = Path.GetFileName(file.FileName);
                            if (filename != null)
                            {
                                var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), filename);
                                file.SaveAs(physicalPath);

                                var mimetypeid =
                                    _lookupRepo.GetMimeTypes()
                                        .SingleOrDefault(p => p.Name.Equals(file.ContentType.ToLower()))
                                        .Id;
                                var context = new MediaWebsiteEGroupContext()
                                {
                                    MediaWebsiteEGroupId = mediawebsiteegroup.Id,
                                    FileName = filename,
                                    ContextText = GetFile(physicalPath),
                                    DocumentExtension = Path.GetExtension(physicalPath),
                                    MimeTypeId = mimetypeid
                                };

                                try
                                {
                                    db.MediaWebsiteEGroupContexts.Add(context);
                                    db.SaveChanges();
                                    System.IO.File.Delete(physicalPath);
                                }
                                catch (ValidationException e)
                                {
                                    var message = e.Message;
                                }
                            }
                        }
                    }

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
                return RedirectToAction("Details", new { id = mediawebsiteegroup.Id });
            }
            //var context = new MediaWebsiteEGroupContext();
            //var entity = new MediaWebsiteEGroup
            //{
            //    MediaTypeId = _lookupRepo.GetMediaTypes().SingleOrDefault(p => p.Name.Equals("Image")).Id,
            //    MediaWebsiteEGroupContext = context,
            //};
            //return View(entity);
            return View(mediawebsiteegroup);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(CurrentUser);
            var mediaWebsiteEGroup = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroup(id);

            if (mediaWebsiteEGroup != null)
            {
                return View(mediaWebsiteEGroup);
            }
            return View("WebsiteEGroup404");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(MediaWebsiteEGroup mediawebsiteegroup)
        {
            if (ModelState.IsValid)
            {
                _mediaWebsiteEGroupRepo.InsertOrUpdate(mediawebsiteegroup);
                try
                {
                    _mediaWebsiteEGroupRepo.Save();
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

                return RedirectToAction("Details", new { id = mediawebsiteegroup.Id });
            }
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(CurrentUser);
            return View(mediawebsiteegroup);
        }

        public ActionResult RemoveWebsiteEgroup(int id)
        {
            var website = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroup(CurrentUser, id);
            return View(website);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult RemoveWebsiteEgroup(int id, string removalreason)
        {
            var website = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroup(CurrentUser, id);
            website.RemovalReason = removalreason;

            website.RemovalStatusId = 1;
            _mediaWebsiteEGroupRepo.InsertOrUpdate(website);
            _mediaWebsiteEGroupRepo.Save();

            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            return View(_mediaWebsiteEGroupRepo.GetMediaWebsiteEGroup(id));
        }

        //
        // POST: /MediaWebsiteEGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _mediaWebsiteEGroupRepo.Delete(id);
            _mediaWebsiteEGroupRepo.Save();

            return RedirectToAction("Index");
        }

        #endregion


        #region Media Audio Video

        public ActionResult GetMediaWebsiteEGroupMediaAudioVideos(int mediaWebsiteEGroupId, int mediaAudioVideoId)
        {
            IQueryable<MediaWebsiteEGroupMediaAudioVideoRel> mediaWebsiteEGroupMediaAudioVideos;
            if (mediaAudioVideoId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.Controller = "MediaWebsiteEGroups";
                mediaWebsiteEGroupMediaAudioVideos = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroupMediaAudioVideos(p => p.MediaWebsiteEGroup.Id.Equals(mediaWebsiteEGroupId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.Controller = "MediaAudioVideos";
                mediaWebsiteEGroupMediaAudioVideos = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroupMediaAudioVideos(p => p.MediaAudioVideo.Id.Equals(mediaAudioVideoId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaWebsiteEGroupMediaAudioVideoList", mediaWebsiteEGroupMediaAudioVideos);
            }
            return View(mediaWebsiteEGroupMediaAudioVideos);
        }

        //
        // GET: /MediaAudioVideos/CreatePersonMediaAudioVideo
        public ActionResult CreateMediaWebsiteEGroupMediaAudioVideo(int mediaAudioVideoId, int mediaWebsiteEGroupId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaWebsiteEGroupMediaAudioVideoRel = new MediaWebsiteEGroupMediaAudioVideoRel
            {
                MediaAudioVideoId = mediaAudioVideoId,
                MediaWebsiteEGroupId = mediaWebsiteEGroupId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaAudioVideoId == -1)
            {
                mediaWebsiteEGroupMediaAudioVideoRel.MediaAudioVideo = new MediaAudioVideo();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaWebsiteEGroups";
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
            }
            else
            {
                mediaWebsiteEGroupMediaAudioVideoRel.MediaWebsiteEGroup = new MediaWebsiteEGroup();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaAudioVideos";
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaWebsiteEGroupMediaAudioVideo", mediaWebsiteEGroupMediaAudioVideoRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaWebsiteEGroupMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,MediaAudioVideoId")] MediaWebsiteEGroupMediaAudioVideoRel mediaWebsiteEGroupmediaAudioVideorel)
        {
            if (ModelState.IsValid)
            {
                if (mediaWebsiteEGroupmediaAudioVideorel.MediaWebsiteEGroup == null)
                {
                    mediaWebsiteEGroupmediaAudioVideorel.MediaAudioVideo = null;
                    _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupMediaAudioVideo(mediaWebsiteEGroupmediaAudioVideorel);
                    _mediaWebsiteEGroupRepo.Save();
                    return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = mediaWebsiteEGroupmediaAudioVideorel.MediaWebsiteEGroupId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaWebsiteEGroupOrganizationRel.
                    mediaWebsiteEGroupmediaAudioVideorel.MediaWebsiteEGroup = null;
                    _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupMediaAudioVideo(mediaWebsiteEGroupmediaAudioVideorel);
                    _mediaWebsiteEGroupRepo.Save();
                    return RedirectToAction("Details", "MediaAudioVideos", new { id = mediaWebsiteEGroupmediaAudioVideorel.MediaAudioVideoId });
                }
            }
            return View();
        }

        //
        // GET: /MediaAudioVideos/EditPersonMediaAudioVideo/5
        public ActionResult EditMediaWebsiteEGroupMediaAudioVideo(int id, string source)
        {
            var mediaWebsiteEGroupMediaAudioVideoRel = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroupMediaAudioVideo(id);
            if (source == "MediaWebsiteEGroups")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaWebsiteEGroupMediaAudioVideoRel.MediaWebsiteEGroupId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaAudioVideoId = mediaWebsiteEGroupMediaAudioVideoRel.MediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaWebsiteEGroupMediaAudioVideo", mediaWebsiteEGroupMediaAudioVideoRel);
            }
            return View(mediaWebsiteEGroupMediaAudioVideoRel);

        }

        //
        // POST: /MediaAudioVideos/EditPersonMediaAudioVideo/5
        [HttpPost]
        public ActionResult EditMediaWebsiteEGroupMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,MediaAudioVideoId")] MediaWebsiteEGroupMediaAudioVideoRel mediaWebsiteEGroupmediaAudioVideorel)
        {
            if (ModelState.IsValid)
            {
                if (mediaWebsiteEGroupmediaAudioVideorel.MediaWebsiteEGroup == null)
                {
                    //reset the mediaWebsiteEGroup object.  This is only added from Organization, not MediaWebsiteEGroupOrganizationRel.
                    mediaWebsiteEGroupmediaAudioVideorel.MediaAudioVideo = null;
                    _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupMediaAudioVideo(mediaWebsiteEGroupmediaAudioVideorel);
                    _mediaWebsiteEGroupRepo.Save();
                    return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = mediaWebsiteEGroupmediaAudioVideorel.MediaWebsiteEGroupId });
                }
                //reset the organization object.  This is only added from Organization, not MediaWebsiteEGroupOrganizationRel.
                mediaWebsiteEGroupmediaAudioVideorel.MediaWebsiteEGroup = null;
                _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupMediaAudioVideo(mediaWebsiteEGroupmediaAudioVideorel);
                _mediaWebsiteEGroupRepo.Save();
                return RedirectToAction("Details", "MediaAudioVideos", new { id = mediaWebsiteEGroupmediaAudioVideorel.MediaAudioVideoId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaWebsiteEGroupMediaAudioVideo(int id)
        {
            _mediaWebsiteEGroupRepo.DeleteMediaWebsiteEGroupMediaAudioVideo(id);
            _mediaWebsiteEGroupRepo.Save();
        }

        #endregion


        #region Media Website/EGroup

        public ActionResult GetMediaWebsiteEGroupMediaWebsiteEGroups(int mediaWebsiteEGroupId)
        {
            MediaWebsiteEGroup mediaWebsiteEGroups;
            //if (subscriptionId == -1)
            //{
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
            ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
            ViewBag.MediaWebsiteEGroup2Id = mediaWebsiteEGroupId;
            ViewBag.Controller = "MediaWebsiteEGroups";
            try
            {
                mediaWebsiteEGroups = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroup(mediaWebsiteEGroupId);
                //                subscriptions = _eventRepository.Get(p => p.Id.Equals(eventId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaWebsiteEGroupMediaWebsiteEGroupList", mediaWebsiteEGroups);
            }
            return View(mediaWebsiteEGroups);
        }

        public ActionResult CreateMediaWebsiteEGroupMediaWebsiteEGroup(int mediawebsiteegroupId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaWebsiteEGroupMediaWebsiteEGroupRel = new MediaWebsiteEGroupMediaWebsiteEGroupRel
            {
                MediaWebsiteEGroupId = mediawebsiteegroupId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now
            };

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
            return PartialView("_CreateOrEditMediaWebsiteEGroupMediaWebsiteEGroup", mediaWebsiteEGroupMediaWebsiteEGroupRel);
        }

        [HttpPost]
        public ActionResult CreateMediaWebsiteEGroupMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,MediaWebsiteEGroup2Id")] MediaWebsiteEGroupMediaWebsiteEGroupRel mediaWebsiteEGroupMediaWebsiteEGroupRel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupMediaWebsiteEGroup(mediaWebsiteEGroupMediaWebsiteEGroupRel);
            _mediaWebsiteEGroupRepo.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult EditMediaWebsiteEGroupMediaWebsiteEGroup(int id)
        {
            var mediaWebsiteEGroupMediaWebsiteEGroupRel = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroupMediaWebsiteEGroup(id);
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);

            return PartialView("_CreateOrEditMediaWebsiteEGroupMediaWebsiteEGroup", mediaWebsiteEGroupMediaWebsiteEGroupRel);
        }

        [HttpPost]
        public ActionResult EditMediaWebsiteEGroupMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,MediaWebsiteEGroup2Id")] MediaWebsiteEGroupMediaWebsiteEGroupRel mediaWebsiteEGroupMediaWebsiteEGroupRel)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupMediaWebsiteEGroup(mediaWebsiteEGroupMediaWebsiteEGroupRel);
            _mediaWebsiteEGroupRepo.Save();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public void DeleteMediaWebsiteEGroupMediaWebsiteEGroup(int id)
        {
            _mediaWebsiteEGroupRepo.DeleteMediaWebsiteEGroupMediaWebsiteEGroup(id);
            _mediaWebsiteEGroupRepo.Save();
        }

        #endregion


        #region Vehicles

        public ActionResult GetMediaWebsiteEGroupVehicles(int mediaWebsiteEGroupId, int vehicleId)
        {
            IQueryable<MediaWebsiteEGroupVehicleRel> mediaWebsiteEGroupVehicles;
            if (vehicleId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.Controller = "MediaWebsiteEGroups";
                mediaWebsiteEGroupVehicles = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroupVehicles(p => p.MediaWebsiteEGroup.Id.Equals(mediaWebsiteEGroupId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.Controller = "Vehicles";
                mediaWebsiteEGroupVehicles = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroupVehicles(p => p.Vehicle.Id.Equals(vehicleId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaWebsiteEGroupVehicleList", mediaWebsiteEGroupVehicles);
            }
            return View(mediaWebsiteEGroupVehicles);
        }

        //
        // GET: /Vehicles/CreatePersonVehicle
        public ActionResult CreateMediaWebsiteEGroupVehicle(int vehicleId, int mediaWebsiteEGroupId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaWebsiteEGroupVehicleRel = new MediaWebsiteEGroupVehicleRel
            {
                VehicleId = vehicleId,
                MediaWebsiteEGroupId = mediaWebsiteEGroupId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (vehicleId == -1)
            {
                mediaWebsiteEGroupVehicleRel.Vehicle = new Vehicle();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaWebsiteEGroups";
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
            }
            else
            {
                mediaWebsiteEGroupVehicleRel.MediaWebsiteEGroup = new MediaWebsiteEGroup();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Vehicles";
                ViewBag.VehicleId = vehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaWebsiteEGroupVehicle", mediaWebsiteEGroupVehicleRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaWebsiteEGroupVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,VehicleId")] MediaWebsiteEGroupVehicleRel mediaWebsiteEGroupvehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (mediaWebsiteEGroupvehiclerel.MediaWebsiteEGroup == null)
                {
                    mediaWebsiteEGroupvehiclerel.Vehicle = null;
                    _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupVehicle(mediaWebsiteEGroupvehiclerel);
                    _mediaWebsiteEGroupRepo.Save();
                    return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = mediaWebsiteEGroupvehiclerel.MediaWebsiteEGroupId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaWebsiteEGroupOrganizationRel.
                    mediaWebsiteEGroupvehiclerel.MediaWebsiteEGroup = null;
                    _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupVehicle(mediaWebsiteEGroupvehiclerel);
                    _mediaWebsiteEGroupRepo.Save();
                    return RedirectToAction("Details", "Vehicles", new { id = mediaWebsiteEGroupvehiclerel.VehicleId });
                }
            }
            return View();
        }

        //
        // GET: /Vehicles/EditPersonVehicle/5
        public ActionResult EditMediaWebsiteEGroupVehicle(int id, string source)
        {
            var mediaWebsiteEGroupVehicleRel = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroupVehicle(id);
            if (source == "MediaWebsiteEGroups")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaWebsiteEGroupVehicleRel.MediaWebsiteEGroupId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.VehicleId = mediaWebsiteEGroupVehicleRel.VehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaWebsiteEGroupVehicle", mediaWebsiteEGroupVehicleRel);
            }
            return View(mediaWebsiteEGroupVehicleRel);

        }

        //
        // POST: /Vehicles/EditPersonVehicle/5
        [HttpPost]
        public ActionResult EditMediaWebsiteEGroupVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,VehicleId")] MediaWebsiteEGroupVehicleRel mediaWebsiteEGroupvehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (mediaWebsiteEGroupvehiclerel.MediaWebsiteEGroup == null)
                {
                    //reset the mediaWebsiteEGroup object.  This is only added from Organization, not MediaWebsiteEGroupOrganizationRel.
                    mediaWebsiteEGroupvehiclerel.Vehicle = null;
                    _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupVehicle(mediaWebsiteEGroupvehiclerel);
                    _mediaWebsiteEGroupRepo.Save();
                    return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = mediaWebsiteEGroupvehiclerel.MediaWebsiteEGroupId });
                }
                //reset the organization object.  This is only added from Organization, not MediaWebsiteEGroupOrganizationRel.
                mediaWebsiteEGroupvehiclerel.MediaWebsiteEGroup = null;
                _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupVehicle(mediaWebsiteEGroupvehiclerel);
                _mediaWebsiteEGroupRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = mediaWebsiteEGroupvehiclerel.VehicleId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaWebsiteEGroupVehicle(int id)
        {
            _mediaWebsiteEGroupRepo.DeleteMediaWebsiteEGroupVehicle(id);
            _mediaWebsiteEGroupRepo.Save();
        }

        #endregion


        #region Subscriptions

        public ActionResult GetMediaWebsiteEGroupSubscriptions(int mediaWebsiteEGroupId, int subscriptionId)
        {
            IQueryable<MediaWebsiteEGroupSubscriptionRel> mediaWebsiteEGroupSubscriptions;
            if (subscriptionId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.Controller = "MediaWebsiteEGroups";
                mediaWebsiteEGroupSubscriptions = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroupSubscriptions(p => p.MediaWebsiteEGroup.Id.Equals(mediaWebsiteEGroupId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.Controller = "Subscriptions";
                mediaWebsiteEGroupSubscriptions = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroupSubscriptions(p => p.Subscription.Id.Equals(subscriptionId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaWebsiteEGroupSubscriptionList", mediaWebsiteEGroupSubscriptions);
            }
            return View(mediaWebsiteEGroupSubscriptions);
        }

        //
        // GET: /Subscriptions/CreatePersonSubscription
        public ActionResult CreateMediaWebsiteEGroupSubscription(int subscriptionId, int mediaWebsiteEGroupId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaWebsiteEGroupSubscriptionRel = new MediaWebsiteEGroupSubscriptionRel
            {
                SubscriptionId = subscriptionId,
                MediaWebsiteEGroupId = mediaWebsiteEGroupId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (subscriptionId == -1)
            {
                mediaWebsiteEGroupSubscriptionRel.Subscription = new Subscription();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaWebsiteEGroups";
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
            }
            else
            {
                mediaWebsiteEGroupSubscriptionRel.MediaWebsiteEGroup = new MediaWebsiteEGroup();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Subscriptions";
                ViewBag.SubscriptionId = subscriptionId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaWebsiteEGroupSubscription", mediaWebsiteEGroupSubscriptionRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaWebsiteEGroupSubscription([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,SubscriptionId")] MediaWebsiteEGroupSubscriptionRel mediaWebsiteEGroupsubscriptionrel)
        {
            if (ModelState.IsValid)
            {
                if (mediaWebsiteEGroupsubscriptionrel.MediaWebsiteEGroup == null)
                {
                    mediaWebsiteEGroupsubscriptionrel.Subscription = null;
                    _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupSubscription(mediaWebsiteEGroupsubscriptionrel);
                    _mediaWebsiteEGroupRepo.Save();
                    return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = mediaWebsiteEGroupsubscriptionrel.MediaWebsiteEGroupId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaWebsiteEGroupOrganizationRel.
                    mediaWebsiteEGroupsubscriptionrel.MediaWebsiteEGroup = null;
                    _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupSubscription(mediaWebsiteEGroupsubscriptionrel);
                    _mediaWebsiteEGroupRepo.Save();
                    return RedirectToAction("Details", "Subscriptions", new { id = mediaWebsiteEGroupsubscriptionrel.SubscriptionId });
                }
            }
            return View();
        }

        //
        // GET: /Subscriptions/EditPersonSubscription/5
        public ActionResult EditMediaWebsiteEGroupSubscription(int id, string source)
        {
            var mediaWebsiteEGroupSubscriptionRel = _mediaWebsiteEGroupRepo.GetMediaWebsiteEGroupSubscription(id);
            if (source == "MediaWebsiteEGroups")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaWebsiteEGroupSubscriptionRel.MediaWebsiteEGroupId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.SubscriptionId = mediaWebsiteEGroupSubscriptionRel.SubscriptionId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaWebsiteEGroupSubscription", mediaWebsiteEGroupSubscriptionRel);
            }
            return View(mediaWebsiteEGroupSubscriptionRel);

        }

        //
        // POST: /Subscriptions/EditPersonSubscription/5
        [HttpPost]
        public ActionResult EditMediaWebsiteEGroupSubscription([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,SubscriptionId")] MediaWebsiteEGroupSubscriptionRel mediaWebsiteEGroupsubscriptionrel)
        {
            if (ModelState.IsValid)
            {
                if (mediaWebsiteEGroupsubscriptionrel.MediaWebsiteEGroup == null)
                {
                    //reset the mediaWebsiteEGroup object.  This is only added from Organization, not MediaWebsiteEGroupOrganizationRel.
                    mediaWebsiteEGroupsubscriptionrel.Subscription = null;
                    _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupSubscription(mediaWebsiteEGroupsubscriptionrel);
                    _mediaWebsiteEGroupRepo.Save();
                    return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = mediaWebsiteEGroupsubscriptionrel.MediaWebsiteEGroupId });
                }
                //reset the organization object.  This is only added from Organization, not MediaWebsiteEGroupOrganizationRel.
                mediaWebsiteEGroupsubscriptionrel.MediaWebsiteEGroup = null;
                _mediaWebsiteEGroupRepo.InsertOrUpdateMediaWebsiteEGroupSubscription(mediaWebsiteEGroupsubscriptionrel);
                _mediaWebsiteEGroupRepo.Save();
                return RedirectToAction("Details", "Subscriptions", new { id = mediaWebsiteEGroupsubscriptionrel.SubscriptionId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaWebsiteEGroupSubscription(int id)
        {
            _mediaWebsiteEGroupRepo.DeleteMediaWebsiteEGroupSubscription(id);
            _mediaWebsiteEGroupRepo.Save();
        }

        #endregion


    }
}




