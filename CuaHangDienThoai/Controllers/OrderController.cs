using CuaHangMayTinh.Areas.Admin.Models;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CuaHangMayTinh.Controllers
{
    public class OrderController : BaseController
    {
        private MobileStore db = new MobileStore();
        // GET: Order
        public ActionResult Index()
        {
            TaiKhoan loginAccount = (TaiKhoan)Session["LoginAccount"];
            List<HoaDonBan> hoaDonBans = db.HoaDonBans.Where(x => x.khach_hang == loginAccount.tai_khoan).ToList();

            return View(hoaDonBans);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            if (hoaDonBan == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonBan);
        }

        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            hoaDonBan.tinh_trang = OrderStatus.HUY_BO;
            db.SaveChanges();
            if (hoaDonBan == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("index");
        }

        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string phoneNumber)
        {
            TaiKhoan loginAccount = (TaiKhoan)Session["LoginAccount"];
            List<HoaDonBan> hoaDonBans = db.HoaDonBans.Where(x => x.so_dien_thoai == phoneNumber).ToList();

            return View("Index", hoaDonBans);
        }
    }
}