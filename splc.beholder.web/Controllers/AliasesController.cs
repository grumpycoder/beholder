using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using splc.data.repository;
using splc.domain.Models;
using splc.data;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class AliasesController : BaseController
    {
        private readonly ILookupRepository _lookupRepo;
        private readonly IAliasRepository _aliasRepository;
        private readonly IAliasPersonRelRepository _aliaspersonrelRepository;
        private readonly IAliasOrganizationRelRepository _aliasorganizationrelRepository;
        private readonly IAliasChapterRelRepository _aliaschapterrelRepository;
        private readonly IPersonRepository _personRepo;

        public AliasesController(
            IAliasRepository aliasRepository, 
            IAliasChapterRelRepository aliaschapterrelRepository, 
            IAliasPersonRelRepository aliaspersonrelRepository, 
            IAliasOrganizationRelRepository aliasOrganizationRelRepository, 
            ILookupRepository lookupRepo, 
            IPersonRepository personRepo)
        {
            _lookupRepo = lookupRepo;
            _aliasRepository = aliasRepository;
            _aliaschapterrelRepository = aliaschapterrelRepository;
            _aliaspersonrelRepository = aliaspersonrelRepository;
            _aliasorganizationrelRepository = aliasOrganizationRelRepository;
            _personRepo = personRepo;
            ViewBag.PossiblePrimaryStatus = _lookupRepo.GetPrimaryStatuses();
        }

        //
        // GET: /AliasPersonRels/Create
        public ActionResult CreateChapterAlias(int chapterId)
        {
            ViewBag.ChapterId = chapterId;
            var alias = new AliasChapterRel
            {
                ChapterId = chapterId,
                Alias = new Alias()
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterAlias", alias);
            }
            return View(alias);
        }

        //
        // POST: /AliasChapterRels/Create
        [HttpPost]
        public ActionResult CreateChapterAlias(AliasChapterRel aliaschapterrel)
        {
            //if (aliaschapterrel.FirstKnownUseDate > aliaschapterrel.LastKnownUseDate)
            //   this.ModelState.AddModelError("FirstKnownUseDate", "First Known Use Date is greater than Last Known Use Date.");
            try
            {
                if (ModelState.IsValid)
                {
                    _aliaschapterrelRepository.InsertOrUpdate(aliaschapterrel);
                    _aliaschapterrelRepository.Save();
                    return RedirectToAction("Details", "Chapters", new { id = aliaschapterrel.ChapterId });
                }
            }
            catch (Exception ex)
            {
                // Log the exception somewhere to be looked at later
                ModelState.AddModelError("*", "An unexpected error occurred.");
            }

            return View(aliaschapterrel);
        }

        //
        // GET: /AliasChapterRels/Edit/5
        public ActionResult EditChapterAlias(int id)
        {
            var alias = _aliaschapterrelRepository.Find(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditChapterAlias", alias);
            }
            return View(alias);
        }

        //
        // POST: /Aliases/EditChapterAlias/5
        [HttpPost]
        public ActionResult EditChapterAlias(AliasChapterRel aliaschapterrel)
        {
            if (ModelState.IsValid)
            {
                _aliaschapterrelRepository.InsertOrUpdate(aliaschapterrel);
                _aliaschapterrelRepository.Save();
                return RedirectToAction("Details", "Chapters", new { id = aliaschapterrel.ChapterId });
            }
            return View(aliaschapterrel);
        }

        // GET: /Aliases/GetChapterAliases/1
        public ActionResult GetChapterAliases(int id)
        {
            ViewBag.ChapterId = id;
            var aliases = _aliaschapterrelRepository.GetAliases(p => p.ChapterId.Equals(id)).ToArray();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ChapterAliasList", aliases);
            }
            return View("Index");
        }

        [HttpPost]
        public void DeleteChapterAlias(int id)
        {
            _aliaschapterrelRepository.Delete(id);
            _aliaschapterrelRepository.Save();
        }

        //
        // GET: /AliasPersonRels/Create
        public ActionResult CreatePersonAlias(int personId)
        {
//            var person = _personRepo.Get(personId, currentUser);
//            ViewBag.CommonPersonId = personId;

            var alias = new AliasPersonRel
            {
                PersonId = personId,
                Alias = new Alias()
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditPersonAlias", alias);
            }
            return View(alias);
        }

        //
        // POST: /AliasPersonRels/Create
        [HttpPost]
        public ActionResult CreatePersonAlias(AliasPersonRel aliaspersonrel)
        {
            try
            {
                ViewBag.CommonPersonId = aliaspersonrel.PersonId;
                if (ModelState.IsValid)
                {
                    _aliaspersonrelRepository.InsertOrUpdate(aliaspersonrel);
                    _aliaspersonrelRepository.Save();
                    //TODO:  Make sure this is the right PersonId to return
//                    return RedirectToAction("Details", "Persons", new { id = aliaspersonrel.PersonId });
                }
            }
            catch (Exception ex)
            {
                var s = ex.Message;
                // Log the exception somewhere to be looked at later
                ModelState.AddModelError("*", "An unexpected error occurred.");
            }


            return View(aliaspersonrel);
        }

        //
        // GET: /AliasPersonRels/Edit/5
        public ActionResult EditPersonAlias(int id)
        {
            var alias = _aliaspersonrelRepository.Find(id);
            if (Request.IsAjaxRequest())
            {
                //return PartialView("EditPersonAlias", alias);
                return PartialView("_CreateOrEditPersonAlias", alias);
            }
            return View(alias);
        }

        //
        // POST: /Aliases/EditPersonAlias/5
        [HttpPost]
        public ActionResult EditPersonAlias(AliasPersonRel aliaspersonrel)
        {
            ViewBag.CommonPersonId = aliaspersonrel.PersonId;
            if (ModelState.IsValid)
            {
                _aliaspersonrelRepository.InsertOrUpdate(aliaspersonrel);
                _aliaspersonrelRepository.Save();
                //TODO:  Make sure this is the right PersonId to return
//                return RedirectToAction("Details", "Persons", new { id = aliaspersonrel.PersonId });
            }
            return View(aliaspersonrel);
        }

        // GET: /Aliases/GetPersonAliases/1
        public ActionResult GetPersonAliases(int id)
        {
            var aliases = _aliaspersonrelRepository.GetAliases(p => p.CommonPerson.Id.Equals(id)).ToArray();
            //TODO: Unable to get beholderperson from commonperson id passed in to set viewbag variable needed in partial view. 
            var beholderPerson = _personRepo.Get(currentUser, x => x.CommonPersonId == id);

            ViewBag.BeholderPersonId = beholderPerson.First().Id; 
            ViewBag.CommonPersonId = id;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonAliasList", aliases);
            }
            return View("Index");
        }

        [HttpPost]
        public void DeletePersonAlias(int id)
        {
            _aliaspersonrelRepository.Delete(id);
            _aliaspersonrelRepository.Save();

            //var aliaspersonrel = _aliaspersonrelRepository.Find(id);
            //var alias = _aliasRepository.Find(aliaspersonrel.AliasId);
            //_aliasRepository.Save();
            //_aliaspersonrelRepository.Save();
        }

        //
        // GET: /Alias/CreateOrganizationAlias
        public ActionResult CreateOrganizationAlias(int organizationId)
        {
            var alias = new AliasOrganizationRel()
            {
                OrganizationId = organizationId,
                Alias = new Alias()
            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationAlias", alias);
            }
            return View();
        }

        //
        // POST: /Alias/CreateOrganizationAlias
        [HttpPost]
        public ActionResult CreateOrganizationAlias(AliasOrganizationRel aliasorganizationrel)
        {
            if (ModelState.IsValid)
            {
                _aliasorganizationrelRepository.InsertOrUpdate(aliasorganizationrel);
                _aliasorganizationrelRepository.Save();

                return RedirectToAction("Details", "Organizations", new { id = aliasorganizationrel.OrganizationId });
            }

            return View();
        }

        //
        // GET: /AliasPersonRels/Edit/5
        public ActionResult EditOrganizationAlias(int id)
        {
            var aliasorganizationrel = _aliasorganizationrelRepository.Find(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditOrganizationAlias", aliasorganizationrel);
            }
            return View(aliasorganizationrel);
            //            return View(organizationcommentRepository.Find(id));
        }

        //
        // POST: /AliasPersonRels/Edit/5
        [HttpPost]
        public ActionResult EditOrganizationAlias(AliasOrganizationRel aliasorganizationrel)
        {
            if (ModelState.IsValid)
            {
                _aliasorganizationrelRepository.InsertOrUpdate(aliasorganizationrel);
                _aliasorganizationrelRepository.Save();
                return RedirectToAction("Details", "Organizations", new { id = aliasorganizationrel.OrganizationId });
            }
            return View(aliasorganizationrel);
        }

        // GET Comments/GetOrganizationAliases/1
        public ActionResult GetOrganizationAliases(int id)
        {
            ViewBag.OrganizationId = id;
            var aliases = _aliasorganizationrelRepository.GetAliases(p => p.OrganizationId.Equals(id)).ToArray();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_OrganizationAliasList", aliases);
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult DeleteOrganizationAlias(int id)
        {
            _aliasorganizationrelRepository.Delete(id);
            _aliasorganizationrelRepository.Save();

            return View();
        }

        //
        // GET: /Aliases/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // GET: /Aliases/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    Alias alias = context.Aliases.Single(x => x.Id == id);
        //    return View(alias);
        //}

        //
        // POST: /Aliases/Delete/5

        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Alias alias = context.Aliases.Single(x => x.Id == id);
        //    context.Aliases.Remove(alias);
        //    context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _aliaspersonrelRepository.Dispose();
                _aliaschapterrelRepository.Dispose();
                _aliasorganizationrelRepository.Dispose();
                _aliasRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
