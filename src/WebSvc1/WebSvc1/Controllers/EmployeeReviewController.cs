using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using WebSvc1.Models;
using WebSvc1.Services;

namespace WebSvc1.Controllers
{
    public class EmployeeReviewController : ApiController
    {
        private readonly EmployeeService employeeService = new EmployeeService();
        private readonly ReviewService reviewService = new ReviewService();

        public async Task<EmployeeReview> Get(int employeeId, string competency)
        {
            var review = (await reviewService.ListReviewsAsync())
                .FirstOrDefault(r => r.EmployeeId == employeeId && r.Competency == competency);

            var empReview = new EmployeeReview
            {
                Employee = (await employeeService.ListEmployeesAsync()).FirstOrDefault(e => e.Id == employeeId),
                Reviews = new List<Review> { review }
            };

            return empReview;
        }

        public async Task<Review> Put(Review review)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<EmployeeReview>> GetByRating(int rating)
        {
            var matchingEmployees = reviewService.ListReviews()
                .Where(r => r.CompetencyRating == rating)
                .Select(r => r.EmployeeId)
                .Distinct().ToList();

            var empReviews = new List<EmployeeReview>();

            var collation = new List<Action>();
            var id = 0;
            for (var i = 0; i < matchingEmployees.Count; i++)
            {
                id = matchingEmployees[i];
                collation.Add(() => CollateReviewForEmployee(id, empReviews));
            }
            Parallel.ForEach(collation, (coll) => { coll.Invoke(); });

            return empReviews;
        }

        private void CollateReviewForEmployee(int employeeId, IList<EmployeeReview> reviews)
        {
            var employee = employeeService.ListEmployees().First(e => e.Id == employeeId);

            reviews.Add(new EmployeeReview
            {
                Employee = employee,
                Reviews = reviewService.ListReviews()
                    .Where(r => r.EmployeeId == employeeId).ToList()
            });
        }
    }
}