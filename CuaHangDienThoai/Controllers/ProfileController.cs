using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangMayTinh.Controllers
{
    public class ProfileController : BaseController
    {
        public MobileStore db = new MobileStore();
        // GET: Profile
        public ActionResult Index()
        {
            if(TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            TaiKhoan loginAccount = (TaiKhoan)Session["LoginAccount"];
            TaiKhoan _account = db.TaiKhoans.Find(loginAccount.tai_khoan);
            return View(loginAccount);
        }

        //Post
        [HttpPost]
        public ActionResult ChangeProfile(TaiKhoan account)
        {
            TaiKhoan loginAccount = (TaiKhoan)Session["LoginAccount"];
            TaiKhoan _account = db.TaiKhoans.Find(loginAccount.tai_khoan);
            _account.ten = account.ten;
            _account.so_dien_thoai = account.so_dien_thoai;
            _account.email = account.email;
            _account.dia_chi = account.dia_chi;

            loginAccount.ten = account.ten;
            loginAccount.so_dien_thoai = account.so_dien_thoai;
            loginAccount.dia_chi = account.dia_chi;
            loginAccount.email = account.email;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Post
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword)
        {
            TaiKhoan loginAccount = (TaiKhoan)Session["LoginAccount"];
            TaiKhoan _account = db.TaiKhoans.Find(loginAccount.tai_khoan);
            if(_account.mat_khau != oldPassword)
            {
                TempData["Message"] = "Mật khẩu không chính xác";
                return RedirectToAction("Index");
            }
            else
            {
                _account.mat_khau = newPassword;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Remove("LoginAccount");
            return RedirectToAction("Index", "Home");
        }
    }
}