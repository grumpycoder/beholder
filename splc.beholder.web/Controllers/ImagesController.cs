using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using splc.domain.Models;
using splc.data.repository;

namespace splc.beholder.web.Controllers
{   
    public class ImagesController : Controller
    {
		private readonly IMimeTypeRepository mimetypeRepository;
		private readonly IConfidentialityTypeRepository confidentialitytypeRepository;
		private readonly IMediaImageRepository mediaimageRepository;
		private readonly IImageRepository imageRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ImagesController() : this(new MimeTypeRepository(), new ConfidentialityTypeRepository(), new MediaImageRepository(), new ImageRepository())
        {
        }

        public ImagesController(IMimeTypeRepository mimetypeRepository, IConfidentialityTypeRepository confidentialitytypeRepository, IMediaImageRepository mediaimageRepository, IImageRepository imageRepository)
        {
			this.mimetypeRepository = mimetypeRepository;
			this.confidentialitytypeRepository = confidentialitytypeRepository;
			this.mediaimageRepository = mediaimageRepository;
			this.imageRepository = imageRepository;
        }

        //
        // GET: /Images/

        public ViewResult Index()
        {
            return View(imageRepository.AllIncluding(image => image.MimeType, image => image.ConfidentialityType, image => image.MediaImage));
        }

        //
        // GET: /Images/Details/5

        public ViewResult Details(int id)
        {
            return View(imageRepository.Find(id));
        }

        //
        // GET: /Images/Create

        public ActionResult Create()
        {
			ViewBag.PossibleMimeTypes = mimetypeRepository.All;
			ViewBag.PossibleConfidentialityTypes = confidentialitytypeRepository.All;
			ViewBag.PossibleMediaImages = mediaimageRepository.All;
            return View();
        } 

        //
        // POST: /Images/Create

        [HttpPost]
        public ActionResult Create(Image image)
        {
            if (ModelState.IsValid) {
                imageRepository.InsertOrUpdate(image);
                imageRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleMimeTypes = mimetypeRepository.All;
				ViewBag.PossibleConfidentialityTypes = confidentialitytypeRepository.All;
				ViewBag.PossibleMediaImages = mediaimageRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Images/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleMimeTypes = mimetypeRepository.All;
			ViewBag.PossibleConfidentialityTypes = confidentialitytypeRepository.All;
			ViewBag.PossibleMediaImages = mediaimageRepository.All;
             return View(imageRepository.Find(id));
        }

        //
        // POST: /Images/Edit/5

        [HttpPost]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid) {
                imageRepository.InsertOrUpdate(image);
                imageRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleMimeTypes = mimetypeRepository.All;
				ViewBag.PossibleConfidentialityTypes = confidentialitytypeRepository.All;
				ViewBag.PossibleMediaImages = mediaimageRepository.All;
				return View();
			}
        }

        //
        // GET: /Images/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(imageRepository.Find(id));
        }

        //
        // POST: /Images/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            imageRepository.Delete(id);
            imageRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                mimetypeRepository.Dispose();
                confidentialitytypeRepository.Dispose();
                mediaimageRepository.Dispose();
                imageRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

