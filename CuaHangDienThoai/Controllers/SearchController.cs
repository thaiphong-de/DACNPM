using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangMayTinh.Controllers
{
    public class SearchController : Controller
    {
        public MobileStore db = new MobileStore();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        // GET: Search
        [HttpPost]
        public ActionResult Search(string key)
        {
            List<SanPham> products = db.SanPhams.Where(x => x.ten.Contains(key) || x.ma.ToString().Contains(key)).ToList();
            return View(products);
        }
    }
}