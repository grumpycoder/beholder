using Caseiro.Mvc.PagedList.Extensions;
using MvcContrib.Pagination;
using System.Linq;
using System.Web.Mvc;
using splc.domain.Models;
using splc.data.repository;
using System.Data.Entity.Validation;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class NewsSourcesController : BaseController
    {
        private readonly ILookupRepository _lookupRepo;
        private readonly INewsSourceRepository _newsSourceRepo;

        public NewsSourcesController(
            ILookupRepository lookupRepo,
            INewsSourceRepository newsSourceRepo)
        {
            _lookupRepo = lookupRepo;
            _newsSourceRepo = newsSourceRepo;

            ViewBag.PossibleNewsSourceTypes = _lookupRepo.GetNewSourceType();
        }

        public ViewResult Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim();
            IQueryable<NewsSource> list = _newsSourceRepo.GetNewsSources(p => p.NewsSourceName.Contains(searchTerm));

            return View("Index", list);
        }

        public ActionResult Index(int? page, int? pageSize, string searchTerm = "")
        {
            searchTerm = searchTerm.Trim();

            Session["page"] = page ?? 1;
            Session["searchTerm"] = searchTerm;

            var list = searchTerm == null
                ? _newsSourceRepo.GetNewsSources().OrderBy(m => m.NewsSourceName).ToPagedList(page ?? 1, pageSize ?? 15)
                : _newsSourceRepo.GetNewsSources(x => x.NewsSourceName.Contains(searchTerm)).OrderBy(m => m.NewsSourceName).ToPagedList(page ?? 1, pageSize ?? 15);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_NewsSourceList", list);
            }
            return View("Index", list);

        }

        public JsonResult GetNewsSourceList(string term)
        {
            term = term.Trim();
            var list = _newsSourceRepo.GetNewsSources(p => p.NewsSourceName.Contains(term)).ToArray().Select(
                e => new
                {
                    Id = e.Id,
                    label = e.NewsSourceName
                });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /NewsSources/Details/5
        public ViewResult Details(int id)
        {
            ViewBag.NewsSourceId = id;
            ViewBag.Controller = "NewsSources";
            var newsSource = _newsSourceRepo.GetNewsSource(id);
            return View(newsSource);
        }

        // GET: /NewsSources/Create
        public ActionResult Create()
        {
            var newsSource = new NewsSource();
            return View(newsSource);
        }

        [HttpPost]
        public ActionResult Create(NewsSource newssource)
        {
            if (ModelState.IsValid)
            {
                _newsSourceRepo.InsertOrUpdate(newssource);
                try
                {
                    _newsSourceRepo.Save();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        var temp2 = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            var temp = string.Format("- Property: {0}, Error: {1}", ve.PropertyName, ve.ErrorMessage);
                            //Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            //    ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                return RedirectToAction("Details", new { id = newssource.Id });
            }
            var entity = new NewsSource();
            return View(entity);
        }

        public ActionResult Edit(int id)
        {
            return View(_newsSourceRepo.GetNewsSource(id));
        }

        //
        // POST: /NewsSources/Edit/5
        [HttpPost]
        public ActionResult Edit(NewsSource newssource)
        {
            if (ModelState.IsValid)
            {
                _newsSourceRepo.InsertOrUpdate(newssource);
                _newsSourceRepo.Save();
                return RedirectToAction("Details", new { id = newssource.Id });
            }
            return View();
        }

        ////
        //// GET: /NewsSources/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View(_newsSourceRepo.GetNewsSource(id));
        //}

        ////
        //// POST: /NewsSources/Delete/5
        //[HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    _newsSourceRepo.Delete(id);
        //    _newsSourceRepo.Save();

        //    return RedirectToAction("Index");
        //}

        public void Delete(int id)
        {
            _newsSourceRepo.Delete(id);
            _newsSourceRepo.Save();
        }


    }
}

