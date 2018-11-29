using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Web.Http;
using System.Web.Mvc;
using WebSvc1.Models;
using System.Xml.Serialization;
using WebSvc1.Services;

namespace WebSvc1.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly EmployeeService service = new EmployeeService();

        // GET: api/Employee
        public async Task<IEnumerable<Employee>> Get()
        {
            return await service.ListEmployeesAsync();
        }

        // GET: api/Employee/5
        public async Task<Employee> Get(int id)
        {
            var emp = (await service.ListEmployeesAsync()).FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return emp;
        }

    }
}
