using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using splc.beholder.web.Models;
using splc.data.repository;
using splc.domain.Models;
using System.Linq;
using System.Web.Mvc;
using splc.data;

namespace splc.beholder.web.Controllers
{
    [Authorize]
    public class SearchesController : BaseController
    {
        private readonly ISearchRepository _searchRepository;
        private readonly ACDBContext context;

        public SearchesController(ACDBContext Context, ISearchRepository searchRepository)
        {
            context = Context;
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
                    persons = context.BeholderPersons.Where(
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
                    persons = context.BeholderPersons.Where(
                        x => x.CommonPerson.LName.Contains(lastName.Trim()) && x.CommonPerson.FName.Contains(firstName.Trim())).OrderBy(p => p.CommonPerson.LName).ThenBy(p => p.CommonPerson.FName).ToList();
                }
            }
            else if (!searchTerm.Trim().Contains(" "))
            {
                persons = context.BeholderPersons.Where(
                        x => x.CommonPerson.LName.Contains(searchTerm.Trim()) || x.CommonPerson.FName.Contains(searchTerm.Trim())).OrderBy(p => p.CommonPerson.LName).ThenBy(p => p.CommonPerson.FName).ToList();
            }

            var organizations = context.Organizations.Where(x => x.OrganizationName.Contains(searchTerm)).OrderBy(x => x.OrganizationName).ToList();

            var chapters = context.Chapters.Where(x => x.ChapterName.Contains(searchTerm)).OrderBy(x => x.ChapterName).ToList();

            var viewModel = new SearchResultViewModel {BeholderPersons = persons, Organizations = organizations, Chapters = chapters};
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
            var persons = new List<BeholderPerson>();
            if (searchterm.Contains(","))
            {
                if (searchterm.Split(',').Length > 0)
                {
                    lastName = searchterm.Split(',')[0];
                    firstName = searchterm.Split(',')[1];
                    persons = context.BeholderPersons.Where(
                        x => x.CommonPerson.LName == lastName.Trim() && x.CommonPerson.FName == firstName.Trim()).OrderBy(p => p.CommonPerson.LName).ThenBy(p => p.CommonPerson.FName)
                        .ToList();
                }
            }
            else if (searchterm.Trim().Contains(" ") && !searchterm.Contains(","))
            {
                if (searchterm.Split(' ').Length > 0)
                {
                    lastName = searchterm.Split(' ')[1];
                    firstName = searchterm.Split(' ')[0];
                    persons = context.BeholderPersons.Where(
                        x => x.CommonPerson.LName.Contains(lastName.Trim()) && x.CommonPerson.FName.Contains(firstName.Trim())).OrderBy(p => p.CommonPerson.LName).ThenBy(p => p.CommonPerson.FName)
                        .ToList();
                }
            }
            else if (!searchterm.Trim().Contains(" "))
            {
                persons = context.BeholderPersons.Where(
                        x => x.CommonPerson.LName.Contains(searchterm.Trim()) || x.CommonPerson.FName.Contains(searchterm.Trim())).OrderBy(p => p.CommonPerson.LName).ThenBy(p => p.CommonPerson.FName)
                        .ToList();
            }

            var organizations = context.Organizations.Where(x => x.OrganizationName.Contains(searchterm)).OrderBy(x => x.OrganizationName).ToList();
            var chapters = context.Chapters.Where(x => x.ChapterName.Contains(searchterm)).OrderBy(x => x.ChapterName).ToList();

            var viewModel = new SearchResultViewModel { BeholderPersons = persons, Organizations = organizations, Chapters = chapters};
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
