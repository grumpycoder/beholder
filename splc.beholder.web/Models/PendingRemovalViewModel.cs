using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace splc.beholder.web.Models
{
    public class PendingRemovalViewModel
    {
        public int Id { get; set; }
        public string EntityType { get; set; }
        public string Name { get; set; }
        [Display(Name = "Reason for Removal")]
        public string RemovalReason { get; set; }
    }
}