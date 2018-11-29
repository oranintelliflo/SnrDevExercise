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
    public class RatingController : Controller
    {
        // GET: Bonus
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> List(int id)
        {
            var emp = await LoadReviews(id);
            return View(emp);
        }

        public async Task<ActionResult> Edit(int id, string competency)
        {
            var rating = await GetRating(id, competency);
            return View(rating);
        }

        private async Task<PerformanceViewModel> GetRating(int employeeId, string competency)
        {
            PerformanceViewModel rating = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57652/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"api/EmployeeReview?employeeId={employeeId}&competency={Server.UrlEncode(competency)}");
            if (response.IsSuccessStatusCode)
            {
                string contentText = await response.Content.ReadAsStringAsync();

                rating = JsonConvert.DeserializeObject<PerformanceViewModel>(contentText);
            }

            return rating;
        }

        private async Task<List<PerformanceViewModel>> LoadReviews(int ratingCode)
        {
            var employees = new List<PerformanceViewModel>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57652/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"api/EmployeeReview?rating={ratingCode}");
            if (response.IsSuccessStatusCode)
            {
                string contentText = await response.Content.ReadAsStringAsync();

                employees = JsonConvert.DeserializeObject<List<PerformanceViewModel>>(contentText);
            }

            return employees;
        }
    }
}