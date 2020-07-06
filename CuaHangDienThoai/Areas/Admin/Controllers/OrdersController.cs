using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using CuaHangMayTinh.Areas.Admin.Models;
using Model.EF;

namespace CuaHangMayTinh.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private MobileStore db = new MobileStore();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var hoaDonBans = db.HoaDonBans.Include(h => h.TaiKhoan);
            return View(hoaDonBans.ToList());
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            if (hoaDonBan == null)
            {
                return HttpNotFound();
            }
            return View(hoaDonBan);
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.khach_hang = new SelectList(db.TaiKhoans, "tai_khoan", "mat_khau");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ma,khach_hang,ten_khach_hang,email,dia_chi,so_dien_thoai,tinh_trang,ngay_tao")] HoaDonBan hoaDonBan)
        {
            if (ModelState.IsValid)
            {
                db.HoaDonBans.Add(hoaDonBan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.khach_hang = new SelectList(db.TaiKhoans, "tai_khoan", "mat_khau", hoaDonBan.khach_hang);
            return View(hoaDonBan);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.khach_hang = new SelectList(db.TaiKhoans, "tai_khoan", "mat_khau", hoaDonBan.khach_hang);
            return View(hoaDonBan);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult ChangeStatus(int? id, string status)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            hoaDonBan.tinh_trang = status;
            bool ok = true;

            if (status == OrderStatus.HOAN_THANH)
            {
                
                for (int i = 0; i < hoaDonBan.ChiTietHoaDonBans.Count(); i++)
                {
                    ChiTietHoaDonBan chiTiet = hoaDonBan.ChiTietHoaDonBans.ElementAt(i);
                    if (chiTiet != null && chiTiet.SanPham != null && chiTiet.SanPham.so_luong >= chiTiet.so_luong)
                    {
                        chiTiet.SanPham.so_luong -= chiTiet.so_luong;
                    }
                    else
                    {
                        ok = false;
                        if (chiTiet.SanPham != null)
                        {
                            TempData["Message"] = String.Format("Sản phẩm {0} không đủ số lượng để hoàn thành hóa đơn", chiTiet.SanPham.ten);
                        }
                    }
                }
            }
            if (ok)
            {
                db.SaveChanges();
                if (hoaDonBan == null)
                {
                    return HttpNotFound();
                }

                return RedirectToAction("Index");
            }
            return Redirect("/Admin/Orders/Details/" + hoaDonBan.ma);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ma,khach_hang,ten_khach_hang,email,dia_chi,so_dien_thoai,tinh_trang,ngay_tao")] HoaDonBan hoaDonBan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDonBan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.khach_hang = new SelectList(db.TaiKhoans, "tai_khoan", "mat_khau", hoaDonBan.khach_hang);
            return View(hoaDonBan);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDonBan hoaDonBan = db.HoaDonBans.Find(id);
            db.HoaDonBans.Remove(hoaDonBan);
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
