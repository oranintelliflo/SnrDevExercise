using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebApp1.DataContracts;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Bonus
        public async Task<ActionResult> Index()
        {
            List<EmployeeViewModel> employees = await LoadEmployees();
            return View(employees.OrderBy(e => e.Name).ToList());
        }

        public async Task<ActionResult> Detail(int id)
        {
            var emp = await GetEmployee(id);
            return View(emp);
        }

        private async Task<List<EmployeeViewModel>> LoadEmployees()
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57652/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"api/Employee");
            if (response.IsSuccessStatusCode)
            {
                string contentText = await response.Content.ReadAsStringAsync();

                employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(contentText);
            }

            return employees;
        }

        private async Task<EmployeeViewModel> GetEmployee(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57652/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"api/Employee/{id}");
            if (response.IsSuccessStatusCode)
            {
                string contentText = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<EmployeeViewModel>(contentText);
            }

            return null;
        }


        private async Task<List<Employee>> GetEmployees()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57652/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"api/employee");
            if (response.IsSuccessStatusCode)
            {
                string employeeData = await response.Content.ReadAsStringAsync();
                List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(employeeData);

                return employees;
            }

            return new List<Employee>();
        }
    }
}