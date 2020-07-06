using CuaHangMayTinh.Areas.Admin.Models;
using CuaHangMayTinh.Models;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CuaHangMayTinh.Controllers
{
    public class LostPassController : Controller
    {
        private MobileStore db = new MobileStore();
        // GET: LostPass
        public ActionResult ForgetPassWord()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassWord(string EmailID)
        {
            //SendMail(EmailID);
            /*string From = "boxuanhoang2016@gmail.com";
            var mail = new SmtpClient("smtp.gmail.com", 587);
            {
                mail.Credentials = new NetworkCredential("boxuanhoang2016@gmail.com", "Hoang1708");
                mail.EnableSsl = true;
            };
            var message = new MailMessage();
            message.From = new MailAddress(From);
            message.ReplyToList.Add(From);
            message.To.Add(new MailAddress(EmailID));
            // Tạo mật khẩu bằng cách Random
            string pw = "";
            Random rd = new Random();
            for (int i = 0; i < 6; i++)
            {
                int mk = rd.Next(0, 10);
                pw = pw + mk.ToString();
            }
            var account = db.TaiKhoans.Where(a => a.email == EmailID).FirstOrDefault();
            account.mat_khau = pw;
            //db.Configuration.ValidateOnSaveEnabled = false;
            // db.SaveChanges();
            var verifyUrl = "/LostPass/ResetPass";
            message.Subject = "Lấy lại mật khẩu !";
            message.Body = "Mật khẩu mới của bạn là: " + pw;
           // UserModel userModel = new UserModel();
            //userModel.Password = pw;
           // UserDTO userDTO = new UserDTO();
            //userDTO.mat_khau = pw;
            
            //db.SanPhams.Add(productDTO.sanPham);
            mail.Send(message);
            */
            string message = "";
            var v = db.TaiKhoans.Where(a => a.email == EmailID).FirstOrDefault();
            {
                if (v != null)
                {
                    string resetcode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(EmailID, resetcode);
                    v.ma_tim_mk = resetcode;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Vui lòng kiểm tra email của bạn";
                }
                else
                {
                    message = "Tài khoản không tồn tại";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        public void SendVerificationLinkEmail(string emailID,string activationcode)
        {
            
            var verifyUrl = "/LostPass/ResetPass/" +activationcode;
           var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("boxuanhoang2016@gmail.com", "Đặt lại mật khẩu");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "Hoang1708"; // Replace with actual password
            string subject = "đặt lại mật khẩu";

            string body = "<br/><br/>Nếu bạn thật sự quên mật khẩu , hãy bấm vào link bên dưới để đặt lại mật khẩu" +
                
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
       /* public static string SendMail(string To)
        {
            string From = "boxuanhoang2016@gmail.com";
            var mail = new SmtpClient("smtp.gmail.com", 587);
            {
                mail.Credentials = new NetworkCredential("boxuanhoang2016@gmail.com", "Hoang1708");
                mail.EnableSsl = true;
            };
            var message = new MailMessage();
            message.From = new MailAddress(From);
            message.ReplyToList.Add(From);
            message.To.Add(new MailAddress(To));
            // Tạo mật khẩu bằng cách Random
            string pw = "";
            Random rd = new Random();
            for (int i = 0; i < 6; i++)
            {
                int mk = rd.Next(0, 10);
                pw = pw + mk.ToString();
            }

            message.Subject = "Lấy lại mật khẩu !";
            message.Body = "Mật khẩu mới của bạn là: " + pw;
            UserDTO userDTO = new UserDTO();
           userDTO.mat_khau = pw;
            //ProductDTO productDTO = new ProductDTO();
            //productDTO.sanPham = sanPham;
            //db.SanPhams.Add(productDTO.sanPham);
            mail.Send(message);
            return pw;
        }*/
        public ActionResult ResetPass(string id)
        {
            using (MobileStore db = new MobileStore())
            {
                var taikhoan = db.TaiKhoans.Where(a => a.ma_tim_mk == id).FirstOrDefault();
                if(taikhoan!=null)
                {
                    ResetPassModel model = new ResetPassModel();
                    model.Resetcode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPass(ResetPassModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using(MobileStore db =new MobileStore())
                {
                    var taikhoan = db.TaiKhoans.Where(a => a.ma_tim_mk == model.Resetcode).FirstOrDefault();
                    if(taikhoan!=null)
                    {
                        taikhoan.mat_khau = model.Password;
                        taikhoan.ma_tim_mk = "";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        message = "Đừng làm mất mật khẩu nữa nha bạn =)))";
                        //message = taikhoan.email;
                        //message = taikhoan.mat_khau;
                    }
                    else
                    {
                        message = "có gì đó sai sai ở đây =)))";
                    }
                }
                
            }
            ViewBag.Message = message;
            return View(model);
        }
    }
}