using Caseiro.Mvc.PagedList.Extensions;
using splc.beholder.web.Utility;
using splc.data.repository;
using splc.domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class MediaAudioVideosController : BaseController
    {
        private readonly ILookupRepository _lookupRepo;
        private readonly IMediaAudioVideoRepository _mediaAudioVideoRepo;

        public MediaAudioVideosController(
            ILookupRepository lookupRepo,
            IMediaAudioVideoRepository mediaAudioVideoRepo)
        {
            _lookupRepo = lookupRepo;
            _mediaAudioVideoRepo = mediaAudioVideoRepo;

            ViewBag.PossibleApprovalStatus = _lookupRepo.GetApprovalStatuses();
            ViewBag.PossibleMediaTypes = _lookupRepo.GetMediaTypes();
            ViewBag.PossibleAudioVideoTypes = _lookupRepo.GetAudioVideoTypes();
            ViewBag.PossibleNewsSources = _lookupRepo.GetNewsSources();
            ViewBag.PossibleMovementClasses = _lookupRepo.GetMovementClasses();
            ViewBag.PossibleRenewalPermissionTypes = _lookupRepo.RenewalPermissionTypes();
            ViewBag.PossiblePrimaryStatuses = _lookupRepo.GetPrimaryStatuses();

            ViewBag.PossibleStates = _lookupRepo.GetStates();
            ViewBag.PossibleRemovalStatuses = _lookupRepo.GetRemovalStatus();
        }

        public ViewResult Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim();
            IQueryable<MediaAudioVideo> list = _mediaAudioVideoRepo.GetMediaAudioVideos(CurrentUser, p => p.SpeakerCommentator.Contains(searchTerm) || p.Title.Contains(searchTerm));

            return View("Index", list);
        }

        //
        // GET: /Organizations/
        //public ViewResult Index()
        public ActionResult Index(string audiovideotitle = "", DateTime? daterecordfrom = null, DateTime? daterecordto = null, DateTime? dateairfrom = null, DateTime? dateairto = null, string comment = "", List<int> movementclassid = null, string movementclassid_string = "", int? page = 1, int? pageSize = 15)
        {

            if (!string.IsNullOrWhiteSpace(movementclassid_string))
            {
                movementclassid = movementclassid_string.Split(',').Select(int.Parse).ToList();
            }
            audiovideotitle = audiovideotitle.Trim();

            Session["movementclassid"] = movementclassid;
            Session["audiovideotitle"] = audiovideotitle;
            Session["daterecordfrom"] = daterecordfrom;
            Session["daterecordto"] = daterecordto;
            Session["dateairfrom"] = dateairfrom;
            Session["dateairto"] = dateairto;
            Session["page"] = page;
            Session["pageSize"] = pageSize;

            var pred = PredicateBuilder.True<MediaAudioVideo>();
            if (movementclassid != null) pred = pred.And(p => movementclassid.Contains((int)p.MovementClassId));
            if (!string.IsNullOrWhiteSpace(audiovideotitle)) pred = pred.And(p => p.Title.Contains(audiovideotitle));
            if (!string.IsNullOrWhiteSpace(comment)) pred = pred.And(p => p.MediaAudioVideoComments.Any(c => c.Comment.Contains(comment)));

            if (dateairto != null) pred = pred.And(p => dateairto <= p.DateAired);
            if (dateairfrom != null) pred = pred.And(p => dateairfrom >= p.DateAired);

            if (daterecordfrom != null) pred = pred.And(p => daterecordfrom <= p.DateReceivedRecorded);
            if (daterecordto != null) pred = pred.And(p => daterecordto >= p.DateReceivedRecorded);

            var list = _mediaAudioVideoRepo.GetMediaAudioVideos(CurrentUser, pred).OrderBy(m => m.Title).ToPagedList(page ?? 1, pageSize ?? 15);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaAudioVideoList", list);
            }

            return View("Index", list);

        }

        // GET: Search for BeholderPersons 
        public JsonResult GetMediaAudioVideoList(string term)
        {
            term = term.Trim();
            var list = _mediaAudioVideoRepo.GetMediaAudioVideos(CurrentUser, p => p.Title.Contains(term)).ToArray().Select(
                e => new
                {
                    Id = e.Id,
                    label = e.Title
                });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MediaAudioVideos/Details/5
        public ViewResult Details(int id)
        {
            ViewBag.OrganizationId = -1;
            ViewBag.ChapterId = -1;
            ViewBag.BeholderPersonId = -1;
            ViewBag.PersonId = -1;
            ViewBag.EventId = -1;
            ViewBag.VehicleId = -1;
            ViewBag.MediaAudioVideoId = id;
            ViewBag.MediaCorrespondenceId = -1;
            ViewBag.Controller = "MediaAudioVideos";

            var mediaAudioVideo = _mediaAudioVideoRepo.GetMediaAudioVideo(CurrentUser, id);
            if (mediaAudioVideo != null)
            {
                return View(mediaAudioVideo);
            }
            return View("MediaAudioVideo404");
        }

        // GET: /MediaAudioVideos/Create
        public ActionResult Create()
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(CurrentUser);
            var mediaAudioVideo = new MediaAudioVideo();
            return View(mediaAudioVideo);
        }

        [HttpPost]
        public ActionResult Create(MediaAudioVideo mediaaudiovideo)
        {
            if (ModelState.IsValid)
            {
                _mediaAudioVideoRepo.InsertOrUpdate(mediaaudiovideo);
                try
                {
                    _mediaAudioVideoRepo.Save();
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
                return RedirectToAction("Details", new { id = mediaaudiovideo.Id });
            }
            var mediaAudioVideo = new MediaAudioVideo();
            return View(mediaAudioVideo);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(CurrentUser);
            var mediaAudioVideo = _mediaAudioVideoRepo.GetMediaAudioVideo(CurrentUser, id);
            if (mediaAudioVideo != null)
            {
                return View(_mediaAudioVideoRepo.GetMediaAudioVideo(CurrentUser, id));
            }
            return View("MediaAudioVideo404");
        }

        //
        // POST: /MediaAudioVideos/Edit/5
        [HttpPost]
        public ActionResult Edit(MediaAudioVideo mediaaudiovideo)
        {
            if (ModelState.IsValid)
            {
                _mediaAudioVideoRepo.InsertOrUpdate(mediaaudiovideo);
                _mediaAudioVideoRepo.Save();
                return RedirectToAction("Details", new { id = mediaaudiovideo.Id });
            }
            return View();
        }

        public ActionResult RemoveAudioVideo(int id)
        {
            var av = _mediaAudioVideoRepo.GetMediaAudioVideo(CurrentUser, id);
            return View(av);
        }

        [HttpPost]
        public ActionResult RemoveAudioVideo(int id, string removalreason)
        {
            var av = _mediaAudioVideoRepo.GetMediaAudioVideo(CurrentUser, id);
            av.RemovalReason = removalreason;

            av.RemovalStatusId = 1;
            _mediaAudioVideoRepo.InsertOrUpdate(av);
            _mediaAudioVideoRepo.Save();

            return RedirectToAction("Index");

        }

        //
        // GET: /MediaAudioVideos/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_mediaAudioVideoRepo.GetMediaAudioVideo(id));
        }

        //
        // POST: /MediaAudioVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _mediaAudioVideoRepo.Delete(id);
            _mediaAudioVideoRepo.Save();

            return RedirectToAction("Index");
        }

        public ImageResult ShowImage(int id)
        {
            var image = _mediaAudioVideoRepo.GetImage(id);

            byte[] img = ImageHelper.Decompress(image.IMAGE1);
            string contentType = image.MimeType.Name;

            // Here we are calling the extension method and returning    
            // the ImageResult.
            //return splc.beholder.web.Utility.ControllerExtensions.Image(this, image, contentType);
            return this.Image(img, contentType);
        }

        #region Vehicles

        public ActionResult GetMediaAudioVideoVehicles(int mediaAudioVideoId, int vehicleId)
        {
            IQueryable<MediaAudioVideoVehicleRel> mediaAudioVideoVehicles;
            if (vehicleId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.Controller = "MediaAudioVideos";
                mediaAudioVideoVehicles = _mediaAudioVideoRepo.GetMediaAudioVideoVehicles(p => p.MediaAudioVideo.Id.Equals(mediaAudioVideoId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.Controller = "Vehicles";
                mediaAudioVideoVehicles = _mediaAudioVideoRepo.GetMediaAudioVideoVehicles(p => p.Vehicle.Id.Equals(vehicleId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaAudioVideoVehicleList", mediaAudioVideoVehicles);
            }
            return View(mediaAudioVideoVehicles);
        }

        //
        // GET: /Vehicles/CreatePersonVehicle
        public ActionResult CreateMediaAudioVideoVehicle(int vehicleId, int mediaAudioVideoId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaAudioVideoVehicleRel = new MediaAudioVideoVehicleRel
            {
                VehicleId = vehicleId,
                MediaAudioVideoId = mediaAudioVideoId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (vehicleId == -1)
            {
                mediaAudioVideoVehicleRel.Vehicle = new Vehicle();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaAudioVideos";
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
            }
            else
            {
                mediaAudioVideoVehicleRel.MediaAudioVideo = new MediaAudioVideo();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Vehicles";
                ViewBag.VehicleId = vehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaAudioVideoVehicle", mediaAudioVideoVehicleRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaAudioVideoVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaAudioVideoId,VehicleId")] MediaAudioVideoVehicleRel mediaAudioVideovehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (mediaAudioVideovehiclerel.MediaAudioVideo == null)
                {
                    mediaAudioVideovehiclerel.Vehicle = null;
                    _mediaAudioVideoRepo.InsertOrUpdateMediaAudioVideoVehicle(mediaAudioVideovehiclerel);
                    _mediaAudioVideoRepo.Save();
                    return RedirectToAction("Details", "MediaAudioVideos", new { id = mediaAudioVideovehiclerel.MediaAudioVideoId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaAudioVideoOrganizationRel.
                    mediaAudioVideovehiclerel.MediaAudioVideo = null;
                    _mediaAudioVideoRepo.InsertOrUpdateMediaAudioVideoVehicle(mediaAudioVideovehiclerel);
                    _mediaAudioVideoRepo.Save();
                    return RedirectToAction("Details", "Vehicles", new { id = mediaAudioVideovehiclerel.VehicleId });
                }
            }
            return View();
        }

        //
        // GET: /Vehicles/EditPersonVehicle/5
        public ActionResult EditMediaAudioVideoVehicle(int id, string source)
        {
            var mediaAudioVideoVehicleRel = _mediaAudioVideoRepo.GetMediaAudioVideoVehicle(id);
            if (source == "MediaAudioVideos")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaAudioVideoVehicleRel.MediaAudioVideoId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.VehicleId = mediaAudioVideoVehicleRel.VehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaAudioVideoVehicle", mediaAudioVideoVehicleRel);
            }
            return View(mediaAudioVideoVehicleRel);

        }

        //
        // POST: /Vehicles/EditPersonVehicle/5
        [HttpPost]
        public ActionResult EditMediaAudioVideoVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,VehicleId,MediaAudioVideoId")] MediaAudioVideoVehicleRel mediaAudioVideovehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (mediaAudioVideovehiclerel.MediaAudioVideo == null)
                {
                    //reset the mediaAudioVideo object.  This is only added from Organization, not MediaAudioVideoOrganizationRel.
                    mediaAudioVideovehiclerel.Vehicle = null;
                    _mediaAudioVideoRepo.InsertOrUpdateMediaAudioVideoVehicle(mediaAudioVideovehiclerel);
                    _mediaAudioVideoRepo.Save();
                    return RedirectToAction("Details", "MediaAudioVideos", new { id = mediaAudioVideovehiclerel.MediaAudioVideoId });
                }
                //reset the organization object.  This is only added from Organization, not MediaAudioVideoOrganizationRel.
                mediaAudioVideovehiclerel.MediaAudioVideo = null;
                _mediaAudioVideoRepo.InsertOrUpdateMediaAudioVideoVehicle(mediaAudioVideovehiclerel);
                _mediaAudioVideoRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = mediaAudioVideovehiclerel.VehicleId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaAudioVideoVehicle(int id)
        {
            _mediaAudioVideoRepo.DeleteMediaAudioVideoVehicle(id);
            _mediaAudioVideoRepo.Save();
        }

        #endregion


    }
}

