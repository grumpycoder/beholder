using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using splc.domain.Models;
using splc.beholder.web;
using splc.data.repository;

namespace splc.beholder.web.Controllers
{   
    public class OrganizationMediaImageRelsController : Controller
    {
		private readonly IOrganizationRepository organizationRepository;
		private readonly IMediaImageRepository mediaimageRepository;
		private readonly IRelationshipTypeRepository relationshiptypeRepository;
		private readonly IApprovalStatusRepository approvalstatusRepository;
		private readonly IPrimaryStatusRepository primarystatusRepository;
		private readonly IOrganizationMediaImageRelRepository organizationmediaimagerelRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrganizationMediaImageRelsController() : this(new OrganizationRepository(), new MediaImageRepository(), new RelationshipTypeRepository(), new ApprovalStatusRepository(), new PrimaryStatusRepository(), new OrganizationMediaImageRelRepository())
        {
        }

        public OrganizationMediaImageRelsController(IOrganizationRepository organizationRepository, IMediaImageRepository mediaimageRepository, IRelationshipTypeRepository relationshiptypeRepository, IApprovalStatusRepository approvalstatusRepository, IPrimaryStatusRepository primarystatusRepository, IOrganizationMediaImageRelRepository organizationmediaimagerelRepository)
        {
			this.organizationRepository = organizationRepository;
			this.mediaimageRepository = mediaimageRepository;
			this.relationshiptypeRepository = relationshiptypeRepository;
			this.approvalstatusRepository = approvalstatusRepository;
			this.primarystatusRepository = primarystatusRepository;
			this.organizationmediaimagerelRepository = organizationmediaimagerelRepository;
        }

        //
        // GET: /OrganizationMediaImageRels/

        public ViewResult Index()
        {
            return View(organizationmediaimagerelRepository.AllIncluding(organizationmediaimagerel => organizationmediaimagerel.Organization, organizationmediaimagerel => organizationmediaimagerel.MediaImage, organizationmediaimagerel => organizationmediaimagerel.RelationshipType, organizationmediaimagerel => organizationmediaimagerel.ApprovalStatus, organizationmediaimagerel => organizationmediaimagerel.PrimaryStatus));
        }

        //
        // GET: /OrganizationMediaImageRels/Details/5

        public ViewResult DetailsOrgMedImgRels(long id)
        {
            return View(organizationmediaimagerelRepository.Find(id));
        }

        public ViewResult Details(long id)
        {
            return View(organizationmediaimagerelRepository.Find(id));
        }

        //
        // GET: /OrganizationMediaImageRels/Create

        public ActionResult Create()
        {
			ViewBag.PossibleOrganizations = organizationRepository.All;
			ViewBag.PossibleMediaImages = mediaimageRepository.All;
			ViewBag.PossibleRelationshipTypes = relationshiptypeRepository.All;
			ViewBag.PossibleApprovalStatus = approvalstatusRepository.All;
			ViewBag.PossiblePrimaryStatus = primarystatusRepository.All;
            return View();
        } 

        //
        // POST: /OrganizationMediaImageRels/Create

        [HttpPost]
        public ActionResult Create(OrganizationMediaImageRel organizationmediaimagerel)
        {
            if (ModelState.IsValid) {
                organizationmediaimagerelRepository.InsertOrUpdate(organizationmediaimagerel);
                organizationmediaimagerelRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleOrganizations = organizationRepository.All;
				ViewBag.PossibleMediaImages = mediaimageRepository.All;
				ViewBag.PossibleRelationshipTypes = relationshiptypeRepository.All;
				ViewBag.PossibleApprovalStatus = approvalstatusRepository.All;
				ViewBag.PossiblePrimaryStatus = primarystatusRepository.All;
				return View();
			}
        }
        
        //
        // GET: /OrganizationMediaImageRels/Edit/5
 
        public ActionResult Edit(long id)
        {
			ViewBag.PossibleOrganizations = organizationRepository.All;
			ViewBag.PossibleMediaImages = mediaimageRepository.All;
			ViewBag.PossibleRelationshipTypes = relationshiptypeRepository.All;
			ViewBag.PossibleApprovalStatus = approvalstatusRepository.All;
			ViewBag.PossiblePrimaryStatus = primarystatusRepository.All;
             return View(organizationmediaimagerelRepository.Find(id));
        }

        //
        // POST: /OrganizationMediaImageRels/Edit/5

        [HttpPost]
        public ActionResult Edit(OrganizationMediaImageRel organizationmediaimagerel)
        {
            if (ModelState.IsValid) {
                organizationmediaimagerelRepository.InsertOrUpdate(organizationmediaimagerel);
                organizationmediaimagerelRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleOrganizations = organizationRepository.All;
				ViewBag.PossibleMediaImages = mediaimageRepository.All;
				ViewBag.PossibleRelationshipTypes = relationshiptypeRepository.All;
				ViewBag.PossibleApprovalStatus = approvalstatusRepository.All;
				ViewBag.PossiblePrimaryStatus = primarystatusRepository.All;
				return View();
			}
        }

        //
        // GET: /OrganizationMediaImageRels/Delete/5
 
        public ActionResult Delete(long id)
        {
            return View(organizationmediaimagerelRepository.Find(id));
        }

        //
        // POST: /OrganizationMediaImageRels/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            organizationmediaimagerelRepository.Delete(id);
            organizationmediaimagerelRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                organizationRepository.Dispose();
                mediaimageRepository.Dispose();
                relationshiptypeRepository.Dispose();
                approvalstatusRepository.Dispose();
                primarystatusRepository.Dispose();
                organizationmediaimagerelRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

