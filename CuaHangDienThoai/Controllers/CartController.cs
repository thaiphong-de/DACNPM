
using CuaHangMayTinh.Models;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangMayTinh.Controllers
{
    public class CartController : Controller
    {
        private MobileStore db = new MobileStore();
        // GET: Cart
        public ActionResult Index()
        {
            List<ProductCartDTO> cart = (Session["Cart"] as List<ProductCartDTO>);
            double total = 0;
            if(cart != null)
            {
                foreach (var item in cart)
                {
                    if (item.Product.gia_moi != null)
                    {
                        total += item.Quantity * (double)item.Product.gia_moi;
                    }
                }
                ViewBag.Total = total;
            }
            else
            {
                ViewBag.Message = "Bạn chưa mua sản phẩm nào";
            }
            if (cart == null) cart = new List<ProductCartDTO>();
            return View(cart);
        }

        // GET: Cart/AddToCart/:productId
        public ActionResult AddToCart(string id)
        {
            SanPham product = db.SanPhams.Find(id);
            int quantity = Request["quantity"] != null ? int.Parse(Request["quantity"]) : 1;
            List<ProductCartDTO> cart = null;


            ProductCartDTO productCartDTO = new ProductCartDTO();
            productCartDTO.Product = product;
            productCartDTO.Quantity = quantity;
            if (product.so_luong == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if ((Session["Cart"] as List<ProductCartDTO>) == null)
            {
                cart = new List<ProductCartDTO>();
                (cart as List<ProductCartDTO>).Add(productCartDTO);
            } else
            {
                cart = Session["Cart"] as List<ProductCartDTO>;
                bool flag = false;
                foreach (ProductCartDTO item in cart)
                {
                    if (item.Product == null) continue;
                    
                    if(item.Product.ma.Trim() == id.Trim())
                    {
                        flag = true;
                        item.Quantity += quantity;
                        break;
                    }
                }
                if (!flag)
                {
                    (cart as List<ProductCartDTO>).Add(productCartDTO);
                }
            }
            Session["Cart"] = cart;

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpdateCart(List<string> cart)
        {
            List<ProductCartDTO> carts = null;

            carts = Session["Cart"] as List<ProductCartDTO>;
            for (int i = 0; i < cart.Count; i++)
            {
                carts[i].Quantity = int.Parse(cart[i]);
            }
            Session["Cart"] = carts;
            if (Request.Form["checkout"] != null && Request.Form["checkout"] != "")
            {
                return RedirectToAction("Index","CheckOut");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public RedirectToRouteResult DeleteCart(string masp)
        {
            // tìm carditem muon sua
            List<ProductCartDTO> cart = (Session["Cart"] as List<ProductCartDTO>);
            ProductCartDTO itemSua = cart.FirstOrDefault(m => m.Product.ma == masp);
            if (itemSua != null)
            {
                cart.Remove(itemSua);
            }
            return RedirectToAction("Index", "Cart");

        }




    }
}