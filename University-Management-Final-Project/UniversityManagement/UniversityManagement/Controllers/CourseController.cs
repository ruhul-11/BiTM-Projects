using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagement.DataContext;
using UniversityManagement.Models;
using Vereyon.Web;

namespace UniversityManagement.Controllers
{
    public class CourseController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        public ActionResult SaveCourse()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "Name");
            return View();
        }

        
        [HttpPost]
        public ActionResult SaveCourse([Bind(Include = "CourseId,CourseCode,CourseName,CourseCredit,CourseDescription,CourseAssignTo,CourseStatus,DepartmentId,SemesterId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                FlashMessage.Confirmation("Course Information saved successfully");
                return RedirectToAction("Save");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "Name", course.SemesterId);
            return View(course);
        }


        public JsonResult IsCodeUnique(string Code)
        {
            return Json(!db.Courses.Any(m => m.Code == Code), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsNameUnique(string Name)
        {
            return Json(!db.Courses.Any(course => course.Name == Name), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnassignCourses()
        {
            return View();
        }


        public JsonResult UnassignAllCourses(bool decision)
        {
            var courses = db.Courses.Where(m => m.Status == true).ToList();
            if (courses.Count == 0)
            {
                return Json(false);
            }
            else
            {
                foreach (var course in courses)
                {
                    course.Status = false;
                    course.AssignTo = "";
                    db.Courses.AddOrUpdate(course);
                    db.SaveChanges();
                }
                return Json(true);

            }
        }

    }
}
