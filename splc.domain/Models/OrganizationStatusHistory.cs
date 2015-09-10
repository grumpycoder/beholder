using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace splc.domain.Models
{
    public class OrganizationStatusHistory : StateInfo
    {
        [Key]
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        [Display(Name = "Active Status")]
        public int ActiveStatusId { get; set; }
        [Display(Name = "Active Year")]
        public int ActiveYear { get; set; }
        [Display(Name = "Report Status")]
        public bool ReportStatusFlag { get; set; }

        [Display(Name = "Active Status")]
        public virtual ActiveStatus ActiveStatus { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
