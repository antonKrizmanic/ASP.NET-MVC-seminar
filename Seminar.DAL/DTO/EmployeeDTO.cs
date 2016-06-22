using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar.DAL.DTO
{
    public class EmployeeDTO
    {
        public int? ID { get; set; }
        [Required]
        [Display(Name = "Ime i prezime radnika:")]
        public string Name { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Oib mora imati 11 znakova")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "OIB mora sadrzavati samo brojeve")]
        [Display(Name = "OIB radnika:")]
        public string OIB { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }

        public EmployeeDTO()
        {
            ID = 0;
        }
    }
}
