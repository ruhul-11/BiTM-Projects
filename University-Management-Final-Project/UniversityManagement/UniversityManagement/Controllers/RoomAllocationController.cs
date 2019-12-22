using System.Data.Entity;
using System.Data.Entity.Migrations;
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
            ViewBag.Courses = new SelectList(db.Courses, "CourseId", "Name");
            ViewBag.Days = new SelectList(db.Days, "DayId", "Name");
            ViewBag.Rooms = new SelectList(db.Rooms, "RoomId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Allocate(RoomAllocation roomAllocation)
        {
            if (ModelState.IsValid)
            {
                db.RoomAllocations.Add(roomAllocation);
                db.SaveChanges();
                FlashMessage.Confirmation("Department saved successfully");
                return RedirectToAction("Allocate");
            }

            ViewBag.Departments = new SelectList(db.Departments, "DepartmentId", "Name");
            ViewBag.Courses = new SelectList(db.Courses, "CourseId", "Name");
            ViewBag.Days = new SelectList(db.Days, "DayId", "Name");
            ViewBag.Rooms = new SelectList(db.Rooms, "RoomId", "Name");

            return RedirectToAction("Allocate");
        }


        public JsonResult GetCoursesByDeptId(int deptId)
        {
            var courses = db.Courses.Where(m => m.DepartmentId == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveRoomSchedule(RoomAllocation roomAllocation)
        {
            var scheduleList = db.RoomAllocations.Where(m => m.RoomId == roomAllocation.RoomId && m.DayId == roomAllocation.DayId && m.RoomStatus == "Allocated").ToList();
            if (scheduleList.Count == 0)
            {
                roomAllocation.RoomStatus = "Allocated";
                db.RoomAllocations.Add(roomAllocation);
                db.SaveChanges();
                return Json(true);
            }
            else
            {
                bool status = false;
                foreach (var allocation in scheduleList)
                {
                    if ((roomAllocation.StartTime >= allocation.StartTime && roomAllocation.StartTime < allocation.EndTime)
                         || (roomAllocation.EndTime > allocation.StartTime && roomAllocation.EndTime <= allocation.EndTime) && roomAllocation.RoomStatus == "Allocated")
                    {
                        status = true;
                    }
                }
                if (status == false)
                {
                    roomAllocation.RoomStatus = "Allocated";
                    db.RoomAllocations.Add(roomAllocation);
                    db.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }

        }


        public ActionResult UnAllocatedClassRoom()
        {
            return View();
        }

        public JsonResult UnAllocateAllRooms(bool decision)
        {
            var roomStatus = db.RoomAllocations.Where(m => m.RoomStatus == "Allocated").ToList();
            if (roomStatus.Count == 0)
            {
                return Json(false);
            }
            else
            {
                foreach (var room in roomStatus)
                {
                    room.RoomStatus = "";
                    db.RoomAllocations.AddOrUpdate(room);
                    db.SaveChanges();

                }
                return Json(true);
            }

        }

    }
}
