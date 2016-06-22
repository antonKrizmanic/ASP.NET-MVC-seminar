using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foolproof;

namespace Seminar.Model
{
    public class TravelWarrant:EntityBase
    {
        [Required]
        [Display(Name = "Relacija:")]
        public string Relation { get; set; }
        [Required]
        [Display(Name = "Početna kilometraža:")]
        public int StartKilometer { get; set; }
        [Required]
        [Display(Name = "Kilometraža na kraju:")]
        [GreaterThan("StartKilometer",ErrorMessage = "Mora biti veci od pocetne!")]
        public int EndKilometer { get; set; }
        [Required]
        public int Kilometer { get; set; }
        [Required]
        [Display(Name = "Datum:")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Svrha puta")]
        public string Description { get; set; }
        [ForeignKey("Car")]
        [Display(Name = "Automobil:")]
        public int CarID { get; set; }
        public virtual Car Car { get; set; } 
        [ForeignKey("Employee")]
        [Display(Name = "Radnik:")]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        [ForeignKey("Company")]
        [Display(Name = "Tvrtka:")]
        public int CompanyID { get; set; }
        public virtual Company Company { get; set; }
    }
}
