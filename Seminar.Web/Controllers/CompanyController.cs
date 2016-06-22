using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;
using Ninject;
using Seminar.DAL.DTO;
using Seminar.DAL.Repository;
using Seminar.Model;
using Seminar.Web.Models;
using Font = iTextSharp.text.Font;

namespace Seminar.Web.Controllers
{
    [RequireHttps]
    [Authorize]
    [RoutePrefix("Kompanije")]
    public class CompanyController : Controller
    {
        private string _username = System.Web.HttpContext.Current.GetOwinContext().Authentication.User.Identity.GetUserName();
        [Inject]
        public CompanyRepository CompanyRepository { get; set; }
        [Inject]
        public TravelWarrantRepository TravelWarrantRepository { get; set; }
        [Inject]
        public CarRepository CarRepository { get; set; }
        [Inject]
        public EmployeeRepository EmployeeRepository { get; set; }

        [Route("")]
        public ActionResult Index()
        {
            List<Company> model;
            if (User.IsInRole("Admin"))
            {
                model = this.CompanyRepository.GetList(null);
            }
            else
            {
                model = this.CompanyRepository.GetList(_username);
            }
            return View(model);
        }

        [Authorize]
        [Route("Detalji")]
        public ActionResult Details(int id)
        {
            var company = this.CompanyRepository.GetDTO(id);
            FillDropDownMonths();
            return View(company);
        }

        [Authorize]
        [Route("Dodaj")]
        public ActionResult Create()
        {
            var model=new CompanyDTO();
            return View(model);
        }

        
        [Authorize]
        [HttpPost]
        [Route("Dodaj")]
        public ActionResult Create(CompanyDTO model)
        {
            try
            {
                this.CompanyRepository.AddDTO(model, _username);
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
            //var model=new CompanyDTO();
            var model = this.CompanyRepository.GetDTO(id);
            return View(model);
        }

        
        [Authorize]
        [HttpPost]
        [ActionName("Edit")]
        [Route("Uredi")]
        public ActionResult EditPost(CompanyDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.CompanyRepository.UpdateDTO(model, _username);
                    return RedirectToAction("Index");
                }
                
                return View();
            }
            catch
            {
                return View();
            }
        }

        [Authorize (Roles = "Admin")]
        public JsonResult Delete(int id)
        {
            try
            {
                this.CompanyRepository.Delete(id);
                return new JsonResult() { Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch
            {
                return new JsonResult() { Data = "False", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [Authorize]
        public PartialViewResult AddNewCar()
        {
            CarDTO car=new CarDTO();
            return PartialView("_SingleCarEdit",car);
        }

        [Authorize]
        public JsonResult CheckCar(int id)
        {
            if (this.CarRepository.GetTravelWarrantsNumber(id) == 0)
            {
                return new JsonResult() {Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }
            else
            {
                return new JsonResult() {Data = "False", JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }
        }
        [Authorize]
        public PartialViewResult AddNewEmployee()
        {
            EmployeeDTO employee=new EmployeeDTO();
            return PartialView("_SingleEmployeeEdit",employee);
        }

        [Authorize]
        public JsonResult CheckEmployee(int id)
        {
            if (this.EmployeeRepository.GetTravelWarrantsNumber(id)==0)
            {
                return new JsonResult() { Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult() { Data = "False", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public void CreatePdf(FormCollection formData)
        {
            var id =Int32.Parse(formData["id"]);
            var month = Int32.Parse(formData["month"]);
            var travelWarrants = this.TravelWarrantRepository.FindWarrants(id,month);
            var company = this.CompanyRepository.Find(id);
            using (MemoryStream ms = new MemoryStream())
            using (Document document = new Document(PageSize.A4, 25, 25, 30, 30))
            using (PdfWriter writer = PdfWriter.GetInstance(document, ms))
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, false);
                Font times = new Font(bfTimes, 12, Font.BOLD, iTextSharp.text.BaseColor.BLACK);
                BaseFont bfCurier=BaseFont.CreateFont(BaseFont.TIMES_ROMAN,BaseFont.CP1250,false);
                Font curier = new Font(bfCurier, 12, Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                document.Open();
                
                document.Add(new Paragraph("Tvrtka: "+company.Name,curier));
                document.Add(new Paragraph("Adresa: "+company.Address,curier));
                document.Add(new Paragraph("Grad: " + company.City, curier));
                document.Add(new Paragraph("Izvješće o putnim nalozima.",curier));
                document.Add(new Paragraph(" "));
                PdfPTable table=new PdfPTable(8);
                table.WidthPercentage = 100;
                PdfPCell cell;
                
                table.AddCell(new Phrase("Relacija", times));
                table.AddCell(new Phrase("Vozilo", times));
                table.AddCell(new Phrase("Zaposlenik", times));
                table.AddCell(new Phrase("Svrha puta", times));
                table.AddCell(new Phrase("Datum", times));
                table.AddCell(new Phrase("Početna kilometraža", times));
                table.AddCell(new Phrase("Završna kilometraža", times));
                table.AddCell(new Phrase("Prijeđeni kilometri", times));
                foreach (var travelWarrant in travelWarrants)
                {
                    cell = new PdfPCell(new Phrase(travelWarrant.Relation, curier));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(travelWarrant.Car.Name, curier));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(travelWarrant.Employee.Name, curier));
                    table.AddCell(cell);
                    cell =new PdfPCell(new Phrase(travelWarrant.Description, curier));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(travelWarrant.Date.ToShortDateString(), curier));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(travelWarrant.StartKilometer.ToString(), curier));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(travelWarrant.EndKilometer.ToString(), curier));
                    table.AddCell(cell);
                    cell =new PdfPCell(new Phrase(travelWarrant.Kilometer.ToString(), curier));
                    table.AddCell(cell);
                }
                cell=new PdfPCell(new Phrase("Ukupno: ",times));
                cell.Colspan=7;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                
                table.AddCell(cell);
                cell=new PdfPCell(new Phrase(travelWarrants.Sum(p=>p.Kilometer).ToString()));
                table.AddCell(cell);
                document.Add(table);
                document.Close();
                writer.Close();
                ms.Close();
                Response.ContentType = "pdf/application";
                Response.AddHeader("content-disposition", "attachment;filename=Putni nalozi "+company.Name+".pdf");
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            }
        }
        public void FillDropDownMonths()
        {
            
            var selectedList = new List<System.Web.Mvc.SelectListItem>();
            var listItem = new SelectListItem();
            listItem.Text = "Odaberite";
            listItem.Value = "";
            selectedList.Add(listItem);
            listItem = new SelectListItem();
            listItem.Text = "Siječanj";
            listItem.Value = "1";
            selectedList.Add(listItem);
            listItem = new SelectListItem();
            listItem.Text = "Veljača";
            listItem.Value = "2";
            selectedList.Add(listItem);
            listItem = new SelectListItem();
            listItem.Text = "Ožujak";
            listItem.Value = "3";
            selectedList.Add(listItem);
            listItem = new SelectListItem();
            listItem.Text = "Travanj";
            listItem.Value = "4";
            selectedList.Add(listItem);
            listItem = new SelectListItem();
            listItem.Text = "Svibanj";
            listItem.Value = "5";
            selectedList.Add(listItem);
            listItem = new SelectListItem();
            listItem.Text = "Lipanj";
            listItem.Value = "6";
            selectedList.Add(listItem);
            listItem = new SelectListItem();
            listItem.Text = "Srpanj";
            listItem.Value = "7";
            selectedList.Add(listItem);
            listItem = new SelectListItem();
            listItem.Text = "Kolovoz";
            listItem.Value = "8";
            selectedList.Add(listItem);
            listItem = new SelectListItem();
            listItem.Text = "Rujan";
            listItem.Value = "9";
            selectedList.Add(listItem);
            listItem = new SelectListItem();
            listItem.Text = "Listopad";
            listItem.Value = "10";
            selectedList.Add(listItem);
            listItem = new SelectListItem();
            listItem.Text = "Studeni";
            listItem.Value = "11";
            selectedList.Add(listItem);
            listItem = new SelectListItem();
            listItem.Text = "Prosinac";
            listItem.Value = "12";
            selectedList.Add(listItem);

            ViewBag.PossibleCompanies = selectedList;
        }
    }
}
