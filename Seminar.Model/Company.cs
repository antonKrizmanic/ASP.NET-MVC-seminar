using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar.Model
{
    public class Company : EntityBase
    {
        
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Naziv tvrtke:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email je obvezno unijeti (nesto@nesto.com)")]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Oib mora imati 11 znakova")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "OIB mora sadrzavati samo brojeve")]
        [Display(Name = "OIB:")]
        public string OIB { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5,ErrorMessage = "Adresa treba sadrzavati izmedu 5 i 100 znakova")]
        [Display(Name = "Adresa:")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Grad:")]
        public string City { get; set; }
        public virtual ICollection<Car> Cars { get; set; } 
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<TravelWarrant> TravelWarrants { get; set; }  
    }
}
