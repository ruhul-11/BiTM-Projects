using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagement.DAL;
using UniversityManagement.Models;

namespace UniversityManagement.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentGateway gateway = new DepartmentGateway();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Department()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Department(Department department)
        {
            string msg = gateway.SaveDepartment(department);

            return View();
        }

        public ActionResult GetAllDepartment()
        {
            var departments = gateway.GetAllDepartment();

            return View(departments);
        }
    }
}