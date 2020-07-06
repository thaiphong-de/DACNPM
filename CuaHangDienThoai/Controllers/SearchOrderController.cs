using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CuaHangMayTinh.Controllers
{
    public class SearchOrderController : Controller
    {
        private MobileStore db = new MobileStore();
        // GET: SearchOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(int ma)
        {
            //TaiKhoan loginAccount = (TaiKhoan)Session["LoginAccount"];
            List<HoaDonBan> hoaDonBans = db.HoaDonBans.Where(x => x.ma == ma).ToList();
            
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
    }
}