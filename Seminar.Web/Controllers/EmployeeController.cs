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
    [RoutePrefix("Zaposlenici")]
    public class EmployeeController : Controller
    {
        private string _username = System.Web.HttpContext.Current.GetOwinContext().Authentication.User.Identity.GetUserName();
        [Inject]
        public EmployeeRepository EmployeeRepository { get; set; }
        [Inject]
        public CompanyRepository CompanyRepository { get; set; }
        
        [Route("")]
        public ActionResult Index()
        {
            List<Employee> model;
            if (User.IsInRole("Admin"))
            {
                model = this.EmployeeRepository.GetList(null);
            }
            else
            {
                model = this.EmployeeRepository.GetList(_username);
            }
            return View(model);
        }
        [Authorize]
        [Route("Dodaj")]
        public ActionResult Create()
        {
            FillDropDownCompany();
            return View();
        }
        [Authorize]
        [HttpPost]
        [Route("Dodaj")]
        public ActionResult Create(Employee model)
        {
            try
            {
                this.EmployeeRepository.Add(model, _username);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize]
        [Route("Uredi")]
        public ActionResult Edit(int id)
        {
            FillDropDownCompany();
            var model = this.EmployeeRepository.Find(id);
            return View(model);
        }
        [Authorize]
        [HttpPost]
        [Route("Uredi")]
        [ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            var model=this.EmployeeRepository.Find(id);
            var didUpdate = TryUpdateModel(model);
            if (didUpdate && ModelState.IsValid)
            {
                this.EmployeeRepository.Update(model, _username);
                return RedirectToAction("Index");
            }
            this.FillDropDownCompany();
            return View();
        }
        [Authorize]
        [Route("Detalji")]
        public ActionResult Details(int? id=null)
        {
            if (id == null)
            {
                return View();
            }
            else
            {
                var model = this.EmployeeRepository.Find(id.Value);
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        public JsonResult Delete(int id)
        {
            try
            {
                this.EmployeeRepository.Delete(id);
                return new JsonResult() {Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }
            catch
            {
                return new JsonResult() {Data="False", JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }
        }
        public void FillDropDownCompany()
        {
            List<Company> possibleChoise;
            if (User.IsInRole("Admin"))
            {
                possibleChoise = this.CompanyRepository.GetList(null);
            }
            else
            {
                possibleChoise = this.CompanyRepository.GetList(_username);
            }
            var selectedList=new List<System.Web.Mvc.SelectListItem>();
            
            var listItem=new SelectListItem();
            listItem.Text = "Odaberite";
            listItem.Value = "";
            selectedList.Add(listItem);
            foreach (var company in possibleChoise)
            {
                listItem=new SelectListItem();
                listItem.Text = company.Name;
                listItem.Value = company.ID.ToString();
                listItem.Selected = false;
                selectedList.Add(listItem);
            }
            ViewBag.PossibleCompanies= selectedList;
        }
    }
}