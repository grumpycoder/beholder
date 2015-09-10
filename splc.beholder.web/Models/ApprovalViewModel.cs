using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using splc.domain.Models;

namespace splc.beholder.web.Models
{
    public class ApprovalViewModel
    {
        public int Id { get; set; }
        public string EntityType { get; set; }
        public string Name { get; set; }
        public int? ActiveYear { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
    }
}