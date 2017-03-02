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
            ViewBag.Groups = new SelectList(UserRepo.GetGroups().ToList(), "Id", "Name");
            ViewBag.Prefixes = new SelectList(lookupRepository.GetPrefixes().ToList(), "Id", "Name");
            ViewBag.Suffixes = new SelectList(lookupRepository.GetSuffixes().ToList(), "Id", "Name");
            ViewBag.Confidentiality = new SelectList(lookupRepository.GetConfidentialityTypes().ToList(), "Id", "Name");
        }

        private IEnumerable<SelectListItem> GetGroups()
        {
            var groups = UserRepo.GetGroups().Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            });

            return groups;
        }

        public JsonResult GetUsers(string term)
        {
            term = term.Trim();
            var users = UserRepo.GetUsers(u => u.UserName.Contains(term)).ToList().Select(
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
            var users = UserRepo.GetUsers();
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
            var user = UserRepo.Find(id);
            return PartialView("_UserDetail", user);
        }

        [HttpPost]
        public void ToggleDisableUser(int id)
        {
            var user = UserRepo.Find(id);
            if (user == null) return;

            if (user.Disabled == false)
            {
                UserRepo.Disable(id, CurrentUser);
            }
            else
            {
                UserRepo.Enable(id, CurrentUser);
            }
            UserRepo.Save();
        }

        [HttpGet]
        public ActionResult UserGroups(int id)
        {
            var groups = UserRepo.GetUserGroups(id);
            return PartialView("_UserGroups", groups);
        }

        [HttpPost]
        public void DeleteGroupUser(int id)
        {
            UserRepo.DeleteGroupUser(id);
            UserRepo.Save();
        }

        [HttpGet]
        public void CreateGroupUser(int userId, int groupId)
        {
            var user = UserRepo.Find(userId);
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
                UserRepo.InsertOrUpdate(groupUser);
            }
            UserRepo.Save();
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
            UserRepo.InsertOrUpdate(user);
            UserRepo.Save();
            return null;
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var user = UserRepo.Find(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditUser", user);
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            user.ModifiedUserId = CurrentUser.Id;
            if (ModelState.IsValid)
            {
                UserRepo.InsertOrUpdate(user);
                UserRepo.Save();
                return RedirectToAction("Users", "Security");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Groups()
        {
            var groups = UserRepo.GetGroups();
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
            var group = UserRepo.GetGroup(id);
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
            UserRepo.DeleteGroup(id);
            UserRepo.Save();
        }


        [HttpPost]
        public ActionResult CreateGroup(Group group)
        {
            group.CreatedUserId = CurrentUser.Id;
            UserRepo.InsertOrUpdate(group);
            UserRepo.Save();
            return null;
        }

        [HttpGet]
        public ActionResult EditGroup(int id)
        {
            var group = UserRepo.GetGroup(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CreateOrEditGroup", group);
            }
            return View(group);
        }

        [HttpPost]
        public ActionResult EditGroup(Group group)
        {
            group.ModifiedUserId = CurrentUser.Id;

            if (ModelState.IsValid)
            {
                UserRepo.InsertOrUpdate(group);
                UserRepo.Save();
                return RedirectToAction("Groups", "Security");
            }
            return View(group);
        }

    }
}