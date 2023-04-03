using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Course.Models;
using Microsoft.AspNet.Identity;

namespace Course.Controllers
{
    public class CoursesController : Controller
    {
        private ModelContext db = new ModelContext();

        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.AspNetUser).Include(c => c.Category);
            return View(courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "Name");
            return View(new Courses());
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title,description,price,duration")] Courses courses, HttpPostedFileBase ImageUpload)
        {

            

            if (ModelState.IsValid)
            {
                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                    string extentions = Path.GetExtension(ImageUpload.FileName);
                    fileName = fileName + extentions;
                    courses.img_course = "~/Content/Images/" + fileName;
                    string _part = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                    ImageUpload.SaveAs(_part);

                }
                db.Courses.Add(courses);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

                /*return RedirectToAction("Index");*/
            courses.userid = User.Identity.GetUserId();
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", courses.userid);
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "Name", courses.category_id);
            db.Courses.Add(courses);
            db.SaveChanges();
            return View(courses);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }

            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", courses.userid);
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "Name", courses.category_id);
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "course_id,title,description,price,duration,userid,category_id")] Courses courses, HttpPostedFileBase ImageUpload)
        {
            if (ModelState.IsValid)
            {
                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                    string extentions = Path.GetExtension(ImageUpload.FileName);
                    fileName = fileName + extentions;
                    courses.img_course = "~/Content/Images/" + fileName;
                    string _part = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                    ImageUpload.SaveAs(_part);

                }
                db.Entry(courses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", courses.userid);
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "Name", courses.category_id);
            return View(courses);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Courses courses = db.Courses.Find(id);
            db.Courses.Remove(courses);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       /* public ActionResult Unit(int id) { 
            return 
        }
*/

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
