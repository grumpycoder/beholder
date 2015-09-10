using System.Linq;
using System.Web.Mvc;
using splc.domain.Models;
using splc.data.repository;
using splc.data;
using System;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class AddressesController : BaseController
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressPersonRelRepository _addresspersonrelRepository;
        private readonly IAddressChapterRelRepository _addresschapterrelRepository;
        private readonly IAddressEventRelRepository _addresseventrelRepository;
        private readonly IAddressContactRelRepository _addresscontactrelRepository;
        private readonly IAddressSubscriptionsRelRepository _addresssubscriptionsrelRepository;
        private readonly ILookupRepository _lookupRepo;
        private readonly IPersonRepository _personRepo;
        private readonly IBeholderPersonRepository _beholderPersonRepo;

        public AddressesController(IAddressRepository addressRepository,
                                   IAddressChapterRelRepository addresschapterrelRepository,
                                   IAddressPersonRelRepository addresspersonrelRepository,
                                   IAddressEventRelRepository addresseventrelRepository,
                                   IAddressContactRelRepository addresscontactrelRepository,
                                   IAddressSubscriptionsRelRepository addresssubscriptionsrelRepository, 
                                   ILookupRepository lookupRepo, 
                                   IPersonRepository personRepo,
                                   IBeholderPersonRepository beholderPersonRepo)
        {
            _lookupRepo = lookupRepo;
            _addressRepository = addressRepository;
            _addresspersonrelRepository = addresspersonrelRepository;
            _addresschapterrelRepository = addresschapterrelRepository;
            _addresseventrelRepository = addresseventrelRepository;
            _addresscontactrelRepository = addresscontactrelRepository;
            _addresssubscriptionsrelRepository = addresssubscriptionsrelRepository;
            _beholderPersonRepo = beholderPersonRepo;
            _personRepo = personRepo; 

            ViewBag.PossibleStates = _lookupRepo.GetStates();
            ViewBag.PossibleAddressTypes = _lookupRepo.GetAddressTypes();
            ViewBag.PossiblePrimaryStatus = _lookupRepo.GetPrimaryStatuses();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _addressRepository.Dispose();
                _addresspersonrelRepository.Dispose();
                _addresschapterrelRepository.Dispose();
                _addresseventrelRepository.Dispose();
                _addresscontactrelRepository.Dispose();
                _addresssubscriptionsrelRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Person

        public ActionResult GetPersonAddresses(int id)
        {
            ViewBag.CommonPersonId = id;
            var addresses = _addresspersonrelRepository.GetAddresses(p => p.PersonId.Equals(id)).OrderByDescending(p => p.DateModified);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonAddressList", addresses);
            }
            return View("Index");
        }

        //
        // GET: /Addresses/CreatePersonAddress
        public ActionResult CreatePersonAddress(int personId)
        {
            //var person = context.BeholderPersons.Find(personId);
            //var person = _beholderPersonRepo.Find(currentUser, personId);

            var addresslist = _addresspersonrelRepository.GetAddresses(p => p.PersonId.Equals(personId)).Where(x => x.PrimaryStatusId == 1);
            ViewBag.hasPrimary = addresslist.Any();
            
            var address = new AddressPersonRel
            {
                PersonId = personId,
                FirstKnownUseDate = DateTime.Now,
                Address = new Address()
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonAddress", address);
            }
            return View(address);
        }

        //
        // POST: /Addresses/CreatePersonAddress
        [HttpPost]
        public ActionResult CreatePersonAddress(AddressPersonRel addresspersonrel)
        {
            ViewBag.CommonPersonId = addresspersonrel.PersonId;
            if (ModelState.IsValid)
            {
                _addresspersonrelRepository.InsertOrUpdate(addresspersonrel);
                _addresspersonrelRepository.Save();

//                return RedirectToAction("Details", "Persons", new { id = addresspersonrel.PersonId });
            }
            return View(addresspersonrel);
        }

        //
        // GET: /Addresses/EditPersonAddress/5
        public ActionResult EditPersonAddress(int id)
        {
            var address = _addresspersonrelRepository.Find(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonAddress", address);
            }

            return View(address);
        }

        //
        // POST: /Addresses/EditPersonAddress/5
        [HttpPost]
        public ActionResult EditPersonAddress(AddressPersonRel addresspersonrel)
        {
            ViewBag.CommonPersonId = addresspersonrel.PersonId;
            if (ModelState.IsValid)
            {
                _addresspersonrelRepository.InsertOrUpdate(addresspersonrel);
                _addresspersonrelRepository.Save();

                //return RedirectToAction("Details", "AddressPersonRels", new { id = addresspersonrel.Id });
            }
            return View(addresspersonrel);
        }

        //
        // POST: /Addresses/DeletePersonAddress/5
        [HttpPost]
        public void DeletePersonAddress(int id)
        {

            _addresspersonrelRepository.Delete(id);
            _addresspersonrelRepository.Save();
            //_addressRepository.Delete(id);
            //_addressRepository.Save();
        }

        #endregion


        #region Contact

        public ActionResult GetContactAddresses(int id)
        {
            ViewBag.CommonPersonId = id;

            var addresses = _addresscontactrelRepository.GetAddresses(p => p.ContactId.Equals(id)).OrderByDescending(p => p.DateModified);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ContactAddressList", addresses);
            }
            return View("Index");
        }

        //
        // GET: /Addresses/CreateContactAddress
        public ActionResult CreateContactAddress(int contactId)
        {
            var address = new AddressContactRel
            {
                ContactId = contactId,
                FirstKnownUseDate = DateTime.Now,
                Address = new Address()            
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditContactAddress", address);
            }
            return View(address);
        }

        //
        // POST: /Addresses/CreateContactAddress
        [HttpPost]
        public ActionResult CreateContactAddress(AddressContactRel addresscontactrel)
        {
            ViewBag.ContactId = addresscontactrel.ContactId;
            if (ModelState.IsValid)
            {
                _addresscontactrelRepository.InsertOrUpdate(addresscontactrel);
                _addresscontactrelRepository.Save();
                return null;
                //return RedirectToAction("Details", "Contacts", new { id = addresscontactrel.ContactId });
            }
            return View();
        }

        //
        // GET: /Addresses/EditContactAddress/5
        public ActionResult EditContactAddress(int id)
        {
            var address = _addresscontactrelRepository.Find(id);
            ViewBag.ContactId = address.ContactId;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditContactAddress", address);
            }

            return View(address);
        }

        //
        // POST: /Addresses/EditContactAddress/5
        [HttpPost]
        public ActionResult EditContactAddress(AddressContactRel addresscontactrel)
        {
            if (ModelState.IsValid)
            {
                _addresscontactrelRepository.InsertOrUpdate(addresscontactrel);
                _addresscontactrelRepository.Save();
                return null;
                //return RedirectToAction("Details", "AddressContactRels", new { id = addresscontactrel.Id });
            }
            return View();
        }

        //
        // POST: /Addresses/DeleteContactAddress/5
        [HttpPost]
        public void DeleteContactAddress(int id)
        {
            _addresscontactrelRepository.Delete(id);
            _addresscontactrelRepository.Save();
            //_addressRepository.Delete(id);
            //_addressRepository.Save();
        }

        #endregion


        #region Chapter

        public ActionResult GetChapterAddresses(int id)
        {
            ViewBag.ChapterId = id;
            var addresses = _addresschapterrelRepository.GetAddresses(p => p.ChapterId.Equals(id)).OrderByDescending(p=>p.DateModified);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterAddressList", addresses);
            }
            return View("Index");
        }

        //
        // GET: /Addresses/CreatePersonAddress
        public ActionResult CreateChapterAddress(int chapterId)
        {
            var address = new AddressChapterRel
            {
                ChapterId = chapterId,
                FirstKnownUseDate = DateTime.Now,
                Address = new Address()
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterAddress", address);
            }
            return View(address);
        }

        //
        // POST: /Addresses/CreatePersonAddress
        [HttpPost]
        public ActionResult CreateChapterAddress(AddressChapterRel addresschapterrel)
        {
            if (ModelState.IsValid)
            {
                _addresschapterrelRepository.InsertOrUpdate(addresschapterrel);
                _addresschapterrelRepository.Save();

                return RedirectToAction("Details", "Chapters", new { id = addresschapterrel.ChapterId });
            }
            return View();
        }

        //
        // GET: /Addresses/EditPersonAddress/5
        public ActionResult EditChapterAddress(int id)
        {
            var address = _addresschapterrelRepository.Find(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterAddress", address);
            }

            return View(address);
        }

        //
        // POST: /Addresses/EditPersonAddress/5
        [HttpPost]
        public ActionResult EditChapterAddress(AddressChapterRel addresschapterrel)
        {
            if (ModelState.IsValid)
            {
                _addresschapterrelRepository.InsertOrUpdate(addresschapterrel);
                _addresschapterrelRepository.Save();

                return RedirectToAction("Details", "Chapters", new { id = addresschapterrel.ChapterId });
            }
            return View();
        }

        //
        // POST: /Addresses/DeletePersonAddress/5
        [HttpPost]
        public void DeleteChapterAddress(int id)
        {

            _addresschapterrelRepository.Delete(id);
            _addresschapterrelRepository.Save();
            //_addressRepository.Delete(id);
            //_addressRepository.Save();
        }

        #endregion


        #region Event

        public ActionResult GetEventAddresses(int id)
        {
            ViewBag.EventId = id;
            var addresses = _addresseventrelRepository.GetAddresses(p => p.EventId.Equals(id)).OrderByDescending(p => p.DateModified);

            //if (Request.IsAjaxRequest())
            //{
                return PartialView("_EventAddressList", addresses);
            //}
            //return View("Index");
        }

        //
        // GET: /Addresses/CreatePersonAddress
        public ActionResult CreateEventAddress(int eventId)
        {
            var address = new AddressEventRel
            {
                EventId = eventId,
                FirstKnownUseDate = DateTime.Now,
                Address = new Address()
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventAddress", address);
            }
            return View(address);
        }

        //
        // POST: /Addresses/CreatePersonAddress
        [HttpPost]
        public ActionResult CreateEventAddress(AddressEventRel addresseventrel)
        {
            if (ModelState.IsValid)
            {
                _addresseventrelRepository.InsertOrUpdate(addresseventrel);
                _addresseventrelRepository.Save();

                return RedirectToAction("Details", "Events", new { id = addresseventrel.EventId });
            }
            return View();
        }

        //
        // GET: /Addresses/EditPersonAddress/5
        public ActionResult EditEventAddress(int id)
        {
            var address = _addresseventrelRepository.Find(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditEventAddress", address);
            }

            return View(address);
        }

        //
        // POST: /Addresses/EditPersonAddress/5
        [HttpPost]
        public ActionResult EditEventAddress(AddressEventRel addresseventrel)
        {
            if (ModelState.IsValid)
            {
                _addresseventrelRepository.InsertOrUpdate(addresseventrel);
                _addresseventrelRepository.Save();

                return RedirectToAction("Details", "Events", new { id = addresseventrel.EventId });
            }
            return View();
        }

        //
        // POST: /Addresses/DeletePersonAddress/5
        [HttpPost]
        public void DeleteEventAddress(int id)
        {

            _addresseventrelRepository.Delete(id);
            _addresseventrelRepository.Save();
            //_addressRepository.Delete(id);
            //_addressRepository.Save();
        }

        #endregion


        #region Subscriptions

        public ActionResult GetSubscriptionsAddresses(int id)
        {
            ViewBag.SubscriptionsId = id;
            var addresses = _addresssubscriptionsrelRepository.GetAddresses(p => p.SubscriptionsId.Equals(id)).OrderByDescending(p => p.DateModified);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SubscriptionsAddressList", addresses);
            }
            return View("Index");
        }

        //
        // GET: /Addresses/CreatePersonAddress
        public ActionResult CreateSubscriptionsAddress(int subscriptionId)
        {
            var address = new AddressSubscriptionsRel
            {
                SubscriptionsId = subscriptionId,
                FirstKnownUseDate = DateTime.Now,
                Address = new Address()
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditSubscriptionsAddress", address);
            }
            return View(address);
        }

        //
        // POST: /Addresses/CreatePersonAddress
        [HttpPost]
        public ActionResult CreateSubscriptionsAddress(AddressSubscriptionsRel addresssubscriptionsrel)
        {
            ViewBag.SubscriptionId = addresssubscriptionsrel.SubscriptionsId;
            if (ModelState.IsValid)
            {
                _addresssubscriptionsrelRepository.InsertOrUpdate(addresssubscriptionsrel);
                _addresssubscriptionsrelRepository.Save();
                return null;
                //return RedirectToAction("Details", "Subscriptions", new { id = addresssubscriptionsrel.SubscriptionsId });
            }
            return View();
        }

        //
        // GET: /Addresses/EditPersonAddress/5
        public ActionResult EditSubscriptionsAddress(int id)
        {
            var address = _addresssubscriptionsrelRepository.Find(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditSubscriptionsAddress", address);
            }

            return View(address);
        }

        //
        // POST: /Addresses/EditPersonAddress/5
        [HttpPost]
        public ActionResult EditSubscriptionsAddress(AddressSubscriptionsRel addresssubscriptionsrel)
        {
            if (ModelState.IsValid)
            {
                _addresssubscriptionsrelRepository.InsertOrUpdate(addresssubscriptionsrel);
                _addresssubscriptionsrelRepository.Save();
                return null;
                //return RedirectToAction("Details", "Subscriptions", new { id = addresssubscriptionsrel.SubscriptionsId });
            }
            return View();
        }

        //
        // POST: /Addresses/DeletePersonAddress/5
        [HttpPost]
        public void DeleteSubscriptionsAddress(int id)
        {

            _addresssubscriptionsrelRepository.Delete(id);
            _addresssubscriptionsrelRepository.Save();
            //_addressRepository.Delete(id);
            //_addressRepository.Save();
        }

        #endregion

    }
}

