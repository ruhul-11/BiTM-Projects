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
        public ActionResult Allocate([Bind(Include = "RoomAllocationId,DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime,RoomStatus")] RoomAllocation roomAllocation)
        {
            if (ModelState.IsValid)
            {
                db.RoomAllocations.Add(roomAllocation);
                db.SaveChanges();
                FlashMessage.Confirmation("Department saved successfully");
                return RedirectToAction("Allocate");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", roomAllocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomAllocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", roomAllocation.Course.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomAllocation.RoomId);

            //return View(roomAllocation);
            return RedirectToAction("Allocate");
        }

        public JsonResult GetCoursesByDeptId(int deptId)
        {
            var courses = db.Courses.Where(m => m.DepartmentId == deptId).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveRoomSchedule(RoomAllocation classRoomAllocation)
        {
            var scheduleList = db.RoomAllocations.Where(m => m.RoomId == classRoomAllocation.RoomId && m.DayId == classRoomAllocation.DayId && m.RoomStatus == "Allocated").ToList();
            if (scheduleList.Count == 0)
            {
                classRoomAllocation.RoomStatus = "Allocated";
                db.RoomAllocations.Add(classRoomAllocation);
                db.SaveChanges();
                return Json(true);
            }
            else
            {
                bool status = false;
                foreach (var allocation in scheduleList)
                {
                    if ((classRoomAllocation.StartTime >= allocation.StartTime && classRoomAllocation.StartTime < allocation.EndTime)
                         || (classRoomAllocation.EndTime > allocation.StartTime && classRoomAllocation.EndTime <= allocation.EndTime) && classRoomAllocation.RoomStatus == "Allocated")
                    {
                        status = true;
                    }
                }
                if (status == false)
                {
                    classRoomAllocation.RoomStatus = "Allocated";
                    db.RoomAllocations.Add(classRoomAllocation);
                    db.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }

        }


        public ActionResult UnallocatedClassRoom()
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
