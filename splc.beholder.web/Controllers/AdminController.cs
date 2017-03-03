using splc.data;
using splc.data.repository;
using splc.domain.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        private readonly ILookupRepository _lookupRepo;
        private readonly ACDBContext _ctx;

        public AdminController(ACDBContext ctx, ILookupRepository lookupRepo)
        {
            _ctx = ctx;
            _lookupRepo = lookupRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lookup()
        {
            return View();
        }

        public ActionResult ActiveStatus()
        {
            var list = _lookupRepo.GetActiveStatuses();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ActiveStatusList", list);
            }
            return View("ActiveStatus", list);
        }

        public ActionResult CreateActiveStatus()
        {
            var list = _lookupRepo.GetActiveStatuses();
            var sortOrder = list.Max(x => x.SortOrder);

            var activeStatus = new ActiveStatus { SortOrder = sortOrder + 1 };

            return PartialView("_CreateOrEditActiveStatus", activeStatus);
        }

        [HttpPost]
        public ActionResult CreateActiveStatus(ActiveStatus activestatus)
        {
            if (!ModelState.IsValid) return null;
            _ctx.ActiveStatus.Add(activestatus);
            _ctx.SaveChanges();
            HttpRuntime.Cache.Remove("ActiveStatus");
            _lookupRepo.GetActiveStatuses(); //Refresh cache
            return null;
        }

        public ActionResult EditActiveStatus(int id)
        {
            var activeStatus = _lookupRepo.GetActiveStatuses().Single(x => x.Id == id);
            return PartialView("_CreateOrEditActiveStatus", activeStatus);
        }

        [HttpPost]
        public ActionResult EditActiveStatus(ActiveStatus activestatus)
        {
            if (!ModelState.IsValid) return View(activestatus);
            _ctx.Entry(activestatus).State = EntityState.Modified;
            _ctx.SaveChanges();
            _lookupRepo.GetActiveStatuses(); //Refresh cache
            return RedirectToAction("Index");
        }

        // GET: /Admin/DeleteActiveStatus/5
        public void DeleteActiveStatus(int id)
        {
            ActiveStatus activestatus = _ctx.ActiveStatus.Single(x => x.Id == id);
            activestatus.DateDeleted = DateTime.Now;
            _ctx.Entry(activestatus).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult AddressType()
        {
            var list = _lookupRepo.GetAddressTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_AddressTypeList", list);
            }
            return View("AddressType", list);
        }

        // GET: /Admin/CreateAddressType
        public ActionResult CreateAddressType()
        {
            var list = _lookupRepo.GetAddressTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var addressType = new AddressType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditAddressType", addressType);
        }

        // POST: /Admin/CreateAddressType
        [HttpPost]
        public ActionResult CreateAddressType(AddressType addressType)
        {
            if (ModelState.IsValid)
            {
                _ctx.AddressTypes.Add(addressType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditAddressType/5
        public ActionResult EditAddressType(int id)
        {
            var addressType = _lookupRepo.GetAddressTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditAddressType", addressType);
        }

        // POST: /Admin/EditAddressType
        [HttpPost]
        public ActionResult EditAddressType(AddressType addressType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(addressType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addressType);
        }

        // GET: /Admin/DeleteAddressType/5
        public void DeleteAddressType(int id)
        {
            AddressType addressType = _ctx.AddressTypes.Single(x => x.Id == id);
            addressType.DateDeleted = DateTime.Now;
            _ctx.Entry(addressType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }



        public ActionResult ApprovalStatus()
        {
            var list = _lookupRepo.GetApprovalStatuses();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ApprovalStatusList", list);
            }
            return View("ApprovalStatus", list);
        }

        // GET: /Admin/CreateActiveStatus
        public ActionResult CreateApprovalStatus()
        {
            var list = _lookupRepo.GetApprovalStatuses();
            var sortOrder = list.Max(x => x.SortOrder);

            var approvalStatus = new ApprovalStatus
            {
                DateCreated = DateTime.Now,
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditApprovalStatus", approvalStatus);
        }

        // POST: /Admin/CreateApprovalStatus
        [HttpPost]
        public ActionResult CreateApprovalStatus(ApprovalStatus approvalStatus)
        {
            if (ModelState.IsValid)
            {
                _ctx.ApprovalStatus.Add(approvalStatus);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditApprovalStatus/5
        public ActionResult EditApprovalStatus(int id)
        {
            var approvalStatus = _lookupRepo.GetApprovalStatuses().Single(x => x.Id == id);
            return PartialView("_CreateOrEditApprovalStatus", approvalStatus);
        }

        // POST: /Admin/EditApprovalStatus
        [HttpPost]
        public ActionResult EditApprovalStatus(ApprovalStatus approvalStatus)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(approvalStatus).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(approvalStatus);
        }

        // GET: /Admin/DeleteApprovalStatus/5
        public void DeleteApprovalStatus(int id)
        {
            ApprovalStatus approvalStatus = _ctx.ApprovalStatus.Single(x => x.Id == id);
            approvalStatus.DateDeleted = DateTime.Now;
            _ctx.Entry(approvalStatus).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult AudioVideoType()
        {
            var list = _lookupRepo.GetAudioVideoTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_AudioVideoTypeList", list);
            }
            return View("AudioVideoType", list);
        }

        // GET: /Admin/CreateAudioVideoType
        public ActionResult CreateAudioVideoType()
        {
            var list = _lookupRepo.GetAudioVideoTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var audioVideoType = new AudioVideoType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditAudioVideoType", audioVideoType);
        }

        // POST: /Admin/CreateAudioVideoType
        [HttpPost]
        public ActionResult CreateAudioVideoType(AudioVideoType audioVideoType)
        {
            if (ModelState.IsValid)
            {
                _ctx.AudioVideoTypes.Add(audioVideoType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditAudioVideoType/5
        public ActionResult EditAudioVideoType(int id)
        {
            var audioVideoType = _lookupRepo.GetAudioVideoTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditAudioVideoType", audioVideoType);
        }

        // POST: /Admin/EditAudioVideoType
        [HttpPost]
        public ActionResult EditAudioVideoType(AudioVideoType audioVideoType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(audioVideoType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(audioVideoType);
        }

        // GET: /Admin/DeleteAudioVideoType/5
        public void DeleteAudioVideoType(int id)
        {
            AudioVideoType audioVideoType = _ctx.AudioVideoTypes.Single(x => x.Id == id);
            audioVideoType.DateDeleted = DateTime.Now;
            _ctx.Entry(audioVideoType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult ChapterType()
        {
            var list = _lookupRepo.GetChapterTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterTypeList", list);
            }
            return View("ChapterType", list);
        }

        // GET: /Admin/CreateChapterType
        public ActionResult CreateChapterType()
        {
            var list = _lookupRepo.GetChapterTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var chapterType = new ChapterType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditChapterType", chapterType);
        }

        // POST: /Admin/CreateChapterType
        [HttpPost]
        public ActionResult CreateChapterType(ChapterType chapterType)
        {
            if (ModelState.IsValid)
            {
                _ctx.ChapterTypes.Add(chapterType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditChapterType/5
        public ActionResult EditChapterType(int id)
        {
            var chapterType = _lookupRepo.GetChapterTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditChapterType", chapterType);
        }

        // POST: /Admin/EditChapterType
        [HttpPost]
        public ActionResult EditChapterType(ChapterType chapterType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(chapterType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chapterType);
        }

        // GET: /Admin/DeleteChapterType/5
        public void DeleteChapterType(int id)
        {
            ChapterType chapterType = _ctx.ChapterTypes.Single(x => x.Id == id);
            chapterType.DateDeleted = DateTime.Now;
            _ctx.Entry(chapterType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public ActionResult ConfidentialityType()
        {
            var list = _lookupRepo.GetConfidentialityTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ConfidentialityTypeList", list);
            }
            return View("ConfidentialityType", list);
        }

        // GET: /Admin/CreateConfidentialityType
        public ActionResult CreateConfidentialityType()
        {
            var list = _lookupRepo.GetConfidentialityTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var confidentialityType = new ConfidentialityType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditConfidentialityType", confidentialityType);
        }

        // POST: /Admin/CreateConfidentialityType
        [HttpPost]
        public ActionResult CreateConfidentialityType(ConfidentialityType confidentialityType)
        {
            if (ModelState.IsValid)
            {
                _ctx.ConfidentialityTypes.Add(confidentialityType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditConfidentialityType/5
        public ActionResult EditConfidentialityType(int id)
        {
            var confidentialityType = _lookupRepo.GetConfidentialityTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditConfidentialityType", confidentialityType);
        }

        // POST: /Admin/EditConfidentialityType
        [HttpPost]
        public ActionResult EditConfidentialityType(ConfidentialityType confidentialityType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(confidentialityType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(confidentialityType);
        }

        // GET: /Admin/DeleteConfidentialityType/5
        public void DeleteConfidentialityType(int id)
        {
            ConfidentialityType confidentialityType = _ctx.ConfidentialityTypes.Single(x => x.Id == id);
            confidentialityType.DateDeleted = DateTime.Now;
            _ctx.Entry(confidentialityType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult ContactInfoType()
        {
            var list = _lookupRepo.GetContactInfoTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ContactInfoTypeList", list);
            }
            return View("ContactInfoType", list);
        }

        // GET: /Admin/CreateContactInfoType
        public ActionResult CreateContactInfoType()
        {
            var list = _lookupRepo.GetContactInfoTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var contactInfoType = new ContactInfoType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditContactInfoType", contactInfoType);
        }

        // POST: /Admin/CreateContactInfoType
        [HttpPost]
        public ActionResult CreateContactInfoType(ContactInfoType contactInfoType)
        {
            if (ModelState.IsValid)
            {
                _ctx.ContactInfoTypes.Add(contactInfoType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditContactInfoType/5
        public ActionResult EditContactInfoType(int id)
        {
            var contactInfoType = _lookupRepo.GetContactInfoTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditContactInfoType", contactInfoType);
        }

        // POST: /Admin/EditContactInfoType
        [HttpPost]
        public ActionResult EditContactInfoType(ContactInfoType contactInfoType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(contactInfoType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactInfoType);
        }

        // GET: /Admin/DeleteContactInfoType/5
        public void DeleteContactInfoType(int id)
        {
            ContactInfoType contactInfoType = _ctx.ContactInfoTypes.Single(x => x.Id == id);
            contactInfoType.DateDeleted = DateTime.Now;
            _ctx.Entry(contactInfoType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult CorrespondenceType()
        {
            var list = _lookupRepo.GetCorrespondenceTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CorrespondenceTypeList", list);
            }
            return View("CorrespondenceType", list);
        }

        // GET: /Admin/CreateCorrespondenceType
        public ActionResult CreateCorrespondenceType()
        {
            var list = _lookupRepo.GetCorrespondenceTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var correspondenceType = new CorrespondenceType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditCorrespondenceType", correspondenceType);
        }

        // POST: /Admin/CreateCorrespondenceType
        [HttpPost]
        public ActionResult CreateCorrespondenceType(CorrespondenceType correspondenceType)
        {
            if (ModelState.IsValid)
            {
                _ctx.CorrespondenceTypes.Add(correspondenceType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditCorrespondenceType/5
        public ActionResult EditCorrespondenceType(int id)
        {
            var correspondenceType = _lookupRepo.GetCorrespondenceTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditCorrespondenceType", correspondenceType);
        }

        // POST: /Admin/EditCorrespondenceType
        [HttpPost]
        public ActionResult EditCorrespondenceType(CorrespondenceType correspondenceType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(correspondenceType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(correspondenceType);
        }

        // GET: /Admin/DeleteCorrespondenceType/5
        public void DeleteCorrespondenceType(int id)
        {
            CorrespondenceType correspondenceType = _ctx.CorrespondenceTypes.Single(x => x.Id == id);
            correspondenceType.DateDeleted = DateTime.Now;
            _ctx.Entry(correspondenceType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult EventDocumentationType()
        {
            var list = _lookupRepo.GetEventDocumentationTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_EventDocumentationTypeList", list);
            }
            return View("EventDocumentationType", list);
        }

        // GET: /Admin/CreateEventDocumentationType
        public ActionResult CreateEventDocumentationType()
        {
            var list = _lookupRepo.GetEventDocumentationTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var eventDocumentationType = new EventDocumentationType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditEventDocumentationType", eventDocumentationType);
        }

        // POST: /Admin/CreateEventDocumentationType
        [HttpPost]
        public ActionResult CreateEventDocumentationType(EventDocumentationType eventDocumentationType)
        {
            if (ModelState.IsValid)
            {
                _ctx.EventDocumentationTypes.Add(eventDocumentationType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditEventDocumentationType/5
        public ActionResult EditEventDocumentationType(int id)
        {
            var eventDocumentationType = _lookupRepo.GetEventDocumentationTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditEventDocumentationType", eventDocumentationType);
        }

        // POST: /Admin/EditEventDocumentationType
        [HttpPost]
        public ActionResult EditEventDocumentationType(EventDocumentationType eventDocumentationType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(eventDocumentationType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventDocumentationType);
        }

        // GET: /Admin/DeleteEventDocumentationType/5
        public void DeleteEventDocumentationType(int id)
        {
            EventDocumentationType eventDocumentationType = _ctx.EventDocumentationTypes.Single(x => x.Id == id);
            eventDocumentationType.DateDeleted = DateTime.Now;
            _ctx.Entry(eventDocumentationType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult EyeColor()
        {
            var list = _lookupRepo.GetEyeColors();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_EyeColorList", list);
            }
            return View("EyeColor", list);
        }

        // GET: /Admin/CreateEyeColor
        public ActionResult CreateEyeColor()
        {
            var list = _lookupRepo.GetEyeColors();
            var sortOrder = list.Max(x => x.SortOrder);

            var eyeColor = new EyeColor
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditEyeColor", eyeColor);
        }

        // POST: /Admin/CreateEyeColor
        [HttpPost]
        public ActionResult CreateEyeColor(EyeColor eyeColor)
        {
            if (ModelState.IsValid)
            {
                _ctx.EyeColors.Add(eyeColor);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditEyeColor/5
        public ActionResult EditEyeColor(int id)
        {
            var eyeColor = _lookupRepo.GetEyeColors().Single(x => x.Id == id);
            return PartialView("_CreateOrEditEyeColor", eyeColor);
        }

        // POST: /Admin/EditEyeColor
        [HttpPost]
        public ActionResult EditEyeColor(EyeColor eyeColor)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(eyeColor).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eyeColor);
        }

        // GET: /Admin/DeleteEyeColor/5
        public void DeleteEyeColor(int id)
        {
            EyeColor eyeColor = _ctx.EyeColors.Single(x => x.Id == id);
            eyeColor.DateDeleted = DateTime.Now;
            _ctx.Entry(eyeColor).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public ActionResult HairColor()
        {
            var list = _lookupRepo.GetHairColors();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_HairColorList", list);
            }
            return View("HairColor", list);
        }

        // GET: /Admin/CreateHairColor
        public ActionResult CreateHairColor()
        {
            var list = _lookupRepo.GetHairColors();
            var sortOrder = list.Max(x => x.SortOrder);

            var hairColor = new HairColor
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditHairColor", hairColor);
        }

        // POST: /Admin/CreateHairColor
        [HttpPost]
        public ActionResult CreateHairColor(HairColor hairColor)
        {
            if (ModelState.IsValid)
            {
                _ctx.HairColors.Add(hairColor);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditHairColor/5
        public ActionResult EditHairColor(int id)
        {
            var hairColor = _lookupRepo.GetHairColors().Single(x => x.Id == id);
            return PartialView("_CreateOrEditHairColor", hairColor);
        }

        // POST: /Admin/EditHairColor
        [HttpPost]
        public ActionResult EditHairColor(HairColor hairColor)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(hairColor).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hairColor);
        }

        // GET: /Admin/DeleteHairColor/5
        public void DeleteHairColor(int id)
        {
            HairColor hairColor = _ctx.HairColors.Single(x => x.Id == id);
            hairColor.DateDeleted = DateTime.Now;
            _ctx.Entry(hairColor).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult HairPattern()
        {
            var list = _lookupRepo.GetHairPatterns();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_HairPatternList", list);
            }
            return View("HairPattern", list);
        }

        // GET: /Admin/CreateHairPattern
        public ActionResult CreateHairPattern()
        {
            var list = _lookupRepo.GetHairPatterns();
            var sortOrder = list.Max(x => x.SortOrder);

            var hairPattern = new HairPattern
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditHairPattern", hairPattern);
        }

        // POST: /Admin/CreateHairPattern
        [HttpPost]
        public ActionResult CreateHairPattern(HairPattern hairPattern)
        {
            if (ModelState.IsValid)
            {
                _ctx.HairPatterns.Add(hairPattern);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditHairPattern/5
        public ActionResult EditHairPattern(int id)
        {
            var hairPattern = _lookupRepo.GetHairPatterns().Single(x => x.Id == id);
            return PartialView("_CreateOrEditHairPattern", hairPattern);
        }

        // POST: /Admin/EditHairPattern
        [HttpPost]
        public ActionResult EditHairPattern(HairPattern hairPattern)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(hairPattern).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hairPattern);
        }

        // GET: /Admin/DeleteHairPattern/5
        public void DeleteHairPattern(int id)
        {
            HairPattern hairPattern = _ctx.HairPatterns.Single(x => x.Id == id);
            hairPattern.DateDeleted = DateTime.Now;
            _ctx.Entry(hairPattern).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult ImageType()
        {
            var list = _lookupRepo.GetImageTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ImageTypeList", list);
            }
            return View("ImageType", list);
        }

        // GET: /Admin/CreateImageType
        public ActionResult CreateImageType()
        {
            var list = _lookupRepo.GetImageTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var imageType = new ImageType
            {
                SortOrder = sortOrder + 1

            };
            return PartialView("_CreateOrEditImageType", imageType);
        }

        // POST: /Admin/CreateImageType
        [HttpPost]
        public ActionResult CreateImageType(ImageType imageType)
        {
            if (ModelState.IsValid)
            {
                _ctx.ImageTypes.Add(imageType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditImageType/5
        public ActionResult EditImageType(int id)
        {
            var imageType = _lookupRepo.GetImageTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditImageType", imageType);
        }

        // POST: /Admin/EditImageType
        [HttpPost]
        public ActionResult EditImageType(ImageType imageType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(imageType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(imageType);
        }

        // GET: /Admin/DeleteImageType/5
        public void DeleteImageType(int id)
        {
            ImageType imageType = _ctx.ImageTypes.Single(x => x.Id == id);
            imageType.DateDeleted = DateTime.Now;
            _ctx.Entry(imageType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public ActionResult LibraryCategoryType()
        {
            var list = _lookupRepo.GetLibraryCategoryTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_LibraryCategoryTypeList", list);
            }
            return View("LibraryCategoryType", list);
        }

        // GET: /Admin/CreateLibraryCategoryType
        public ActionResult CreateLibraryCategoryType()
        {
            var list = _lookupRepo.GetLibraryCategoryTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var libraryCategoryType = new LibraryCategoryType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditLibraryCategoryType", libraryCategoryType);
        }

        // POST: /Admin/CreateLibraryCategoryType
        [HttpPost]
        public ActionResult CreateLibraryCategoryType(LibraryCategoryType libraryCategoryType)
        {
            if (ModelState.IsValid)
            {
                _ctx.LibraryCategoryTypes.Add(libraryCategoryType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditLibraryCategoryType/5
        public ActionResult EditLibraryCategoryType(int id)
        {
            var libraryCategoryType = _lookupRepo.GetLibraryCategoryTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditLibraryCategoryType", libraryCategoryType);
        }

        // POST: /Admin/EditLibraryCategoryType
        [HttpPost]
        public ActionResult EditLibraryCategoryType(LibraryCategoryType libraryCategoryType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(libraryCategoryType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(libraryCategoryType);
        }

        // GET: /Admin/DeleteLibraryCategoryType/5
        public void DeleteLibraryCategoryType(int id)
        {
            LibraryCategoryType libraryCategoryType = _ctx.LibraryCategoryTypes.Single(x => x.Id == id);
            libraryCategoryType.DateDeleted = DateTime.Now;
            _ctx.Entry(libraryCategoryType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult LicenseType()
        {
            var list = _lookupRepo.GetLicenseTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_LicenseTypeList", list);
            }
            return View("LicenseType", list);
        }

        // GET: /Admin/CreateLicenseType
        public ActionResult CreateLicenseType()
        {
            var list = _lookupRepo.GetLicenseTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var licenseType = new LicenseType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditLicenseType", licenseType);
        }

        // POST: /Admin/CreateLicenseType
        [HttpPost]
        public ActionResult CreateLicenseType(LicenseType licenseType)
        {
            if (ModelState.IsValid)
            {
                _ctx.LicenseTypes.Add(licenseType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditLicenseType/5
        public ActionResult EditLicenseType(int id)
        {
            var licenseType = _lookupRepo.GetLicenseTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditLicenseType", licenseType);
        }

        // POST: /Admin/EditLicenseType
        [HttpPost]
        public ActionResult EditLicenseType(LicenseType licenseType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(licenseType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(licenseType);
        }

        // GET: /Admin/DeleteLicenseType/5
        public void DeleteLicenseType(int id)
        {
            LicenseType licenseType = _ctx.LicenseTypes.Single(x => x.Id == id);
            licenseType.DateDeleted = DateTime.Now;
            _ctx.Entry(licenseType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult MaritialStatus()
        {
            var list = _lookupRepo.GetMaritialStatuses();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_MaritialStatusList", list);
            }
            return View("MaritialStatus", list);
        }

        // GET: /Admin/CreateMaritialStatus
        public ActionResult CreateMaritialStatus()
        {
            var list = _lookupRepo.GetMaritialStatuses();
            var sortOrder = list.Max(x => x.SortOrder);

            var maritialStatus = new MaritialStatus
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditMaritialStatus", maritialStatus);
        }

        // POST: /Admin/CreateMaritialStatus
        [HttpPost]
        public ActionResult CreateMaritialStatus(MaritialStatus maritialStatus)
        {
            if (ModelState.IsValid)
            {
                _ctx.MaritialStatus.Add(maritialStatus);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditMaritialStatus/5
        public ActionResult EditMaritialStatus(int id)
        {
            var maritialStatus = _lookupRepo.GetMaritialStatuses().Single(x => x.Id == id);
            return PartialView("_CreateOrEditMaritialStatus", maritialStatus);
        }

        // POST: /Admin/EditMaritialStatus
        [HttpPost]
        public ActionResult EditMaritialStatus(MaritialStatus maritialStatus)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(maritialStatus).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maritialStatus);
        }

        // GET: /Admin/DeleteMaritialStatus/5
        public void DeleteMaritialStatus(int id)
        {
            MaritialStatus maritialStatus = _ctx.MaritialStatus.Single(x => x.Id == id);
            maritialStatus.DateDeleted = DateTime.Now;
            _ctx.Entry(maritialStatus).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public ActionResult MediaType()
        {
            var list = _lookupRepo.GetMediaTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaTypeList", list);
            }
            return View("MediaType", list);
        }

        // GET: /Admin/CreateMediaType
        public ActionResult CreateMediaType()
        {
            var list = _lookupRepo.GetMediaTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var mediaType = new MediaType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditMediaType", mediaType);
        }

        // POST: /Admin/CreateMediaType
        [HttpPost]
        public ActionResult CreateMediaType(MediaType mediaType)
        {
            if (ModelState.IsValid)
            {
                _ctx.MediaTypes.Add(mediaType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditMediaType/5
        public ActionResult EditMediaType(int id)
        {
            var mediaType = _lookupRepo.GetMediaTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditMediaType", mediaType);
        }

        // POST: /Admin/EditMediaType
        [HttpPost]
        public ActionResult EditMediaType(MediaType mediaType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(mediaType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mediaType);
        }

        // GET: /Admin/DeleteMediaType/5
        public void DeleteMediaType(int id)
        {
            MediaType mediaType = _ctx.MediaTypes.Single(x => x.Id == id);
            mediaType.DateDeleted = DateTime.Now;
            _ctx.Entry(mediaType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult MimeType()
        {
            var list = _lookupRepo.GetMimeTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_MimeTypeList", list);
            }
            return View("MimeType", list);
        }

        // GET: /Admin/CreateMimeType
        public ActionResult CreateMimeType()
        {
            var list = _lookupRepo.GetMimeTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var mimeType = new MimeType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditMimeType", mimeType);
        }

        // POST: /Admin/CreateMimeType
        [HttpPost]
        public ActionResult CreateMimeType(MimeType mimeType)
        {
            if (ModelState.IsValid)
            {
                _ctx.MimeTypes.Add(mimeType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditMimeType/5
        public ActionResult EditMimeType(int id)
        {
            var mimeType = _lookupRepo.GetMimeTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditMimeType", mimeType);
        }

        // POST: /Admin/EditMimeType
        [HttpPost]
        public ActionResult EditMimeType(MimeType mimeType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(mimeType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mimeType);
        }

        // GET: /Admin/DeleteMimeType/5
        public void DeleteMimeType(int id)
        {
            MimeType mimeType = _ctx.MimeTypes.Single(x => x.Id == id);
            mimeType.DateDeleted = DateTime.Now;
            _ctx.Entry(mimeType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult MovementClass()
        {
            var list = _lookupRepo.GetMovementClasses();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_MovementClassList", list);
            }
            return View("MovementClass", list);
        }

        // GET: /Admin/CreateMovementClass
        public ActionResult CreateMovementClass()
        {
            var list = _lookupRepo.GetMovementClasses();
            var sortOrder = list.Max(x => x.SortOrder);

            var movementClass = new MovementClass
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditMovementClass", movementClass);
        }

        // POST: /Admin/CreateMovementClass
        [HttpPost]
        public ActionResult CreateMovementClass(MovementClass movementClass)
        {
            if (ModelState.IsValid)
            {
                _ctx.MovementClasses.Add(movementClass);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditMovementClass/5
        public ActionResult EditMovementClass(int id)
        {
            var movementClass = _lookupRepo.GetMovementClasses().Single(x => x.Id == id);
            return PartialView("_CreateOrEditMovementClass", movementClass);
        }

        // POST: /Admin/EditMovementClass
        [HttpPost]
        public ActionResult EditMovementClass(MovementClass movementClass)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(movementClass).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movementClass);
        }

        // GET: /Admin/DeleteMovementClass/5
        public void DeleteMovementClass(int id)
        {
            MovementClass movementClass = _ctx.MovementClasses.Single(x => x.Id == id);
            movementClass.DateDeleted = DateTime.Now;
            _ctx.Entry(movementClass).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult NewsSourceType()
        {
            var list = _lookupRepo.GetNewSourceType();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_NewsSourceTypeList", list);
            }
            return View("NewsSourceType", list);
        }

        // GET: /Admin/CreateNewsSourceType
        public ActionResult CreateNewsSourceType()
        {
            var list = _lookupRepo.GetNewSourceType();
            var sortOrder = list.Max(x => x.SortOrder);

            var newsSourceType = new NewsSourceType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditNewsSourceType", newsSourceType);
        }

        // POST: /Admin/CreateNewsSourceType
        [HttpPost]
        public ActionResult CreateNewsSourceType(NewsSourceType newsSourceType)
        {
            if (ModelState.IsValid)
            {
                _ctx.NewsSourceTypes.Add(newsSourceType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditNewsSourceType/5
        public ActionResult EditNewsSourceType(int id)
        {
            var newsSourceType = _lookupRepo.GetNewSourceType().Single(x => x.Id == id);
            return PartialView("_CreateOrEditNewsSourceType", newsSourceType);
        }

        // POST: /Admin/EditNewsSourceType
        [HttpPost]
        public ActionResult EditNewsSourceType(NewsSourceType newsSourceType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(newsSourceType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsSourceType);
        }

        // GET: /Admin/DeleteNewsSourceType/5
        public void DeleteNewsSourceType(int id)
        {
            NewsSourceType newsSourceType = _ctx.NewsSourceTypes.Single(x => x.Id == id);
            newsSourceType.DateDeleted = DateTime.Now;
            _ctx.Entry(newsSourceType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public ActionResult OrganizationType()
        {
            var list = _lookupRepo.GetOrganizationTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationTypeList", list);
            }
            return View("OrganizationType", list);
        }

        // GET: /Admin/CreateOrganizationType
        public ActionResult CreateOrganizationType()
        {
            var list = _lookupRepo.GetOrganizationTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var organizationType = new OrganizationType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditOrganizationType", organizationType);
        }

        // POST: /Admin/CreateOrganizationType
        [HttpPost]
        public ActionResult CreateOrganizationType(OrganizationType organizationType)
        {
            if (ModelState.IsValid)
            {
                _ctx.OrganizationTypes.Add(organizationType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditOrganizationType/5
        public ActionResult EditOrganizationType(int id)
        {
            var organizationType = _lookupRepo.GetOrganizationTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditOrganizationType", organizationType);
        }

        // POST: /Admin/EditOrganizationType
        [HttpPost]
        public ActionResult EditOrganizationType(OrganizationType organizationType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(organizationType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organizationType);
        }

        // GET: /Admin/DeleteOrganizationType/5
        public void DeleteOrganizationType(int id)
        {
            OrganizationType organizationType = _ctx.OrganizationTypes.Single(x => x.Id == id);
            organizationType.DateDeleted = DateTime.Now;
            _ctx.Entry(organizationType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public ActionResult Prefix()
        {
            var list = _lookupRepo.GetPrefixes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PrefixList", list);
            }
            return View("Prefix", list);
        }

        // GET: /Admin/CreatePrefix
        public ActionResult CreatePrefix()
        {
            var list = _lookupRepo.GetPrefixes();
            var sortOrder = list.Max(x => x.SortOrder);

            var prefix = new Prefix
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditPrefix", prefix);
        }

        // POST: /Admin/CreatePrefix
        [HttpPost]
        public ActionResult CreatePrefix(Prefix prefix)
        {
            if (ModelState.IsValid)
            {
                //_ctx.Prefixes.Add(prefix);
                //_ctx.SaveChanges();
                _lookupRepo.SavePrefixes(prefix);
            }
            return null;
        }

        // GET: /Admin/EditPrefix/5
        public ActionResult EditPrefix(int id)
        {
            var prefix = _lookupRepo.GetPrefixes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditPrefix", prefix);
        }

        // POST: /Admin/EditPrefix
        [HttpPost]
        public ActionResult EditPrefix(Prefix prefix)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(prefix).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prefix);
        }

        // GET: /Admin/DeletePrefix/5
        public void DeletePrefix(int id)
        {
            Prefix prefix = _ctx.Prefixes.Single(x => x.Id == id);
            prefix.DateDeleted = DateTime.Now;
            _ctx.Entry(prefix).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public ActionResult PrimaryStatus()
        {
            var list = _lookupRepo.GetPrimaryStatuses();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PrimaryStatusList", list);
            }
            return View("PrimaryStatus", list);
        }

        // GET: /Admin/CreatePrimaryStatus
        public ActionResult CreatePrimaryStatus()
        {
            var list = _lookupRepo.GetPrimaryStatuses();
            var sortOrder = list.Max(x => x.SortOrder);

            var primaryStatus = new PrimaryStatus
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditPrimaryStatus", primaryStatus);
        }

        // POST: /Admin/CreatePrimaryStatus
        [HttpPost]
        public ActionResult CreatePrimaryStatus(PrimaryStatus primaryStatus)
        {
            if (ModelState.IsValid)
            {
                _ctx.PrimaryStatus.Add(primaryStatus);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditPrimaryStatus/5
        public ActionResult EditPrimaryStatus(int id)
        {
            var primaryStatus = _lookupRepo.GetPrimaryStatuses().Single(x => x.Id == id);
            return PartialView("_CreateOrEditPrimaryStatus", primaryStatus);
        }

        // POST: /Admin/EditPrimaryStatus
        [HttpPost]
        public ActionResult EditPrimaryStatus(PrimaryStatus primaryStatus)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(primaryStatus).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(primaryStatus);
        }

        // GET: /Admin/DeletePrimaryStatus/5
        public void DeletePrimaryStatus(int id)
        {
            PrimaryStatus primaryStatus = _ctx.PrimaryStatus.Single(x => x.Id == id);
            primaryStatus.DateDeleted = DateTime.Now;
            _ctx.Entry(primaryStatus).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult PublishedType()
        {
            var list = _lookupRepo.GetPublishedTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PublishedTypeList", list);
            }
            return View("PublishedType", list);
        }

        // GET: /Admin/CreatePublishedType
        public ActionResult CreatePublishedType()
        {
            var list = _lookupRepo.GetPublishedTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var publishedType = new PublishedType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditPublishedType", publishedType);
        }

        // POST: /Admin/CreatePublishedType
        [HttpPost]
        public ActionResult CreatePublishedType(PublishedType publishedType)
        {
            if (ModelState.IsValid)
            {
                _ctx.PublishedTypes.Add(publishedType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditPublishedType/5
        public ActionResult EditPublishedType(int id)
        {
            var publishedType = _lookupRepo.GetPublishedTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditPublishedType", publishedType);
        }

        // POST: /Admin/EditPublishedType
        [HttpPost]
        public ActionResult EditPublishedType(PublishedType publishedType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(publishedType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publishedType);
        }

        // GET: /Admin/DeletePublishedType/5
        public void DeletePublishedType(int id)
        {
            PublishedType publishedType = _ctx.PublishedTypes.Single(x => x.Id == id);
            publishedType.DateDeleted = DateTime.Now;
            _ctx.Entry(publishedType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult Race()
        {
            var list = _lookupRepo.GetRaces();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_RaceList", list);
            }
            return View("Race", list);
        }

        // GET: /Admin/CreateRace
        public ActionResult CreateRace()
        {
            var list = _lookupRepo.GetRaces();
            var sortOrder = list.Max(x => x.SortOrder);

            var race = new Race
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditRace", race);
        }

        // POST: /Admin/CreateRace
        [HttpPost]
        public ActionResult CreateRace(Race race)
        {
            if (ModelState.IsValid)
            {
                _ctx.Races.Add(race);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditRace/5
        public ActionResult EditRace(int id)
        {
            var race = _lookupRepo.GetRaces().Single(x => x.Id == id);
            return PartialView("_CreateOrEditRace", race);
        }

        // POST: /Admin/EditRace
        [HttpPost]
        public ActionResult EditRace(Race race)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(race).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(race);
        }

        // GET: /Admin/DeleteRace/5
        public void DeleteRace(int id)
        {
            Race race = _ctx.Races.Single(x => x.Id == id);
            race.DateDeleted = DateTime.Now;
            _ctx.Entry(race).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult RelationshipType()
        {

            var list = _lookupRepo.GetRelationshipTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_RelationshipTypeList", list);
            }
            return View("RelationshipType", list);
        }

        // GET: /Admin/CreateRelationshipType
        public ActionResult CreateRelationshipType()
        {
            var list = _lookupRepo.GetRelationshipTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var relationshipType = new RelationshipType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditRelationshipType", relationshipType);
        }

        // POST: /Admin/CreateRelationshipType
        [HttpPost]
        public ActionResult CreateRelationshipType(RelationshipType relationshipType)
        {
            if (ModelState.IsValid)
            {
                _ctx.RelationshipTypes.Add(relationshipType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditRelationshipType/5
        public ActionResult EditRelationshipType(int id)
        {
            var relationshipType = _lookupRepo.GetRelationshipTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditRelationshipType", relationshipType);
        }

        // POST: /Admin/EditRelationshipType
        [HttpPost]
        public ActionResult EditRelationshipType(RelationshipType relationshipType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(relationshipType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(relationshipType);
        }

        // GET: /Admin/DeleteRelationshipType/5
        public void DeleteRelationshipType(int id)
        {
            RelationshipType relationshipType = _ctx.RelationshipTypes.Single(x => x.Id == id);
            relationshipType.DateDeleted = DateTime.Now;
            _ctx.Entry(relationshipType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult RemovalStatus()
        {
            var list = _lookupRepo.GetRemovalStatus();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_RemovalStatusList", list);
            }
            return View("RemovalStatus", list);
        }

        // GET: /Admin/CreateRemovalStatus
        public ActionResult CreateRemovalStatus()
        {
            var list = _lookupRepo.GetRemovalStatus();
            var sortOrder = list.Max(x => x.SortOrder);

            var removalStatus = new RemovalStatus
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditRemovalStatus", removalStatus);
        }

        // POST: /Admin/CreateRemovalStatus
        [HttpPost]
        public ActionResult CreateRemovalStatus(RemovalStatus removalStatus)
        {
            if (ModelState.IsValid)
            {
                _ctx.RemovalStatus.Add(removalStatus);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditRemovalStatus/5
        public ActionResult EditRemovalStatus(int id)
        {
            var removalStatus = _lookupRepo.GetRemovalStatus().Single(x => x.Id == id);
            return PartialView("_CreateOrEditRemovalStatus", removalStatus);
        }

        // POST: /Admin/EditRemovalStatus
        [HttpPost]
        public ActionResult EditRemovalStatus(RemovalStatus removalStatus)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(removalStatus).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(removalStatus);
        }

        // GET: /Admin/DeleteRemovalStatus/5
        public void DeleteRemovalStatus(int id)
        {
            RemovalStatus removalStatus = _ctx.RemovalStatus.Single(x => x.Id == id);
            removalStatus.DateDeleted = DateTime.Now;
            _ctx.Entry(removalStatus).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult RenewalPermissionType()
        {
            var list = _lookupRepo.RenewalPermissionTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_RenewalPermissionTypeList", list);
            }
            return View("RenewalPermissionType", list);
        }

        // GET: /Admin/CreateRenewalPermissionType
        public ActionResult CreateRenewalPermissionType()
        {
            var list = _lookupRepo.RenewalPermissionTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var renewalPermissionType = new RenewalPermmisionType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditRenewalPermissionType", renewalPermissionType);
        }

        // POST: /Admin/CreateRenewalPermissionType
        [HttpPost]
        public ActionResult CreateRenewalPermissionType(RenewalPermmisionType renewalPermissionType)
        {
            if (ModelState.IsValid)
            {
                _ctx.RenewalPermmisionTypes.Add(renewalPermissionType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditRenewalPermissionType/5
        public ActionResult EditRenewalPermissionType(int id)
        {
            var renewalPermissionType = _lookupRepo.RenewalPermissionTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditRenewalPermissionType", renewalPermissionType);
        }

        // POST: /Admin/EditRenewalPermissionType
        [HttpPost]
        public ActionResult EditRenewalPermissionType(RenewalPermmisionType renewalPermissionType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(renewalPermissionType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(renewalPermissionType);
        }

        // GET: /Admin/DeleteRenewalPermissionType/5
        public void DeleteRenewalPermissionType(int id)
        {
            RenewalPermmisionType renewalPermissionType = _ctx.RenewalPermmisionTypes.Single(x => x.Id == id);
            renewalPermissionType.DateDeleted = DateTime.Now;
            _ctx.Entry(renewalPermissionType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult Suffix()
        {
            var list = _lookupRepo.GetSuffixes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_SuffixList", list);
            }
            return View("Suffix", list);
        }


        // GET: /Admin/CreateSuffix
        public ActionResult CreateSuffix()
        {
            var list = _lookupRepo.GetSuffixes();
            var sortOrder = list.Max(x => x.SortOrder);

            var suffix = new Suffix
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditSuffix", suffix);
        }

        // POST: /Admin/CreateSuffix
        [HttpPost]
        public ActionResult CreateSuffix(Suffix suffix)
        {
            if (ModelState.IsValid)
            {
                _ctx.Suffixes.Add(suffix);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditSuffix/5
        public ActionResult EditSuffix(int id)
        {
            var suffix = _lookupRepo.GetSuffixes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditSuffix", suffix);
        }

        // POST: /Admin/EditSuffix
        [HttpPost]
        public ActionResult EditSuffix(Suffix suffix)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(suffix).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suffix);
        }

        // GET: /Admin/DeleteSuffix/5
        public void DeleteSuffix(int id)
        {
            Suffix suffix = _ctx.Suffixes.Single(x => x.Id == id);
            suffix.DateDeleted = DateTime.Now;
            _ctx.Entry(suffix).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult States()
        {
            var list = _lookupRepo.GetStates();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_StatesList", list);
            }
            return View("States", list);
        }

        // GET: /Admin/CreateState
        public ActionResult CreateState()
        {
            var list = _lookupRepo.GetStates();

            var state = new State();
            return PartialView("_CreateOrEditState", state);
        }

        // POST: /Admin/CreateState
        [HttpPost]
        public ActionResult CreateState(State state)
        {
            if (ModelState.IsValid)
            {
                state.CreatedUserId = CurrentUser.Id;
                _ctx.States.Add(state);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditState/5
        public ActionResult EditState(int id)
        {
            var suffix = _lookupRepo.GetStates().Single(x => x.Id == id);
            return PartialView("_CreateOrEditState", suffix);
        }

        // POST: /Admin/EditState
        [HttpPost]
        public ActionResult EditState(State state)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(state).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(state);
        }

        // GET: /Admin/DeleteState/5
        public void DeleteState(int id)
        {
            State state = _ctx.States.Single(x => x.Id == id);
            _ctx.Entry(state).State = EntityState.Deleted;
            _ctx.SaveChanges();
        }

        public ActionResult TagType()
        {
            var list = _lookupRepo.GetTagTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_TagTypeList", list);
            }
            return View("TagType", list);
        }

        // GET: /Admin/CreateTagType
        public ActionResult CreateTagType()
        {
            var list = _lookupRepo.GetTagTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var tagType = new TagType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditTagType", tagType);
        }

        // POST: /Admin/CreateTagType
        [HttpPost]
        public ActionResult CreateTagType(TagType tagType)
        {
            if (ModelState.IsValid)
            {
                _ctx.TagTypes.Add(tagType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditTagType/5
        public ActionResult EditTagType(int id)
        {
            var tagType = _lookupRepo.GetTagTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditTagType", tagType);
        }

        // POST: /Admin/EditTagType
        [HttpPost]
        public ActionResult EditTagType(TagType tagType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(tagType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tagType);
        }

        // GET: /Admin/DeleteTagType/5
        public void DeleteTagType(int id)
        {
            TagType tagType = _ctx.TagTypes.Single(x => x.Id == id);
            tagType.DateDeleted = DateTime.Now;
            _ctx.Entry(tagType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }



        public ActionResult UserType()
        {
            var list = _lookupRepo.GetUserTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_UserTypeList", list);
            }
            return View("UserType", list);
        }

        // GET: /Admin/CreateUserType
        public ActionResult CreateUserType()
        {
            var list = _lookupRepo.GetUserTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var userType = new UserType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditUserType", userType);
        }

        // POST: /Admin/CreateUserType
        [HttpPost]
        public ActionResult CreateUserType(UserType userType)
        {
            if (ModelState.IsValid)
            {
                _ctx.UserTypes.Add(userType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditUserType/5
        public ActionResult EditUserType(int id)
        {
            var userType = _lookupRepo.GetUserTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditUserType", userType);
        }

        // POST: /Admin/EditUserType
        [HttpPost]
        public ActionResult EditUserType(UserType userType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(userType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userType);
        }

        // GET: /Admin/DeleteUserType/5
        public void DeleteUserType(int id)
        {
            UserType userType = _ctx.UserTypes.Single(x => x.Id == id);
            userType.DateDeleted = DateTime.Now;
            _ctx.Entry(userType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult VehicleColor()
        {
            var list = _lookupRepo.GetVehicleColors();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_VehicleColorList", list);
            }
            return View("VehicleColor", list);
        }

        // GET: /Admin/CreateVehicleColor
        public ActionResult CreateVehicleColor()
        {
            var list = _lookupRepo.GetVehicleColors();
            var sortOrder = list.Max(x => x.SortOrder);

            var vehicleColor = new VehicleColor
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditVehicleColor", vehicleColor);
        }

        // POST: /Admin/CreateVehicleColor
        [HttpPost]
        public ActionResult CreateVehicleColor(VehicleColor vehicleColor)
        {
            if (ModelState.IsValid)
            {
                _ctx.VehicleColors.Add(vehicleColor);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditVehicleColor/5
        public ActionResult EditVehicleColor(int id)
        {
            var vehicleColor = _lookupRepo.GetVehicleColors().Single(x => x.Id == id);
            return PartialView("_CreateOrEditVehicleColor", vehicleColor);
        }

        // POST: /Admin/EditVehicleColor
        [HttpPost]
        public ActionResult EditVehicleColor(VehicleColor vehicleColor)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(vehicleColor).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleColor);
        }

        // GET: /Admin/DeleteVehicleColor/5
        public void DeleteVehicleColor(int id)
        {
            VehicleColor vehicleColor = _ctx.VehicleColors.Single(x => x.Id == id);
            vehicleColor.DateDeleted = DateTime.Now;
            _ctx.Entry(vehicleColor).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult VehicleMake()
        {
            var list = _lookupRepo.GetVehicleMakes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_VehicleMakeList", list);
            }
            return View("VehicleMake", list);
        }

        // GET: /Admin/CreateVehicleMake
        public ActionResult CreateVehicleMake()
        {
            var list = _lookupRepo.GetVehicleMakes();
            var sortOrder = list.Max(x => x.SortOrder);

            var vehicleMake = new VehicleMake
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditVehicleMake", vehicleMake);
        }

        // POST: /Admin/CreateVehicleMake
        [HttpPost]
        public ActionResult CreateVehicleMake(VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                _ctx.VehicleMakes.Add(vehicleMake);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditVehicleMake/5
        public ActionResult EditVehicleMake(int id)
        {
            var vehicleMake = _lookupRepo.GetVehicleMakes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditVehicleMake", vehicleMake);
        }

        // POST: /Admin/EditVehicleMake
        [HttpPost]
        public ActionResult EditVehicleMake(VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(vehicleMake).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleMake);
        }

        // GET: /Admin/DeleteVehicleMake/5
        public void DeleteVehicleMake(int id)
        {
            VehicleMake vehicleMake = _ctx.VehicleMakes.Single(x => x.Id == id);
            vehicleMake.DateDeleted = DateTime.Now;
            _ctx.Entry(vehicleMake).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        //TODO: VehicleModel Lookup needs further attention because of FK constraint
        public ActionResult VehicleModel()
        {
            var list = _lookupRepo.GetVehicleModels();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_VehicleModelList", list);
            }
            return View("VehicleModel", list);
        }

        // GET: /Admin/CreateVehicleModel
        public ActionResult CreateVehicleModel()
        {
            var list = _lookupRepo.GetVehicleMakes();
            ViewBag.VehicleMakes = list.ToList();

            var sortOrder = list.Max(x => x.SortOrder);

            var vehicleModel = new VehicleModel
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditVehicleModel", vehicleModel);
        }

        // POST: /Admin/CreateVehicleModel
        [HttpPost]
        public ActionResult CreateVehicleModel([Bind(Include = "VehicleMakeId,Id,Name")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                _ctx.VehicleModels.Add(vehicleModel);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditVehicleModel/5
        public ActionResult EditVehicleModel(int id)
        {
            var vehicleModel = _lookupRepo.GetVehicleModels().Where(x => x.Id == id).FirstOrDefault();
            var list = _lookupRepo.GetVehicleMakes();
            ViewBag.VehicleMakes = list.ToList();

            return PartialView("_CreateOrEditVehicleModel", vehicleModel);
        }

        // POST: /Admin/EditVehicleModel
        [HttpPost]
        public ActionResult EditVehicleModel([Bind(Include = "VehicleMakeId,Id,Name")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                //TODO: Fix this!!
                vehicleModel.CreatedUserId = CurrentUser.Id;
                _ctx.Entry(vehicleModel).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicleModel);
        }

        // GET: /Admin/DeleteVehicleModel/5
        public void DeleteVehicleModel(int id)
        {
            VehicleModel vehicleModel = _ctx.VehicleModels.Single(x => x.Id == id);
            vehicleModel.DateDeleted = DateTime.Now;
            _ctx.Entry(vehicleModel).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult WebIncidentType()
        {
            var list = _lookupRepo.GetWebIncidentTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_WebIncidentTypeList", list);
            }
            return View("WebIncidentType", list);
        }

        // GET: /Admin/CreateWebIncidentType
        public ActionResult CreateWebIncidentType()
        {
            var list = _lookupRepo.GetWebIncidentTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var webIncidentType = new WebIncidentType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditWebIncidentType", webIncidentType);
        }

        // POST: /Admin/CreateWebIncidentType
        [HttpPost]
        public ActionResult CreateWebIncidentType(WebIncidentType webIncidentType)
        {
            if (ModelState.IsValid)
            {
                _ctx.WebIncidentTypes.Add(webIncidentType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditWebIncidentType/5
        public ActionResult EditWebIncidentType(int id)
        {
            var webIncidentType = _lookupRepo.GetWebIncidentTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditWebIncidentType", webIncidentType);
        }

        // POST: /Admin/EditWebIncidentType
        [HttpPost]
        public ActionResult EditWebIncidentType(WebIncidentType webIncidentType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(webIncidentType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(webIncidentType);
        }

        // GET: /Admin/DeleteWebIncidentType/5
        public void DeleteWebIncidentType(int id)
        {
            WebIncidentType webIncidentType = _ctx.WebIncidentTypes.Single(x => x.Id == id);
            webIncidentType.DateDeleted = DateTime.Now;
            _ctx.Entry(webIncidentType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }


        public ActionResult WebsiteGroupType()
        {
            var list = _lookupRepo.GetWebsiteGroupTypes();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_WebsiteGroupTypeList", list);
            }
            return View("WebsiteGroupType", list);
        }

        // GET: /Admin/CreateWebsiteGroupType
        public ActionResult CreateWebsiteGroupType()
        {
            var list = _lookupRepo.GetWebsiteGroupTypes();
            var sortOrder = list.Max(x => x.SortOrder);

            var websiteGroupType = new WebsiteGroupType
            {
                SortOrder = sortOrder + 1
            };
            return PartialView("_CreateOrEditWebsiteGroupType", websiteGroupType);
        }

        // POST: /Admin/CreateWebsiteGroupType
        [HttpPost]
        public ActionResult CreateWebsiteGroupType(WebsiteGroupType websiteGroupType)
        {
            if (ModelState.IsValid)
            {
                _ctx.WebsiteGroupTypes.Add(websiteGroupType);
                _ctx.SaveChanges();
            }
            return null;
        }

        // GET: /Admin/EditWebsiteGroupType/5
        public ActionResult EditWebsiteGroupType(int id)
        {
            var websiteGroupType = _lookupRepo.GetWebsiteGroupTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditWebsiteGroupType", websiteGroupType);
        }

        // POST: /Admin/EditWebsiteGroupType
        [HttpPost]
        public ActionResult EditWebsiteGroupType(WebsiteGroupType websiteGroupType)
        {
            if (ModelState.IsValid)
            {
                _ctx.Entry(websiteGroupType).State = EntityState.Modified;
                _ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(websiteGroupType);
        }

        // GET: /Admin/DeleteWebsiteGroupType/5
        public void DeleteWebsiteGroupType(int id)
        {
            WebsiteGroupType websiteGroupType = _ctx.WebsiteGroupTypes.Single(x => x.Id == id);
            websiteGroupType.DateDeleted = DateTime.Now;
            _ctx.Entry(websiteGroupType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }
    }
}
