using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDL.Context;

namespace WebDL.Areas.Admin.Controllers
{
    public class DatChoController : Controller
    {
        private WebDLEntities db = new WebDLEntities();

        // GET: Admin/DatCho
        public ActionResult Index()
        {
            var datChoes = db.DatChoes.Include(d => d.NguoiDung).Include(d => d.TourDuLich);
            return View(datChoes.ToList());
        }

        // GET: Admin/DatCho/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatCho datCho = db.DatChoes.Find(id);
            if (datCho == null)
            {
                return HttpNotFound();
            }
            return View(datCho);
        }

        // GET: Admin/DatCho/Create
        public ActionResult Create()
        {
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TenDangNhap");
            ViewBag.MaTour = new SelectList(db.TourDuLiches, "MaTour", "TenTour");
            return View();
        }

        // POST: Admin/DatCho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDatCho,MaNguoiDung,MaTour,SoLuongNguoi,NgayDat,TrangThai")] DatCho datCho)
        {
            if (ModelState.IsValid)
            {
                db.DatChoes.Add(datCho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TenDangNhap", datCho.MaNguoiDung);
            ViewBag.MaTour = new SelectList(db.TourDuLiches, "MaTour", "TenTour", datCho.MaTour);
            return View(datCho);
        }

        // GET: Admin/DatCho/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatCho datCho = db.DatChoes.Find(id);
            if (datCho == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TenDangNhap", datCho.MaNguoiDung);
            ViewBag.MaTour = new SelectList(db.TourDuLiches, "MaTour", "TenTour", datCho.MaTour);
            return View(datCho);
        }

        // POST: Admin/DatCho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDatCho,MaNguoiDung,MaTour,SoLuongNguoi,NgayDat,TrangThai")] DatCho datCho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datCho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TenDangNhap", datCho.MaNguoiDung);
            ViewBag.MaTour = new SelectList(db.TourDuLiches, "MaTour", "TenTour", datCho.MaTour);
            return View(datCho);
        }

        // GET: Admin/DatCho/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatCho datCho = db.DatChoes.Find(id);
            if (datCho == null)
            {
                return HttpNotFound();
            }
            return View(datCho);
        }

        // POST: Admin/DatCho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatCho datCho = db.DatChoes.Find(id);
            db.DatChoes.Remove(datCho);
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
