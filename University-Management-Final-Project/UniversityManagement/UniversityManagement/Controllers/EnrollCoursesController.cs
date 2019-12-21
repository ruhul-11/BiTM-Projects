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

namespace UniversityManagement.Controllers
{
    public class EnrollCoursesController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();



        public ActionResult Enroll()
        {
            //ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            ViewBag.Registrations = new SelectList(db.Students, "StudentId", "RegNo");
            return View();
        }

        [HttpPost]
        public ActionResult Enroll(EnrollCourse enrollCourse)
        {
            if (ModelState.IsValid)
            {
                db.EnrollCourses.Add(enrollCourse);
                db.SaveChanges();
                return RedirectToAction("Enroll");
            }

            //ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", enrollCourse.CourseId);
            ViewBag.Registrations = new SelectList(db.Students, "StudentId", "RegNo", enrollCourse.StudentId);
            //return View(enrollCourse);
            return RedirectToAction("Enroll");
        }


        public JsonResult GetStudentById(int studentId)
        {
            var students = db.Students.Where(m => m.StudentId == studentId).First();
            return Json(students, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoursesbyDeptId(int deptId)
        {
            var courses = db.Courses.Where(m => m.Department.DepartmentId == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsAlreadyEnrolled(string regNo, int courseId)
        {
            var enrollCourses = db.EnrollCourses.Where(m => m.Student.RegNo == regNo && m.Course.CourseId == courseId);

            if (enrollCourses.Count() == 0)
            {
                return Json(false);
            }
            return Json(true);
        }

        public JsonResult EnrollStudentToCourse(EnrollCourse enrollCourse)
        {

            var enrollCourses = db.EnrollCourses.Where(m => m.Student.RegNo == enrollCourse.Student.RegNo && m.CourseId == enrollCourse.CourseId).ToList();

            if (enrollCourses.Count() == 1)
            {
                var id = enrollCourses[0].EnrollCourseId;
                var date = enrollCourses[0].EnrollDate;
                enrollCourse.EnrollCourseId = id;
                enrollCourse.EnrollDate = date;
                db.EnrollCourses.AddOrUpdate(enrollCourse);
            }
            else
            {
                db.EnrollCourses.Add(enrollCourse);
            }

            db.SaveChanges();
            return Json(true);
        }



        public ActionResult SaveResult()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name");
            ViewBag.GradeList = new SelectList(db.Grades, "GradeId", "Name");

            //ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            //ViewBag.StudentList = db.Students.ToList();
            //ViewBag.GradeList = db.Grades.ToList();

            return View();
        }

        public JsonResult GetCoursesbyRegNo(string regNo)
        {
            var courses = db.EnrollCourses.Where(m => m.Student.RegNo == regNo).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ViewResult()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name");
            ViewBag.EnrollCourses = db.EnrollCourses.ToList();
            //ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            //ViewBag.StudentList = db.Students.ToList();
            return View();
        }







        //// GET: EnrollCourses
        //public ActionResult Index()
        //{
        //    var enrollCourses = db.EnrollCourses.Include(e => e.Course).Include(e => e.Student);
        //    return View(enrollCourses.ToList());
        //}

        //// GET: EnrollCourses/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
        //    if (enrollCourse == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(enrollCourse);
        //}




        //// GET: EnrollCourses/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
        //    if (enrollCourse == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", enrollCourse.CourseId);
        //    ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", enrollCourse.StudentId);
        //    return View(enrollCourse);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "EnrollCourseId,StudentId,CourseId")] EnrollCourse enrollCourse)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(enrollCourse).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", enrollCourse.CourseId);
        //    ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", enrollCourse.StudentId);
        //    return View(enrollCourse);
        //}

        //// GET: EnrollCourses/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
        //    if (enrollCourse == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(enrollCourse);
        //}

        //// POST: EnrollCourses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
        //    db.EnrollCourses.Remove(enrollCourse);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
