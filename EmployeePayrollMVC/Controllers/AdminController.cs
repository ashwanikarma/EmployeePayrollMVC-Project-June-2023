using BusinessLayer.Service;
using CommonLayer.Model;
using EmployeePayrollMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeePayrollMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEmployeeBL iemployeeBL;
        public AdminController(IEmployeeBL iemployeeBL)
        {
            this.iemployeeBL = iemployeeBL;
        }
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetInt32("roleID");
            Notification notification = new Notification
            {
                Message = "You are logged in successfully!!!",
                Type = "success"
            };
            string userName = HttpContext.Session.GetString("Name");
            ViewBag.Name = userName;
            if (role == 1)
            {
                TempData["Notification"] = notification;
                List<EmployeeModel> lstEmployee = new List<EmployeeModel>();
                lstEmployee = iemployeeBL.GetAllEmployees().ToList();
                return View(lstEmployee);
            }
            else
            {
                return RedirectToAction("Login","Admin");
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {

                var result = iemployeeBL.GetUser(loginModel);
                if (result.email!=null)
                {

                    HttpContext.Session.SetInt32("EmployeeID", result.employeeID);
                    HttpContext.Session.SetString("Name", result.Name);
                    HttpContext.Session.SetString("email", result.email);
                    HttpContext.Session.SetString("u_password", result.password);
                    HttpContext.Session.SetInt32("roleID", result.roleID);
                    if (result.roleID.Equals(1))
                    {
                        
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (result.roleID.Equals(2))
                    {
                        return RedirectToAction("Details", "Employee", new { id = result.employeeID });
                    }
                }
                else
                {
                    ViewData["message"] = "Invalid Details Login Failed!!!";
                }
            }
            return View();
        }
        public IActionResult Logout()
        {

            HttpContext.Session.Remove("EmployeeID");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("u_password");
            HttpContext.Session.Remove("roleID");
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}
