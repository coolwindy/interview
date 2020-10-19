using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeMVC.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Formatting;

namespace EmployeeMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configure;
        private string apiBaseUrl;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configure = configuration;

            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }

        public IActionResult Index()
        {
            IEnumerable<EmployeeViewModel> employees = null;

            using(var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                HttpResponseMessage response = client.GetAsync("employees").Result;

                if(response.IsSuccessStatusCode)
                {
                    var employeeJsonString = response.Content.ReadAsStringAsync();

                    employees = JsonConvert.DeserializeObject<IEnumerable<EmployeeViewModel>>(employeeJsonString.Result);
                }
                else
                {
                    employees = Enumerable.Empty<EmployeeViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }

            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                var postTask = client.PostAsJsonAsync<EmployeeViewModel>("employees", employee);
                postTask.Wait();

                var result = postTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server error");

            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            EmployeeViewModel employee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                HttpResponseMessage response = client.GetAsync("employees/" + id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    var employeeJsonString = response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<EmployeeViewModel>(employeeJsonString.Result);
                }
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<EmployeeViewModel>("employees", employee);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("employees/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
