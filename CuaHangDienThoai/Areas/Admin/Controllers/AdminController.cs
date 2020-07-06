using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangMayTinh.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            TaiKhoan loginAccount = (TaiKhoan)Session["LoginAccount"];
            if (loginAccount == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
            else
            {
                if (loginAccount.loai_tai_khoan == 1)
                {
                    filterContext.Result = new RedirectResult("~/Admin/Products");
                }
                else if (loginAccount.loai_tai_khoan == 2)
                {
                    filterContext.Result = new RedirectResult("~/Home/Index");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}