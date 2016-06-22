using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar.DAL.DTO
{
    public class CompanyDTO
    {
        public int ID { get; set; }
        [Display(Name = "Naziv tvrtke:")]
        public string Name { get; set; }
        [Display(Name = "E-mail:")]
        public string Email { get; set; }
        [Display(Name = "Adresa:")]
        public string Address { get; set; }
        [Display(Name = "Grad:")]
        public string City { get; set; }
        [Display(Name = "OIB:")]
        public string OIB { get; set; }
        public List<CarDTO> Cars { get; set; }
        public List<EmployeeDTO> Employers { get; set; } 
        public List<TravelWarrantDTO> TravelWarrants { get; set; }
        public int TotalKilometer { get; set; }
        public CompanyDTO()
        {
            Cars=new List<CarDTO>();
            Employers=new List<EmployeeDTO>();
            TravelWarrants=new List<TravelWarrantDTO>();
        }
    }
}
