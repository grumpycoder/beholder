using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace splc.beholder.web.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Summary { get; set; }
        public string EventDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string WebIncidentType { get; set; }
        public string ApprovalStatus { get; set; }
    }
}