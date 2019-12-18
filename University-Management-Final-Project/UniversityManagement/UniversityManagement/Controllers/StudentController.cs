using System.Web.Mvc;
using UniversityManagement.DataContext;
using UniversityManagement.Models;
using Vereyon.Web;

namespace UniversityManagement.Controllers
{
    public class StudentController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        public ActionResult Register()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code");
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "StudentId,Name,RegNo,Email,ContactNo,Date,Address,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                FlashMessage.Confirmation("Department saved successfully");
                return RedirectToAction("Register");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", student.DepartmentId);
            return View(student);
        }

    }
}
