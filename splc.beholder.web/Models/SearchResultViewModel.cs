using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using splc.domain.Models;

namespace splc.beholder.web.Models
{
    public class SearchResultViewModel
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Location { get; set; }
        //public DateTime ViewDate { get; set; }
        //public string[] Alias { get; set; }
        //public string Comment { get; set; }
        public List<BeholderPerson> BeholderPersons { get; set; }
        public List<Organization> Organizations { get; set; }
        public List<Chapter> Chapters { get; set; }
    }
}