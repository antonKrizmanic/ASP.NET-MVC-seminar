using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Seminar.Model;

namespace Seminar.Web.Models
{
    public class CarTravel
    {
        public Car Car { get; set; }
        public List<TravelWarrant> TravelWarrants { get; set; }
    }

    public class EmployeeTravel
    {
        public Employee Employee { get; set; }
        public List<TravelWarrant> TravelWarrants{get;set;}
    }

    public class CompanyTravel
    {
        public Company Company { get; set; }
        public List<TravelWarrant> TravelWarrants { get; set; } 
    }
}