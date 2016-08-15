using Caseiro.Mvc.PagedList;
using Caseiro.Mvc.PagedList.Extensions;
using splc.data;
using splc.data.repository;
using splc.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class EventsController : BaseController
    {
        private readonly ILookupRepository _lookupRepo;
        private readonly IEventRepository _eventRepo;
        private readonly ACDBContext _ctx;

        public EventsController(
            ILookupRepository lookupRepo,
            IEventRepository eventRepo,
            ACDBContext ctx)
        {
            _lookupRepo = lookupRepo;
            _eventRepo = eventRepo;
            _ctx = ctx;

            ViewBag.PossibleEventDocumentationTypes = _lookupRepo.GetEventDocumentationTypes();
            ViewBag.PossibleEventTypes = _lookupRepo.GetEventTypes();
            ViewBag.PossibleWebIncidentTypes = _lookupRepo.GetWebIncidentTypes();
            ViewBag.PossibleApprovalStatus = _lookupRepo.GetApprovalStatuses();
            ViewBag.PossibleActiveStatus = _lookupRepo.GetActiveStatuses();
            ViewBag.PossibleMovementClasses = _lookupRepo.GetMovementClasses();
            ViewBag.PossibleRemovalStatus = _lookupRepo.GetRemovalStatus();
            ViewBag.PossiblePrimaryStatus = _lookupRepo.GetPrimaryStatuses();
            ViewBag.PossibleNewsSources = _lookupRepo.GetNewsSources();
            ViewBag.PossibleReligions = _lookupRepo.GetReligions();
        }


        public JsonResult GetEventList(string term)
        {
            var list = _eventRepo.GetEvents(currentUser, x => x.EventName.Contains(term)).ToArray().Select(e => new
            {
                Id = e.Id,
                label = e.EventName
            });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Event/
        public ActionResult Index(int? activeyear, int? activestatusid, int? approvalstatusid, string eventname = "", DateTime? datefrom = null, DateTime? dateto = null, List<int> eventtypeid = null, string eventtypeid_string = "", int? page = 1, int? pageSize = 15)
        {
            if (!String.IsNullOrWhiteSpace(eventtypeid_string))
            {
                eventtypeid = ((List<int>)eventtypeid_string.Split(',').Select(int.Parse).ToList());
            }
            eventname = eventname.Trim();

            Session["activeyear"] = activeyear;
            Session["activestatusid"] = activestatusid;
            Session["approvalstatusid"] = approvalstatusid;
            Session["eventtypeid"] = eventtypeid;
            Session["eventname"] = eventname;
            Session["datefrom"] = datefrom;
            Session["dateto"] = dateto;
            Session["page"] = page;
            Session["pageSize"] = pageSize;

            PagedList<Event> list = null;
            if (eventtypeid == null)
            {
                list = _eventRepo.GetEvents(currentUser, x =>
                                                             x.EventName.Contains(eventname)
                                                          && x.EventDate >= (datefrom.HasValue ? datefrom : x.EventDate)
                                                          && x.EventDate <= (dateto.HasValue ? dateto : x.EventDate)
                                                          && x.ActiveYear == (activeyear.HasValue ? activeyear : x.ActiveYear)
                                                          && x.ActiveStatusId == (activestatusid.HasValue ? activestatusid : x.ActiveStatusId)
                                                          && x.ApprovalStatusId == (approvalstatusid.HasValue ? approvalstatusid : x.ApprovalStatusId)
                                                          ).OrderBy(m => m.EventDate).ToPagedList(page ?? 1, pageSize ?? 15);
            }
            else
            {
                list = _eventRepo.GetEvents(currentUser, x =>
                                                             x.EventName.Contains(eventname)
                                                          && x.EventDate >= (datefrom.HasValue ? datefrom : x.EventDate)
                                                          && x.EventDate >= (dateto.HasValue ? dateto : x.EventDate)
                                                          && x.ActiveYear == (activeyear.HasValue ? activeyear : x.ActiveYear)
                                                          && x.ActiveStatusId == (activestatusid.HasValue ? activestatusid : x.ActiveStatusId)
                                                          && x.ApprovalStatusId == (approvalstatusid.HasValue ? approvalstatusid : x.ApprovalStatusId)
                                                          && (x.EventEventTypeRels.Count(m => eventtypeid.Contains(m.EventTypeId)) > 0)
                                                          ).OrderBy(m => m.EventDate).ToPagedList(page ?? 1, pageSize ?? 15);
            }
            return View("Index", list);
        }

        public ViewResult Search(string searchTerm)
        {
            var list = _eventRepo.GetEvents(currentUser, e => e.EventName.Contains(searchTerm));
            return View("Index", list);
        }

        //
        // GET: /Event/Details/5
        public ViewResult Details(int id)
        {
            ViewBag.BeholderPersonId = -1;
            ViewBag.PersonId = -1;
            ViewBag.OrganizationId = -1;
            ViewBag.EventId = -1;
            ViewBag.EventId = id;
            ViewBag.MediaImageId = -1;
            ViewBag.MediaAudioVideoId = -1;
            ViewBag.MediaCorrespondenceId = -1;
            ViewBag.MediaWebsiteEGroupId = -1;
            ViewBag.VehicleId = -1;
            ViewBag.SubscriptionId = -1;
            ViewBag.Controller = "Events";
            var e = _eventRepo.GetEvent(currentUser, id);
            if (e != null)
            {
                return View(e);
            }
            return View("Event404");
        }

        //
        // GET: /Event/Create
        public ActionResult Create()
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            //var approvalStatus = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New"));
            var entity = new Event
            {
                EventDate = DateTime.Now,
                //ApprovalStatusId = approvalStatus.Id,
                ActiveYear = DateTime.Now.Year,
                //ReportStatusFlag = false,

            };
            return View(entity);
        }

        //
        // POST: /Event/Create
        [HttpPost]
        public ActionResult Create(Event evnt, string[] eventTypes)
        {
            if (ModelState.IsValid)
            {
                _eventRepo.InsertOrUpdate(evnt);
                _eventRepo.Save();
                if (eventTypes != null)
                {
                    var l = eventTypes.ToList().Select(int.Parse).ToList();
                    var types = _lookupRepo.GetEventTypes().Where(x => l.Contains(x.Id)).ToList();
                    _eventRepo.InsertOrUpdate(evnt, types);
                    _eventRepo.Save();
                }
                return RedirectToAction("Details", new { id = evnt.Id });
            }
            return View();
        }

        //
        // GET: /Event/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);

            var e = _eventRepo.GetEvent(currentUser, id);

            //var selectList = listOfCategories.Select(x =>new SelectListItem{Text = x.Name, Value = x.Id.ToString(), Selected = x.Id.Equals(blogToEdit.Category.Id)}).ToList();
            var listOfCategories = _lookupRepo.GetEventTypes();
            var selectList = listOfCategories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            ViewBag.selectList = selectList;

            //ViewBag.EventTypes = _lookupRepo.GetEventTypes(currentUser).Select(x => new SelectListItem()
            //{
            //    Text = x.Name,
            //    Value = x.Id.ToString(),
            //    Selected = x.Id.Equals(e.EventEventTypeRels.Any(e => e.EventTypeId))
            //})
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEdit", e);
            }
            //if (e != null)
            //{
            //    return View(_eventRepo.GetEvent(id));
            //}
            return View(_eventRepo.GetEvent(id));
            //return View("Event404");
        }

        //
        // POST: /Event/Edit/5
        [HttpPost]
        public ActionResult Edit(Event eventincident, string[] eventTypes)
        {
            if (!ModelState.IsValid) return View();
            if (eventTypes != null)
            {
                var l = eventTypes.ToList().Select(int.Parse).ToList();
                var types = _lookupRepo.GetEventTypes().Where(x => l.Contains(x.Id)).ToList();
                _eventRepo.InsertOrUpdate(eventincident, types);
            }
            else
            {
                _eventRepo.InsertOrUpdate(eventincident);
            }
            _eventRepo.Save();

            return RedirectToAction("Details", new { id = eventincident.Id });
        }

        public ActionResult RemoveEvent(int id)
        {
            var evt = _eventRepo.GetEvent(currentUser, id);
            return View(evt);
        }

        [HttpPost]
        public ActionResult RemoveEvent(int id, string removalreason)
        {
            var evt = _eventRepo.GetEvent(currentUser, id);
            evt.RemovalReason = removalreason;

            evt.RemovalStatusId = 1;
            _eventRepo.InsertOrUpdate(evt);
            _eventRepo.Save();

            return RedirectToAction("Index");

        }

        //
        // GET: /Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_eventRepo.GetEvent(id));
        }

        //
        // POST: /Event/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _eventRepo.Delete(id);
            _eventRepo.Save();

            return RedirectToAction("Index");
        }

        #region Vehicles

        public ActionResult GetEventVehicles(int eventId, int vehicleId)
        {
            IQueryable<EventVehicleRel> eventVehicles;
            if (vehicleId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.EventId = eventId;
                ViewBag.Controller = "Events";
                eventVehicles = _eventRepo.GetEventVehicles(p => p.Event.Id.Equals(eventId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.EventId = eventId;
                ViewBag.Controller = "Vehicles";
                eventVehicles = _eventRepo.GetEventVehicles(p => p.Vehicle.Id.Equals(vehicleId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventVehicleList", eventVehicles);
            }
            return View(eventVehicles);
        }

        //
        // GET: /Vehicles/CreatePersonVehicle
        public ActionResult CreateEventVehicle(int vehicleId, int eventId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var eventVehicleRel = new EventVehicleRel
            {
                VehicleId = vehicleId,
                EventId = eventId,
                ApprovalStatusId = approvalStatusId,
                DateStart = System.DateTime.Now,
            };

            if (vehicleId == -1)
            {
                eventVehicleRel.Vehicle = new Vehicle();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Events";
                ViewBag.EventId = eventId;
            }
            else
            {
                eventVehicleRel.Event = new Event();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Vehicles";
                ViewBag.VehicleId = vehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventVehicle", eventVehicleRel);
            }

            return View();

        }

        //
        // POST: /PersonVehicleRels/CreatePersonVehicle
        [HttpPost]
        public ActionResult CreateEventVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,VehicleId")] EventVehicleRel eventvehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (eventvehiclerel.Event == null)
                {
                    eventvehiclerel.Vehicle = null;
                    _eventRepo.InsertOrUpdateEventVehicle(eventvehiclerel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Events", new { id = eventvehiclerel.EventId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not EventOrganizationRel.
                    eventvehiclerel.Event = null;
                    _eventRepo.InsertOrUpdateEventVehicle(eventvehiclerel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Vehicles", new { id = eventvehiclerel.VehicleId });
                }
            }
            return View();
        }

        //
        // GET: /Vehicles/EditPersonVehicle/5
        public ActionResult EditEventVehicle(int id, string source)
        {
            var eventVehicleRel = _eventRepo.GetEventVehicle(id);
            if (source == "Events")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = eventVehicleRel.EventId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.VehicleId = eventVehicleRel.VehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventVehicle", eventVehicleRel);
            }
            return View(eventVehicleRel);

        }

        //
        // POST: /Vehicles/EditPersonVehicle/5
        [HttpPost]
        public ActionResult EditEventVehicle(EventVehicleRel eventvehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (eventvehiclerel.Event == null)
                {
                    //reset the event object.  This is only added from Organization, not EventOrganizationRel.
                    eventvehiclerel.Vehicle = null;
                    _eventRepo.InsertOrUpdateEventVehicle(eventvehiclerel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Events", new { id = eventvehiclerel.EventId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not EventOrganizationRel.
                    eventvehiclerel.Event = null;
                    _eventRepo.InsertOrUpdateEventVehicle(eventvehiclerel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Vehicles", new { id = eventvehiclerel.VehicleId });
                }

            }
            return View();
        }

        [HttpPost]
        public void DeleteEventVehicle(int id)
        {
            _eventRepo.DeleteEventVehicle(id);
            _eventRepo.Save();
        }

        #endregion


        #region Media Images
        public ActionResult GetEventMediaImages(int eventId, int mediaImageId)
        {
            IQueryable<EventMediaImageRel> eventMediaImages;
            if (mediaImageId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.EventId = eventId;
                ViewBag.Controller = "Events";
                eventMediaImages = _eventRepo.GetEventMediaImages(p => p.Event.Id.Equals(eventId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.EventId = eventId;
                ViewBag.Controller = "MediaImages";
                eventMediaImages = _eventRepo.GetEventMediaImages(p => p.MediaImage.Id.Equals(mediaImageId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventMediaImageList", eventMediaImages);
            }
            return View(eventMediaImages);
        }

        public ActionResult CreateEventMediaImage(int eventId, int mediaImageId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var eventMediaImageRel = new EventMediaImageRel
            {
                MediaImageId = mediaImageId,
                EventId = eventId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaImageId == -1)
            {
                eventMediaImageRel.MediaImage = new MediaImage();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Events";
                ViewBag.EventId = eventId;
            }
            else
            {
                eventMediaImageRel.Event = new Event();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaImages";
                ViewBag.MediaImageId = mediaImageId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventMediaImage", eventMediaImageRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateEventMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,MediaImageId")] EventMediaImageRel eventMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                if (eventMediaImageRel.Event == null)
                {
                    eventMediaImageRel.MediaImage = null;
                    _eventRepo.InsertOrUpdateEventMedia(eventMediaImageRel);
                    _eventRepo.Save();
                    //return RedirectToAction("Details", "Events", new { id = eventMediaImageRel.EventId });
                    return null;
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not EventOrganizationRel.
                    eventMediaImageRel.Event = null;
                    _eventRepo.InsertOrUpdateEventMedia(eventMediaImageRel);
                    _eventRepo.Save();
                    //return RedirectToAction("Details", "MediaImages", new { id = eventMediaImageRel.MediaImageId });
                    return null;
                }
            }
            return View();

        }

        public ActionResult EditEventMediaImage(int id, string source)
        {
            var eventMediaImageRel = _eventRepo.GetEventMediaImage(id);
            if (source == "Events")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.EventId = eventMediaImageRel.EventId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = eventMediaImageRel.MediaImageId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventMediaImage", eventMediaImageRel);
            }
            return View(eventMediaImageRel);

        }

        //
        // POST: /EventPersonRels/EditEventPerson
        [HttpPost]
        public ActionResult EditEventMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,EventId,MediaImageId")] EventMediaImageRel eventMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                if (eventMediaImageRel.Event == null)
                {
                    //reset the event object.  This is only added from Organization, not EventOrganizationRel.
                    eventMediaImageRel.MediaImage = null;
                    _eventRepo.InsertOrUpdateEventMedia(eventMediaImageRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Events", new { id = eventMediaImageRel.EventId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not EventOrganizationRel.
                    eventMediaImageRel.Event = null;
                    _eventRepo.InsertOrUpdateEventMedia(eventMediaImageRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "MediaImages", new { id = eventMediaImageRel.MediaImageId });
                }

            }
            return View();
        }

        [HttpPost]
        public void DeleteEventMediaImage(int id)
        {
            _eventRepo.DeleteEventMedia(id);
            _eventRepo.Save();
        }

        #endregion


        #region Events

        public ActionResult GetEventEvents(int eventId)
        {
            Event events;
            //if (eventId == -1)
            //{
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
            ViewBag.EventId = eventId;
            ViewBag.Event2Id = eventId;
            ViewBag.Controller = "Events";
            try
            {
                events = _eventRepo.GetEvent(eventId);
                //                events = _eventRepository.Get(p => p.Id.Equals(eventId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventEventList", events);
            }
            return View(events);
        }

        public ActionResult CreateEventEvent(int eventId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var eventEventRel = new EventEventRel
            {
                EventId = eventId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
                Event2 = new Event(),
            };

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
            ViewBag.EventId = eventId;
            ViewBag.Event2Id = -1;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventEvent", eventEventRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateEventEvent([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,Event2Id")] EventEventRel eventEventRel)
        {
            if (ModelState.IsValid)
            {
                eventEventRel.Event2 = null;
                _eventRepo.InsertOrUpdateEventEvent(eventEventRel);
                _eventRepo.Save();
                return RedirectToAction("Details", "Events", new { id = eventEventRel.EventId });
            }
            return View();

        }

        public ActionResult EditEventEvent(int id)
        {
            var eventEventRel = _eventRepo.GetEventEvent(id);
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventEvent", eventEventRel);
            }
            return View(eventEventRel);

        }

        //
        // POST: /EventPersonRels/EditEventPerson
        [HttpPost]
        public ActionResult EditEventEvent(EventEventRel eventEventRel)
        {
            if (ModelState.IsValid)
            {
                _eventRepo.InsertOrUpdateEventEvent(eventEventRel);
                _eventRepo.Save();
                return RedirectToAction("Details", "Events", new { id = eventEventRel.EventId });
            }

            return View();
        }

        [HttpPost]
        public void DeleteEventEvent(int id)
        {
            _eventRepo.DeleteEventEvent(id);
            _eventRepo.Save();
        }

        #endregion


        #region Media Audio Video
        public ActionResult GetEventMediaAudioVideos(int eventId, int mediaAudioVideoId)
        {
            IQueryable<EventMediaAudioVideoRel> eventMediaAudioVideos;
            if (mediaAudioVideoId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.EventId = eventId;
                ViewBag.Controller = "Events";
                eventMediaAudioVideos = _eventRepo.GetEventMediaAudioVideos(p => p.Event.Id.Equals(eventId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.EventId = eventId;
                ViewBag.Controller = "MediaAudioVideos";
                eventMediaAudioVideos = _eventRepo.GetEventMediaAudioVideos(p => p.MediaAudioVideo.Id.Equals(mediaAudioVideoId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventMediaAudioVideoList", eventMediaAudioVideos);
            }
            return View(eventMediaAudioVideos);
        }

        public ActionResult CreateEventMediaAudioVideo(int eventId, int mediaAudioVideoId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var eventMediaAudioVideoRel = new EventMediaAudioVideoRel
            {
                MediaAudioVideoId = mediaAudioVideoId,
                EventId = eventId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaAudioVideoId == -1)
            {
                eventMediaAudioVideoRel.MediaAudioVideo = new MediaAudioVideo();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Events";
                ViewBag.EventId = eventId;
            }
            else
            {
                eventMediaAudioVideoRel.Event = new Event();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaAudioVideos";
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventMediaAudioVideo", eventMediaAudioVideoRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateEventMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,MediaAudioVideoId")] EventMediaAudioVideoRel eventMediaAudioVideoRel)
        {
            if (ModelState.IsValid)
            {
                if (eventMediaAudioVideoRel.Event == null)
                {
                    eventMediaAudioVideoRel.MediaAudioVideo = null;
                    _eventRepo.InsertOrUpdateEventMediaAudioVideo(eventMediaAudioVideoRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Events", new { id = eventMediaAudioVideoRel.EventId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not EventOrganizationRel.
                    eventMediaAudioVideoRel.Event = null;
                    _eventRepo.InsertOrUpdateEventMediaAudioVideo(eventMediaAudioVideoRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "MediaAudioVideos", new { id = eventMediaAudioVideoRel.MediaAudioVideoId });
                }
            }
            return View();

        }

        public ActionResult EditEventMediaAudioVideo(int id, string source)
        {
            var eventMediaAudioVideoRel = _eventRepo.GetEventMediaAudioVideo(id);
            if (source == "Events")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.EventId = eventMediaAudioVideoRel.EventId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaAudioVideoId = eventMediaAudioVideoRel.MediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventMediaAudioVideo", eventMediaAudioVideoRel);
            }
            return View(eventMediaAudioVideoRel);

        }

        //
        // POST: /EventPersonRels/EditEventPerson
        [HttpPost]
        public ActionResult EditEventMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,EventId,MediaAudioVideoId")] EventMediaAudioVideoRel eventMediaAudioVideoRel)
        {
            if (ModelState.IsValid)
            {
                if (eventMediaAudioVideoRel.Event == null)
                {
                    //reset the event object.  This is only added from Organization, not EventOrganizationRel.
                    eventMediaAudioVideoRel.MediaAudioVideo = null;
                    _eventRepo.InsertOrUpdateEventMediaAudioVideo(eventMediaAudioVideoRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Events", new { id = eventMediaAudioVideoRel.EventId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not EventOrganizationRel.
                    eventMediaAudioVideoRel.Event = null;
                    _eventRepo.InsertOrUpdateEventMediaAudioVideo(eventMediaAudioVideoRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "MediaAudioVideos", new { id = eventMediaAudioVideoRel.MediaAudioVideoId });
                }

            }
            return View();
        }

        [HttpPost]
        public void DeleteEventMediaAudioVideo(int id)
        {
            _eventRepo.DeleteEventMediaAudioVideo(id);
            _eventRepo.Save();
        }

        #endregion


        #region Media Website/EGroup
        public ActionResult GetEventMediaWebsiteEGroups(int eventId, int mediaWebsiteEGroupId)
        {
            IQueryable<EventMediaWebsiteEGroupRel> eventMediaWebsiteEGroups;
            if (mediaWebsiteEGroupId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.EventId = eventId;
                ViewBag.Controller = "Events";
                eventMediaWebsiteEGroups = _eventRepo.GetEventMediaWebsiteEGroups(p => p.Event.Id.Equals(eventId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.EventId = eventId;
                ViewBag.Controller = "MediaWebsiteEGroups";
                eventMediaWebsiteEGroups = _eventRepo.GetEventMediaWebsiteEGroups(p => p.MediaWebsiteEGroup.Id.Equals(mediaWebsiteEGroupId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventMediaWebsiteEGroupList", eventMediaWebsiteEGroups);
            }
            return View(eventMediaWebsiteEGroups);
        }

        public ActionResult CreateEventMediaWebsiteEGroup(int eventId, int mediaWebsiteEGroupId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var eventMediaWebsiteEGroupRel = new EventMediaWebsiteEGroupRel
            {
                MediaWebsiteEGroupId = mediaWebsiteEGroupId,
                EventId = eventId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaWebsiteEGroupId == -1)
            {
                eventMediaWebsiteEGroupRel.MediaWebsiteEGroup = new MediaWebsiteEGroup();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Events";
                ViewBag.EventId = eventId;
            }
            else
            {
                eventMediaWebsiteEGroupRel.Event = new Event();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaWebsiteEGroups";
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventMediaWebsiteEGroup", eventMediaWebsiteEGroupRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateEventMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,MediaWebsiteEGroupId")] EventMediaWebsiteEGroupRel eventMediaWebsiteEGroupRel)
        {
            if (ModelState.IsValid)
            {
                if (eventMediaWebsiteEGroupRel.Event == null)
                {
                    eventMediaWebsiteEGroupRel.MediaWebsiteEGroup = null;
                    _eventRepo.InsertOrUpdateEventMediaWebsiteEGroup(eventMediaWebsiteEGroupRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Events", new { id = eventMediaWebsiteEGroupRel.EventId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not EventOrganizationRel.
                    eventMediaWebsiteEGroupRel.Event = null;
                    _eventRepo.InsertOrUpdateEventMediaWebsiteEGroup(eventMediaWebsiteEGroupRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = eventMediaWebsiteEGroupRel.MediaWebsiteEGroupId });
                }
            }
            return View();

        }

        public ActionResult EditEventMediaWebsiteEGroup(int id, string source)
        {
            var eventMediaWebsiteEGroupRel = _eventRepo.GetEventMediaWebsiteEGroup(id);
            if (source == "Events")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.EventId = eventMediaWebsiteEGroupRel.EventId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaWebsiteEGroupId = eventMediaWebsiteEGroupRel.MediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventMediaWebsiteEGroup", eventMediaWebsiteEGroupRel);
            }
            return View(eventMediaWebsiteEGroupRel);

        }

        //
        // POST: /EventPersonRels/EditEventPerson
        [HttpPost]
        public ActionResult EditEventMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,EventId")] EventMediaWebsiteEGroupRel eventMediaWebsiteEGroupRel)
        {
            if (ModelState.IsValid)
            {
                if (eventMediaWebsiteEGroupRel.Event == null)
                {
                    //reset the event object.  This is only added from Organization, not EventOrganizationRel.
                    eventMediaWebsiteEGroupRel.MediaWebsiteEGroup = null;
                    _eventRepo.InsertOrUpdateEventMediaWebsiteEGroup(eventMediaWebsiteEGroupRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Events", new { id = eventMediaWebsiteEGroupRel.EventId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not EventOrganizationRel.
                    eventMediaWebsiteEGroupRel.Event = null;
                    _eventRepo.InsertOrUpdateEventMediaWebsiteEGroup(eventMediaWebsiteEGroupRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = eventMediaWebsiteEGroupRel.MediaWebsiteEGroupId });
                }

            }
            return View();
        }

        [HttpPost]
        public void DeleteEventMediaWebsiteEGroup(int id)
        {
            _eventRepo.DeleteEventMediaWebsiteEGroup(id);
            _eventRepo.Save();
        }

        #endregion


        #region Subscription
        public ActionResult GetEventSubscriptions(int eventId, int subscriptionId)
        {
            IQueryable<EventSubscriptionRel> eventSubscriptions;
            if (subscriptionId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.EventId = eventId;
                ViewBag.Controller = "Events";
                eventSubscriptions = _eventRepo.GetEventSubscriptions(p => p.Event.Id.Equals(eventId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.EventId = eventId;
                ViewBag.Controller = "Subscriptions";
                eventSubscriptions = _eventRepo.GetEventSubscriptions(p => p.Subscription.Id.Equals(subscriptionId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventSubscriptionList", eventSubscriptions);
            }
            return View(eventSubscriptions);
        }

        public ActionResult CreateEventSubscription(int eventId, int subscriptionId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var eventSubscriptionRel = new EventSubscriptionRel
            {
                SubscriptionId = subscriptionId,
                EventId = eventId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (subscriptionId == -1)
            {
                eventSubscriptionRel.Subscription = new Subscription();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Events";
                ViewBag.EventId = eventId;
            }
            else
            {
                eventSubscriptionRel.Event = new Event();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Subscriptions";
                ViewBag.SubscriptionId = subscriptionId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventSubscription", eventSubscriptionRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateEventSubscription([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,SubscriptionId")] EventSubscriptionRel eventSubscriptionRel)
        {
            if (ModelState.IsValid)
            {
                if (eventSubscriptionRel.Event == null)
                {
                    eventSubscriptionRel.Subscription = null;
                    _eventRepo.InsertOrUpdateEventSubscription(eventSubscriptionRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Events", new { id = eventSubscriptionRel.EventId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not EventOrganizationRel.
                    eventSubscriptionRel.Event = null;
                    _eventRepo.InsertOrUpdateEventSubscription(eventSubscriptionRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Subscriptions", new { id = eventSubscriptionRel.SubscriptionId });
                }
            }
            return View();

        }

        public ActionResult EditEventSubscription(int id, string source)
        {
            var eventSubscriptionRel = _eventRepo.GetEventSubscription(id);
            if (source == "Events")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.EventId = eventSubscriptionRel.EventId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.SubscriptionId = eventSubscriptionRel.SubscriptionId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventSubscription", eventSubscriptionRel);
            }
            return View(eventSubscriptionRel);

        }

        //
        // POST: /EventPersonRels/EditEventPerson
        [HttpPost]
        public ActionResult EditEventSubscription([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,SubscriptionId")] EventSubscriptionRel eventSubscriptionRel)
        {
            if (ModelState.IsValid)
            {
                if (eventSubscriptionRel.Event == null)
                {
                    //reset the event object.  This is only added from Organization, not EventOrganizationRel.
                    eventSubscriptionRel.Subscription = null;
                    _eventRepo.InsertOrUpdateEventSubscription(eventSubscriptionRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Events", new { id = eventSubscriptionRel.EventId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not EventOrganizationRel.
                    eventSubscriptionRel.Event = null;
                    _eventRepo.InsertOrUpdateEventSubscription(eventSubscriptionRel);
                    _eventRepo.Save();
                    return RedirectToAction("Details", "Subscriptions", new { id = eventSubscriptionRel.SubscriptionId });
                }

            }
            return View();
        }

        [HttpPost]
        public void DeleteEventSubscription(int id)
        {
            _eventRepo.DeleteEventSubscription(id);
            _eventRepo.Save();
        }

        #endregion


        #region Status History

        public ActionResult GetEventStatusHistories(int id)
        {
            var entities = _eventRepo.GetEventStatusHistories(p => p.EventId.Equals(id)).ToArray().OrderBy(c => c.ActiveYear);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventStatusHistoryList", entities);
            }
            return View("Index");
        }

        #endregion

        //-----------------------------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}