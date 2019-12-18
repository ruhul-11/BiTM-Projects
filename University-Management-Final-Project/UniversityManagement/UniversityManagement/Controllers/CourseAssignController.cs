using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

            return RedirectToAction("Save");
        }

        public JsonResult GetTeacherByDeptId(int deptId)
        {
            var teachers = db.Teachers.Where(m => m.DepartmentId == deptId).ToList();
            return Json(teachers);
        }

        public JsonResult GetCourseByDeptId(int deptId)
        {
            var courses = db.Courses.Where(m => m.DepartmentId == deptId).ToList();
            return Json(courses);
        }

        public JsonResult GetTeacherById(int TeacherId)
        {
            var teacher = db.Teachers.FirstOrDefault(m => m.TeacherId == TeacherId);
            return Json(teacher);
        }

        public JsonResult GetCourseById(int courseId)
        {
            Course aCourse = db.Courses.FirstOrDefault(m => m.CourseId == courseId);
            return Json(aCourse);
        }

        //Course Statistics
        public ActionResult ViewCourseStatistics()
        {
            ViewBag.Departments = new SelectList(db.Departments, "DepartmentId", "Name");
            return View();
        }

        public JsonResult ShowCourseStatistics(int deptId)
        {
            var courses = db.Courses.Where(m => m.DepartmentId == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveCourseAssign(CourseAssign courseAssign)
        {
            var checkAssignedCourses =
                db.CourseAssigns.Where(m => m.CourseId == courseAssign.CourseId && m.Course.CourseStatus == true)
                    .ToList();

            if (checkAssignedCourses.Count > 0)
                return Json(false);

            else
            {
                db.CourseAssigns.Add(courseAssign);
                db.SaveChanges();

                var teacher = db.Teachers.FirstOrDefault(m => m.TeacherId == courseAssign.TeacherId);

                if (teacher != null)
                {
                    teacher.CreditLeft = courseAssign.CreditLeft;
                    db.Teachers.AddOrUpdate(teacher);
                    db.SaveChanges();

                    var course = db.Courses.FirstOrDefault(m => m.CourseId == courseAssign.CourseId);

                    if (course != null)
                    {
                        course.CourseStatus = true;
                        course.CourseAssignTo = teacher.Name;
                        db.Courses.AddOrUpdate(course);
                        db.SaveChanges();

                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                else
                {
                    return Json(false);
                }
            }
        }
    }
}