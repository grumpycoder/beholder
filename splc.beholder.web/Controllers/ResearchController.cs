using Caseiro.Mvc.PagedList.Extensions;
using splc.beholder.web.Utility;
using splc.data;
using splc.domain.Models;
using System.Linq;
using System.Web.Mvc;

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

            var pred = PredicateBuilder.True<Customer>();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                pred = pred.And(c => c.Lastname.Contains(searchTerm) || c.Firstname.Contains(searchTerm));
            }
            var customers = context.Customers.Where(pred).OrderBy(c => c.Lastname).ToPagedList(page ?? 1, pageSize ?? 25);

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