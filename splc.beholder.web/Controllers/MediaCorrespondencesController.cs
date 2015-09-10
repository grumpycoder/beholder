using System;
using System.ComponentModel.DataAnnotations;
using Caseiro.Mvc.PagedList;
using Caseiro.Mvc.PagedList.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using splc.beholder.web.Utility;
using splc.data;
using splc.domain.Models;
using splc.data.repository;
using System.IO;
using System.Web;
using System.Data.Entity.Validation;
using splc.data.Utility;
using System.Text;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class MediaCorrespondencesController : BaseController
    {
        private readonly ILookupRepository _lookupRepo;
        private readonly IMediaCorrespondenceRepository _mediaCorrespondenceRepo;
        private readonly IUserRepository _userRepository; 
        private readonly ACDBContext db;

        public MediaCorrespondencesController(
            ILookupRepository lookupRepo,
            IMediaCorrespondenceRepository mediaCorrespondenceRepo,
            IUserRepository userRepository, 
            ACDBContext _db)
        {
            _lookupRepo = lookupRepo;
            _userRepository = userRepository; 
            _mediaCorrespondenceRepo = mediaCorrespondenceRepo;
            db = _db;

            ViewBag.PossibleApprovalStatus = _lookupRepo.GetApprovalStatuses();
            ViewBag.PossibleMediaTypes = _lookupRepo.GetMediaTypes();
            ViewBag.PossibleMimeTypes = _lookupRepo.GetMimeTypes();
            ViewBag.PossibleCorrespondenceTypes = _lookupRepo.GetCorrespondenceTypes();
            ViewBag.PossibleNewsSources = _lookupRepo.GetNewsSources();
            ViewBag.PossibleMovementClasses = _lookupRepo.GetMovementClasses();
            ViewBag.PossibleRenewalPermissionTypes = _lookupRepo.RenewalPermissionTypes();
            ViewBag.PossibleCorrespondenceTypes = _lookupRepo.GetCorrespondenceTypes();
            ViewBag.PossiblePrimaryStatuses = _lookupRepo.GetPrimaryStatuses();

            ViewBag.PossibleStates = _lookupRepo.GetStates();
            ViewBag.PossibleRemovalStatuses = _lookupRepo.GetRemovalStatus();
            ViewBag.Users = _userRepository.GetUsers(); 
        }

        #region Base

        public ActionResult GetContextList(int correspondenceId)
        {
            var list = db.MediaCorrespondenceContexts.Where(x => x.MediaCorrespondenceId == correspondenceId).ToList();
            return View("_DocumentList", list);
        }

        [HttpPost]
        public JsonResult SaveTextAsContent(int correspondenceId, string filename, string content)
        {
            var context = new MediaCorrespondenceContext()
            {
                MediaCorrespondenceId = correspondenceId,
                FileName = filename,
                ContextText = Encoding.ASCII.GetBytes(content),
                DocumentExtension = "txt",
                MimeTypeId = 22
            };

            try
            {
                db.MediaCorrespondenceContexts.Add(context);
                db.SaveChanges();
            }
            catch (ValidationException e)
            {
                var message = e.Message;
            }
            return Json(context, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaveAttachments(IEnumerable<HttpPostedFileBase> attachments, int correspondenceId)
        {
        
            if (attachments != null)
            {
                foreach (var file in attachments)
                {
                    try
                    {
                        var fileName = Path.GetFileName(file.FileName.ToValidFileName());

                        var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                        file.SaveAs(physicalPath);

                        var mimetypeid =
                            _lookupRepo.GetMimeTypes()
                                .SingleOrDefault(p => p.Name.Equals(file.ContentType.ToLower()))
                                .Id;
                        var context = new MediaCorrespondenceContext()
                        {
                            MediaCorrespondenceId = correspondenceId,
                            FileName = fileName,
                            ContextText = GetFile(physicalPath),
                            DocumentExtension = Path.GetExtension(physicalPath),
                            MimeTypeId = mimetypeid
                        };

                        try
                        {
                            db.MediaCorrespondenceContexts.Add(context);
                            db.SaveChanges();
                            System.IO.File.Delete(physicalPath);
                        }
                        catch (ValidationException e)
                        {
                            return Content(e.Message);
                        }

                    }
                    catch (ArgumentException aeException)
                    {
                        return Content(string.Format(@"File contains invalid characters \/:*?""<>|"));
                    }
                    catch (Exception e)
                    {

                        return Content(e.Message);
                    }


                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult DeleteAttachment(int id)
        {
            var context = db.MediaCorrespondenceContexts.Find(id);
            db.MediaCorrespondenceContexts.Remove(context);
            db.SaveChanges();
            return null;
        }

        public ViewResult Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim();
            IQueryable<MediaCorrespondence> list = _mediaCorrespondenceRepo.GetMediaCorrespondences(currentUser, p => p.FromName.Contains(searchTerm) || p.CorrespondenceName.Contains(searchTerm));

            return View("Index", list);
        }

        //
        // GET: /Organizations/
        //public ViewResult Index()
        public ActionResult Index(int? userid, string correspondencename = "", string fromname = "", DateTime? datefrom = null, DateTime? dateto = null, 
            List<int> correspondencetypeid = null, string correspondencetypeid_string = "", string docsearch = "", int? page = 1, int? pageSize = 15)
        {
            if (!String.IsNullOrWhiteSpace(correspondencetypeid_string))
            {
                correspondencetypeid = ((List<int>)correspondencetypeid_string.Split(',').Select(int.Parse).ToList());
            }
            //List<int?> webincidenttypeid = null;
            //= webincidenttypeid_string.Split(',').Select(i => int.Parse(i)).ToList());

            correspondencename = correspondencename.Trim();
            fromname = fromname.Trim();
            docsearch = docsearch.Trim();

            Session["correspondencetypeid"] = correspondencetypeid;
            Session["correspondencename"] = correspondencename;
            Session["fromname"] = fromname;
            Session["datefrom"] = datefrom;
            Session["dateto"] = dateto;
            Session["userid"] = userid;
            Session["page"] = page;
            Session["pageSize"] = pageSize;
            Session["docsearch"] = docsearch;


            //TODO:  need to handle multiple search expressions.
            //this is to prepare the search term for full text index search.  s variable is used for full text index search.
            string s = null;
            if (!String.IsNullOrEmpty(docsearch))
            {
                s = String.Format("\"{0}\"", FtsInterceptor.Fts(docsearch.Replace("\"", "")));
            }

            PagedList<MediaCorrespondence> list = null;
            if (s == null)
            {
                if (correspondencetypeid == null)
                {
                    list = _mediaCorrespondenceRepo.GetMediaCorrespondences(currentUser,
                        x => (fromname.Length == 0 || x.FromName.Contains(fromname))
                             && (x.CreatedUserId == (userid ?? x.CreatedUserId)) 
                             && (correspondencename.Length == 0 || x.CorrespondenceName.Contains(correspondencename))
                             && (x.DateReceived ?? DateTime.MinValue) >= (datefrom.HasValue ? datefrom : (x.DateReceived ?? DateTime.MinValue))
                             && (x.DateReceived ?? DateTime.MinValue) <= (dateto.HasValue ? dateto : (x.DateReceived ?? DateTime.MinValue))
                             )
                        .OrderBy(m => m.CorrespondenceName).ToPagedList(page ?? 1, pageSize ?? 15);
                }
                else
                {
                    list = _mediaCorrespondenceRepo.GetMediaCorrespondences(currentUser,
                        x => (fromname.Length == 0 || x.FromName.Contains(fromname))
                             && (x.CreatedUserId == (userid ?? x.CreatedUserId)) 
                             && (correspondencename.Length == 0 || x.CorrespondenceName.Contains(correspondencename))
                             && (x.DateReceived ?? DateTime.MinValue) >= (datefrom.HasValue ? datefrom : (x.DateReceived ?? DateTime.MinValue))
                             && (x.DateReceived ?? DateTime.MinValue) <= (dateto.HasValue ? dateto : (x.DateReceived ?? DateTime.MinValue))
                             && (correspondencetypeid.Contains((int)x.CorrespondenceTypeId))
                             )
                        .OrderBy(m => m.CorrespondenceName).ToPagedList(page ?? 1, pageSize ?? 15);
                }
            }
            else
            {
                if (correspondencetypeid == null)
                {
                    list = _mediaCorrespondenceRepo.GetMediaCorrespondences(currentUser,
                        x => (fromname.Length == 0 || x.FromName.Contains(fromname))
                             && (x.CreatedUserId == (userid ?? x.CreatedUserId)) 
                             && (correspondencename.Length == 0 || x.CorrespondenceName.Contains(correspondencename))
                             && (x.DateReceived ?? DateTime.MinValue) >= (datefrom.HasValue ? datefrom : (x.DateReceived ?? DateTime.MinValue))
                             && (x.DateReceived ?? DateTime.MinValue) <= (dateto.HasValue ? dateto : (x.DateReceived ?? DateTime.MinValue))
                             && (x.MediaCorrespondenceContext_Indexes.Any(m => m.ContextText.Contains(s)))
                        //&& x.MediaCorrespondenceContext_Index.ContextText.Contains(s)
                             )
                        .OrderBy(m => m.CorrespondenceName).ToPagedList(page ?? 1, pageSize ?? 15);
                }
                else
                {
                    list = _mediaCorrespondenceRepo.GetMediaCorrespondences(currentUser,
                        x => (fromname.Length == 0 || x.FromName.Contains(fromname))
                             && (x.CreatedUserId == (userid ?? x.CreatedUserId)) 
                             && (correspondencename.Length == 0 || x.CorrespondenceName.Contains(correspondencename))
                             && (x.DateReceived ?? DateTime.MinValue) >= (datefrom.HasValue ? datefrom : (x.DateReceived ?? DateTime.MinValue))
                             && (x.DateReceived ?? DateTime.MinValue) <= (dateto.HasValue ? dateto : (x.DateReceived ?? DateTime.MinValue))
                             && (correspondencetypeid.Contains((int)x.CorrespondenceTypeId))
                             && (x.MediaCorrespondenceContext_Indexes.Any(m => m.ContextText.Contains(s)))
                        //&& x.MediaCorrespondenceContext_Index.ContextText.Contains(s)
                             )
                        .OrderBy(m => m.CorrespondenceName).ToPagedList(page ?? 1, pageSize ?? 15);
                }
            }
            //slj had to move filter of deleted records here because having the filter build here for the full context search and in the repository for the date deleted filter was messing up the full context search and causing an error.
            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaCorrespondenceList", list); //.Where(x => x.DateDeleted == null).AsPagination(page ?? 1, pageSize ?? 15));
            }

            return View("Index", list); //.Where(x => x.DateDeleted == null).AsPagination(page ?? 1, pageSize ?? 15));

        }

        // GET: Search for BeholderPersons 
        public JsonResult GetMediaCorrespondenceList(string term)
        {
            term = term.Trim();
            var list = _mediaCorrespondenceRepo.GetMediaCorrespondences(currentUser, p => p.CorrespondenceName.Contains(term)).ToArray().Select(
                e => new
                {
                    Id = e.Id,
                    label = e.CorrespondenceName
                });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MediaCorrespondences/Details/5
        public ViewResult Details(int id)
        {
            ViewBag.OrganizationId = -1;
            ViewBag.ChapterId = -1;
            ViewBag.ContactId = -1;
            ViewBag.BeholderPersonId = -1;
            ViewBag.PersonId = -1;
            ViewBag.EventId = -1;
            ViewBag.SubscriptionId = -1;
            ViewBag.VehicleId = -1;
            ViewBag.MediaImageId = -1;
            ViewBag.MediaAudioVideoId = -1;
            ViewBag.MediaWebsiteEGroupId = -1;
            ViewBag.SubscriptionId = -1;
            ViewBag.MediaCorrespondenceId = id;
            ViewBag.Controller = "MediaCorrespondences";

            var correspondence = _mediaCorrespondenceRepo.GetMediaCorrespondence(currentUser, id);
            if (correspondence != null)
            {
                return View(correspondence);
            }
            return View("Correspondence404");

        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            var filePath = Path.Combine(Server.MapPath("~/Content/upload/"), file.FileName);
            System.IO.File.WriteAllBytes(Uri.UnescapeDataString(filePath), ReadData(file.InputStream));

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

        private bool FileExists(string fileName)
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/Content/upload/")))
            {
                return false;
            }

            return System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/upload/"), fileName ?? ""));
        }

        private byte[] GetFile(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Open);
            byte[] tmp;

            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }    
                stream.Close();
                tmp = ms.ToArray();
            }
            return tmp;
        }

        public ActionResult Download(int id)
        {
            var document = _mediaCorrespondenceRepo.GetMediaCorrespondenceContext(id);
            var cd = new System.Net.Mime.ContentDisposition
            {
                // for example foo.bak
                FileName = document.FileName,

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(document.ContextText, document.MimeType.Name);
        }

        public byte[] CreateFile(string fileText)
        {
            return Encoding.ASCII.GetBytes(fileText);

        }

        // GET: /MediaCorrespondences/Create
        public ActionResult Create()
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            var image = new MediaCorrespondenceContext { };
            var mediaCorrespondence = new MediaCorrespondence
            {
                //MediaCorrespondenceContext = image
            };
            return View(mediaCorrespondence);
        }


        [HttpPost]
        public ActionResult Create(MediaCorrespondence mediacorrespondence, FormCollection form, IEnumerable<HttpPostedFileBase> attachments)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _mediaCorrespondenceRepo.InsertOrUpdate(mediacorrespondence);
                    _mediaCorrespondenceRepo.Save();

                    var content = form["fileContent"];
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        var filename = form["filename"];
                        var context = new MediaCorrespondenceContext()
                        {
                            MediaCorrespondenceId = mediacorrespondence.Id,
                            FileName = filename,
                            ContextText = Encoding.ASCII.GetBytes(content),
                            DocumentExtension = "txt",
                            MimeTypeId = 22
                        };

                        try
                        {
                            db.MediaCorrespondenceContexts.Add(context);
                            db.SaveChanges();
                        }
                        catch (ValidationException e)
                        {
                            var message = e.Message;
                        }
                    }

                    if (attachments != null)
                    {
                        foreach (var file in attachments)
                        {
                            var filename = Path.GetFileName(file.FileName);
                            if (filename != null)
                            {
                                var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), filename);
                                file.SaveAs(physicalPath);

                                var mimetypeid =
                                    _lookupRepo.GetMimeTypes()
                                        .SingleOrDefault(p => p.Name.Equals(file.ContentType.ToLower()))
                                        .Id;
                                var context = new MediaCorrespondenceContext()
                                {
                                    MediaCorrespondenceId = mediacorrespondence.Id,
                                    FileName = filename,
                                    ContextText = GetFile(physicalPath),
                                    DocumentExtension = Path.GetExtension(physicalPath),
                                    MimeTypeId = mimetypeid
                                };

                                try
                                {
                                    db.MediaCorrespondenceContexts.Add(context);
                                    db.SaveChanges();
                                    System.IO.File.Delete(physicalPath);
                                }
                                catch (ValidationException e)
                                {
                                    var message = e.Message;
                                }
                            }
                        }
                    }

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
                return RedirectToAction("Details", new { id = mediacorrespondence.Id });
            }
            //var image = new MediaCorrespondenceContext { };
            //var entity = new MediaCorrespondence
            //    {
            //        MediaTypeId = _lookupRepo.GetMediaTypes().SingleOrDefault(p => p.Name.Equals("Correspondence")).Id,
            //        MediaCorrespondenceContext = image
            //    };
            //return View(entity);
            return View(mediacorrespondence);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            var correspondence = _mediaCorrespondenceRepo.GetMediaCorrespondence(currentUser, id);

            if (correspondence != null)
            {
                //if (correspondence.MediaCorrespondenceContextId == null)
                //{
                //    correspondence.MediaCorrespondenceContext = new MediaCorrespondenceContext();
                //};
                return View(correspondence);
            }

            return View("Correspondence404");
        }

        //
        // POST: /MediaCorrespondences/Edit/5
        [HttpPost]
        public ActionResult Edit(MediaCorrespondence mediacorrespondence, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                _mediaCorrespondenceRepo.InsertOrUpdate(mediacorrespondence);

                try
                {
                    _mediaCorrespondenceRepo.Save();
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

                return RedirectToAction("Details", new { id = mediacorrespondence.Id });
            }
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            return View(mediacorrespondence);

        }

        public ActionResult RemoveCorrespondence(int id)
        {
            var c = _mediaCorrespondenceRepo.GetMediaCorrespondence(currentUser, id);
            return View(c);
        }

        [HttpPost]
        public ActionResult RemoveCorrespondence(int id, string removalreason)
        {
            var c = _mediaCorrespondenceRepo.GetMediaCorrespondence(currentUser, id);
            c.RemovalReason = removalreason;

            c.RemovalStatusId = 1;
            _mediaCorrespondenceRepo.InsertOrUpdate(c);
            _mediaCorrespondenceRepo.Save();

            return RedirectToAction("Index");

        }

        //
        // GET: /MediaCorrespondences/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_mediaCorrespondenceRepo.GetMediaCorrespondence(id));
        }

        //
        // POST: /MediaCorrespondences/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _mediaCorrespondenceRepo.Delete(id);
            _mediaCorrespondenceRepo.Save();

            return RedirectToAction("Index");
        }

        #endregion


        #region Vehicles

        public ActionResult GetMediaCorrespondenceVehicles(int mediaCorrespondenceId, int vehicleId)
        {
            IQueryable<MediaCorrespondenceVehicleRel> mediaCorrespondenceVehicles;
            if (vehicleId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.Controller = "MediaCorrespondences";
                mediaCorrespondenceVehicles = _mediaCorrespondenceRepo.GetMediaCorrespondenceVehicles(p => p.MediaCorrespondence.Id.Equals(mediaCorrespondenceId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.Controller = "Vehicles";
                mediaCorrespondenceVehicles = _mediaCorrespondenceRepo.GetMediaCorrespondenceVehicles(p => p.Vehicle.Id.Equals(vehicleId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaCorrespondenceVehicleList", mediaCorrespondenceVehicles);
            }
            return View(mediaCorrespondenceVehicles);
        }

        //
        // GET: /Vehicles/CreatePersonVehicle
        public ActionResult CreateMediaCorrespondenceVehicle(int vehicleId, int mediaCorrespondenceId)
        {
            //var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaCorrespondenceVehicleRel = new MediaCorrespondenceVehicleRel
            {
                VehicleId = vehicleId,
                MediaCorrespondenceId = mediaCorrespondenceId,
                //ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (vehicleId == -1)
            {
                mediaCorrespondenceVehicleRel.Vehicle = new Vehicle();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaCorrespondences";
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
            }
            else
            {
                mediaCorrespondenceVehicleRel.MediaCorrespondence = new MediaCorrespondence();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Vehicles";
                ViewBag.VehicleId = vehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceVehicle", mediaCorrespondenceVehicleRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaCorrespondenceVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaCorrespondenceId,VehicleId")]MediaCorrespondenceVehicleRel mediaCorrespondencevehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (mediaCorrespondencevehiclerel.MediaCorrespondence == null)
                {
                    mediaCorrespondencevehiclerel.Vehicle = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceVehicle(mediaCorrespondencevehiclerel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondencevehiclerel.MediaCorrespondenceId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                    mediaCorrespondencevehiclerel.MediaCorrespondence = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceVehicle(mediaCorrespondencevehiclerel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "Vehicles", new { id = mediaCorrespondencevehiclerel.VehicleId });
                }
            }
            return View();
        }

        //
        // GET: /Vehicles/EditPersonVehicle/5
        public ActionResult EditMediaCorrespondenceVehicle(int id, string source)
        {
            var mediaCorrespondenceVehicleRel = _mediaCorrespondenceRepo.GetMediaCorrespondenceVehicle(id);
            if (source == "MediaCorrespondences")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaCorrespondenceVehicleRel.MediaCorrespondenceId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.VehicleId = mediaCorrespondenceVehicleRel.VehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceVehicle", mediaCorrespondenceVehicleRel);
            }
            return View(mediaCorrespondenceVehicleRel);

        }

        //
        // POST: /Vehicles/EditPersonVehicle/5
        [HttpPost]
        public ActionResult EditMediaCorrespondenceVehicle(MediaCorrespondenceVehicleRel mediaCorrespondencevehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (mediaCorrespondencevehiclerel.MediaCorrespondence == null)
                {
                    //reset the mediaCorrespondence object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                    mediaCorrespondencevehiclerel.Vehicle = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceVehicle(mediaCorrespondencevehiclerel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondencevehiclerel.MediaCorrespondenceId });
                }
                //reset the organization object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                mediaCorrespondencevehiclerel.MediaCorrespondence = null;
                _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceVehicle(mediaCorrespondencevehiclerel);
                _mediaCorrespondenceRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = mediaCorrespondencevehiclerel.VehicleId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaCorrespondenceVehicle(int id)
        {
            _mediaCorrespondenceRepo.DeleteMediaCorrespondenceVehicle(id);
            _mediaCorrespondenceRepo.Save();
        }

        #endregion


        #region Media Correspondence

        public ActionResult GetMediaCorrespondenceMediaCorrespondences(int mediaCorrespondenceId)
        {
            MediaCorrespondence mediaCorrespondences;
            //if (subscriptionId == -1)
            //{
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
            ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
            ViewBag.MediaCorrespondence2Id = mediaCorrespondenceId;
            ViewBag.Controller = "MediaCorrespondences";
            try
            {
                mediaCorrespondences = _mediaCorrespondenceRepo.GetMediaCorrespondence(currentUser, mediaCorrespondenceId);
                //                subscriptions = _eventRepository.Get(p => p.Id.Equals(eventId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaCorrespondenceMediaCorrespondenceList", mediaCorrespondences);
            }
            return View(mediaCorrespondences);
        }

        public ActionResult CreateMediaCorrespondenceMediaCorrespondence(int mediacorrespondenceId)
        {
            //var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaCorrespondenceMediaCorrespondenceRel = new MediaCorrespondenceMediaCorrespondenceRel
            {
                MediaCorrespondenceId = mediacorrespondenceId,
                //ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
                MediaCorrespondence2 = new MediaCorrespondence(),
            };

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
            ViewBag.MediaCorrespondenceId = mediacorrespondenceId;
            ViewBag.MediaCorrespondence2Id = -1;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceMediaCorrespondence", mediaCorrespondenceMediaCorrespondenceRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateMediaCorrespondenceMediaCorrespondence([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaCorrespondence2Id,MediaCorrespondenceId")] MediaCorrespondenceMediaCorrespondenceRel mediaCorrespondenceMediaCorrespondenceRel)
        {
            if (ModelState.IsValid)
            {
                mediaCorrespondenceMediaCorrespondenceRel.MediaCorrespondence2 = null;
                _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaCorrespondence(mediaCorrespondenceMediaCorrespondenceRel);
                _mediaCorrespondenceRepo.Save();
                return null;
                //return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondenceMediaCorrespondenceRel.MediaCorrespondenceId });
            }
            return View(mediaCorrespondenceMediaCorrespondenceRel);

        }

        public ActionResult EditMediaCorrespondenceMediaCorrespondence(int id)
        {
            var mediaCorrespondenceMediaCorrespondenceRel = _mediaCorrespondenceRepo.GetMediaCorrespondenceMediaCorrespondence(id);
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceMediaCorrespondence", mediaCorrespondenceMediaCorrespondenceRel);
            }
            return View(mediaCorrespondenceMediaCorrespondenceRel);

        }

        //
        // POST: /EventPersonRels/EditEventPerson
        [HttpPost]
        public ActionResult EditMediaCorrespondenceMediaCorrespondence([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaCorrespondence2Id,MediaCorrespondenceId")] MediaCorrespondenceMediaCorrespondenceRel mediaCorrespondenceMediaCorrespondenceRel)
        {
            if (ModelState.IsValid)
            {
                _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaCorrespondence(mediaCorrespondenceMediaCorrespondenceRel);
                _mediaCorrespondenceRepo.Save();
                return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondenceMediaCorrespondenceRel.MediaCorrespondenceId });
            }

            return View();
        }

        [HttpPost]
        public void DeleteMediaCorrespondenceMediaCorrespondence(int id)
        {
            _mediaCorrespondenceRepo.DeleteMediaCorrespondenceMediaCorrespondence(id);
            _mediaCorrespondenceRepo.Save();
        }

        #endregion


        #region Events

        public ActionResult GetMediaCorrespondenceEvents(int mediaCorrespondenceId, int eventId)
        {
            IQueryable<MediaCorrespondenceEventRel> mediaCorrespondenceEvents;
            if (eventId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.EventId = eventId;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.Controller = "MediaCorrespondences";
                mediaCorrespondenceEvents = _mediaCorrespondenceRepo.GetMediaCorrespondenceEvents(p => p.MediaCorrespondence.Id.Equals(mediaCorrespondenceId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.EventId = eventId;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.Controller = "Events";
                mediaCorrespondenceEvents = _mediaCorrespondenceRepo.GetMediaCorrespondenceEvents(p => p.Event.Id.Equals(eventId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaCorrespondenceEventList", mediaCorrespondenceEvents);
            }
            return View(mediaCorrespondenceEvents);
        }

        //
        // GET: /Events/CreatePersonEvent
        public ActionResult CreateMediaCorrespondenceEvent(int eventId, int mediaCorrespondenceId)
        {
            //var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaCorrespondenceEventRel = new MediaCorrespondenceEventRel
            {
                EventId = eventId,
                MediaCorrespondenceId = mediaCorrespondenceId,
                //ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (eventId == -1)
            {
                mediaCorrespondenceEventRel.Event = new Event();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaCorrespondences";
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
            }
            else
            {
                mediaCorrespondenceEventRel.MediaCorrespondence = new MediaCorrespondence();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Events";
                ViewBag.EventId = eventId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceEvent", mediaCorrespondenceEventRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaCorrespondenceEvent([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,MediaCorrespondenceId")] MediaCorrespondenceEventRel mediaCorrespondenceeventrel)
        {
            if (ModelState.IsValid)
            {
                //if (mediaCorrespondenceeventrel.MediaCorrespondence == null)
                //{
                //    mediaCorrespondenceeventrel.Event = null;
                _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceEvent(mediaCorrespondenceeventrel);
                _mediaCorrespondenceRepo.Save();
                return null;
                //    return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondenceeventrel.MediaCorrespondenceId });
                //}
                //else
                //{
                //    //reset the organization object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                //    mediaCorrespondenceeventrel.MediaCorrespondence = null;
                //    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceEvent(mediaCorrespondenceeventrel);
                //    _mediaCorrespondenceRepo.Save();
                //    return RedirectToAction("Details", "Events", new { id = mediaCorrespondenceeventrel.EventId });
                //}
            }
            return View(mediaCorrespondenceeventrel);
        }

        //
        // GET: /Events/EditPersonEvent/5
        public ActionResult EditMediaCorrespondenceEvent(int id, string source)
        {
            var mediaCorrespondenceEventRel = _mediaCorrespondenceRepo.GetMediaCorrespondenceEvent(id);
            if (source == "MediaCorrespondences")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaCorrespondenceEventRel.MediaCorrespondenceId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.EventId = mediaCorrespondenceEventRel.EventId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceEvent", mediaCorrespondenceEventRel);
            }
            return View(mediaCorrespondenceEventRel);

        }

        //
        // POST: /Events/EditPersonEvent/5
        [HttpPost]
        public ActionResult EditMediaCorrespondenceEvent([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,MediaCorrespondenceId")] MediaCorrespondenceEventRel mediaCorrespondenceeventrel)
        {
            if (ModelState.IsValid)
            {
                if (mediaCorrespondenceeventrel.MediaCorrespondence == null)
                {
                    //reset the mediaCorrespondence object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                    mediaCorrespondenceeventrel.Event = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceEvent(mediaCorrespondenceeventrel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondenceeventrel.MediaCorrespondenceId });
                }
                //reset the organization object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                mediaCorrespondenceeventrel.MediaCorrespondence = null;
                _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceEvent(mediaCorrespondenceeventrel);
                _mediaCorrespondenceRepo.Save();
                return RedirectToAction("Details", "Events", new { id = mediaCorrespondenceeventrel.EventId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaCorrespondenceEvent(int id)
        {
            _mediaCorrespondenceRepo.DeleteMediaCorrespondenceEvent(id);
            _mediaCorrespondenceRepo.Save();
        }

        #endregion


        #region Media Audio Video

        public ActionResult GetMediaCorrespondenceMediaAudioVideos(int mediaCorrespondenceId, int mediaAudioVideoId)
        {
            IQueryable<MediaCorrespondenceMediaAudioVideoRel> mediaCorrespondenceMediaAudioVideos;
            if (mediaAudioVideoId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.Controller = "MediaCorrespondences";
                mediaCorrespondenceMediaAudioVideos = _mediaCorrespondenceRepo.GetMediaCorrespondenceMediaAudioVideos(p => p.MediaCorrespondence.Id.Equals(mediaCorrespondenceId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.Controller = "MediaAudioVideos";
                mediaCorrespondenceMediaAudioVideos = _mediaCorrespondenceRepo.GetMediaCorrespondenceMediaAudioVideos(p => p.MediaAudioVideo.Id.Equals(mediaAudioVideoId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaCorrespondenceMediaAudioVideoList", mediaCorrespondenceMediaAudioVideos);
            }
            return View(mediaCorrespondenceMediaAudioVideos);
        }

        //
        // GET: /MediaAudioVideos/CreatePersonMediaAudioVideo
        public ActionResult CreateMediaCorrespondenceMediaAudioVideo(int mediaAudioVideoId, int mediaCorrespondenceId)
        {
            //var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaCorrespondenceMediaAudioVideoRel = new MediaCorrespondenceMediaAudioVideoRel
            {
                MediaAudioVideoId = mediaAudioVideoId,
                MediaCorrespondenceId = mediaCorrespondenceId,
                //ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaAudioVideoId == -1)
            {
                mediaCorrespondenceMediaAudioVideoRel.MediaAudioVideo = new MediaAudioVideo();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaCorrespondences";
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
            }
            else
            {
                mediaCorrespondenceMediaAudioVideoRel.MediaCorrespondence = new MediaCorrespondence();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaAudioVideos";
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceMediaAudioVideo", mediaCorrespondenceMediaAudioVideoRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaCorrespondenceMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaCorrespondenceId,MediaAudioVideoId")] MediaCorrespondenceMediaAudioVideoRel mediaCorrespondencemediaAudioVideorel)
        {
            if (ModelState.IsValid)
            {
                if (mediaCorrespondencemediaAudioVideorel.MediaCorrespondence == null)
                {
                    mediaCorrespondencemediaAudioVideorel.MediaAudioVideo = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaAudioVideo(mediaCorrespondencemediaAudioVideorel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondencemediaAudioVideorel.MediaCorrespondenceId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                    mediaCorrespondencemediaAudioVideorel.MediaCorrespondence = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaAudioVideo(mediaCorrespondencemediaAudioVideorel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "MediaAudioVideos", new { id = mediaCorrespondencemediaAudioVideorel.MediaAudioVideoId });
                }
            }
            return View();
        }

        //
        // GET: /MediaAudioVideos/EditPersonMediaAudioVideo/5
        public ActionResult EditMediaCorrespondenceMediaAudioVideo(int id, string source)
        {
            var mediaCorrespondenceMediaAudioVideoRel = _mediaCorrespondenceRepo.GetMediaCorrespondenceMediaAudioVideo(id);
            if (source == "MediaCorrespondences")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaCorrespondenceMediaAudioVideoRel.MediaCorrespondenceId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaAudioVideoId = mediaCorrespondenceMediaAudioVideoRel.MediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceMediaAudioVideo", mediaCorrespondenceMediaAudioVideoRel);
            }
            return View(mediaCorrespondenceMediaAudioVideoRel);

        }

        //
        // POST: /MediaAudioVideos/EditPersonMediaAudioVideo/5
        [HttpPost]
        public ActionResult EditMediaCorrespondenceMediaAudioVideo(MediaCorrespondenceMediaAudioVideoRel mediaCorrespondencemediaAudioVideorel)
        {
            if (ModelState.IsValid)
            {
                if (mediaCorrespondencemediaAudioVideorel.MediaCorrespondence == null)
                {
                    //reset the mediaCorrespondence object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                    mediaCorrespondencemediaAudioVideorel.MediaAudioVideo = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaAudioVideo(mediaCorrespondencemediaAudioVideorel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondencemediaAudioVideorel.MediaCorrespondenceId });
                }
                //reset the organization object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                mediaCorrespondencemediaAudioVideorel.MediaCorrespondence = null;
                _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaAudioVideo(mediaCorrespondencemediaAudioVideorel);
                _mediaCorrespondenceRepo.Save();
                return RedirectToAction("Details", "MediaAudioVideos", new { id = mediaCorrespondencemediaAudioVideorel.MediaAudioVideoId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaCorrespondenceMediaAudioVideo(int id)
        {
            _mediaCorrespondenceRepo.DeleteMediaCorrespondenceMediaAudioVideo(id);
            _mediaCorrespondenceRepo.Save();
        }

        #endregion


        #region Media Images

        public ActionResult GetMediaCorrespondenceMediaImages(int mediaCorrespondenceId, int mediaImageId)
        {
            IQueryable<MediaCorrespondenceMediaImageRel> mediaCorrespondenceMediaImages;
            if (mediaImageId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.Controller = "MediaCorrespondences";
                mediaCorrespondenceMediaImages = _mediaCorrespondenceRepo.GetMediaCorrespondenceMediaImages(p => p.MediaCorrespondence.Id.Equals(mediaCorrespondenceId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.Controller = "MediaImages";
                mediaCorrespondenceMediaImages = _mediaCorrespondenceRepo.GetMediaCorrespondenceMediaImages(p => p.MediaImage.Id.Equals(mediaImageId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaCorrespondenceMediaImageList", mediaCorrespondenceMediaImages);
            }
            return View(mediaCorrespondenceMediaImages);
        }

        public ActionResult CreateMediaCorrespondenceMediaImage(int mediaCorrespondenceId, int mediaImageId)
        {
            //var approvalStatusId = Queryable.SingleOrDefault(_lookupRepo.GetApprovalStatuses(), p => p.Name.Equals("New")).Id;
            var mediaCorrespondenceMediaImageRel = new MediaCorrespondenceMediaImageRel()
            {
                MediaImageId = mediaImageId,
                MediaCorrespondenceId = mediaCorrespondenceId,
                //ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaImageId == -1)
            {
                mediaCorrespondenceMediaImageRel.MediaImage = new MediaImage();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaCorrespondences";
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
            }
            else
            {
                mediaCorrespondenceMediaImageRel.MediaCorrespondence = new MediaCorrespondence();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaImages";
                ViewBag.MediaImageId = mediaImageId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceMediaImage", mediaCorrespondenceMediaImageRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateMediaCorrespondenceMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaCorrespondenceId,MediaImageId")] MediaCorrespondenceMediaImageRel mediaCorrespondenceMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                if (mediaCorrespondenceMediaImageRel.MediaCorrespondence == null)
                {
                    mediaCorrespondenceMediaImageRel.MediaImage = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaImage(mediaCorrespondenceMediaImageRel);
                    _mediaCorrespondenceRepo.Save();
                    //return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondenceMediaImageRel.MediaCorrespondenceId });
                    return null;
                }
                //reset the mediaCorrespondence object.  This is only added from MediaCorrespondence, not ChapterMediaCorrespondenceRel.
                mediaCorrespondenceMediaImageRel.MediaCorrespondence = null;
                _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaImage(mediaCorrespondenceMediaImageRel);
                _mediaCorrespondenceRepo.Save();
                return null;
                //return RedirectToAction("Details", "MediaImage", new { id = mediaCorrespondenceMediaImageRel.MediaImageId });
            }
            return View();

        }

        public ActionResult EditMediaCorrespondenceMediaImage(int id, string source)
        {
            var mediaCorrespondenceMediaImageRel = _mediaCorrespondenceRepo.GetMediaCorrespondenceMediaImage(id);
            if (source == "MediaCorrespondences")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceMediaImageRel.MediaCorrespondenceId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = mediaCorrespondenceMediaImageRel.MediaImageId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceMediaImage", mediaCorrespondenceMediaImageRel);
            }
            return View(mediaCorrespondenceMediaImageRel);

        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditMediaCorrespondenceMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,MediaCorrespondenceId,MediaImageId")] MediaCorrespondenceMediaImageRel mediaCorrespondenceMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                if (mediaCorrespondenceMediaImageRel.MediaCorrespondence == null)
                {
                    //reset the chapter object.  This is only added from MediaCorrespondence, not ChapterMediaCorrespondenceRel.
                    mediaCorrespondenceMediaImageRel.MediaImage = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaImage(mediaCorrespondenceMediaImageRel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondenceMediaImageRel.MediaCorrespondenceId });
                }
                //reset the mediaCorrespondence object.  This is only added from MediaCorrespondence, not ChapterMediaCorrespondenceRel.
                mediaCorrespondenceMediaImageRel.MediaCorrespondence = null;
                _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaImage(mediaCorrespondenceMediaImageRel);
                _mediaCorrespondenceRepo.Save();
                return RedirectToAction("Details", "MediaImages", new { id = mediaCorrespondenceMediaImageRel.MediaImageId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaCorrespondenceMediaImage(int id)
        {
            _mediaCorrespondenceRepo.DeleteMediaCorrespondenceMediaImage(id);
            _mediaCorrespondenceRepo.Save();

        }

        #endregion


        #region MediaWebsiteEGroups

        public ActionResult GetMediaCorrespondenceMediaWebsiteEGroups(int mediaCorrespondenceId, int mediaWebsiteEGroupId)
        {
            IQueryable<MediaCorrespondenceMediaWebsiteEGroupRel> mediaCorrespondenceMediaWebsiteEGroups;
            if (mediaWebsiteEGroupId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.Controller = "MediaCorrespondences";
                mediaCorrespondenceMediaWebsiteEGroups = _mediaCorrespondenceRepo.GetMediaCorrespondenceMediaWebsiteEGroups(p => p.MediaCorrespondence.Id.Equals(mediaCorrespondenceId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.Controller = "MediaWebsiteEGroups";
                mediaCorrespondenceMediaWebsiteEGroups = _mediaCorrespondenceRepo.GetMediaCorrespondenceMediaWebsiteEGroups(p => p.MediaWebsiteEGroup.Id.Equals(mediaWebsiteEGroupId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaCorrespondenceMediaWebsiteEGroupList", mediaCorrespondenceMediaWebsiteEGroups);
            }
            return View(mediaCorrespondenceMediaWebsiteEGroups);
        }

        //
        // GET: /MediaWebsiteEGroups/CreatePersonMediaWebsiteEGroup
        public ActionResult CreateMediaCorrespondenceMediaWebsiteEGroup(int mediaWebsiteEGroupId, int mediaCorrespondenceId)
        {
            //var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaCorrespondenceMediaWebsiteEGroupRel = new MediaCorrespondenceMediaWebsiteEGroupRel
            {
                MediaWebsiteEGroupId = mediaWebsiteEGroupId,
                MediaCorrespondenceId = mediaCorrespondenceId,
                //ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaWebsiteEGroupId == -1)
            {
                mediaCorrespondenceMediaWebsiteEGroupRel.MediaWebsiteEGroup = new MediaWebsiteEGroup();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaCorrespondences";
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
            }
            else
            {
                mediaCorrespondenceMediaWebsiteEGroupRel.MediaCorrespondence = new MediaCorrespondence();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaWebsiteEGroups";
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceMediaWebsiteEGroup", mediaCorrespondenceMediaWebsiteEGroupRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaCorrespondenceMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaCorrespondenceId,MediaWebsiteEGroupId")] MediaCorrespondenceMediaWebsiteEGroupRel mediaCorrespondencemediaWebsiteEGrouprel)
        {
            if (ModelState.IsValid)
            {
                if (mediaCorrespondencemediaWebsiteEGrouprel.MediaCorrespondence == null)
                {
                    mediaCorrespondencemediaWebsiteEGrouprel.MediaWebsiteEGroup = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaWebsiteEGroup(mediaCorrespondencemediaWebsiteEGrouprel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondencemediaWebsiteEGrouprel.MediaCorrespondenceId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                    mediaCorrespondencemediaWebsiteEGrouprel.MediaCorrespondence = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaWebsiteEGroup(mediaCorrespondencemediaWebsiteEGrouprel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = mediaCorrespondencemediaWebsiteEGrouprel.MediaWebsiteEGroupId });
                }
            }
            return View();
        }

        //
        // GET: /MediaWebsiteEGroups/EditPersonMediaWebsiteEGroup/5
        public ActionResult EditMediaCorrespondenceMediaWebsiteEGroup(int id, string source)
        {
            var mediaCorrespondenceMediaWebsiteEGroupRel = _mediaCorrespondenceRepo.GetMediaCorrespondenceMediaWebsiteEGroup(id);
            if (source == "MediaCorrespondences")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaCorrespondenceMediaWebsiteEGroupRel.MediaCorrespondenceId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaWebsiteEGroupId = mediaCorrespondenceMediaWebsiteEGroupRel.MediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceMediaWebsiteEGroup", mediaCorrespondenceMediaWebsiteEGroupRel);
            }
            return View(mediaCorrespondenceMediaWebsiteEGroupRel);

        }

        //
        // POST: /MediaWebsiteEGroups/EditPersonMediaWebsiteEGroup/5
        [HttpPost]
        public ActionResult EditMediaCorrespondenceMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,MediaCorrespondenceId")] MediaCorrespondenceMediaWebsiteEGroupRel mediaCorrespondencemediaWebsiteEGrouprel)
        {
            if (ModelState.IsValid)
            {
                if (mediaCorrespondencemediaWebsiteEGrouprel.MediaCorrespondence == null)
                {
                    //reset the mediaCorrespondence object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                    mediaCorrespondencemediaWebsiteEGrouprel.MediaWebsiteEGroup = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaWebsiteEGroup(mediaCorrespondencemediaWebsiteEGrouprel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondencemediaWebsiteEGrouprel.MediaCorrespondenceId });
                }
                //reset the organization object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                mediaCorrespondencemediaWebsiteEGrouprel.MediaCorrespondence = null;
                _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceMediaWebsiteEGroup(mediaCorrespondencemediaWebsiteEGrouprel);
                _mediaCorrespondenceRepo.Save();
                return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = mediaCorrespondencemediaWebsiteEGrouprel.MediaWebsiteEGroupId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaCorrespondenceMediaWebsiteEGroup(int id)
        {
            _mediaCorrespondenceRepo.DeleteMediaCorrespondenceMediaWebsiteEGroup(id);
            _mediaCorrespondenceRepo.Save();
        }

        #endregion


        #region Subscriptions

        public ActionResult GetMediaCorrespondenceSubscriptions(int mediaCorrespondenceId, int subscriptionId)
        {
            IQueryable<MediaCorrespondenceSubscriptionRel> mediaCorrespondenceSubscriptions;
            if (subscriptionId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.Controller = "MediaCorrespondences";
                mediaCorrespondenceSubscriptions = _mediaCorrespondenceRepo.GetMediaCorrespondenceSubscriptions(p => p.MediaCorrespondence.Id.Equals(mediaCorrespondenceId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.Controller = "Subscriptions";
                mediaCorrespondenceSubscriptions = _mediaCorrespondenceRepo.GetMediaCorrespondenceSubscriptions(p => p.Subscription.Id.Equals(subscriptionId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaCorrespondenceSubscriptionList", mediaCorrespondenceSubscriptions);
            }
            return View(mediaCorrespondenceSubscriptions);
        }

        //
        // GET: /Subscriptions/CreatePersonSubscription
        public ActionResult CreateMediaCorrespondenceSubscription(int subscriptionId, int mediaCorrespondenceId)
        {
            //var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaCorrespondenceSubscriptionRel = new MediaCorrespondenceSubscriptionRel
            {
                SubscriptionId = subscriptionId,
                MediaCorrespondenceId = mediaCorrespondenceId,
                //ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (subscriptionId == -1)
            {
                mediaCorrespondenceSubscriptionRel.Subscription = new Subscription();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaCorrespondences";
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
            }
            else
            {
                mediaCorrespondenceSubscriptionRel.MediaCorrespondence = new MediaCorrespondence();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Subscriptions";
                ViewBag.SubscriptionId = subscriptionId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceSubscription", mediaCorrespondenceSubscriptionRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaCorrespondenceSubscription([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaCorrespondenceId,SubscriptionId")] MediaCorrespondenceSubscriptionRel mediaCorrespondencesubscriptionrel)
        {
            if (ModelState.IsValid)
            {
                if (mediaCorrespondencesubscriptionrel.MediaCorrespondence == null)
                {
                    mediaCorrespondencesubscriptionrel.Subscription = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceSubscription(mediaCorrespondencesubscriptionrel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondencesubscriptionrel.MediaCorrespondenceId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                    mediaCorrespondencesubscriptionrel.MediaCorrespondence = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceSubscription(mediaCorrespondencesubscriptionrel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "Subscriptions", new { id = mediaCorrespondencesubscriptionrel.SubscriptionId });
                }
            }
            return View();
        }

        //
        // GET: /Subscriptions/EditPersonSubscription/5
        public ActionResult EditMediaCorrespondenceSubscription(int id, string source)
        {
            var mediaCorrespondenceSubscriptionRel = _mediaCorrespondenceRepo.GetMediaCorrespondenceSubscription(id);
            if (source == "MediaCorrespondences")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Correspondence") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaCorrespondenceSubscriptionRel.MediaCorrespondenceId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Media Correspondence")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.SubscriptionId = mediaCorrespondenceSubscriptionRel.SubscriptionId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaCorrespondenceSubscription", mediaCorrespondenceSubscriptionRel);
            }
            return View(mediaCorrespondenceSubscriptionRel);

        }

        //
        // POST: /Subscriptions/EditPersonSubscription/5
        [HttpPost]
        public ActionResult EditMediaCorrespondenceSubscription(MediaCorrespondenceSubscriptionRel mediaCorrespondencesubscriptionrel)
        {
            if (ModelState.IsValid)
            {
                if (mediaCorrespondencesubscriptionrel.MediaCorrespondence == null)
                {
                    //reset the mediaCorrespondence object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                    mediaCorrespondencesubscriptionrel.Subscription = null;
                    _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceSubscription(mediaCorrespondencesubscriptionrel);
                    _mediaCorrespondenceRepo.Save();
                    return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaCorrespondencesubscriptionrel.MediaCorrespondenceId });
                }
                //reset the organization object.  This is only added from Organization, not MediaCorrespondenceOrganizationRel.
                mediaCorrespondencesubscriptionrel.MediaCorrespondence = null;
                _mediaCorrespondenceRepo.InsertOrUpdateMediaCorrespondenceSubscription(mediaCorrespondencesubscriptionrel);
                _mediaCorrespondenceRepo.Save();
                return RedirectToAction("Details", "Subscriptions", new { id = mediaCorrespondencesubscriptionrel.SubscriptionId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaCorrespondenceSubscription(int id)
        {
            _mediaCorrespondenceRepo.DeleteMediaCorrespondenceSubscription(id);
            _mediaCorrespondenceRepo.Save();
        }

        #endregion


    }
}




