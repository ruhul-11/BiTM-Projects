﻿using System.Data.Entity;
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

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Name", roomAllocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "Name", roomAllocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Code", roomAllocation.Course.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "Name", roomAllocation.RoomId);

            //return View(roomAllocation);
            return RedirectToAction("Allocate");
        }


    }
}
