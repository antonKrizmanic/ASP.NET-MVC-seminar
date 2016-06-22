using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar.Model
{
    public class Car : EntityBase
    {
        [Required]
        [Display(Name = "Proizvođač i model:")]
        public string Name { get; set; }
        [Display(Name = "Godina kupnje:")]
        public int Year { get; set; }
        [Range(1,100)]
        [Display(Name = "Potrošnja goriva:")]
        public decimal FuelConsumption { get; set; }

        [ForeignKey("Company")]
        [Display(Name = "Tvrtka:")]
        public int CompanyID { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<TravelWarrant> TravelWarrants { get; set; } 
    }
}
