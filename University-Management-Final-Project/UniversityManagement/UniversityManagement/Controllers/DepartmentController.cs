using System.Linq;
using System.Web.Mvc;
using UniversityManagement.DataContext;
using UniversityManagement.Models;
using Vereyon.Web;

namespace UniversityManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        public ActionResult SaveDepartment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveDepartment(Department department)
        {


            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                FlashMessage.Confirmation("Department saved successfully");
                return RedirectToAction("SaveDepartment");

            }
            return View();
        }

        public JsonResult IsDeptCodeExist(string Code)
        {
            var dept = db.Departments.ToList();
            if (!dept.Any(department => department.Code.ToLower() == Code.ToLower()))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewDetails()
        {
            ViewBag.DepartmentIds = new SelectList(db.Departments, "DepartmentId", "Code");
            return View();
        }

        //public JsonResult GetStudentByDeptId(int departmentId)
        //{
        //    var students = db.Students.Where(x => x.DepartmentId == departmentId).ToList();
        //    return Json(students);
        //}

        //public JsonResult GetStudentById(int id)
        //{
        //    var student = db.Students.FirstOrDefault(x => x.StudentId == id);
        //    return Json(student);
        //}
    }
}