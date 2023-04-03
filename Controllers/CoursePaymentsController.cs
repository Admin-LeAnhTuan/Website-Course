
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Course.Models;

namespace Course.Controllers
{
    public class CoursePaymentsController : Controller
    {
        private ModelContext db = new ModelContext();

        // GET: CoursePayments
        public async Task<ActionResult> Index()
        {
            var coursePayments = db.CoursePayments.Include(c => c.Enrollment);
            return View(await coursePayments.ToListAsync());
        }

        // GET: CoursePayments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursePayment coursePayment = await db.CoursePayments.FindAsync(id);
            if (coursePayment == null)
            {
                return HttpNotFound();
            }
            return View(coursePayment);
        }

        // GET: CoursePayments/Create
        public ActionResult Create()
        {
            ViewBag.enrollment_id = new SelectList(db.Enrollments, "enrollment_id", "users_id");
            return View();
        }

        // POST: CoursePayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Payment_id,enrollment_id,Payment_date,amount")] CoursePayment coursePayment)
        {
            if (ModelState.IsValid)
            {
                db.CoursePayments.Add(coursePayment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.enrollment_id = new SelectList(db.Enrollments, "enrollment_id", "users_id", coursePayment.enrollment_id);
            return View(coursePayment);
        }

        // GET: CoursePayments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursePayment coursePayment = await db.CoursePayments.FindAsync(id);
            if (coursePayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.enrollment_id = new SelectList(db.Enrollments, "enrollment_id", "users_id", coursePayment.enrollment_id);
            return View(coursePayment);
        }

        // POST: CoursePayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Payment_id,enrollment_id,Payment_date,amount")] CoursePayment coursePayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursePayment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.enrollment_id = new SelectList(db.Enrollments, "enrollment_id", "users_id", coursePayment.enrollment_id);
            return View(coursePayment);
        }

        // GET: CoursePayments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursePayment coursePayment = await db.CoursePayments.FindAsync(id);
            if (coursePayment == null)
            {
                return HttpNotFound();
            }
            return View(coursePayment);
        }

        // POST: CoursePayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CoursePayment coursePayment = await db.CoursePayments.FindAsync(id);
            db.CoursePayments.Remove(coursePayment);
            await db.SaveChangesAsync();
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
