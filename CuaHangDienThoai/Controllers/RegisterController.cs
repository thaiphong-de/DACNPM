using CuaHangMayTinh.Models;
using Model.EF;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CuaHangMayTinh.Controllers
{
    public class RegisterController : Controller
    {
        public MobileStore db = new MobileStore();
        // GET: Register
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }
        public bool checkemail(string Email)
        {
            return db.TaiKhoans.Count(x => x.email == Email) > 0;
        }
        //POST
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (db.TaiKhoans.Count(x => x.tai_khoan == model.Username) > 0)
            {

                TempData["Message"] = "Tài khoản đã tồn tại";
                //return RedirectToAction("Index");
                return Content("Tài khoản đã tồn tại");

            }
            else
            {
                if (checkemail(model.Email))
                {

                    return Content("Email đã tồn tại");
                    //return View("Index");
                }
                else
                {
                    TaiKhoan taiKhoan = new TaiKhoan();
                    taiKhoan.tai_khoan = model.Username;
                    taiKhoan.mat_khau = model.Password;
                    taiKhoan.email = model.Email;
                    taiKhoan.loai_tai_khoan = 2;
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();

                    return View(model);
                }

            }



        }
        //Captcha
        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            string recaptcha_token = frm["g-recaptcha-response"];
            string responseInString = "";
            string url = "https://www.google.com/recaptcha/api/siteverify";
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["response"] = recaptcha_token;
                data["secret"] = "6LfPra0ZAAAAAPK3NbG9o7O9Yw029-qF1x_K2cKY";
                var response = wb.UploadValues(url, "POST", data);
                responseInString = Encoding.UTF8.GetString(response);
            }
            dynamic result = JObject.Parse(responseInString);
            if (result.success == false)
            {
                // Báo lỗi "recaptcha khong hop le"    
                ModelState.AddModelError("", "recaptcha khong hop le");
            }
            else
            {
                // tiep tuc xu ly
                ViewBag.MessageSuccess = "Xác thực reCAPTCHA thành công";
            }
            return View();
        }
    }
}