using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagement.DataContext;
using UniversityManagement.Models;
using Vereyon.Web;

namespace UniversityManagement.Controllers
{
    public class CourseAssignController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        public ActionResult Save()
        {
            ViewBag.Departments = new SelectList(db.Departments, "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Save([Bind(Include = "CourseAssignId,DepartmentId,TeacherId,CreditTaken,CreditLeft,CourseID,Name,Credit")] CourseAssign courseassign)
        {
            if (ModelState.IsValid)
            {
                db.CourseAssigns.Add(courseassign);
                db.SaveChanges();
                FlashMessage.Confirmation("Course assigned successfull");

                return RedirectToAction("Save");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseId", "CourseCode", courseassign.CourseID);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", courseassign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", courseassign.TeacherId);
            return View(courseassign);
        }

        public JsonResult GetTeacherByDeptId(int deptId)
        {
            var teachers = db.Teachers.Where(m => m.DepartmentId == deptId).ToList();
            return Json(teachers);
        }

        public JsonResult GetCourseByDeptId(int deptId)
        {
            var courses = db.Courses.Where(m => m.DepartmentId == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeacherById(int TeacherId)
        {
            var teacher = db.Teachers.FirstOrDefault(m => m.TeacherId == TeacherId);
            return Json(teacher, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseById(int courseId)
        {
            Course aCourse = db.Courses.FirstOrDefault(m => m.CourseId == courseId);
            return Json(aCourse, JsonRequestBehavior.AllowGet);
        }
    }
}