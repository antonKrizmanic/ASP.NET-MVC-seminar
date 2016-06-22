using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar.Model
{
    public class Employee : EntityBase
    {
        [Required]
        [Display(Name = "Ime i prezime radnika:")]
        public string Name { get; set; }
        [Required]
        [StringLength(11,MinimumLength = 11,ErrorMessage = "Oib mora imati 11 znakova")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "OIB mora sadrzavati samo brojeve")]
        [Display(Name = "OIB:")]
        public string OIB { get; set; }
        
        [ForeignKey("Company")]
        [Display(Name = "Tvrtka:")]
        public int CompanyID { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<TravelWarrant> TravelWarrants { get; set; }
    }
}
