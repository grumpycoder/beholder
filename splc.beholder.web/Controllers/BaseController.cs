using splc.data;
using splc.data.repository;
using splc.domain.Models;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace splc.beholder.web.Controllers
{

    public class BaseController : Controller
    {
        protected IUserRepository UserRepo;
        protected User CurrentUser;

        public BaseController() { }

        public BaseController(IUserRepository userRepository)
        {
            UserRepo = userRepository;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            UserRepo = new UserRepository(new ACDBContext());
            if (User.Identity.Name == "") return;

            if (HttpContext.Session != null && HttpContext.Session["UserId"] == null)
            {
                var user = UserRepo.Find(User.Identity.Name);

                CurrentUser = user;

                HttpContext.Session["UserId"] = user.Id;
                HttpContext.Session["User"] = user;
                var u = (User)HttpContext.Session["User"];
                Debug.WriteLine(u.UserName);
            }
            else
            {
                CurrentUser = (User)HttpContext.Session["User"];
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var s = filterContext.Controller.GetType().Name;

            if (User.Identity.IsAuthenticated)
            {
                var user = (User)HttpContext.Session["User"];
                if (user == null && s != "HomeController")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Home",
                        action = "UserNotDefined"
                    }));
                }
                if (user != null && (s == "AdminController" || s == "SecurityController"))
                {
                    var g = CurrentUser.GroupUsers.Where(x => x.Group.Name == "Admin");
                    var b = g.Any(groupUser => groupUser.DateDeleted == null);

                    if (!b)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "UnauthorizedAccess"
                        }));
                    }
                }
                if (user != null && s == "ApprovalController")
                {
                    var g = CurrentUser.GroupUsers.Where(x => x.Group.Name == "Approver");
                    var b = g.Any(groupUser => groupUser.DateDeleted == null);

                    if (!b)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "UnauthorizedAccess"
                        }));
                    }
                }

                if (user != null && s == "ResearchController")
                {
                    var g = CurrentUser.GroupUsers.Where(x => x.Group.Name == "Researchers");
                    var b = g.Any(groupUser => groupUser.DateDeleted == null);

                    if (!b)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "UnauthorizedAccess"
                        }));
                    }
                }
            }
        }

    }
}