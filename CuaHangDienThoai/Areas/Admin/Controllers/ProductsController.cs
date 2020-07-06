using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using CuaHangMayTinh.Areas.Admin.Models;

namespace CuaHangMayTinh.Areas.Admin.Controllers
{
    public class ProductsController : AdminController
    {
        private MobileStore db = new MobileStore();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMuc).Include(s => s.NhaCungCap);
            return View(sanPhams.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.ma_danh_muc = new SelectList(db.DanhMucs, "ma", "ten");
            ViewBag.ma_nha_cung_cap = new SelectList(db.NhaCungCaps, "ma", "ten");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productDTO.sanPham.ngay_tao = DateTime.Now;

                    String contentType = "jpg";
                    productDTO.sanPham.hinh_anh = productDTO.sanPham.ma + ".jpg";

                    if (productDTO.file != null && productDTO.file.ContentLength > 0)
                    {
                        productDTO.file.SaveAs(Server.MapPath("~/Public/upload/product/") + productDTO.sanPham.ma + "." + contentType);
                    }

                    db.SanPhams.Add(productDTO.sanPham);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewBag.Message = "Không thể thêm mới sản phẩm (Vui long kiểm tra mã sản phẩm đã tồn tại)";
                }
                
            }
            

            ViewBag.ma_danh_muc = new SelectList(db.DanhMucs, "ma", "ten", productDTO.sanPham.ma_danh_muc);
            ViewBag.ma_nha_cung_cap = new SelectList(db.NhaCungCaps, "ma", "ten", productDTO.sanPham.ma_nha_cung_cap);
            return View(productDTO);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.ma_danh_muc = new SelectList(db.DanhMucs, "ma", "ten", sanPham.ma_danh_muc);
            ViewBag.ma_nha_cung_cap = new SelectList(db.NhaCungCaps, "ma", "ten", sanPham.ma_nha_cung_cap);
            ProductDTO productDTO = new ProductDTO();
            productDTO.sanPham = sanPham;
            return View(productDTO);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductDTO productDTO)
        {
            //SanPham _product = db.SanPhams.Find(productDTO.sanPham.ma);
            if (ModelState.IsValid)
            {
                productDTO.sanPham.mo_ta = HttpUtility.HtmlDecode(Request.Form["mo_ta"]).Replace(System.Environment.NewLine, "");
                //if (_product.hinh_anh != null)
                //{
                //    productDTO.sanPham.hinh_anh = _product.hinh_anh;
                //    _product = null;
                //}
                productDTO.sanPham.hinh_anh = Request.Form["oldImage"];
                productDTO.sanPham.hot = Request.Form["newHot"] == "on";
                db.Entry(productDTO.sanPham).State = EntityState.Modified;
                if (productDTO.file != null && productDTO.file.ContentLength > 0)
                {
                    String contentType = "jpg";
                    productDTO.file.SaveAs(Server.MapPath("~/Public/upload/product/") + productDTO.sanPham.ma + "." + contentType);
                    productDTO.sanPham.hinh_anh = productDTO.sanPham.ma + ".jpg";
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ma_danh_muc = new SelectList(db.DanhMucs, "ma", "ten", productDTO.sanPham.ma_danh_muc);
            ViewBag.ma_nha_cung_cap = new SelectList(db.NhaCungCaps, "ma", "ten", productDTO.sanPham.ma_nha_cung_cap);
            return View(productDTO.sanPham);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
