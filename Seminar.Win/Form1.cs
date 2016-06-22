using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seminar.DAL.DTO;

namespace Seminar.Win
{
    public partial class Form1 : Form
    {
        private const string BaseUrl = "http://localhost:51692/";
        public Form1()
        {
            InitializeComponent();
        }

        private async void bAddCompany_Click(object sender, EventArgs e)
        {
            var dto=new CompanyDTO();
            dto.Name = txtCompanyName.Text;
            dto.Address = txtCompanyAddress.Text;
            dto.Email = txtCompanyMail.Text;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response =
                    await client.PostAsJsonAsync(BaseUrl + "api/company/add", dto);
                response.EnsureSuccessStatusCode();
            }

        }
    }
}
