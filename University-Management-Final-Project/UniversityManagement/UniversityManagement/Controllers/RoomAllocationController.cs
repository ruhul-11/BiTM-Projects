using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using UniversityManagement.DataContext;
using UniversityManagement.Models;
using Vereyon.Web;

namespace UniversityManagement.Controllers
{
    public class RoomAllocationController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        public ActionResult Allocate()
        {
            ViewBag.Departments = new SelectList(db.Departments, "DepartmentId", "Name");
            ViewBag.Courses = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewBag.Days = new SelectList(db.Days, "DayId", "Name");
            ViewBag.Rooms = new SelectList(db.Rooms, "RoomId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Allocate([Bind(Include = "RoomAllocationId,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime,RoomStatus")] RoomAllocation roomAllocation)
        {
            if (ModelState.IsValid)
            {
                db.RoomAllocations.Add(roomAllocation);
                db.SaveChanges();
                FlashMessage.Confirmation("Department saved successfully");
                return RedirectToAction("Allocate");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", roomAllocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomAllocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", roomAllocation.Course.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomAllocation.RoomId);
            return View(roomAllocation);
        }

        public JsonResult GetCoursesByDeptId(int deptId)
        {
            var courses = db.Courses.Where(m => m.DepartmentId == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

    }
}
