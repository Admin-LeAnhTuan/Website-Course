using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Course.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Migrations;

namespace Course.Controllers
{
    public class UnitsController : Controller
    {
        private ModelContext db = new ModelContext();
        private List<Unit> units = new List<Unit>();
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public Boolean isBuyed(int? course_id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user_id = User.Identity.GetUserId();
                ModelContext db = new ModelContext();
                var isBuyed = db.Payments.FirstOrDefault(p => p.users_id== user_id);
                if(isBuyed != null )
                {
                    return true;
                }
                return false;
                
            }
            return false;
        }

        public Boolean isWriter(int unit_id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ModelContext db = new ModelContext();
                var unit= db.Units.Where(u => unit_id == u.Unit_id).FirstOrDefault();
                var course = db.Courses.Where(c => c.course_id == unit.course_id).FirstOrDefault();
                var isWriter = course.userid == User.Identity.GetUserId();
                if(isWriter)
                    return true;
                else
                    return false;
            }
            return false;
        }
            // GET: Units
            public ActionResult Index()
        {
            return RedirectToAction("Index", "Home", new { });

        }

        // GET: Units/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null )          
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.Units.Where(u => u.Unit_id == id).FirstOrDefault();
           unit.lstUnit= db.Units.Where(u => u.course_id == unit.course_id).ToList();
            if(!isBuyed(unit.course_id))
            {
                return RedirectToAction("Index", "Home", new { });
            }
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // GET: Units/Create
        public ActionResult Create()
        {
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "title");
            return View(new Unit());
        }

        // POST: Units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Unit_id,lesson,title,description,url_unit,duration,course_id")] Unit unit)

        {
            if (ModelState.IsValid)
            {
                Courses courses = db.Courses.FirstOrDefault(c => c.course_id == unit.course_id);
                if (courses.duration == null)
                {
                    courses.duration = unit.duration;
                }
                else
                {
                    TimeSpan dur_unit = TimeSpan.Parse(unit.duration);
                    TimeSpan dur_course = TimeSpan.Parse(courses.duration);
                    TimeSpan result = dur_course.Add(dur_unit);
                    courses.duration = result.ToString();
                }
                db.Courses.AddOrUpdate(courses);
                db.Units.Add(unit);
                db.SaveChanges();
                return RedirectToAction("Index", "Courses");
            }

            ViewBag.course_id = new SelectList(db.Courses, "course_id", "title", unit.course_id);
            return View(unit);
        }

        // GET: Units/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "title", unit.course_id);
            return View(unit);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Unit_id,lesson,title,description,url_unit,duration,course_id")] Unit unit)

        {
            if (ModelState.IsValid)
            {
                db.Entry(unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.course_id = new SelectList(db.Courses, "course_id", "title", unit.course_id);
            return View(unit);
        }

        // GET: Units/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Unit unit = db.Units.Find(id);
            db.Units.Remove(unit);
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
