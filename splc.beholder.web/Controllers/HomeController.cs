using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult UserNotDefined()
        {
            return View();
        }

        public ActionResult UnauthorizedAccess()
        {
            return View();
        }

        public ActionResult UploadFiles()
        {
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            var filePath = Path.Combine(Server.MapPath("~/Content/upload/"), file.FileName);
            System.IO.File.WriteAllBytes(filePath, ReadData(file.InputStream));

            return Json("All files have been successfully stored.");
        }

        private byte[] ReadData(Stream stream)
        {
            var buffer = new byte[16 * 1024];

            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
    }
}


