using Caseiro.Mvc.PagedList.Extensions;
using splc.beholder.web.Utility;
using splc.data.repository;
using splc.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class VehiclesController : BaseController
    {
        private readonly ILookupRepository _lookupRepo;
        private readonly IVehicleRepository _vehicleRepo;

        public VehiclesController(
            ILookupRepository lookupRepo,
            IVehicleRepository vehicleRepo)
        {
            _lookupRepo = lookupRepo;
            _vehicleRepo = vehicleRepo;

            ViewBag.PossibleVehicleMakes = _lookupRepo.GetVehicleMakes();
            ViewBag.PossibleVehicleModels = _lookupRepo.GetVehicleModels();
            ViewBag.PossibleVehicleTypes = _lookupRepo.GetVehicleTypes();
            ViewBag.PossibleVehicleColors = _lookupRepo.GetVehicleColors();
            ViewBag.PossibleRemovalStatus = _lookupRepo.GetRemovalStatus();
            ViewBag.PossiblePrimaryStatus = _lookupRepo.GetPrimaryStatuses();
            ViewBag.PossibleApprovalStatus = _lookupRepo.GetApprovalStatuses();
        }

        public JsonResult GetVehicleList(string term)
        {
            term = term.Trim();
            var pred = PredicateBuilder.True<Vehicle>();
            if (!string.IsNullOrWhiteSpace(term))
            {
                pred = pred.And(
                        x =>
                            x.VIN.Contains(term) || x.VehicleMake.Name.Contains(term) ||
                            x.VehicleModel.Name.Contains(term));
            }

            var list = _vehicleRepo.GetVehicles(pred).ToArray().Select(e => new { e.Id, label = e.ToString() });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Vehicles/
        public ActionResult Index(int? page, int? pageSize, string searchTerm = "")
        {
            searchTerm = searchTerm.Trim();

            Session["page"] = page ?? 1;
            Session["searchTerm"] = searchTerm;

            var pred = PredicateBuilder.True<Vehicle>();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                pred = pred.And(
                        x =>
                            x.VIN.Contains(searchTerm) || x.VehicleMake.Name.Contains(searchTerm) ||
                            x.VehicleModel.Name.Contains(searchTerm));
            }
            var list = _vehicleRepo.GetVehicles(pred)
                               .OrderBy(m => m.VehicleMake.Name)
                               .ThenBy(m => m.VehicleModel.Name)
                               .ToPagedList(page ?? 1, pageSize ?? 15);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_VehicleList", list);
            }
            return View("Index", list);
        }

        //
        // GET: /Vehicles/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.VehicleId = id;
            ViewBag.BeholderPersonId = -1;
            ViewBag.PersonId = -1;
            ViewBag.OrganizationId = -1;
            ViewBag.ChapterId = -1;
            ViewBag.EventId = -1;
            ViewBag.MediaImageId = -1;
            ViewBag.MediaCorrespondenceId = -1;
            ViewBag.Controller = "Vehicles";
            return View(_vehicleRepo.GetVehicle(id));
        }

        //
        // GET: /Vehicles/Create
        public ActionResult Create()
        {
            var tags = new List<VehicleTag>().ToList(); // _lookupRepo.GetEventTypes().Where(x => l.Contains(x.Id)).ToList();
            var vehicle = new Vehicle();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEdit", vehicle);
            }
            return View(vehicle);
        }

        //
        // POST: /Vehicles/Create

        [HttpPost]
        public ActionResult Create(Vehicle vehicle, string[] vehicletags)
        {
            if (ModelState.IsValid)
            {
                _vehicleRepo.InsertOrUpdate(vehicle);
                _vehicleRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = vehicle.Id });
            }
            return View();
        }

        //
        // GET: /Vehicles/Edit/5
        public ActionResult Edit(int id)
        {
            var vehicle = _vehicleRepo.GetVehicle(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEdit", vehicle);
            }
            return View(vehicle);
        }

        //
        // POST: /Vehicles/Edit/5
        [HttpPost]
        public ActionResult Edit(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _vehicleRepo.InsertOrUpdate(vehicle);
                _vehicleRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = vehicle.Id });
            }
            return View();
        }

        public ActionResult RemoveVehicle(int id)
        {
            var vehicle = _vehicleRepo.GetVehicle(id);
            return View(vehicle);
        }

        [HttpPost]
        public ActionResult RemoveVehicle(int id, string removalreason)
        {
            var vehicle = _vehicleRepo.GetVehicle(id);
            vehicle.RemovalReason = removalreason;

            vehicle.RemovalStatusId = 1;
            _vehicleRepo.InsertOrUpdate(vehicle);
            _vehicleRepo.Save();

            return RedirectToAction("Index");

        }

        public ActionResult GetVehicleVehicles(int vehicleId)
        {
            Vehicle vehicles;
            //if (vehicleId == -1)
            //{
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
            ViewBag.VehicleId = vehicleId;
            ViewBag.Vehicle2Id = vehicleId;
            ViewBag.Controller = "Vehicles";
            try
            {
                vehicles = _vehicleRepo.GetVehicle(vehicleId);
                //                vehicles = _vehicleRepository.Get(p => p.Id.Equals(vehicleId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_VehicleVehicleList", vehicles);
            }
            return View(vehicles);
        }

        public ActionResult CreateVehicleVehicle(int vehicleId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var vehicleVehicleRel = new VehicleVehicleRel
            {
                VehicleId = vehicleId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
                Vehicle2 = new Vehicle()
            };

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
            ViewBag.VehicleId = vehicleId;
            ViewBag.Vehicle2Id = -1;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditVehicleVehicle", vehicleVehicleRel);
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateVehicleVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,VehicleId,Vehicle2Id")] VehicleVehicleRel vehicleVehicleRel)
        {
            if (ModelState.IsValid)
            {
                vehicleVehicleRel.Vehicle2 = null;
                _vehicleRepo.InsertOrUpdateVehicleVehicle(vehicleVehicleRel);
                _vehicleRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = vehicleVehicleRel.VehicleId });
            }
            return View();
        }

        public ActionResult EditVehicleVehicle(int id)
        {
            var vehicleVehicleRel = _vehicleRepo.GetVehicleVehicle(id);

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditVehicleVehicle", vehicleVehicleRel);
            }
            return View(vehicleVehicleRel);
        }

        //
        // POST: /VehiclePersonRels/EditVehiclePerson
        [HttpPost]
        public ActionResult EditVehicleVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,VehicleId,Vehicle2Id")] VehicleVehicleRel vehicleVehicleRel)
        {
            if (ModelState.IsValid)
            {
                _vehicleRepo.InsertOrUpdateVehicleVehicle(vehicleVehicleRel);
                _vehicleRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = vehicleVehicleRel.VehicleId });
            }

            return View();
        }

        [HttpPost]
        public void DeleteVehicleVehicle(int id)
        {
            _vehicleRepo.DeleteVehicleVehicle(id);
            _vehicleRepo.Save();
        }

        //
        // GET: /Vehicles/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_vehicleRepo.GetVehicle(id));
        }

        //
        // POST: /Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _vehicleRepo.Delete(id);
            _vehicleRepo.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}