using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar.DAL.DTO
{
    public class TravelWarrantDTO
    {
        public int ID { get; set; }
        public string Relation { get; set; }
        public int Kilometer { get; set; }
        public int StartKilometer { get; set; }
        public int EndKilometer { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int CarID { get; set; }
        public string CarName { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
    }
}
