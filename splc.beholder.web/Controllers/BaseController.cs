using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using splc.data;
using splc.data.repository;
using splc.domain.Models;

namespace splc.beholder.web.Controllers
{

    public class BaseController : Controller
    {
        //protected IUserRepository userRepo;
        protected IUserRepository userRepo;
        protected User currentUser;

        public BaseController()
        {

        }

        public BaseController(IUserRepository userRepository)
        {
            userRepo = userRepository;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            userRepo = new UserRepository(new ACDBContext());
            if (User.Identity.Name != "")
            {
                //if (HttpContext.Session != null && HttpContext.Session["UserId"] == null)
                //{

                //userRepo = new UserRepository(new ACDBContext());

                //var user = userRepo.Find(User.Identity.Name);
                //currentUser = user;


                if (HttpContext.Session != null && HttpContext.Session["UserId"] == null)
                {
                    //userRepo = new UserRepository(new ACDBContext());
                    var user = userRepo.Find(User.Identity.Name);

                    currentUser = user;

                    HttpContext.Session["UserId"] = user.Id;
                    HttpContext.Session["User"] = user;
                    var u = (User)HttpContext.Session["User"];
                    Debug.WriteLine(u.UserName);
                }
                else
                {
                    currentUser = (User)HttpContext.Session["User"];
                }

            }

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var s = filterContext.Controller.GetType().Name;

            if (User.Identity.IsAuthenticated)
            {

                //var user = userRepo.Find(User.Identity.Name);
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
                    var g = currentUser.GroupUsers.Where(x => x.Group.Name == "Admin");
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
                    var g = currentUser.GroupUsers.Where(x => x.Group.Name == "Approver");
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
                    var g = currentUser.GroupUsers.Where(x => x.Group.Name == "Researchers");
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