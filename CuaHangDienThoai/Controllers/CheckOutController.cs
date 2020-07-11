using CuaHangMayTinh.App_Start;
using CuaHangMayTinh.Models;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace CuaHangMayTinh.Controllers
{
    public class CheckOutController : Controller
    {
        public MobileStore db = new MobileStore();
        // GET: CheckOut
        public ActionResult Index()
        {
            TaiKhoan account = Session["LoginAccount"] as TaiKhoan;
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
            
            if (account == null)
            {
                account = new TaiKhoan();
            }
            return View(account);
        }
        public void SendVerificationLinkEmail(string emailID,int ma)
        {
            var verifyUrl = "/SearchOrder/Search?ma=" + ma;
            
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("phong2941999@gmail.com", "MobileStore");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "THAIPHONG357941"; // Replace with actual password
            string subject = "Đơn hàng mới";

            string body = "<br/><br/>Bạn có đơn hàng mới , bấm để xem chi tiết" +

                " <br/><br/><a href='" + link + "'>Bấm vào đây nè bạn ưiiii  </a> ";


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
            // GET: CheckOut
        public ActionResult CheckOut(TaiKhoan taiKhoan)
        {
            TaiKhoan account = Session["LoginAccount"] as TaiKhoan;

            HoaDonBan bill = new HoaDonBan();
            bill.ngay_tao = DateTime.Now;
            if (account != null)
            {
                bill.khach_hang = account.tai_khoan;
            }

            bill.ten_khach_hang = taiKhoan.ten;
            bill.dia_chi = taiKhoan.dia_chi;
            bill.email = taiKhoan.email;
            bill.so_dien_thoai = taiKhoan.so_dien_thoai;

            List<ChiTietHoaDonBan> detailBills = new List<ChiTietHoaDonBan>();
            List<ProductCartDTO> carts = Session["Cart"] as List<ProductCartDTO>;
            if(carts != null)
            {
                foreach (var item in carts)
                {
                    ChiTietHoaDonBan detailBill = new ChiTietHoaDonBan();
                    detailBill.don_gia = item.Product.gia_moi;
                    detailBill.ma_san_pham = item.Product.ma;
                    detailBill.so_luong = item.Quantity;

                    detailBills.Add(detailBill);
                }
                bill.ChiTietHoaDonBans = detailBills;
                db.HoaDonBans.Add(bill);

                db.SaveChanges();
            }

            Session.Remove("Cart");

            string content = System.IO.File.ReadAllText(Server.MapPath("/Public/template/neworder.html"));
            content = content.Replace("{{Name}}", bill.ten_khach_hang);
           // content = content.Replace("{{Link}}", "http://localhost:56581/" + "Admin/Orders/Details/" + bill.ma);

            
            MailHelper.SendMail("phong2941999@gmail.com", "Có đơn đặt hàng mới","Bạn có đơn đặt hàng mới từ khách hàng");
            //MailHelper.SendMail(bill.email, "bạn đã đặt hàng","Cám ơn bạn đã mua hàng , lần sau mua nhiều hơn nha, xem chi tiết tại đây"+ content1);
            SendVerificationLinkEmail(bill.email, bill.ma);
            return View(bill);
        }
    }
}