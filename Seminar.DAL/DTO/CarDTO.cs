using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar.DAL.DTO
{
    public class CarDTO
    {

        public int? ID { get; set; }
        [Display(Name = "Proizvođač i model:")]
        public string Name { get; set; }

        [Display(Name = "Godina kupnje:")]
        public int Year { get; set; }

        [Display(Name = "Potrošnja goriva:")]
        public decimal FuelConsumption { get; set; }

        public int CompanyID { get; set; }
        public string CompanyName { get; set; }

        public CarDTO()
        {
            ID = 0;
        }
    }
}
