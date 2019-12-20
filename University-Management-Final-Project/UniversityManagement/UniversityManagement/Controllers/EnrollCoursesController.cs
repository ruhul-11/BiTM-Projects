using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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

        // GET: EnrollCourses
        public ActionResult Index()
        {
            var enrollCourses = db.EnrollCourses.Include(e => e.Course).Include(e => e.Student);
            return View(enrollCourses.ToList());
        }

        // GET: EnrollCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
            if (enrollCourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollCourse);
        }

        // GET: EnrollCourses/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name");
            return View();
        }

        // POST: EnrollCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollCourseId,StudentId,CourseId")] EnrollCourse enrollCourse)
        {
            if (ModelState.IsValid)
            {
                db.EnrollCourses.Add(enrollCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", enrollCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", enrollCourse.StudentId);
            return View(enrollCourse);
        }

        // GET: EnrollCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
            if (enrollCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", enrollCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", enrollCourse.StudentId);
            return View(enrollCourse);
        }

        // POST: EnrollCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollCourseId,StudentId,CourseId")] EnrollCourse enrollCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Code", enrollCourse.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", enrollCourse.StudentId);
            return View(enrollCourse);
        }

        // GET: EnrollCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
            if (enrollCourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollCourse);
        }

        // POST: EnrollCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
            db.EnrollCourses.Remove(enrollCourse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
