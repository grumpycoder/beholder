using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using splc.data;
using splc.data.repository;
using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace splc.beholder.web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IChapterRepository chapterRepository;
        private readonly IEventRepository eventRepository;
        private readonly ACDBContext context;

        public ReportController(IChapterRepository chapterRepository, IEventRepository eventRepository, ACDBContext context)
        {
            this.chapterRepository = chapterRepository;
            this.eventRepository = eventRepository;
            this.context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult YearEnd()
        {
            return View();
        }

        public ActionResult Chapters_Read([DataSourceRequest] DataSourceRequest request)
        {
            //TODO: Shouldn't need to do this
            var serializer = new JavaScriptSerializer();
            var result = new ContentResult();
            serializer.MaxJsonLength = Int32.MaxValue;
            result.Content = serializer.Serialize(chapterRepository.GetChaptersReport().ToDataSourceResult(request));
            result.ContentType = "application/json";
            return result;
            //return Json(chapterRepository.GetChaptersReport().ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        public ActionResult ForTheRecord()
        {
            return View();
            //ViewBag.Title = "For The Record";
            //return View("UnderConstruction");
        }

        public ActionResult Events_Read([DataSourceRequest] DataSourceRequest request)
        {
            //TODO: Shouldn't need to do this
            var serializer = new JavaScriptSerializer();
            var result = new ContentResult();
            serializer.MaxJsonLength = Int32.MaxValue;
            //result.Content = serializer.Serialize(chapterRepository.GetChaptersReport().ToDataSourceResult(request));
            result.Content = serializer.Serialize(eventRepository.GetEventsReport().ToDataSourceResult(request));
            result.ContentType = "application/json";
            return result;
            //return Json(context.EventReports.ToDataSourceResult(request));
        }

        public ActionResult HateMap()
        {
            ViewBag.Title = "Hate Map";
            return View("UnderConstruction");
        }
    }
}