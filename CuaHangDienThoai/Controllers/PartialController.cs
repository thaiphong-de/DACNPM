using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangMayTinh.Controllers
{
    public class PartialController : Controller
    {
        private MobileStore db = new MobileStore();
        // GET: Partial
        public ActionResult Index()
        {
            return View();
        }

        // GET: Partial/HotProduct
        public ActionResult HotProduct()
        {
            List<SanPham> hotProduct = db.SanPhams.Where(x => x.hot == true).Take(5).ToList();
            return View(hotProduct);
        }

        // GET: Partial/NewProduct
        public ActionResult NewProduct()
        {
            List<SanPham> newProduct = db.SanPhams.OrderByDescending(x => x.ngay_tao).Take(5).ToList();
            return View(newProduct);
        }

        // GET: Partial/SameCategory/id
        public ActionResult SameCategory(int id)
        {
            List<SanPham> products = db.SanPhams.Where(x=>x.ma_danh_muc == id).Take(3).ToList();
            return View(products);
        }

        // GET: Partial/Categories
        public ActionResult Categories()
        {
            List<DanhMuc> cateogories = db.DanhMucs.Take(7).ToList();
            return View(cateogories);
        }
        // GET: Partial/Cart
        public ActionResult Cart()
        {
            return View();
        }

    }
}