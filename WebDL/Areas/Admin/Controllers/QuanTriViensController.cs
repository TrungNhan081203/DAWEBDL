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
    public class QuanTriViensController : Controller
    {
        private WebDLEntities db = new WebDLEntities();

        // GET: Admin/QuanTriViens
        public ActionResult Index()
        {
            return View(db.QuanTriViens.ToList());
        }

        // GET: Admin/QuanTriViens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTriVien quanTriVien = db.QuanTriViens.Find(id);
            if (quanTriVien == null)
            {
                return HttpNotFound();
            }
            return View(quanTriVien);
        }

        // GET: Admin/QuanTriViens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QuanTriViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQuanTriVien,TenDangNhap,MatKhau,Email")] QuanTriVien quanTriVien)
        {
            if (ModelState.IsValid)
            {
                db.QuanTriViens.Add(quanTriVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quanTriVien);
        }

        // GET: Admin/QuanTriViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTriVien quanTriVien = db.QuanTriViens.Find(id);
            if (quanTriVien == null)
            {
                return HttpNotFound();
            }
            return View(quanTriVien);
        }

        // POST: Admin/QuanTriViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaQuanTriVien,TenDangNhap,MatKhau,Email")] QuanTriVien quanTriVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quanTriVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quanTriVien);
        }

        // GET: Admin/QuanTriViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTriVien quanTriVien = db.QuanTriViens.Find(id);
            if (quanTriVien == null)
            {
                return HttpNotFound();
            }
            return View(quanTriVien);
        }

        // POST: Admin/QuanTriViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuanTriVien quanTriVien = db.QuanTriViens.Find(id);
            db.QuanTriViens.Remove(quanTriVien);
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
