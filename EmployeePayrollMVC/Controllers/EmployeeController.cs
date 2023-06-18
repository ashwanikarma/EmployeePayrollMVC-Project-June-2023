using BusinessLayer.Service;
using CommonLayer.Model;
using EmployeePayrollMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Service;
using System.Collections.Generic;
using System.Linq;

namespace EmployeePayrollMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeBL iemployeeBL;
        public EmployeeController(IEmployeeBL iemployeeBL)
        {
            this.iemployeeBL = iemployeeBL;
        }


        [HttpGet]
        public IActionResult AddEmployee()
        {
            var role = HttpContext.Session.GetInt32("roleID");
            if (role == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee([Bind] EmployeeModel employee)
        {
            var role = HttpContext.Session.GetInt32("roleID");
            if (role == 1)
            {
                if (ModelState.IsValid)
                {
                    iemployeeBL.AddEmployee(employee);
                    return RedirectToAction("Index", "Admin");
                }
                return View(employee);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }


        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var role = HttpContext.Session.GetInt32("roleID");
            if (role == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }
                EmployeeModel employee = iemployeeBL.GetEmployeeById(id);

                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] EmployeeModel employee)
        {
            var role = HttpContext.Session.GetInt32("roleID");
            if (role == 1)
            {
                if (id != employee.EmpId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    iemployeeBL.UpdateEmployee(employee);
                    return RedirectToAction("Index");
                }
                return View(employee);

            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeModel employee = iemployeeBL.GetEmployeeById(id);
            Notification notification = new Notification
            {
                Message = "You are logged in successfully!!!",
                Type = "success"
            };
            string userName = HttpContext.Session.GetString("Name");
            ViewBag.Name = userName;

            if (employee == null)
            {
                return NotFound();
            }
            TempData["Notification"] = notification;

            return View(employee);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var role = HttpContext.Session.GetInt32("roleID");
            if (role == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }
                EmployeeModel employee = iemployeeBL.GetEmployeeById(id);

                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);

            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var role = HttpContext.Session.GetInt32("roleID");
            if (role == 1)
            {
                iemployeeBL.DeleteEmployeeById(id);
                return RedirectToAction("Index", "Admin");

            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }
    }
    }
