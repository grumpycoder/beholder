using Microsoft.Ajax.Utilities;
using splc.beholder.web.Models;
using splc.beholder.web.Utility;
using splc.data;
using splc.data.repository;
using splc.domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class SearchesController : BaseController
    {
        private readonly ISearchRepository _searchRepository;
        private readonly ACDBContext _context;

        public SearchesController(ACDBContext context, ISearchRepository searchRepository)
        {
            _context = context;
            _searchRepository = searchRepository;
        }

        public ViewResult Search(string searchTerm)
        {
            searchTerm = searchTerm.Trim();

            Session["searchTerm"] = searchTerm;
            string firstName, lastName;
            var persons = new List<BeholderPerson>();
            if (searchTerm.Contains(","))
            {
                if (searchTerm.Split(',').Length > 0)
                {
                    lastName = searchTerm.Split(',')[0];
                    firstName = searchTerm.Split(',')[1];
                    persons = _context.BeholderPersons.Where(
                        x => x.CommonPerson.LName == lastName.Trim() && x.CommonPerson.FName == firstName.Trim()).OrderBy(p => p.CommonPerson.LName).ThenBy(p => p.CommonPerson.FName)
                        .ToList();
                }
            }
            else if (searchTerm.Trim().Contains(" ") && !searchTerm.Contains(","))
            {
                if (searchTerm.Split(' ').Length > 0)
                {
                    lastName = searchTerm.Split(' ')[1];
                    firstName = searchTerm.Split(' ')[0];
                    persons = _context.BeholderPersons.Where(
                        x => x.CommonPerson.LName.Contains(lastName.Trim()) && x.CommonPerson.FName.Contains(firstName.Trim())).OrderBy(p => p.CommonPerson.LName).ThenBy(p => p.CommonPerson.FName).ToList();
                }
            }
            else if (!searchTerm.Trim().Contains(" "))
            {
                persons = _context.BeholderPersons.Where(
                        x => x.CommonPerson.LName.Contains(searchTerm.Trim()) || x.CommonPerson.FName.Contains(searchTerm.Trim())).OrderBy(p => p.CommonPerson.LName).ThenBy(p => p.CommonPerson.FName).ToList();
            }

            var organizations = _context.Organizations.Where(x => x.OrganizationName.Contains(searchTerm)).OrderBy(x => x.OrganizationName).ToList();

            var chapters = _context.Chapters.Where(x => x.ChapterName.Contains(searchTerm)).OrderBy(x => x.ChapterName).ToList();

            var viewModel = new SearchResultViewModel { BeholderPersons = persons, Organizations = organizations, Chapters = chapters };
            if (Request.IsAjaxRequest())
            {
                return View("_searchResultList", viewModel);
            }
            return View("Index");

        }

        public ActionResult Index(string searchterm, int? page, int? pageSize)
        {

            if (searchterm.IsNullOrWhiteSpace())
            {
                return View("Index");
            }

            searchterm = searchterm.Trim();

            Session["searchTerm"] = searchterm;
            string firstName, lastName;

            var names = new string[] { };

            if (searchterm.Contains(",")) names = searchterm.Split(',');
            if (!searchterm.Contains(",")) names = searchterm.Split(' ');

            var personPred = PredicateBuilder.True<BeholderPerson>();
            if (searchterm.Contains(","))
            {
                lastName = names[0].Trim();
                firstName = names[1].Trim();
                personPred = personPred.And(e => e.CommonPerson.FName.Contains(firstName) && e.CommonPerson.LName.Contains(lastName));
            }
            else
            {
                if (names.Length > 1)
                {
                    firstName = names[0].Trim();
                    lastName = names[1].Trim();
                    personPred = personPred.And(e => e.CommonPerson.FName.Contains(firstName) && e.CommonPerson.LName.Contains(lastName));
                }
                else
                {
                    firstName = names[0].Trim();
                    lastName = names[0].Trim();
                    personPred = personPred.And(e => e.CommonPerson.FName.Contains(firstName) || e.CommonPerson.LName.Contains(lastName));
                }
            }

            var persons = _context.BeholderPersons.Where(personPred).OrderBy(e => e.CommonPerson.LName).ToList();

            var organizations = _context.Organizations.Where(x => x.OrganizationName.Contains(searchterm)).OrderBy(x => x.OrganizationName).ToList();
            var chapters = _context.Chapters.Where(x => x.ChapterName.Contains(searchterm)).OrderBy(x => x.ChapterName).ToList();

            var viewModel = new SearchResultViewModel { BeholderPersons = persons, Organizations = organizations, Chapters = chapters };
            if (Request.IsAjaxRequest())
            {
                return View("_searchResultList", viewModel);
            }
            return View("Index", viewModel);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _searchRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
