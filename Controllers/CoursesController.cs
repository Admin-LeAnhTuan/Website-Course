using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Course.Models;
using DemoVNPay.Others;
using Microsoft.AspNet.Identity;
using WebGrease;

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
            ViewBag.level_id = new SelectList(db.Levels, "level_id", "Name");
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "course_id,title,description,price,duration,userid,category_id,level_id,courses_date")] Courses courses, HttpPostedFileBase ImageUpload)
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
            courses.Course_date= DateTime.Now;
            ViewBag.level_id = new SelectList(db.Levels, "level_id", "Name", courses.level_id);
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
            ViewBag.level_id = new SelectList(db.Levels, "level_id", "Name", courses.level_id);
            ViewBag.userid = new SelectList(db.AspNetUsers, "Id", "Email", courses.userid);
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "Name", courses.category_id);
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "course_id,title,description,price,duration,userid,category_id,level_id")] Courses courses, HttpPostedFileBase ImageUpload)
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
            ViewBag.level_id = new SelectList(db.Levels, "level_id", "Name", courses.level_id);
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
        [HttpGet]
        public ActionResult Payment(int course_id)
        {
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];


            Courses courses = db.Courses.Find(course_id);
            PayLib pay = new PayLib();

            int price = Int32.Parse(courses.price) * 100;
        
            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", price.ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            DateTime now = DateTime.Now; // Lấy thời gian hiện tại
            DateTime tomorrow = now.AddDays(1); // Thêm một ngày vào thời gian hiện tại

            pay.AddRequestData("vnp_ExpireDate", tomorrow.ToString("yyyyMMddHHmmss"));
            //Billing
            pay.AddRequestData("vnp_Bill_Mobile", "@@");
            pay.AddRequestData("vnp_Bill_Email", "@@");
            var fullName = User.Identity.Name;
            if (!String.IsNullOrEmpty(fullName))
            {
                var indexof = fullName.IndexOf(' ');
                pay.AddRequestData("vnp_Bill_FirstName", fullName.Substring(0, indexof));
                pay.AddRequestData("vnp_Bill_LastName", fullName.Substring(indexof + 1, fullName.Length - indexof - 1));
            }
            // Invoice
            pay.AddRequestData("vnp_Inv_Phone", "0815632641");
            pay.AddRequestData("vnp_Inv_Email", User.Identity.Name);
            pay.AddRequestData("vnp_Inv_Customer", "cus");
            pay.AddRequestData("vnp_Inv_Address","HCM");
            pay.AddRequestData("vnp_Inv_Company", "BCD");

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
            Response.Redirect(paymentUrl);

            return Redirect(paymentUrl);
        }

        public ActionResult PaymentConfirm()
        {
            
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về
                long vnp_Amount = Convert.ToInt64(pay.GetResponseData("vnp_Amount")) / 100;
                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        Enrollment payment = new Enrollment() { };

                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

            return View();
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
