using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using WebSvc1.Models;

namespace WebSvc1.Services
{
    public class EmployeeService
    {
        private static readonly IList<Employee> employeesList= LoadEmployees();

        public IList<Employee> ListEmployees()
        {
            if (employeesList == null || employeesList.Count == 0)
            {
                throw new InvalidDataException("List of employees was not loaded.");
            }

            return employeesList;
        }

        public async Task<IList<Employee>> ListEmployeesAsync()
        {
            if (employeesList == null || employeesList.Count == 0)
            {
                throw new InvalidDataException("List of employees was not loaded.");
            }

            return await Task.Run(() => employeesList);
        }

        private static IList<Employee> LoadEmployees()
        {
            List<Employee> loadedEmployees = new List<Employee>();
            try
            {
                const string resourceName = "WebSvc1.Resources.employees.xml";
                var serialiser = new XmlSerializer(typeof(Employee));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    using (var xmlRdr = XmlReader.Create(stream))
                    {
                        while (xmlRdr.Read())
                        {
                            switch (xmlRdr.NodeType)
                            {
                                case XmlNodeType.Element:
                                {
                                    if (xmlRdr.Name == "employee")
                                    {
                                        var emp = serialiser.Deserialize(xmlRdr) as Employee;
                                        loadedEmployees.Add(emp);
                                    }
                                    break;
                                }
                                default:
                                    break;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Could not read file.", ex);
            }

            return loadedEmployees;
        }
    }
}