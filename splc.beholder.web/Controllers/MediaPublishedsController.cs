using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using Caseiro.Mvc.PagedList;
using Caseiro.Mvc.PagedList.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using splc.data;
using splc.domain.Models;
using splc.data.repository;
using System.IO;
using System.Web;
using System.Data.Entity.Validation;
using splc.data.Utility;
using System.Text;
using splc.beholder.web.Models;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class MediaPublishedsController : BaseController
    {
        private readonly ILookupRepository _lookupRepo;
        private readonly IMediaPublishedRepository _mediaPublishedRepo;
        private readonly ACDBContext db;

        public MediaPublishedsController(
            ILookupRepository lookupRepo,
            IMediaPublishedRepository mediaPublishedRepo,
            ACDBContext _db)
        {
            _lookupRepo = lookupRepo;
            _mediaPublishedRepo = mediaPublishedRepo;
            db = _db;

            ViewBag.PossibleApprovalStatus = _lookupRepo.GetApprovalStatuses();
            ViewBag.PossibleMediaTypes = _lookupRepo.GetMediaTypes();
            ViewBag.PossibleMimeTypes = _lookupRepo.GetMimeTypes();
            ViewBag.PossiblePublishedTypes = _lookupRepo.GetPublishedTypes();
            ViewBag.PossibleNewsSources = _lookupRepo.GetNewsSources();
            ViewBag.PossibleMovementClasses = _lookupRepo.GetMovementClasses();
            ViewBag.PossibleRenewalPermissionTypes = _lookupRepo.RenewalPermissionTypes();
            ViewBag.PossiblePrimaryStatuses = _lookupRepo.GetPrimaryStatuses();
            ViewBag.PossibleLibraryCategoryTypes = _lookupRepo.GetLibraryCategoryTypes();

            ViewBag.PossibleStates = _lookupRepo.GetStates();
            ViewBag.PossibleRemovalStatuses = _lookupRepo.GetRemovalStatus();
        }

        #region Base

        public ActionResult GetContextList(int MediaPublishedId)
        {
            //            var list = db.MediaPublishedContexts.Where(x => x.MediaPublishedId == MediaPublishedId).ToList();
            var list = db.MediaPublishedContexts.Where(x => x.MediaPublishedId == MediaPublishedId).Select(s =>
            new PublishedContextViewModel()
            {
                Id = s.Id,
                FileName = s.FileName,
                MediaPublishedId = s.MediaPublishedId
            }).ToList();
            return View("_DocumentList", list);
        }

        [HttpPost]
        public JsonResult SaveTextAsContent(int mediapublishedid, string filename, string content)
        //public ActionResult SaveTextAsContent(FormCollection form)
        {
            var context = new MediaPublishedContext()
            {
                MediaPublishedId = mediapublishedid,
                FileName = filename,
                ContextText = Encoding.ASCII.GetBytes(content),
                DocumentExtension = "txt",
                MimeTypeId = 22
            };

            try
            {
                db.MediaPublishedContexts.Add(context);
                db.SaveChanges();
            }
            catch (ValidationException e)
            {
                var message = e.Message;
            }
            return Json(context, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaveAttachments(IEnumerable<HttpPostedFileBase> attachments, int mediaPublishedId)
        {

            if (attachments != null)
            {
                foreach (var file in attachments)
                {
                    try
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                        file.SaveAs(physicalPath);

                        var mimetypeid = _lookupRepo.GetMimeTypes().SingleOrDefault(p => p.Name.Equals(file.ContentType.ToLower())).Id;
                        var context = new MediaPublishedContext()
                        {
                            MediaPublishedId = mediaPublishedId,
                            FileName = fileName,
                            ContextText = GetFile(physicalPath),
                            DocumentExtension = Path.GetExtension(physicalPath),
                            MimeTypeId = mimetypeid
                        };
                        try
                        {
                            db.MediaPublishedContexts.Add(context);
                            db.SaveChanges();
                            System.IO.File.Delete(physicalPath);
                        }
                        catch (ValidationException e)
                        {
                            return Content(string.Format(@"File contains invalid characters \/:*?""<>|"));
                        }
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
            var context = db.MediaPublishedContexts.Find(id);
            db.MediaPublishedContexts.Remove(context);
            db.SaveChanges();
            return null;
        }

        public ViewResult Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim();
            IQueryable<MediaPublished> list = _mediaPublishedRepo.GetMediaPublisheds(currentUser, p => p.Name.Contains(searchTerm) || p.Author.Contains(searchTerm));

            return View("Index", list);
        }

        public ActionResult Index(string publishedname = "", string location = "", DateTime? datefrom = null, DateTime? dateto = null, List<int> publishedtypeid = null, string publishedtypeid_string = "", List<int> stateid = null, string stateid_string = "", string docsearch = "", int? page = 1, int? pageSize = 15)
        {
            if (!String.IsNullOrWhiteSpace(publishedtypeid_string))
            {
                publishedtypeid = ((List<int>)publishedtypeid_string.Split(',').Select(int.Parse).ToList());
            }

            if (!String.IsNullOrWhiteSpace(stateid_string))
            {
                stateid = stateid_string.Split(',').Select(int.Parse).ToList();
            }
            publishedname = publishedname.Trim();
            location = location.Trim();
            docsearch = docsearch.Trim();

            Session["stateid"] = stateid;
            Session["publishedname"] = publishedname;
            Session["publishedtypeid"] = publishedtypeid;
            Session["location"] = location;
            Session["datefrom"] = datefrom;
            Session["dateto"] = dateto;
            Session["page"] = page ?? 1;
            Session["pageSize"] = pageSize;
            Session["docsearch"] = docsearch;

            //TODO:  need to handle multiple search expressions.
            //this is to prepare the search term for full text index search.  s variable is used for full text index search.
            string s = null;
            if (!String.IsNullOrEmpty(docsearch))
            {
                s = String.Format("\"{0}\"", FtsInterceptor.Fts(docsearch.Replace("\"", "")));
            }
            //TODO: REFACTOR!!!!
            PagedList<MediaPublished> list = null;
            if (s == null)
            {
                if (publishedtypeid != null)
                {
                    if (stateid != null)
                    {
                        list = _mediaPublishedRepo.GetMediaPublisheds(currentUser, x =>
                            (publishedname.Length == 0 || x.Name.Contains(publishedname))
                            && (x.DatePublished ?? DateTime.MinValue) >= (datefrom.HasValue ? datefrom : (x.DatePublished ?? DateTime.MinValue))
                            && (x.DatePublished ?? DateTime.MinValue) <= (dateto.HasValue ? dateto : (x.DatePublished ?? DateTime.MinValue))
                            && (location.Length == 0 || x.City.Contains(location))
                            && stateid.Contains((int)x.StateId)
                            && (publishedtypeid.Contains((int)x.PublishedTypeId))
                            //&& x.MediaPublishedContext_Index.ContextText.Contains(s)
                            ).OrderByDescending(m => m.DatePublished).ToPagedList(page ?? 1, pageSize ?? 15);
                    }
                    else
                    {
                        list = _mediaPublishedRepo.GetMediaPublisheds(currentUser, x =>
                            (publishedname.Length == 0 || x.Name.Contains(publishedname))
                            && (x.DatePublished ?? DateTime.MinValue) >= (datefrom.HasValue ? datefrom : (x.DatePublished ?? DateTime.MinValue))
                            && (x.DatePublished ?? DateTime.MinValue) <= (dateto.HasValue ? dateto : (x.DatePublished ?? DateTime.MinValue))
                            && (location.Length == 0 || x.City.Contains(location))
                            && (publishedtypeid.Contains((int)x.PublishedTypeId))
                            //&& x.MediaPublishedContext_Index.ContextText.Contains(s)
                            ).OrderByDescending(m => m.DatePublished).ToPagedList(page ?? 1, pageSize ?? 15);
                    }
                }
                else
                {
                    if (stateid != null)
                    {
                        list = _mediaPublishedRepo.GetMediaPublisheds(currentUser, x =>
                            (publishedname.Length == 0 || x.Name.Contains(publishedname))
                            && (x.DatePublished ?? DateTime.MinValue) >= (datefrom.HasValue ? datefrom : (x.DatePublished ?? DateTime.MinValue))
                            && (x.DatePublished ?? DateTime.MinValue) <= (dateto.HasValue ? dateto : (x.DatePublished ?? DateTime.MinValue))
                            && (location.Length == 0 || x.City.Contains(location))
                            && stateid.Contains((int)x.StateId)
                            //&& x.MediaPublishedContext_Index.ContextText.Contains(s)
                            ).OrderByDescending(m => m.DatePublished).ToPagedList(page ?? 1, pageSize ?? 15);
                    }
                    else
                    {
                        list = _mediaPublishedRepo.GetMediaPublisheds(currentUser, x =>
                            (publishedname.Length == 0 || x.Name.Contains(publishedname))
                            && (x.DatePublished ?? DateTime.MinValue) >= (datefrom.HasValue ? datefrom : (x.DatePublished ?? DateTime.MinValue))
                            && (x.DatePublished ?? DateTime.MinValue) <= (dateto.HasValue ? dateto : (x.DatePublished ?? DateTime.MinValue))
                            && (location.Length == 0 || x.City.Contains(location))
                            //&& x.MediaPublishedContext_Index.ContextText.Contains(s)
                            ).OrderByDescending(m => m.DatePublished).ToPagedList(page ?? 1, pageSize ?? 15);
                    }
                }
            }
            else
            {
                if (publishedtypeid != null)
                {
                    if (stateid != null)
                    {
                        list = _mediaPublishedRepo.GetMediaPublisheds(currentUser, x =>
                            (publishedname.Length == 0 || x.Name.Contains(publishedname))
                            && (x.DatePublished ?? DateTime.MinValue) >= (datefrom.HasValue ? datefrom : (x.DatePublished ?? DateTime.MinValue))
                            && (x.DatePublished ?? DateTime.MinValue) <= (dateto.HasValue ? dateto : (x.DatePublished ?? DateTime.MinValue))
                            && (location.Length == 0 || x.City.Contains(location))
                            && stateid.Contains((int)x.StateId)
                            && (publishedtypeid.Contains((int)x.PublishedTypeId))
                            && (x.MediaPublishedContext_Indexes.Any(m => m.ContextText.Contains(s)))
                            //&& x.MediaPublishedContext_Index.ContextText.Contains(s)
                            ).OrderByDescending(m => m.DatePublished).ToPagedList(page ?? 1, pageSize ?? 15);
                    }
                    else
                    {
                        list = _mediaPublishedRepo.GetMediaPublisheds(currentUser, x =>
                            (publishedname.Length == 0 || x.Name.Contains(publishedname))
                            && (x.DatePublished ?? DateTime.MinValue) >= (datefrom.HasValue ? datefrom : (x.DatePublished ?? DateTime.MinValue))
                            && (x.DatePublished ?? DateTime.MinValue) <= (dateto.HasValue ? dateto : (x.DatePublished ?? DateTime.MinValue))
                            && (location.Length == 0 || x.City.Contains(location))
                            && (publishedtypeid.Contains((int)x.PublishedTypeId))
                            && (x.MediaPublishedContext_Indexes.Any(m => m.ContextText.Contains(s)))
                            //&& x.MediaPublishedContext_Index.ContextText.Contains(s)
                            ).OrderByDescending(m => m.DatePublished).ToPagedList(page ?? 1, pageSize ?? 15);
                    }
                }
                else
                {
                    if (stateid != null)
                    {
                        list = _mediaPublishedRepo.GetMediaPublisheds(currentUser, x =>
                            (publishedname.Length == 0 || x.Name.Contains(publishedname))
                            && (x.DatePublished ?? DateTime.MinValue) >= (datefrom.HasValue ? datefrom : (x.DatePublished ?? DateTime.MinValue))
                            && (x.DatePublished ?? DateTime.MinValue) <= (dateto.HasValue ? dateto : (x.DatePublished ?? DateTime.MinValue))
                            && (location.Length == 0 || x.City.Contains(location))
                            && stateid.Contains((int)x.StateId)
                            && (x.MediaPublishedContext_Indexes.Any(m => m.ContextText.Contains(s)))
                            //&& x.MediaPublishedContext_Index.ContextText.Contains(s)
                            ).OrderByDescending(m => m.DatePublished).ToPagedList(page ?? 1, pageSize ?? 15);
                    }
                    else
                    {
                        // DocSearch Only
                        list = _mediaPublishedRepo.GetMediaPublisheds(currentUser, x =>
                            (publishedname.Length == 0 || x.Name.Contains(publishedname))
                            && (x.DatePublished ?? DateTime.MinValue) >= (datefrom.HasValue ? datefrom : (x.DatePublished ?? DateTime.MinValue))
                            && (x.DatePublished ?? DateTime.MinValue) <= (dateto.HasValue ? dateto : (x.DatePublished ?? DateTime.MinValue))
                            && (location.Length == 0 || x.City.Contains(location))
                            && (x.MediaPublishedContext_Indexes.Any(m => m.ContextText.Contains(s)))
                            //&& x.MediaPublishedContext_Index.ContextText.Contains(s)
                            ).OrderByDescending(m => m.DatePublished).ToPagedList(page ?? 1, pageSize ?? 15);
                    }
                }
            }

            //slj had to move filter of deleted records here because having the filter build here for the full context search and in the repository for the date deleted filter was messing up the full context search and causing an error.
            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaPublishedList", list); //.Where(x => x.DateDeleted == null).AsPagination(page ?? 1, pageSize ?? 15));
            }
            return View("Index", list); //.Where(x => x.DateDeleted == null).AsPagination(page ?? 1, pageSize ?? 15));

        }

        // GET: Search for BeholderPersons 
        public JsonResult GetMediaPublishedList(string term)
        {
            term = term.Trim();
            var list = _mediaPublishedRepo.GetMediaPublisheds(currentUser, p => p.Name.Contains(term)).ToArray().Select(
                e => new
                {
                    Id = e.Id,
                    label = e.Name
                });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MediaPublisheds/Details/5
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
            ViewBag.MediaCorrespondenceId = -1;
            ViewBag.MediaWebsiteEGroupId = -1;
            ViewBag.SubscriptionId = -1;
            ViewBag.MediaPublishedId = id;
            ViewBag.Controller = "MediaPublisheds";

            var published = _mediaPublishedRepo.GetMediaPublished(currentUser, id);
            if (published != null)
            {
                return View(published);
            }
            return View("Published404");
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
            var document = _mediaPublishedRepo.GetMediaPublishedContext(id);
            var cd = new System.Net.Mime.ContentDisposition
            {
                // for example foo.bak
                FileName = document.FileName,

                // always prompt the user for downloading, set to true if you want 
                // the browser to try to show the file inline
                Inline = true,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            //return File(ImageHelper.Decompress(document.ContextText), document.MimeType.Name);
            return File(document.ContextText, document.MimeType.Name);
        }

        public byte[] CreateFile(string fileText)
        {
            return Encoding.ASCII.GetBytes(fileText);

        }

        // GET: /MediaPublisheds/Create
        public ActionResult Create()
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            //var entity = new MediaPublishedContext { };
            var mediaPublished = new MediaPublished
            {
                MediaTypeId = _lookupRepo.GetMediaTypes().SingleOrDefault(p => p.Name.Equals("Published")).Id,
                //MediaPublishedContext = entity
            };
            return View(mediaPublished);
        }

        [HttpPost]
        public ActionResult Create(MediaPublished mediapublished, FormCollection form, IEnumerable<HttpPostedFileBase> attachments)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _mediaPublishedRepo.InsertOrUpdate(mediapublished);
                    _mediaPublishedRepo.Save();

                    var content = form["fileContent"];

                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        var filename = form["filename"];
                        var context = new MediaPublishedContext()
                        {
                            MediaPublishedId = mediapublished.Id,
                            FileName = filename,
                            ContextText = Encoding.ASCII.GetBytes(content),
                            DocumentExtension = "txt",
                            MimeTypeId = 22
                        };

                        try
                        {
                            db.MediaPublishedContexts.Add(context);
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
                            if (file == null) break;
                            var filename = Path.GetFileName(file.FileName);
                            if (filename != null)
                            {
                                var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), filename);
                                file.SaveAs(physicalPath);

                                var mimetypeid =
                                    _lookupRepo.GetMimeTypes()
                                        .SingleOrDefault(p => p.Name.Equals(file.ContentType.ToLower()))
                                        .Id;
                                var context = new MediaPublishedContext()
                                {
                                    MediaPublishedId = mediapublished.Id,
                                    FileName = filename,
                                    ContextText = GetFile(physicalPath),
                                    DocumentExtension = Path.GetExtension(physicalPath),
                                    MimeTypeId = mimetypeid
                                };

                                try
                                {
                                    db.MediaPublishedContexts.Add(context);
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
                    var message = e.Message;
                }

                return RedirectToAction("Details", new { id = mediapublished.Id });
            }

            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            return View(mediapublished);

        }

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            var published = _mediaPublishedRepo.GetMediaPublished(currentUser, id);

            if (published != null)
            {
                return View(published);
            }
            return View("Published404");
        }

        //
        // POST: /MediaPublisheds/Edit/5
        [HttpPost]
        public ActionResult Edit(MediaPublished mediapublished)
        {
            if (ModelState.IsValid)
            {
                _mediaPublishedRepo.InsertOrUpdate(mediapublished);

                try
                {
                    _mediaPublishedRepo.Save();
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

                return RedirectToAction("Details", new { id = mediapublished.Id });
            }
            ViewBag.PossibleConfidentialityTypes = _lookupRepo.GetConfidentialityTypes(currentUser);
            return View(mediapublished);
        }

        public ActionResult RemovePublished(int id)
        {
            var published = _mediaPublishedRepo.GetMediaPublished(currentUser, id);
            return View(published);
        }

        [HttpPost]
        public ActionResult RemovePublished(int id, string removalreason)
        {
            var published = _mediaPublishedRepo.GetMediaPublished(currentUser, id);
            published.RemovalReason = removalreason;

            published.RemovalStatusId = 1;
            _mediaPublishedRepo.InsertOrUpdate(published);
            _mediaPublishedRepo.Save();

            return RedirectToAction("Index");

        }

        //
        // GET: /MediaPublisheds/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_mediaPublishedRepo.GetMediaPublished(id));
        }

        //
        // POST: /MediaPublisheds/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _mediaPublishedRepo.Delete(id);
            _mediaPublishedRepo.Save();

            return RedirectToAction("Index");
        }

        #endregion


        #region Events

        public ActionResult GetMediaPublishedEvents(int mediaPublishedId, int eventId)
        {
            IQueryable<MediaPublishedEventRel> mediaPublishedEvents;
            if (eventId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.EventId = eventId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "MediaPublisheds";
                mediaPublishedEvents = _mediaPublishedRepo.GetMediaPublishedEvents(p => p.MediaPublished.Id.Equals(mediaPublishedId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.EventId = eventId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "Events";
                mediaPublishedEvents = _mediaPublishedRepo.GetMediaPublishedEvents(p => p.Event.Id.Equals(eventId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaPublishedEventList", mediaPublishedEvents);
            }
            return View(mediaPublishedEvents);
        }

        //
        // GET: /Events/CreatePersonEvent
        public ActionResult CreateMediaPublishedEvent(int eventId, int mediaPublishedId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaPublishedEventRel = new MediaPublishedEventRel
            {
                EventId = eventId,
                MediaPublishedId = mediaPublishedId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (eventId == -1)
            {
                mediaPublishedEventRel.Event = new Event();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaPublisheds";
                ViewBag.MediaPublishedId = mediaPublishedId;
            }
            else
            {
                mediaPublishedEventRel.MediaPublished = new MediaPublished();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Events";
                ViewBag.EventId = eventId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedEvent", mediaPublishedEventRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaPublishedEvent([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,EventId")] MediaPublishedEventRel mediaPublishedeventrel)
        {
            if (ModelState.IsValid)
            {
                //if (mediaPublishedeventrel.MediaPublished == null)
                //{
                //    mediaPublishedeventrel.Event = null;
                _mediaPublishedRepo.InsertOrUpdateMediaPublishedEvent(mediaPublishedeventrel);
                _mediaPublishedRepo.Save();
                return null;
                //    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedeventrel.MediaPublishedId });
                //}
                //else
                //{
                //    //reset the organization object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                //    mediaPublishedeventrel.MediaPublished = null;
                //    _mediaPublishedRepo.InsertOrUpdateMediaPublishedEvent(mediaPublishedeventrel);
                //    _mediaPublishedRepo.Save();
                //    return RedirectToAction("Details", "Events", new { id = mediaPublishedeventrel.EventId });
                //}
            }
            return View(mediaPublishedeventrel);
        }

        //
        // GET: /Events/EditPersonEvent/5
        public ActionResult EditMediaPublishedEvent(int id, string source)
        {
            var mediaPublishedEventRel = _mediaPublishedRepo.GetMediaPublishedEvent(id);
            if (source == "MediaPublisheds")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Event")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaPublishedEventRel.MediaPublishedId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Event") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.EventId = mediaPublishedEventRel.EventId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedEvent", mediaPublishedEventRel);
            }
            return View(mediaPublishedEventRel);

        }

        //
        // POST: /Events/EditPersonEvent/5
        [HttpPost]
        public ActionResult EditMediaPublishedEvent([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,EventId")] MediaPublishedEventRel mediaPublishedeventrel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedeventrel.MediaPublished == null)
                {
                    //reset the mediaPublished object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                    mediaPublishedeventrel.Event = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedEvent(mediaPublishedeventrel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedeventrel.MediaPublishedId });
                }
                //reset the organization object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                mediaPublishedeventrel.MediaPublished = null;
                _mediaPublishedRepo.InsertOrUpdateMediaPublishedEvent(mediaPublishedeventrel);
                _mediaPublishedRepo.Save();
                return RedirectToAction("Details", "Events", new { id = mediaPublishedeventrel.EventId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaPublishedEvent(int id)
        {
            _mediaPublishedRepo.DeleteMediaPublishedEvent(id);
            _mediaPublishedRepo.Save();
        }

        #endregion


        #region Media Audio Video

        public ActionResult GetMediaPublishedMediaAudioVideos(int mediaPublishedId, int mediaAudioVideoId)
        {
            IQueryable<MediaPublishedMediaAudioVideoRel> mediaPublishedMediaAudioVideos;
            if (mediaAudioVideoId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "MediaPublisheds";
                mediaPublishedMediaAudioVideos = _mediaPublishedRepo.GetMediaPublishedMediaAudioVideos(p => p.MediaPublished.Id.Equals(mediaPublishedId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "MediaAudioVideos";
                mediaPublishedMediaAudioVideos = _mediaPublishedRepo.GetMediaPublishedMediaAudioVideos(p => p.MediaAudioVideo.Id.Equals(mediaAudioVideoId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaPublishedMediaAudioVideoList", mediaPublishedMediaAudioVideos);
            }
            return View(mediaPublishedMediaAudioVideos);
        }

        //
        // GET: /MediaAudioVideos/CreatePersonMediaAudioVideo
        public ActionResult CreateMediaPublishedMediaAudioVideo(int mediaAudioVideoId, int mediaPublishedId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaPublishedMediaAudioVideoRel = new MediaPublishedMediaAudioVideoRel
            {
                MediaAudioVideoId = mediaAudioVideoId,
                MediaPublishedId = mediaPublishedId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaAudioVideoId == -1)
            {
                mediaPublishedMediaAudioVideoRel.MediaAudioVideo = new MediaAudioVideo();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaPublisheds";
                ViewBag.MediaPublishedId = mediaPublishedId;
            }
            else
            {
                mediaPublishedMediaAudioVideoRel.MediaPublished = new MediaPublished();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaAudioVideos";
                ViewBag.MediaAudioVideoId = mediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedMediaAudioVideo", mediaPublishedMediaAudioVideoRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaPublishedMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,MediaAudioVideoId")] MediaPublishedMediaAudioVideoRel mediaPublishedmediaAudioVideorel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedmediaAudioVideorel.MediaPublished == null)
                {
                    mediaPublishedmediaAudioVideorel.MediaAudioVideo = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaAudioVideo(mediaPublishedmediaAudioVideorel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedmediaAudioVideorel.MediaPublishedId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                    mediaPublishedmediaAudioVideorel.MediaPublished = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaAudioVideo(mediaPublishedmediaAudioVideorel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaAudioVideos", new { id = mediaPublishedmediaAudioVideorel.MediaAudioVideoId });
                }
            }
            return View();
        }

        //
        // GET: /MediaAudioVideos/EditPersonMediaAudioVideo/5
        public ActionResult EditMediaPublishedMediaAudioVideo(int id, string source)
        {
            var mediaPublishedMediaAudioVideoRel = _mediaPublishedRepo.GetMediaPublishedMediaAudioVideo(id);
            if (source == "MediaPublisheds")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaPublishedMediaAudioVideoRel.MediaPublishedId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaAudioVideoId = mediaPublishedMediaAudioVideoRel.MediaAudioVideoId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedMediaAudioVideo", mediaPublishedMediaAudioVideoRel);
            }
            return View(mediaPublishedMediaAudioVideoRel);

        }

        //
        // POST: /MediaAudioVideos/EditPersonMediaAudioVideo/5
        [HttpPost]
        public ActionResult EditMediaPublishedMediaAudioVideo([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,MediaAudioVideoId")] MediaPublishedMediaAudioVideoRel mediaPublishedmediaAudioVideorel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedmediaAudioVideorel.MediaPublished == null)
                {
                    //reset the mediaPublished object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                    mediaPublishedmediaAudioVideorel.MediaAudioVideo = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaAudioVideo(mediaPublishedmediaAudioVideorel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedmediaAudioVideorel.MediaPublishedId });
                }
                //reset the organization object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                mediaPublishedmediaAudioVideorel.MediaPublished = null;
                _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaAudioVideo(mediaPublishedmediaAudioVideorel);
                _mediaPublishedRepo.Save();
                return RedirectToAction("Details", "MediaAudioVideos", new { id = mediaPublishedmediaAudioVideorel.MediaAudioVideoId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaPublishedMediaAudioVideo(int id)
        {
            _mediaPublishedRepo.DeleteMediaPublishedMediaAudioVideo(id);
            _mediaPublishedRepo.Save();
        }

        #endregion


        #region Media Correspondence

        public ActionResult GetMediaPublishedMediaCorrespondences(int mediaPublishedId, int mediaCorrespondenceId)
        {
            IQueryable<MediaPublishedMediaCorrespondenceRel> mediaPublishedMediaCorrespondences;
            if (mediaCorrespondenceId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "MediaPublisheds";
                mediaPublishedMediaCorrespondences = _mediaPublishedRepo.GetMediaPublishedMediaCorrespondences(p => p.MediaPublished.Id.Equals(mediaPublishedId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "MediaCorrespondences";
                mediaPublishedMediaCorrespondences = _mediaPublishedRepo.GetMediaPublishedMediaCorrespondences(p => p.MediaCorrespondence.Id.Equals(mediaCorrespondenceId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaPublishedMediaCorrespondenceList", mediaPublishedMediaCorrespondences);
            }
            return View(mediaPublishedMediaCorrespondences);
        }

        //
        // GET: /MediaCorrespondences/CreatePersonMediaCorrespondence
        public ActionResult CreateMediaPublishedMediaCorrespondence(int mediaCorrespondenceId, int mediaPublishedId)
        {
            //var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaPublishedMediaCorrespondenceRel = new MediaPublishedMediaCorrespondenceRel
            {
                MediaCorrespondenceId = mediaCorrespondenceId,
                MediaPublishedId = mediaPublishedId,
                //ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaCorrespondenceId == -1)
            {
                mediaPublishedMediaCorrespondenceRel.MediaCorrespondence = new MediaCorrespondence();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaPublisheds";
                ViewBag.MediaPublishedId = mediaPublishedId;
            }
            else
            {
                mediaPublishedMediaCorrespondenceRel.MediaPublished = new MediaPublished();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaCorrespondences";
                ViewBag.MediaCorrespondenceId = mediaCorrespondenceId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedMediaCorrespondence", mediaPublishedMediaCorrespondenceRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaPublishedMediaCorrespondence([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,MediaCorrespondenceId")] MediaPublishedMediaCorrespondenceRel mediaPublishedmediaCorrespondencerel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedmediaCorrespondencerel.MediaPublished == null)
                {
                    mediaPublishedmediaCorrespondencerel.MediaCorrespondence = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaCorrespondence(mediaPublishedmediaCorrespondencerel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedmediaCorrespondencerel.MediaPublishedId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                    mediaPublishedmediaCorrespondencerel.MediaPublished = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaCorrespondence(mediaPublishedmediaCorrespondencerel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaPublishedmediaCorrespondencerel.MediaCorrespondenceId });
                }
            }
            return View();
        }

        //
        // GET: /MediaCorrespondences/EditPersonMediaCorrespondence/5
        public ActionResult EditMediaPublishedMediaCorrespondence(int id, string source)
        {
            var mediaPublishedMediaCorrespondenceRel = _mediaPublishedRepo.GetMediaPublishedMediaCorrespondence(id);
            if (source == "MediaPublisheds")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Audio/Video")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaPublishedMediaCorrespondenceRel.MediaPublishedId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Audio/Video") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaCorrespondenceId = mediaPublishedMediaCorrespondenceRel.MediaCorrespondenceId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedMediaCorrespondence", mediaPublishedMediaCorrespondenceRel);
            }
            return View(mediaPublishedMediaCorrespondenceRel);

        }

        //
        // POST: /MediaCorrespondences/EditPersonMediaCorrespondence/5
        [HttpPost]
        public ActionResult EditMediaPublishedMediaCorrespondence([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,MediaCorrespondenceId")] MediaPublishedMediaCorrespondenceRel mediaPublishedmediaCorrespondencerel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedmediaCorrespondencerel.MediaPublished == null)
                {
                    //reset the mediaPublished object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                    mediaPublishedmediaCorrespondencerel.MediaCorrespondence = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaCorrespondence(mediaPublishedmediaCorrespondencerel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedmediaCorrespondencerel.MediaPublishedId });
                }
                //reset the organization object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                mediaPublishedmediaCorrespondencerel.MediaPublished = null;
                _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaCorrespondence(mediaPublishedmediaCorrespondencerel);
                _mediaPublishedRepo.Save();
                return RedirectToAction("Details", "MediaCorrespondences", new { id = mediaPublishedmediaCorrespondencerel.MediaCorrespondenceId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaPublishedMediaCorrespondence(int id)
        {
            _mediaPublishedRepo.DeleteMediaPublishedMediaCorrespondence(id);
            _mediaPublishedRepo.Save();
        }

        #endregion


        #region Media Images

        public ActionResult GetMediaPublishedMediaImages(int mediaPublishedId, int mediaImageId)
        {
            IQueryable<MediaPublishedMediaImageRel> mediaPublishedMediaImages;
            if (mediaImageId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "MediaPublisheds";
                mediaPublishedMediaImages = _mediaPublishedRepo.GetMediaPublishedMediaImages(p => p.MediaPublished.Id.Equals(mediaPublishedId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.MediaImageId = mediaImageId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "MediaImages";
                mediaPublishedMediaImages = _mediaPublishedRepo.GetMediaPublishedMediaImages(p => p.MediaImage.Id.Equals(mediaImageId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaPublishedMediaImageList", mediaPublishedMediaImages);
            }
            return View(mediaPublishedMediaImages);
        }

        public ActionResult CreateMediaPublishedMediaImage(int mediaPublishedId, int mediaImageId)
        {
            var approvalStatusId = Queryable.SingleOrDefault(_lookupRepo.GetApprovalStatuses(), p => p.Name.Equals("New")).Id;
            var mediaPublishedMediaImageRel = new MediaPublishedMediaImageRel()
            {
                MediaImageId = mediaImageId,
                MediaPublishedId = mediaPublishedId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaImageId == -1)
            {
                mediaPublishedMediaImageRel.MediaImage = new MediaImage();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaPublisheds";
                ViewBag.MediaPublishedId = mediaPublishedId;
            }
            else
            {
                mediaPublishedMediaImageRel.MediaPublished = new MediaPublished();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaImages";
                ViewBag.MediaImageId = mediaImageId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedMediaImage", mediaPublishedMediaImageRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateMediaPublishedMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,MediaImageId")] MediaPublishedMediaImageRel mediaPublishedMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedMediaImageRel.MediaPublished == null)
                {
                    mediaPublishedMediaImageRel.MediaImage = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaImage(mediaPublishedMediaImageRel);
                    _mediaPublishedRepo.Save();
                    //return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedMediaImageRel.MediaPublishedId });
                    return null;
                }
                //reset the mediaPublished object.  This is only added from MediaPublished, not ChapterMediaPublishedRel.
                mediaPublishedMediaImageRel.MediaPublished = null;
                _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaImage(mediaPublishedMediaImageRel);
                _mediaPublishedRepo.Save();
                //return RedirectToAction("Details", "MediaImage", new { id = mediaPublishedMediaImageRel.MediaImageId });
                return null;
            }
            return View();

        }

        public ActionResult EditMediaPublishedMediaImage(int id, string source)
        {
            var mediaPublishedMediaImageRel = _mediaPublishedRepo.GetMediaPublishedMediaImage(id);
            if (source == "MediaPublisheds")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Image")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaPublishedId = mediaPublishedMediaImageRel.MediaPublishedId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Image") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaImageId = mediaPublishedMediaImageRel.MediaImageId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedMediaImage", mediaPublishedMediaImageRel);
            }
            return View(mediaPublishedMediaImageRel);

        }

        //
        // POST: /ChapterPersonRels/EditChapterPerson
        [HttpPost]
        public ActionResult EditMediaPublishedMediaImage([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,EventId,MediaPublishedId,MediaImageId")] MediaPublishedMediaImageRel mediaPublishedMediaImageRel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedMediaImageRel.MediaPublished == null)
                {
                    //reset the chapter object.  This is only added from MediaPublished, not ChapterMediaPublishedRel.
                    mediaPublishedMediaImageRel.MediaImage = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaImage(mediaPublishedMediaImageRel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedMediaImageRel.MediaPublishedId });
                }
                //reset the mediaPublished object.  This is only added from MediaPublished, not ChapterMediaPublishedRel.
                mediaPublishedMediaImageRel.MediaPublished = null;
                _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaImage(mediaPublishedMediaImageRel);
                _mediaPublishedRepo.Save();
                return RedirectToAction("Details", "MediaImages", new { id = mediaPublishedMediaImageRel.MediaImageId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaPublishedMediaImage(int id)
        {
            _mediaPublishedRepo.DeleteMediaPublishedMediaImage(id);
            _mediaPublishedRepo.Save();

        }

        #endregion


        #region Media Published

        public ActionResult GetMediaPublishedMediaPublisheds(int mediaPublishedId)
        {
            MediaPublished mediaPublisheds;
            //if (subscriptionId == -1)
            //{
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
            ViewBag.MediaPublishedId = mediaPublishedId;
            ViewBag.MediaPublished2Id = mediaPublishedId;
            ViewBag.Controller = "MediaPublisheds";
            try
            {
                mediaPublisheds = _mediaPublishedRepo.GetMediaPublished(mediaPublishedId);
                //                subscriptions = _eventRepository.Get(p => p.Id.Equals(eventId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaPublishedMediaPublishedList", mediaPublisheds);
            }
            return View(mediaPublisheds);
        }

        public ActionResult CreateMediaPublishedMediaPublished(int mediapublishedId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaPublishedMediaPublishedRel = new MediaPublishedMediaPublishedRel
            {
                MediaPublishedId = mediapublishedId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
                MediaPublished2 = new MediaPublished()
            };

            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
            ViewBag.MediaPublishedId = mediapublishedId;
            ViewBag.MediaPublished2Id = -1;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedMediaPublished", mediaPublishedMediaPublishedRel);
            }

            return View();

        }

        [HttpPost]
        public ActionResult CreateMediaPublishedMediaPublished([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,MediaPublished2Id")] MediaPublishedMediaPublishedRel mediaPublishedMediaPublishedRel)
        {
            if (ModelState.IsValid)
            {
                mediaPublishedMediaPublishedRel.MediaPublished2 = null;
                _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaPublished(mediaPublishedMediaPublishedRel);
                _mediaPublishedRepo.Save();
                return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedMediaPublishedRel.MediaPublishedId });
            }
            return View();

        }

        public ActionResult EditMediaPublishedMediaPublished(int id)
        {
            var mediaPublishedMediaPublishedRel = _mediaPublishedRepo.GetMediaPublishedMediaPublished(id);
            ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedMediaPublished", mediaPublishedMediaPublishedRel);
            }
            return View(mediaPublishedMediaPublishedRel);

        }

        //
        // POST: /EventPersonRels/EditEventPerson
        [HttpPost]
        public ActionResult EditMediaPublishedMediaPublished(MediaPublishedMediaPublishedRel mediaPublishedMediaPublishedRel)
        {
            if (ModelState.IsValid)
            {
                _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaPublished(mediaPublishedMediaPublishedRel);
                _mediaPublishedRepo.Save();
                return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedMediaPublishedRel.MediaPublishedId });
            }

            return View();
        }

        [HttpPost]
        public void DeleteMediaPublishedMediaPublished(int id)
        {
            _mediaPublishedRepo.DeleteMediaPublishedMediaPublished(id);
            _mediaPublishedRepo.Save();
        }

        #endregion


        #region Media WebsiteEGroup

        public ActionResult GetMediaPublishedMediaWebsiteEGroups(int mediaPublishedId, int mediaWebsiteEGroupId)
        {
            IQueryable<MediaPublishedMediaWebsiteEGroupRel> mediaPublishedMediaWebsiteEGroups;
            if (mediaWebsiteEGroupId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "MediaPublisheds";
                mediaPublishedMediaWebsiteEGroups = _mediaPublishedRepo.GetMediaPublishedMediaWebsiteEGroups(p => p.MediaPublished.Id.Equals(mediaPublishedId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "MediaWebsiteEGroups";
                mediaPublishedMediaWebsiteEGroups = _mediaPublishedRepo.GetMediaPublishedMediaWebsiteEGroups(p => p.MediaWebsiteEGroup.Id.Equals(mediaWebsiteEGroupId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaPublishedMediaWebsiteEGroupList", mediaPublishedMediaWebsiteEGroups);
            }
            return View(mediaPublishedMediaWebsiteEGroups);
        }

        //
        // GET: /MediaWebsiteEGroups/CreatePersonMediaWebsiteEGroup
        public ActionResult CreateMediaPublishedMediaWebsiteEGroup(int mediaWebsiteEGroupId, int mediaPublishedId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaPublishedMediaWebsiteEGroupRel = new MediaPublishedMediaWebsiteEGroupRel
            {
                MediaWebsiteEGroupId = mediaWebsiteEGroupId,
                MediaPublishedId = mediaPublishedId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (mediaWebsiteEGroupId == -1)
            {
                mediaPublishedMediaWebsiteEGroupRel.MediaWebsiteEGroup = new MediaWebsiteEGroup();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaPublisheds";
                ViewBag.MediaPublishedId = mediaPublishedId;
            }
            else
            {
                mediaPublishedMediaWebsiteEGroupRel.MediaPublished = new MediaPublished();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaWebsiteEGroups";
                ViewBag.MediaWebsiteEGroupId = mediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedMediaWebsiteEGroup", mediaPublishedMediaWebsiteEGroupRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaPublishedMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,MediaWebsiteEGroupId")] MediaPublishedMediaWebsiteEGroupRel mediaPublishedmediaWebsiteEGrouprel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedmediaWebsiteEGrouprel.MediaPublished == null)
                {
                    mediaPublishedmediaWebsiteEGrouprel.MediaWebsiteEGroup = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaWebsiteEGroup(mediaPublishedmediaWebsiteEGrouprel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedmediaWebsiteEGrouprel.MediaPublishedId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                    mediaPublishedmediaWebsiteEGrouprel.MediaPublished = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaWebsiteEGroup(mediaPublishedmediaWebsiteEGrouprel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = mediaPublishedmediaWebsiteEGrouprel.MediaWebsiteEGroupId });
                }
            }
            return View();
        }

        //
        // GET: /MediaWebsiteEGroups/EditPersonMediaWebsiteEGroup/5
        public ActionResult EditMediaPublishedMediaWebsiteEGroup(int id, string source)
        {
            var mediaPublishedMediaWebsiteEGroupRel = _mediaPublishedRepo.GetMediaPublishedMediaWebsiteEGroup(id);
            if (source == "MediaPublisheds")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Media Website/EGroup")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaPublishedMediaWebsiteEGroupRel.MediaPublishedId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Website/EGroup") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.MediaWebsiteEGroupId = mediaPublishedMediaWebsiteEGroupRel.MediaWebsiteEGroupId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedMediaWebsiteEGroup", mediaPublishedMediaWebsiteEGroupRel);
            }
            return View(mediaPublishedMediaWebsiteEGroupRel);

        }

        //
        // POST: /MediaWebsiteEGroups/EditPersonMediaWebsiteEGroup/5
        [HttpPost]
        public ActionResult EditMediaPublishedMediaWebsiteEGroup([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaWebsiteEGroupId,MediaPublishedId")] MediaPublishedMediaWebsiteEGroupRel mediaPublishedmediaWebsiteEGrouprel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedmediaWebsiteEGrouprel.MediaPublished == null)
                {
                    //reset the mediaPublished object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                    mediaPublishedmediaWebsiteEGrouprel.MediaWebsiteEGroup = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaWebsiteEGroup(mediaPublishedmediaWebsiteEGrouprel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedmediaWebsiteEGrouprel.MediaPublishedId });
                }
                //reset the organization object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                mediaPublishedmediaWebsiteEGrouprel.MediaPublished = null;
                _mediaPublishedRepo.InsertOrUpdateMediaPublishedMediaWebsiteEGroup(mediaPublishedmediaWebsiteEGrouprel);
                _mediaPublishedRepo.Save();
                return RedirectToAction("Details", "MediaWebsiteEGroups", new { id = mediaPublishedmediaWebsiteEGrouprel.MediaWebsiteEGroupId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaPublishedMediaWebsiteEGroup(int id)
        {
            _mediaPublishedRepo.DeleteMediaPublishedMediaWebsiteEGroup(id);
            _mediaPublishedRepo.Save();
        }

        #endregion


        #region Vehicles

        public ActionResult GetMediaPublishedVehicles(int mediaPublishedId, int vehicleId)
        {
            IQueryable<MediaPublishedVehicleRel> mediaPublishedVehicles;
            if (vehicleId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "MediaPublisheds";
                mediaPublishedVehicles = _mediaPublishedRepo.GetMediaPublishedVehicles(p => p.MediaPublished.Id.Equals(mediaPublishedId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.VehicleId = vehicleId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "Vehicles";
                mediaPublishedVehicles = _mediaPublishedRepo.GetMediaPublishedVehicles(p => p.Vehicle.Id.Equals(vehicleId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaPublishedVehicleList", mediaPublishedVehicles);
            }
            return View(mediaPublishedVehicles);
        }

        //
        // GET: /Vehicles/CreatePersonVehicle
        public ActionResult CreateMediaPublishedVehicle(int vehicleId, int mediaPublishedId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaPublishedVehicleRel = new MediaPublishedVehicleRel
            {
                VehicleId = vehicleId,
                MediaPublishedId = mediaPublishedId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (vehicleId == -1)
            {
                mediaPublishedVehicleRel.Vehicle = new Vehicle();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaPublisheds";
                ViewBag.MediaPublishedId = mediaPublishedId;
            }
            else
            {
                mediaPublishedVehicleRel.MediaPublished = new MediaPublished();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Vehicles";
                ViewBag.VehicleId = vehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedVehicle", mediaPublishedVehicleRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaPublishedVehicle([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,VehicleId")] MediaPublishedVehicleRel mediaPublishedvehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedvehiclerel.MediaPublished == null)
                {
                    mediaPublishedvehiclerel.Vehicle = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedVehicle(mediaPublishedvehiclerel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedvehiclerel.MediaPublishedId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                    mediaPublishedvehiclerel.MediaPublished = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedVehicle(mediaPublishedvehiclerel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "Vehicles", new { id = mediaPublishedvehiclerel.VehicleId });
                }
            }
            return View();
        }

        //
        // GET: /Vehicles/EditPersonVehicle/5
        public ActionResult EditMediaPublishedVehicle(int id, string source)
        {
            var mediaPublishedVehicleRel = _mediaPublishedRepo.GetMediaPublishedVehicle(id);
            if (source == "MediaPublisheds")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Vehicle")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaPublishedVehicleRel.MediaPublishedId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Vehicle") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.VehicleId = mediaPublishedVehicleRel.VehicleId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedVehicle", mediaPublishedVehicleRel);
            }
            return View(mediaPublishedVehicleRel);

        }

        //
        // POST: /Vehicles/EditPersonVehicle/5
        [HttpPost]
        public ActionResult EditMediaPublishedVehicle(MediaPublishedVehicleRel mediaPublishedvehiclerel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedvehiclerel.MediaPublished == null)
                {
                    //reset the mediaPublished object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                    mediaPublishedvehiclerel.Vehicle = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedVehicle(mediaPublishedvehiclerel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedvehiclerel.MediaPublishedId });
                }
                //reset the organization object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                mediaPublishedvehiclerel.MediaPublished = null;
                _mediaPublishedRepo.InsertOrUpdateMediaPublishedVehicle(mediaPublishedvehiclerel);
                _mediaPublishedRepo.Save();
                return RedirectToAction("Details", "Vehicles", new { id = mediaPublishedvehiclerel.VehicleId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaPublishedVehicle(int id)
        {
            _mediaPublishedRepo.DeleteMediaPublishedVehicle(id);
            _mediaPublishedRepo.Save();
        }

        #endregion


        #region Subscriptions

        public ActionResult GetMediaPublishedSubscriptions(int mediaPublishedId, int subscriptionId)
        {
            IQueryable<MediaPublishedSubscriptionRel> mediaPublishedSubscriptions;
            if (subscriptionId == -1)
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "MediaPublisheds";
                mediaPublishedSubscriptions = _mediaPublishedRepo.GetMediaPublishedSubscriptions(p => p.MediaPublished.Id.Equals(mediaPublishedId));
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.SubscriptionId = subscriptionId;
                ViewBag.MediaPublishedId = mediaPublishedId;
                ViewBag.Controller = "Subscriptions";
                mediaPublishedSubscriptions = _mediaPublishedRepo.GetMediaPublishedSubscriptions(p => p.Subscription.Id.Equals(subscriptionId));
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MediaPublishedSubscriptionList", mediaPublishedSubscriptions);
            }
            return View(mediaPublishedSubscriptions);
        }

        //
        // GET: /Subscriptions/CreatePersonSubscription
        public ActionResult CreateMediaPublishedSubscription(int subscriptionId, int mediaPublishedId)
        {
            var approvalStatusId = _lookupRepo.GetApprovalStatuses().SingleOrDefault(p => p.Name.Equals("New")).Id;
            var mediaPublishedSubscriptionRel = new MediaPublishedSubscriptionRel
            {
                SubscriptionId = subscriptionId,
                MediaPublishedId = mediaPublishedId,
                ApprovalStatusId = approvalStatusId,
                DateStart = DateTime.Now,
            };

            if (subscriptionId == -1)
            {
                mediaPublishedSubscriptionRel.Subscription = new Subscription();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "MediaPublisheds";
                ViewBag.MediaPublishedId = mediaPublishedId;
            }
            else
            {
                mediaPublishedSubscriptionRel.MediaPublished = new MediaPublished();
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = "Subscriptions";
                ViewBag.SubscriptionId = subscriptionId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedSubscription", mediaPublishedSubscriptionRel);
            }

            return View();

        }


        [HttpPost]
        public ActionResult CreateMediaPublishedSubscription([Bind(Include = "Id,RelationshipTypeId,DateStart,DateEnd,MediaPublishedId,SubscriptionId")] MediaPublishedSubscriptionRel mediaPublishedsubscriptionrel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedsubscriptionrel.MediaPublished == null)
                {
                    mediaPublishedsubscriptionrel.Subscription = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedSubscription(mediaPublishedsubscriptionrel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedsubscriptionrel.MediaPublishedId });
                }
                else
                {
                    //reset the organization object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                    mediaPublishedsubscriptionrel.MediaPublished = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedSubscription(mediaPublishedsubscriptionrel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "Subscriptions", new { id = mediaPublishedsubscriptionrel.SubscriptionId });
                }
            }
            return View();
        }

        //
        // GET: /Subscriptions/EditPersonSubscription/5
        public ActionResult EditMediaPublishedSubscription(int id, string source)
        {
            var mediaPublishedSubscriptionRel = _mediaPublishedRepo.GetMediaPublishedSubscription(id);
            if (source == "MediaPublisheds")
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Media Published") && x.ObjectTo.Equals("Subscription")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.PersonId = mediaPublishedSubscriptionRel.MediaPublishedId;
            }
            else
            {
                ViewBag.PossibleRelationshipTypes = _lookupRepo.GetRelationshipTypes().Where(x => x.ObjectFrom.Equals("Subscription") && x.ObjectTo.Equals("Media Published")).OrderBy(x => x.SortOrder);
                ViewBag.Controller = source;
                ViewBag.SubscriptionId = mediaPublishedSubscriptionRel.SubscriptionId;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditMediaPublishedSubscription", mediaPublishedSubscriptionRel);
            }
            return View(mediaPublishedSubscriptionRel);

        }

        //
        // POST: /Subscriptions/EditPersonSubscription/5
        [HttpPost]
        public ActionResult EditMediaPublishedSubscription(MediaPublishedSubscriptionRel mediaPublishedsubscriptionrel)
        {
            if (ModelState.IsValid)
            {
                if (mediaPublishedsubscriptionrel.MediaPublished == null)
                {
                    //reset the mediaPublished object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                    mediaPublishedsubscriptionrel.Subscription = null;
                    _mediaPublishedRepo.InsertOrUpdateMediaPublishedSubscription(mediaPublishedsubscriptionrel);
                    _mediaPublishedRepo.Save();
                    return RedirectToAction("Details", "MediaPublisheds", new { id = mediaPublishedsubscriptionrel.MediaPublishedId });
                }
                //reset the organization object.  This is only added from Organization, not MediaPublishedOrganizationRel.
                mediaPublishedsubscriptionrel.MediaPublished = null;
                _mediaPublishedRepo.InsertOrUpdateMediaPublishedSubscription(mediaPublishedsubscriptionrel);
                _mediaPublishedRepo.Save();
                return RedirectToAction("Details", "Subscriptions", new { id = mediaPublishedsubscriptionrel.SubscriptionId });
            }
            return View();
        }

        [HttpPost]
        public void DeleteMediaPublishedSubscription(int id)
        {
            _mediaPublishedRepo.DeleteMediaPublishedSubscription(id);
            _mediaPublishedRepo.Save();
        }

        #endregion


    }
}





