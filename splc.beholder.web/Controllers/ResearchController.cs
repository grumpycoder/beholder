using System.Linq;
using System.Web.Mvc;
using Caseiro.Mvc.PagedList.Extensions;
using splc.data;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class ResearchController : BaseController
    {
        private readonly ACDBContext context; 

        public ResearchController(ACDBContext Context)
        {
            context = Context;
        }

        // GET: Research
        public ActionResult WhiteRevolution(int? page, int? pageSize, string searchTerm)
        {
            Session["page"] = page ?? 1;
            Session["searchTerm"] = searchTerm;

            var customers = searchTerm == null ? 
                context.Customers.OrderBy(c => c.Lastname).ToPagedList(page ?? 1, pageSize ?? 25) :
                context.Customers.Where(c => c.Lastname.Contains(searchTerm) || c.Firstname.Contains(searchTerm)).OrderBy(c => c.Lastname).ToPagedList(page ?? 1, pageSize ?? 25);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_WhiteRevolutionList", customers);
            }
            return View(customers);
        }

        public ActionResult WhiteRevolutionDetails(string id)
        {
            var customer = context.Customers.Find(id);

            return View(customer);
        }

    }
}