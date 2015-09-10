using Ninject.Activation;
using splc.data.repository;
using splc.domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class SecurityController : BaseController
    {
        private readonly ILookupRepository lookupRepository;

        public SecurityController(ILookupRepository lookupRepo)
        {
            lookupRepository = lookupRepo;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            ViewBag.Groups = new SelectList(userRepo.GetGroups().ToList(), "Id", "Name");
            ViewBag.Prefixes = new SelectList(lookupRepository.GetPrefixes().ToList(), "Id", "Name");
            ViewBag.Suffixes = new SelectList(lookupRepository.GetSuffixes().ToList(), "Id", "Name");
            ViewBag.Confidentiality = new SelectList(lookupRepository.GetConfidentialityTypes().ToList(), "Id", "Name");
        }

        private IEnumerable<SelectListItem> GetGroups()
        {
            var groups = userRepo.GetGroups().Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            });

            return groups;
        }

        public JsonResult GetUsers(string term)
        {
            term = term.Trim();
            var users = userRepo.GetUsers(u => u.UserName.Contains(term)).ToList().Select(
                i => new
                {
                    id = i.Id,
                    value = i.UserName,
                    label = i.UserName
                }
                );

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Security/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Users()
        {
            var users = userRepo.GetUsers();
            ViewBag.gridpage = Request.QueryString["gridpage"];
            if (Request.IsAjaxRequest())
            {
                return PartialView("_UserList", users);
            }
            return View("Users", users);
        }

        [HttpGet]
        public ActionResult UserDetail(int id)
        {
            var user = userRepo.Find(id);
            return PartialView("_UserDetail", user);
        }

        [HttpPost]
        public void ToggleDisableUser(int id)
        {
            var user = userRepo.Find(id);
            if (user == null) return;

            if (user.Disabled == false)
            {
                userRepo.Disable(id, currentUser);
            }
            else
            {
                userRepo.Enable(id, currentUser);
            }
            userRepo.Save();
        }

        [HttpGet]
        public ActionResult UserGroups(int id)
        {
            var groups = userRepo.GetUserGroups(id);
            return PartialView("_UserGroups", groups);
        }

        [HttpPost]
        public void DeleteGroupUser(int id)
        {
            userRepo.DeleteGroupUser(id);
            userRepo.Save();
        }

        [HttpGet]
        public void CreateGroupUser(int userId, int groupId)
        {
            var user = userRepo.Find(userId);
            if (user == null) return;
            //TODO: Check for user already exists in group
            if (user.GroupUsers.Count(x => x.GroupId == groupId) > 0)
            {
                var groups = user.GroupUsers.Where(x => x.GroupId == groupId);
                foreach (var g in groups)
                {
                    g.DateDeleted = null;
                    g.DeletedUserId = null;
                }
            }
            else
            {
                var groupUser = new GroupUser()
                {
                    UserId = userId,
                    GroupId = groupId
                };
                userRepo.InsertOrUpdate(groupUser);
            }
            userRepo.Save();
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            var user = new User();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditUser", user);
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult CreateUser([Bind(Include = "Username,PersonId,Disabled")] User user)
        {
            userRepo.InsertOrUpdate(user);
            userRepo.Save();
            return null;
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var user = userRepo.Find(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditUser", user);
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser( User user)
        {
            user.ModifiedUserId = currentUser.Id;
            if (ModelState.IsValid)
            {
                userRepo.InsertOrUpdate(user);
                userRepo.Save();
                return RedirectToAction("Users", "Security");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Groups()
        {
            var groups = userRepo.GetGroups();
            ViewBag.gridpage = Request.QueryString["gridpage"];
            if (Request.IsAjaxRequest())
            {
                return PartialView("_GroupList", groups);
            }
            return View("Groups", groups);
        }

        [HttpGet]
        public ActionResult GroupDetail(int id)
        {
            var group = userRepo.GetGroup(id);
            return PartialView("_GroupDetail", group);
        }

        [HttpGet]
        public ActionResult CreateGroup()
        {
            var group = new Group();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditGroup", group);
            }
            return View(group);
        }

        [HttpPost]
        public void DeleteGroup(int id)
        {
            userRepo.DeleteGroup(id);
            userRepo.Save();
        }


        [HttpPost]
        public ActionResult CreateGroup(Group group)
        {
            group.CreatedUserId = currentUser.Id;
            userRepo.InsertOrUpdate(group);
            userRepo.Save();
            return null;
        }

        [HttpGet]
        public ActionResult EditGroup(int id)
        {
            var group = userRepo.GetGroup(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditGroup", group);
            }
            return View(group);
        }

        [HttpPost]
        public ActionResult EditGroup(Group group)
        {
            group.ModifiedUserId = currentUser.Id;

            if (ModelState.IsValid)
            {
                userRepo.InsertOrUpdate(group);
                userRepo.Save();
                return RedirectToAction("Groups", "Security");
            }
            return View(group);
        }

    }
}