using System;
using System.Linq;
using System.Web.Mvc;
using MvcContrib.Pagination;
using splc.domain.Models;
using splc.data.repository;
using splc.beholder.web.Utility;
using splc.data;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class ContactInfoPersonRelsController : Controller
    {

        private readonly IContactInfoPersonRelRepository _contactInfoPersonRelRepo;
        private readonly ILookupRepository _lookupRepo;
        private readonly IBeholderPersonRepository _beholderPersonRepo;


        public ContactInfoPersonRelsController(
            IContactInfoPersonRelRepository contactInfoPersonRelRepo,
            ILookupRepository lookupRepo,
            IBeholderPersonRepository beholderPersonRepo)
        {
            _contactInfoPersonRelRepo = contactInfoPersonRelRepo;
            _lookupRepo = lookupRepo;
            _beholderPersonRepo = beholderPersonRepo;

            ViewBag.PossibleContactInfoTypes = _lookupRepo.GetContactInfoTypes();
            ViewBag.PossiblePrimaryStatus = _lookupRepo.GetPrimaryStatuses();
        }

        // GET: /ContactInfoPersonRels/5
        public ActionResult GetPersonContactInfo(int id)
        {
            ViewBag.CommonPersonId = id;
            var contactInfo = _contactInfoPersonRelRepo.GetContactInfo(p => p.CommonPerson.Id.Equals(id));
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonContactInfoList", contactInfo);
            }
            return View("Index");
        }

        ////
        //// GET: /ContactInfoPersonRels/

        //public ViewResult Index()
        //{
        //    return View(_contactInfoPersonRelRepo.AllIncluding(contactinfopersonrel => contactinfopersonrel.ContactInfo, contactinfopersonrel => contactinfopersonrel.ContactInfoType, contactinfopersonrel => contactinfopersonrel.PrimaryStatus).Take(10));
        //}

        ////
        //// GET: /ContactInfoPersonRels/Details/5

        //public ViewResult Details(int id)
        //{
        //    return View(_contactInfoPersonRelRepo.Find(id));
        //}

        //
        // GET: /ContactInfoPersonRels/Create

        public ActionResult Create(int personId)
        {
            //var person = context.BeholderPersons.Find(personId);
            //var person = _beholderPersonRepo.Find(personId);
            var contactInfo = new ContactInfoPersonRel
                                  {
                                      PersonId = personId,
                                      ContactInfo = new ContactInfo()
                                  };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEdit", contactInfo);
            }
            return View(contactInfo);
        }

        //
        // POST: /ContactInfoPersonRels/Create

        [HttpPost]
        public ActionResult Create(ContactInfoPersonRel contactinfopersonrel)
        {
            ViewBag.CommonPersonId = contactinfopersonrel.PersonId;
            if (ModelState.IsValid)
            {
                _contactInfoPersonRelRepo.InsertOrUpdate(contactinfopersonrel);
                _contactInfoPersonRelRepo.Save();
//                return RedirectToAction("Details", "Persons", new { id = contactinfopersonrel.PersonId });
            }
            return View(contactinfopersonrel);
        }

        //
        // GET: /ContactInfoPersonRels/Edit/5

        public ActionResult Edit(int id)
        {
            var contactInfo = _contactInfoPersonRelRepo.Find(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEdit", contactInfo);
            }
            return View(contactInfo);
        }

        //
        // POST: /ContactInfoPersonRels/Edit/5

        [HttpPost]
        public ActionResult Edit(ContactInfoPersonRel contactinfopersonrel)
        {
            ViewBag.CommonPersonId = contactinfopersonrel.PersonId;
            if (ModelState.IsValid)
            {
                _contactInfoPersonRelRepo.InsertOrUpdate(contactinfopersonrel);
                _contactInfoPersonRelRepo.Save();
//                return RedirectToAction("Details", "Persons", new { id = contactinfopersonrel.PersonId });
            }
            return View(contactinfopersonrel);
        }

        //
        // GET: /ContactInfoPersonRels/Delete/5

        public void Delete(int id)
        {
            _contactInfoPersonRelRepo.Delete(id);
            _contactInfoPersonRelRepo.Save();
        }

        //
        // POST: /ContactInfoPersonRels/Delete/5

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _contactInfoPersonRelRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

