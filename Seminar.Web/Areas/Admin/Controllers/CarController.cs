using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Ninject;
using Seminar.DAL.Repository;
using Seminar.Model;
using Seminar.Web.Models;

namespace Seminar.Web.Areas.Admin.Controllers
{
    [RequireHttps]
    [Authorize]
    public class CarController : Controller
    {
        private string _username = System.Web.HttpContext.Current.GetOwinContext().Authentication.User.Identity.GetUserName();
        [Inject]
        public CarRepository CarRepository { get; set; }
        [Inject]
        public CompanyRepository CompanyRepository { get; set; }
        
        public ActionResult Index()
        {
            List<Car> model;
            if (User.IsInRole("Admin"))
            {
                model = this.CarRepository.GetList(null);
            }
            else
            {
                model = this.CarRepository.GetList(_username);
            }
            
            return View(model);
        }
        [Authorize]
        public ActionResult Create()
        {
            FillDropDownCompany();
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(Car model)
        {
            try
            {
                this.CarRepository.Add(model, _username);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var model = this.CarRepository.Find(id);
            FillDropDownCompany();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            var model = this.CarRepository.Find(id);
            var didUpdate = TryUpdateModel(model);
            if (didUpdate && ModelState.IsValid)
            {
                this.CarRepository.Update(model, _username);
                return RedirectToAction("Index");
            }
            FillDropDownCompany();
            return View();
        }
        [Authorize]
        public ActionResult Details(int id)
        {
            var car = this.CarRepository.Find(id);
            var travelWarrants = this.CarRepository.GetTravelWarrants(id);

            var model=new CarTravel {Car=car,TravelWarrants = travelWarrants};
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public JsonResult Delete(int id)
        {

            try
            {
                this.CarRepository.Delete(id);
                return new JsonResult() { Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch 
            {
                return new JsonResult() { Data = "False", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            
        }
        public void FillDropDownCompany()
        {
            List<Company> possible;
            if (User.IsInRole("Admin"))
            {
                possible = this.CompanyRepository.GetList(null);
            }
            else
            {
                possible = this.CompanyRepository.GetList(_username);
            }
            var selectList=new List<System.Web.Mvc.SelectListItem>();

            var selectItem=new SelectListItem();
            selectItem.Text = "Odaberite";
            selectItem.Value = "";
            selectList.Add(selectItem);
            foreach (var company in possible)
            {
                selectItem=new SelectListItem();
                selectItem.Text = company.Name;
                selectItem.Value = company.ID.ToString();
                selectItem.Selected = false;
                selectList.Add(selectItem);
            }
            ViewBag.PossibleCompanies = selectList;
        }
    }
}