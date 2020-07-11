using CuaHangMayTinh.Models;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CuaHangMayTinh.Controllers
{
    public class LoginController : Controller
    {
        private MobileStore db = new MobileStore();
        public LoginController()
        {
        }
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (db.TaiKhoans.Where(x=>x.tai_khoan == username && x.mat_khau == password).Count() == 1)
            {
                TaiKhoan account = db.TaiKhoans.Find(username);
                Session.Add("LoginAccount", account);

                if(account.loai_tai_khoan == 0)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else if(account.loai_tai_khoan == 1)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                } else
                {
                    return RedirectToAction("Index", "Home");
                }
            } else
            {
                TempData["Message"] = "Tài khoản hoặc mật khẩu không chính xác";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("LoginAccount");
            return Redirect("/Home/Index");
        }
    }
}