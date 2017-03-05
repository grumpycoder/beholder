using splc.data;
using splc.data.repository;
using splc.domain.Models;
using System;
using System.Data.Entity;
using System.Linq;
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
        public ActionResult CreateActiveStatus(ActiveStatus model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveActiveStatuses(model);
            return null;
        }

        public ActionResult EditActiveStatus(int id)
        {
            var activeStatus = _lookupRepo.GetActiveStatuses().Single(x => x.Id == id);
            return PartialView("_CreateOrEditActiveStatus", activeStatus);
        }

        [HttpPost]
        public ActionResult EditActiveStatus(ActiveStatus model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveActiveStatuses(model);
            return null;
        }

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

        [HttpPost]
        public ActionResult CreateAddressType(AddressType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveAddressTypes(model);
            return null;
        }

        public ActionResult EditAddressType(int id)
        {
            var addressType = _lookupRepo.GetAddressTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditAddressType", addressType);
        }

        [HttpPost]
        public ActionResult EditAddressType(AddressType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveAddressTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateApprovalStatus(ApprovalStatus model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveApprovalStatuses(model);
            return null;
        }

        public ActionResult EditApprovalStatus(int id)
        {
            var approvalStatus = _lookupRepo.GetApprovalStatuses().Single(x => x.Id == id);
            return PartialView("_CreateOrEditApprovalStatus", approvalStatus);
        }

        [HttpPost]
        public ActionResult EditApprovalStatus(ApprovalStatus model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveApprovalStatuses(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateAudioVideoType(AudioVideoType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveAudioVideoTypes(model);
            return null;
        }

        public ActionResult EditAudioVideoType(int id)
        {
            var audioVideoType = _lookupRepo.GetAudioVideoTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditAudioVideoType", audioVideoType);
        }

        [HttpPost]
        public ActionResult EditAudioVideoType(AudioVideoType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveAudioVideoTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateChapterType(ChapterType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveChapterTypes(model);
            return null;
        }

        public ActionResult EditChapterType(int id)
        {
            var chapterType = _lookupRepo.GetChapterTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditChapterType", chapterType);
        }

        [HttpPost]
        public ActionResult EditChapterType(ChapterType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveChapterTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateConfidentialityType(ConfidentialityType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveConfidentialityTypes(model);
            return null;
        }

        public ActionResult EditConfidentialityType(int id)
        {
            var confidentialityType = _lookupRepo.GetConfidentialityTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditConfidentialityType", confidentialityType);
        }

        [HttpPost]
        public ActionResult EditConfidentialityType(ConfidentialityType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveConfidentialityTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateContactInfoType(ContactInfoType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveContactInfoTypes(model);
            return null;
        }

        public ActionResult EditContactInfoType(int id)
        {
            var contactInfoType = _lookupRepo.GetContactInfoTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditContactInfoType", contactInfoType);
        }

        [HttpPost]
        public ActionResult EditContactInfoType(ContactInfoType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveContactInfoTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateCorrespondenceType(CorrespondenceType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveCorrespondenceTypes(model);
            return null;
        }

        public ActionResult EditCorrespondenceType(int id)
        {
            var correspondenceType = _lookupRepo.GetCorrespondenceTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditCorrespondenceType", correspondenceType);
        }

        [HttpPost]
        public ActionResult EditCorrespondenceType(CorrespondenceType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveCorrespondenceTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateEventDocumentationType(EventDocumentationType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveEventDocumentationTypes(model);
            return null;
        }

        public ActionResult EditEventDocumentationType(int id)
        {
            var eventDocumentationType = _lookupRepo.GetEventDocumentationTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditEventDocumentationType", eventDocumentationType);
        }

        [HttpPost]
        public ActionResult EditEventDocumentationType(EventDocumentationType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveEventDocumentationTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateEyeColor(EyeColor model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveEyeColors(model);
            return null;
        }

        public ActionResult EditEyeColor(int id)
        {
            var eyeColor = _lookupRepo.GetEyeColors().Single(x => x.Id == id);
            return PartialView("_CreateOrEditEyeColor", eyeColor);
        }

        [HttpPost]
        public ActionResult EditEyeColor(EyeColor model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveEyeColors(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateHairColor(HairColor model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveHairColors(model);
            return null;
        }

        public ActionResult EditHairColor(int id)
        {
            var hairColor = _lookupRepo.GetHairColors().Single(x => x.Id == id);
            return PartialView("_CreateOrEditHairColor", hairColor);
        }

        [HttpPost]
        public ActionResult EditHairColor(HairColor model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveHairColors(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateHairPattern(HairPattern model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveHairPatterns(model);
            return null;
        }

        public ActionResult EditHairPattern(int id)
        {
            var hairPattern = _lookupRepo.GetHairPatterns().Single(x => x.Id == id);
            return PartialView("_CreateOrEditHairPattern", hairPattern);
        }

        [HttpPost]
        public ActionResult EditHairPattern(HairPattern model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveHairPatterns(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateImageType(ImageType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveImageTypes(model);
            return null;
        }

        public ActionResult EditImageType(int id)
        {
            var imageType = _lookupRepo.GetImageTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditImageType", imageType);
        }

        [HttpPost]
        public ActionResult EditImageType(ImageType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveImageTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateLibraryCategoryType(LibraryCategoryType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveLibraryCategoryTypes(model);
            return null;
        }

        public ActionResult EditLibraryCategoryType(int id)
        {
            var libraryCategoryType = _lookupRepo.GetLibraryCategoryTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditLibraryCategoryType", libraryCategoryType);
        }

        [HttpPost]
        public ActionResult EditLibraryCategoryType(LibraryCategoryType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveLibraryCategoryTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateLicenseType(LicenseType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveLicenseTypes(model);
            return null;
        }

        public ActionResult EditLicenseType(int id)
        {
            var licenseType = _lookupRepo.GetLicenseTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditLicenseType", licenseType);
        }

        [HttpPost]
        public ActionResult EditLicenseType(LicenseType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveLicenseTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateMaritialStatus(MaritialStatus model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveMaritialStatuses(model);
            return null;
        }

        public ActionResult EditMaritialStatus(int id)
        {
            var maritialStatus = _lookupRepo.GetMaritialStatuses().Single(x => x.Id == id);
            return PartialView("_CreateOrEditMaritialStatus", maritialStatus);
        }

        [HttpPost]
        public ActionResult EditMaritialStatus(MaritialStatus model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveMaritialStatuses(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateMediaType(MediaType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveMediaTypes(model);
            return null;
        }

        public ActionResult EditMediaType(int id)
        {
            var mediaType = _lookupRepo.GetMediaTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditMediaType", mediaType);
        }

        [HttpPost]
        public ActionResult EditMediaType(MediaType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveMediaTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateMimeType(MimeType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveMimeTypes(model);
            return null;
        }

        public ActionResult EditMimeType(int id)
        {
            var mimeType = _lookupRepo.GetMimeTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditMimeType", mimeType);
        }

        [HttpPost]
        public ActionResult EditMimeType(MimeType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveMimeTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateMovementClass(MovementClass model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveMovementClasses(model);
            return null;
        }

        public ActionResult EditMovementClass(int id)
        {
            var movementClass = _lookupRepo.GetMovementClasses().Single(x => x.Id == id);
            return PartialView("_CreateOrEditMovementClass", movementClass);
        }

        [HttpPost]
        public ActionResult EditMovementClass(MovementClass model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveMovementClasses(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateNewsSourceType(NewsSourceType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveNewSourceType(model);
            return null;
        }

        public ActionResult EditNewsSourceType(int id)
        {
            var newsSourceType = _lookupRepo.GetNewSourceType().Single(x => x.Id == id);
            return PartialView("_CreateOrEditNewsSourceType", newsSourceType);
        }

        [HttpPost]
        public ActionResult EditNewsSourceType(NewsSourceType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveNewSourceType(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateOrganizationType(OrganizationType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveOrganizationTypes(model);
            return null;
        }

        public ActionResult EditOrganizationType(int id)
        {
            var organizationType = _lookupRepo.GetOrganizationTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditOrganizationType", organizationType);
        }

        [HttpPost]
        public ActionResult EditOrganizationType(OrganizationType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveOrganizationTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreatePrefix(Prefix model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SavePrefixes(model);
            return null;
        }

        public ActionResult EditPrefix(int id)
        {
            var prefix = _lookupRepo.GetPrefixes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditPrefix", prefix);
        }

        [HttpPost]
        public ActionResult EditPrefix(Prefix model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SavePrefixes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreatePrimaryStatus(PrimaryStatus model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SavePrimaryStatuses(model);
            return null;
        }

        public ActionResult EditPrimaryStatus(int id)
        {
            var primaryStatus = _lookupRepo.GetPrimaryStatuses().Single(x => x.Id == id);
            return PartialView("_CreateOrEditPrimaryStatus", primaryStatus);
        }

        [HttpPost]
        public ActionResult EditPrimaryStatus(PrimaryStatus model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SavePrimaryStatuses(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreatePublishedType(PublishedType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SavePublishedTypes(model);
            return null;
        }

        public ActionResult EditPublishedType(int id)
        {
            var publishedType = _lookupRepo.GetPublishedTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditPublishedType", publishedType);
        }

        [HttpPost]
        public ActionResult EditPublishedType(PublishedType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SavePublishedTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateRace(Race model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveRaces(model);
            return null;
        }

        public ActionResult EditRace(int id)
        {
            var race = _lookupRepo.GetRaces().Single(x => x.Id == id);
            return PartialView("_CreateOrEditRace", race);
        }

        [HttpPost]
        public ActionResult EditRace(Race model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveRaces(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateRelationshipType(RelationshipType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveRelationshipTypes(model);
            return null;
        }

        public ActionResult EditRelationshipType(int id)
        {
            var relationshipType = _lookupRepo.GetRelationshipTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditRelationshipType", relationshipType);
        }

        [HttpPost]
        public ActionResult EditRelationshipType(RelationshipType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveRelationshipTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateRemovalStatus(RemovalStatus model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveRemovalStatus(model);
            return null;
        }

        public ActionResult EditRemovalStatus(int id)
        {
            var removalStatus = _lookupRepo.GetRemovalStatus().Single(x => x.Id == id);
            return PartialView("_CreateOrEditRemovalStatus", removalStatus);
        }

        [HttpPost]
        public ActionResult EditRemovalStatus(RemovalStatus model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveRemovalStatus(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateRenewalPermissionType(RenewalPermmisionType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveRenewalPermissionTypes(model);
            return null;
        }

        public ActionResult EditRenewalPermissionType(int id)
        {
            var renewalPermissionType = _lookupRepo.RenewalPermissionTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditRenewalPermissionType", renewalPermissionType);
        }

        [HttpPost]
        public ActionResult EditRenewalPermissionType(RenewalPermmisionType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveRenewalPermissionTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateSuffix(Suffix model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveSuffixes(model);
            return null;
        }

        public ActionResult EditSuffix(int id)
        {
            var suffix = _lookupRepo.GetSuffixes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditSuffix", suffix);
        }

        [HttpPost]
        public ActionResult EditSuffix(Suffix model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveSuffixes(model);
            return RedirectToAction("Index");
        }

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

        public ActionResult CreateState()
        {
            _lookupRepo.GetStates();
            var state = new State();
            return PartialView("_CreateOrEditState", state);
        }

        [HttpPost]
        public ActionResult CreateState(State model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedUserId = CurrentUser.Id;
                _lookupRepo.SaveStates(model);
            }
            return null;
        }

        public ActionResult EditState(int id)
        {
            var suffix = _lookupRepo.GetStates().Single(x => x.Id == id);
            return PartialView("_CreateOrEditState", suffix);
        }

        [HttpPost]
        public ActionResult EditState(State model)
        {
            if (ModelState.IsValid)
            {
                _lookupRepo.SaveStates(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

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

        [HttpPost]
        public ActionResult CreateTagType(TagType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveTagTypes(model);
            return null;
        }

        public ActionResult EditTagType(int id)
        {
            var tagType = _lookupRepo.GetTagTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditTagType", tagType);
        }

        [HttpPost]
        public ActionResult EditTagType(TagType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveTagTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateUserType(UserType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveUserTypes(model);
            return null;
        }

        public ActionResult EditUserType(int id)
        {
            var userType = _lookupRepo.GetUserTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditUserType", userType);
        }

        [HttpPost]
        public ActionResult EditUserType(UserType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveUserTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateVehicleColor(VehicleColor model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveVehicleColors(model);
            return null;
        }

        public ActionResult EditVehicleColor(int id)
        {
            var vehicleColor = _lookupRepo.GetVehicleColors().Single(x => x.Id == id);
            return PartialView("_CreateOrEditVehicleColor", vehicleColor);
        }

        [HttpPost]
        public ActionResult EditVehicleColor(VehicleColor model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveVehicleColors(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateVehicleMake(VehicleMake model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveVehicleMakes(model);
            return null;
        }

        public ActionResult EditVehicleMake(int id)
        {
            var vehicleMake = _lookupRepo.GetVehicleMakes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditVehicleMake", vehicleMake);
        }

        [HttpPost]
        public ActionResult EditVehicleMake(VehicleMake model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveVehicleMakes(model);
            return RedirectToAction("Index");
        }

        public void DeleteVehicleMake(int id)
        {
            VehicleMake vehicleMake = _ctx.VehicleMakes.Single(x => x.Id == id);
            vehicleMake.DateDeleted = DateTime.Now;
            _ctx.Entry(vehicleMake).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public ActionResult VehicleModel()
        {
            var list = _lookupRepo.GetVehicleModels();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_VehicleModelList", list);
            }
            return View("VehicleModel", list);
        }

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

        [HttpPost]
        public ActionResult CreateVehicleModel([Bind(Include = "VehicleMakeId,Id,Name")] VehicleModel model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveVehicleModels(model);
            return null;
        }

        public ActionResult EditVehicleModel(int id)
        {
            var vehicleModel = _lookupRepo.GetVehicleModels().FirstOrDefault(x => x.Id == id);
            var list = _lookupRepo.GetVehicleMakes();
            ViewBag.VehicleMakes = list.ToList();

            return PartialView("_CreateOrEditVehicleModel", vehicleModel);
        }

        [HttpPost]
        public ActionResult EditVehicleModel([Bind(Include = "VehicleMakeId,Id,Name")] VehicleModel model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveVehicleModels(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateWebIncidentType(WebIncidentType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveWebIncidentTypes(model);
            return null;
        }

        public ActionResult EditWebIncidentType(int id)
        {
            var webIncidentType = _lookupRepo.GetWebIncidentTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditWebIncidentType", webIncidentType);
        }

        [HttpPost]
        public ActionResult EditWebIncidentType(WebIncidentType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveWebIncidentTypes(model);
            return RedirectToAction("Index");
        }

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

        [HttpPost]
        public ActionResult CreateWebsiteGroupType(WebsiteGroupType model)
        {
            if (!ModelState.IsValid) return null;
            _lookupRepo.SaveWebsiteGroupTypes(model);
            return null;
        }

        public ActionResult EditWebsiteGroupType(int id)
        {
            var websiteGroupType = _lookupRepo.GetWebsiteGroupTypes().Single(x => x.Id == id);
            return PartialView("_CreateOrEditWebsiteGroupType", websiteGroupType);
        }

        [HttpPost]
        public ActionResult EditWebsiteGroupType(WebsiteGroupType model)
        {
            if (!ModelState.IsValid) return View(model);
            _lookupRepo.SaveWebsiteGroupTypes(model);
            return RedirectToAction("Index");
        }

        public void DeleteWebsiteGroupType(int id)
        {
            WebsiteGroupType websiteGroupType = _ctx.WebsiteGroupTypes.Single(x => x.Id == id);
            websiteGroupType.DateDeleted = DateTime.Now;
            _ctx.Entry(websiteGroupType).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

    }
}
