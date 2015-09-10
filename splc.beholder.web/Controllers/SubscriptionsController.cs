using Caseiro.Mvc.PagedList.Extensions;
using MvcContrib.Pagination;
using splc.beholder.web.Utility;
using splc.data.repository;
using splc.domain.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class SubscriptionsController : BaseController
    {
        private readonly ISubscriptionRepository _subscriptionRepo;
        private readonly ILookupRepository _lookupRepo;

        public SubscriptionsController(
            ILookupRepository lookupRepo,
            ISubscriptionRepository subscriptionRepo)
        {
            _lookupRepo = lookupRepo;
            _subscriptionRepo = subscriptionRepo;

            ViewBag.PossiblePrimaryStatuses = _lookupRepo.GetPrimaryStatuses().OrderBy(x => x.SortOrder);
            ViewBag.PossibleApprovalStatus = _lookupRepo.GetApprovalStatuses().OrderBy(x => x.SortOrder);
            ViewBag.PossibleActiveStatus = _lookupRepo.GetActiveStatuses().OrderBy(x => x.SortOrder);
            ViewBag.PossibleMovementClasses = _lookupRepo.GetMovementClasses().OrderBy(x => x.SortOrder);
            ViewBag.PossibleRemovalStatuses = _lookupRepo.GetRemovalStatus().OrderBy(x => x.SortOrder);
        }

        // GET: Search for BeholderPersons 
        public JsonResult GetDropdown(string term)
        {
            term = term.Trim();
            var list = _subscriptionRepo.GetSubscriptions(p => p.PublicationName.Contains(term)).ToArray();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubscriptionList(string term)
        {
            term = term.Trim();
            var list = _subscriptionRepo.GetSubscriptions(x => x.PublicationName.Contains(term)).ToArray().Select(e => new
            {
                Id = e.Id,
                label = e.PublicationName
            });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ImageResult ShowPrimaryImage(int id)
        {
            byte[] picture;
            string contentType = null;
            Image image = null;

            var mediaImage = _subscriptionRepo.GetPrimaryImage(id);

            if (mediaImage.MediaImage != null || mediaImage.MediaImage.Image == null)
            {
                picture = System.IO.File.ReadAllBytes(Server.MapPath("~/content/images/image_unavailable.jpg"));
                contentType = "image/x-png";
            }
            else
            {
                picture = ImageHelper.Decompress(mediaImage.MediaImage.Image.IMAGE1);
                contentType = image.MimeType.Name;
            }

            return this.Image(picture, contentType);
        }

        public ViewResult Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim();
            return Search(searchTerm, null);
        }

        public ViewResult Search(string searchTerm, int? activeStatus)
        {
            searchTerm = searchTerm.Trim();
            var list = activeStatus == null ?
                                  _subscriptionRepo.GetSubscriptions(p => p.PublicationName.Contains(searchTerm)) :
                                  _subscriptionRepo.GetSubscriptions(p => p.PublicationName.Contains(searchTerm) && p.ActiveStatus.Equals(activeStatus));

            return View("Index", list as IPagination<Subscription>);
        }

        //
        // GET: /Subscriptions/
        //public ViewResult Index()
        public ActionResult Index(string searchterm = "", int? page = 1, int? pageSize = 15)
        {
            searchterm = searchterm.Trim();
            Session["page"] = page;
            Session["pageSize"] = pageSize;
            Session["searchTerm"] = searchterm;

            var list = _subscriptionRepo.GetSubscriptions(x => x.PublicationName.Contains(searchterm)).OrderBy(m => m.PublicationName).ToPagedList(page ?? 1, pageSize ?? 15);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SubscriptionList", list);
            }

            return View("Index", list);

        }

        //
        // GET: /Subscriptions/Details/5
        public ViewResult Details(int id)
        {
            ViewBag.SubscriptionId = id;
            //ViewBag.Subscription2Id = id;
            //ViewBag.ChapterId = -1;
            //ViewBag.SubscriptionId = -1;
            //ViewBag.BeholderPersonId = -1;
            //ViewBag.PersonId = -1;
            //ViewBag.EventId = -1;
            //ViewBag.MediaImageId = -1;
            //ViewBag.MediaAudioVideoId = -1;
            //ViewBag.MediaCorrespondenceId = -1;
            //ViewBag.MediaPublishedId = -1;
            //ViewBag.VehicleId = -1;
            ViewBag.Controller = "Subscriptions";

            return View(_subscriptionRepo.GetSubscription(id));
        }

        public ActionResult Create()
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes().OrderBy(x => x.SortOrder);
            var subscription = new Subscription
                {
                    ActiveStatusId = Queryable.SingleOrDefault(_lookupRepo.GetActiveStatuses(), p => p.Name.Equals("Active")).Id,
                    //SubscriptionRate = DateTime.Now,
                    CreatedUserId = 1,
                    DateCreated = DateTime.Now,
                    ModifiedUserId = 1,
                    DateModified = DateTime.Now,
                };
            return View(subscription);

        }

        //
        // POST: /Subscriptions/Create
        [HttpPost]
        public ActionResult Create(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                _subscriptionRepo.InsertOrUpdate(subscription);
                _subscriptionRepo.Save();
                return RedirectToAction("Details", new { id = subscription.Id });
            }
            return View();
        }

        //
        // GET: /Subscriptions/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes().OrderBy(x => x.SortOrder);
            var subscription = _subscriptionRepo.GetSubscription(id);
            return View(subscription);
        }

        //
        // POST: /Subscriptions/Edit/5
        [HttpPost]
        public ActionResult Edit(Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                _subscriptionRepo.InsertOrUpdate(subscription);
                _subscriptionRepo.Save();
                return RedirectToAction("Details", new { id = subscription.Id });
            }
            return View();
        }

        public ActionResult RemoveSubscription(int id)
        {
            var subscription = _subscriptionRepo.GetSubscription(id);
            return View(subscription);
        }

        [HttpPost]
        public ActionResult RemoveSubscription(int id, string removalreason)
        {
            var subscription = _subscriptionRepo.GetSubscription(id);
            subscription.RemovalReason = removalreason;

            subscription.RemovalStatusId = 1;
            _subscriptionRepo.InsertOrUpdate(subscription);
            _subscriptionRepo.Save();

            return RedirectToAction("Index");

        }

        //
        // GET: /Subscriptions/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_subscriptionRepo.GetSubscription(id));
        }

        //
        // POST: /Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _subscriptionRepo.Delete(id);
            _subscriptionRepo.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }


        #region Vehicles

        public ActionResult GetSubscriptionVehicles(int subscriptionId, int vehicleId)
        {
            IQueryable<SubscriptionVehicleRel> subscriptionVehicles;
            if (vehicleId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.Controller = "Subscriptions";
                subscriptionVehicles = _subscriptionRepo.GetSubscriptionVehicles(p => p.Subscription.Id.Equals(subscriptionId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.Controller = "Vehicles";
                subscriptionVehicles = _subscriptionRepo.GetSubscriptionVehicles(p => p.Vehicle.Id.Equals(vehicleId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SubscriptionVehicleList", subscriptionVehicles);
            }
            return View(subscriptionVehicles);
        }

        //
        // GET: /Vehicles/CreatePersonVehicle
        public ActionResult CreateSubscriptionVehicle(int vehicleId, int subscriptionId)
        {
            var approvalStatusId = Queryable.SingleOrDefault(_lookupRepo.GetApprovalStatuses(), p => p.Name.Equals("New")).Id;
            var subscriptionVehicleRel = new SubscriptionVehicleRel()
            {
                VehicleId = vehicleId,
                SubscriptionId = subscriptionId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (vehicleId == -1)
            {
                subscriptionVehicleRel.Vehicle = new Vehicle { CreatedUserId = 1 };
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Subscriptions";
                ViewBag.SubscriptionId = subscriptionId;
            }
            else
            {
                subscriptionVehicleRel.Subscription = new Subscription { CreatedUserId = 1 };
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Vehicles";
                ViewBag.VehicleId = vehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditSubscriptionVehicle", subscriptionVehicleRel);
            }

            return View();

        }

        //
        // POST: /PersonVehicleRels/CreatePersonVehicle
        [HttpPost]
        public ActionResult CreateSubscriptionVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,SubscriptionId,VehicleId")] SubscriptionVehicleRel subscriptionvehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (subscriptionvehiclerel.Subscription == null)
                {
                    subscriptionvehiclerel.Vehicle = null;
                    _subscriptionRepo.InsertOrUpdateSubscriptionVehicle(subscriptionvehiclerel);
                    _subscriptionRepo.Save();
                    return RedirectToAction("Details", "Subscriptions", new { id = subscriptionvehiclerel.SubscriptionId });
                }
                //reset the subscription object.  This is only added from Subscription, not ChapterSubscriptionRel.
                subscriptionvehiclerel.Subscription = null;
                _subscriptionRepo.InsertOrUpdateSubscriptionVehicle(subscriptionvehiclerel);
                _subscriptionRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = subscriptionvehiclerel.VehicleId });
            }
            return View();
        }

        //
        // GET: /Vehicles/EditPersonVehicle/5
        public ActionResult EditSubscriptionVehicle(int id, string source)
        {
            var subscriptionVehicleRel = _subscriptionRepo.GetSubscriptionVehicle(id);
            if (source == "Subscriptions")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = subscriptionVehicleRel.SubscriptionId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.VehicleId = subscriptionVehicleRel.VehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditSubscriptionVehicle", subscriptionVehicleRel);
            }
            return View(subscriptionVehicleRel);

        }

        //
        // POST: /Vehicles/EditPersonVehicle/5
        [HttpPost]
        public ActionResult EditSubscriptionVehicle(SubscriptionVehicleRel subscriptionvehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (subscriptionvehiclerel.Subscription == null)
                {
                    //reset the chapter object.  This is only added from Subscription, not ChapterSubscriptionRel.
                    subscriptionvehiclerel.Vehicle = null;
                    _subscriptionRepo.InsertOrUpdateSubscriptionVehicle(subscriptionvehiclerel);
                    _subscriptionRepo.Save();
                    return RedirectToAction("Details", "Subscriptions", new { id = subscriptionvehiclerel.SubscriptionId });
                }
                //reset the subscription object.  This is only added from Subscription, not ChapterSubscriptionRel.
                subscriptionvehiclerel.Subscription = null;
                _subscriptionRepo.InsertOrUpdateSubscriptionVehicle(subscriptionvehiclerel);
                _subscriptionRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = subscriptionvehiclerel.VehicleId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteSubscriptionVehicle(int id)
        {
            _subscriptionRepo.DeleteSubscriptionVehicle(id);
            _subscriptionRepo.Save();
        }

        #endregion


        #region Subscriptions

        public ActionResult GetSubscriptionSubscriptions(int subscriptionId)
        {
            //if (chapterId == -1)
            //{
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
            ViewBag.SubscriptionId = subscriptionId;
            ViewBag.Subscription2Id = subscriptionId;
            ViewBag.Controller = "Subscription";
            Subscription subscription = _subscriptionRepo.GetSubscription(subscriptionId);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SubscriptionSubscriptionList", subscription);
            }
            return View(subscription);
        }

        public ActionResult CreateSubscriptionSubscription(int subscriptionId)
        {
            var approvalStatusId = Queryable.SingleOrDefault(_lookupRepo.GetApprovalStatuses(), p => p.Name.Equals("New")).Id;
            var subscriptionSubscriptionRel = new SubscriptionSubscriptionRel
                {
                    SubscriptionId = subscriptionId,
                    ApprovalStatusId = approvalStatusId,
                    DateStart = DateTime.Now,
                    Subscription2 = new Subscription { CreatedUserId = 1 },
                };

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
            ViewBag.SubscriptionId = subscriptionId;
            ViewBag.Subscription2Id = -1;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditSubscriptionSubscription", subscriptionSubscriptionRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateSubscriptionSubscription([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,SubscriptionId,Subscription2Id")] SubscriptionSubscriptionRel subscriptionSubscriptionRel)
        {
            if (ModelState.IsValid)
            {
                subscriptionSubscriptionRel.Subscription2 = null;
                _subscriptionRepo.InsertOrUpdateSubscriptionSubscription(subscriptionSubscriptionRel);
                _subscriptionRepo.Save();
                return RedirectToAction("Details", "Subscriptions", new { id = subscriptionSubscriptionRel.SubscriptionId });
            }
            return View();

        }

        public ActionResult EditSubscriptionSubscription(int id)
        {
            var subscriptionSubscriptionRel = _subscriptionRepo.GetSubscriptionSubscription(id);

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditSubscriptionSubscription", subscriptionSubscriptionRel);
            }
            return View(subscriptionSubscriptionRel);

        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditSubscriptionSubscription(SubscriptionSubscriptionRel subscriptionSubscriptionRel)
        {
            if (ModelState.IsValid)
            {
                _subscriptionRepo.InsertOrUpdateSubscriptionSubscription(subscriptionSubscriptionRel);
                _subscriptionRepo.Save();
                return RedirectToAction("Details", "Subscriptions", new { id = subscriptionSubscriptionRel.SubscriptionId });
            }

            return View();
        }

        [HttpPost]
        public void DeleteSubscriptionSubscription(int id)
        {
            _subscriptionRepo.DeleteSubscriptionSubscription(id);
            _subscriptionRepo.Save();
        }

        #endregion


    }
}


