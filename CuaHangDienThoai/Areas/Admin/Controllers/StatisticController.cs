using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangMayTinh.Areas.Admin.Controllers
{
    public class StatisticController : AdminController
    {
        // GET: Admin/Statistic
        public ActionResult Index()
        {
            return View();
        }
    }
}