using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Seminar.DAL.DTO;
using Seminar.DAL.Repository;
using Seminar.Model;

namespace Seminar.Web.Areas.Api.Controllers
{
    public class CompanyController : ApiController
    {
        private CompanyRepository _companyRepository { get; set; }
        public CompanyController(CompanyRepository companyRepository)
        {
            this._companyRepository = companyRepository;
        }
        public List<CompanyDTO> Get()
        {
            return this._companyRepository.GetAllDTO();
        }

        public CompanyDTO Get(int id)
        {
            return this._companyRepository.GetCompany(id);
        }

        [Route("api/Company/Pretraga/{q}")]
        public List<CompanyDTO> Get(string q)
        {
            return this._companyRepository.GetAllDTO(q);
        }

        [Route("api/Company/add")]
        public IHttpActionResult Post([FromBody]CompanyDTO value)
        {
            var company=new Company();
            company.Name = value.Name;
            company.Email = value.Email;
            company.Address = value.Address;
            this._companyRepository.Add(company,"admin");

            return this.Ok();
        } 
    }
}
