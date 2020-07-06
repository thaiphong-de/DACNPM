using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangMayTinh.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        public MobileStore db = new MobileStore();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.NumberOfAccount = db.TaiKhoans.Count();
            ViewBag.NumberOfProduct = db.SanPhams.Count();
            ViewBag.NumberOfSuplier = db.NhaCungCaps.Count();
            ViewBag.NumberOfCategory = db.DanhMucs.Count();

            return View();
        }
    }
}