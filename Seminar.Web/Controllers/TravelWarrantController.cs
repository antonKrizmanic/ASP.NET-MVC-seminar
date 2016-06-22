using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Ninject;
using Seminar.DAL.Repository;
using Seminar.Model;

namespace Seminar.Web.Controllers
{
    [RequireHttps]
    [Authorize]
    public class TravelWarrantController : Controller
    {
        private string _username = System.Web.HttpContext.Current.GetOwinContext().Authentication.User.Identity.GetUserName();
        [Inject]
        public TravelWarrantRepository TravelWarrantRepository { get; set; }
        [Inject]
        public CompanyRepository CompanyRepository { get; set; }
        [Inject]
        public CarRepository CarRepository { get; set; }
        [Inject]
        public EmployeeRepository EmployeeRepository { get; set; }
        // GET: TravelWarrant
        public ActionResult Index()
        {
            List<TravelWarrant> model;
            if (User.IsInRole("Admin"))
            {
                model = this.TravelWarrantRepository.GetList(null);
            }
            else
            {
                model = this.TravelWarrantRepository.GetList(_username);
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Create()
        {
            FillCars();
            FillCompanies();
            FillEmployees();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(TravelWarrant model)
        {
            if (ModelState.IsValid)
            {
                this.TravelWarrantRepository.Add(model, _username);
                return RedirectToAction("Index");
            }

            FillCars();
            FillCompanies();
            FillEmployees();
            return View();
            
            
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            FillCars();
            FillCompanies();
            FillEmployees();
            var model = this.TravelWarrantRepository.Find(id);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            var model = this.TravelWarrantRepository.Find(id);
            var didUpdate = this.TryUpdateModel(model);
            if (didUpdate && ModelState.IsValid)
            {
                this.TravelWarrantRepository.Update(model, _username);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize]
        public ActionResult Details(int? id=null)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                var model = this.TravelWarrantRepository.Find(id.Value);
                return View(model);
            }
        }
        [Authorize (Roles = "Admin")]
        public JsonResult Delete(int id)
        {

            this.TravelWarrantRepository.Delete(id);
            return new JsonResult() { Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult SelectAjaxCar(int id)
        {

            var possibleCars = this.CarRepository.GetByCompany(id);
            var selectList = new List<System.Web.Mvc.SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = "Odaberite";
            listItem.Value = "";
            selectList.Add(listItem);

            foreach (var possibleCar in possibleCars)
            {
                listItem = new SelectListItem();
                listItem.Text = possibleCar.Name;
                listItem.Value = possibleCar.ID.ToString();
                listItem.Selected = false;
                selectList.Add(listItem);
            }
            return new JsonResult() { Data = selectList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult SelectAjaxEmployer(int id)
        {
            var possibleEmployees = this.EmployeeRepository.GetByCompany(id);
            var selectList = new List<System.Web.Mvc.SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = "Odaberite";
            listItem.Value = "";
            selectList.Add(listItem);

            foreach (var possibleEmployee in possibleEmployees)
            {
                listItem = new SelectListItem();
                listItem.Text = possibleEmployee.Name;
                listItem.Value = possibleEmployee.ID.ToString();
                listItem.Selected = false;
                selectList.Add(listItem);
            }
            return new JsonResult() { Data = selectList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public void FillCompanies()
        {
            List<Company> possibleCompanies;
            if (User.IsInRole("Admin"))
            {
                possibleCompanies = this.CompanyRepository.GetList(null);
            }
            else
            {
                possibleCompanies = this.CompanyRepository.GetList(_username);
            }
            var selectList=new List<System.Web.Mvc.SelectListItem>();

            var listItem=new SelectListItem();
            listItem.Text = "Odaberite";
            listItem.Value = "";
            selectList.Add(listItem);

            foreach (var possibleCompany in possibleCompanies)
            {
                listItem=new SelectListItem();
                listItem.Text = possibleCompany.Name;
                listItem.Value = possibleCompany.ID.ToString();
                listItem.Selected = false;
                selectList.Add(listItem);
            }
            ViewBag.PossibleCompanies = selectList;
        }

        public void FillCars()
        {
            List<Car> possibleCars;
            if (User.IsInRole("Admin"))
            {
                possibleCars = this.CarRepository.GetList(null);
            }
            else
            {
                possibleCars = this.CarRepository.GetList(_username);
            }
            var selectList = new List<System.Web.Mvc.SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = "Odaberite";
            listItem.Value = "";
            selectList.Add(listItem);

            foreach (var possibleCar in possibleCars)
            {
                listItem = new SelectListItem();
                listItem.Text = possibleCar.Name;
                listItem.Value = possibleCar.ID.ToString();
                listItem.Selected = false;
                selectList.Add(listItem);
            }
            ViewBag.PossibleCars = selectList;
        }

        public void FillEmployees()
        {
            
            List<Employee> possibleEmployees;
            if (User.IsInRole("Admin"))
            {
                possibleEmployees = this.EmployeeRepository.GetList(null);
            }
            else
            {
                possibleEmployees = this.EmployeeRepository.GetList(_username);
            }
            var selectList = new List<System.Web.Mvc.SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = "Odaberite";
            listItem.Value = "";
            selectList.Add(listItem);

            foreach (var possibleEmployee in possibleEmployees)
            {
                listItem = new SelectListItem();
                listItem.Text = possibleEmployee.Name;
                listItem.Value = possibleEmployee.ID.ToString();
                listItem.Selected = false;
                selectList.Add(listItem);
            }
            ViewBag.PossibleEmployees = selectList;
        }
    }
}