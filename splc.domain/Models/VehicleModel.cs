using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace splc.domain.Models
{
    public partial class VehicleModel : StateInfo
    {
        [Key]
        public int Id { get; set; }
        public int VehicleMakeId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Sort Order")]
        public int? SortOrder { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
        [Display(Name = "Make")]
        public virtual VehicleMake VehicleMake { get; set; }
    }
}
