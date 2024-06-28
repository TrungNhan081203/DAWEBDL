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
    public class DiemDensController : Controller
    {
        private WebDLEntities db = new WebDLEntities();

        // GET: Admin/DiemDens
        public ActionResult Index()
        {
            return View(db.DiemDens.ToList());
        }

        // GET: Admin/DiemDens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemDen diemDen = db.DiemDens.Find(id);
            if (diemDen == null)
            {
                return HttpNotFound();
            }
            return View(diemDen);
        }

        // GET: Admin/DiemDens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DiemDens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDiemDen,TenDiemDen,MoTa,ViTri,LinkAnh")] DiemDen diemDen)
        {
            if (ModelState.IsValid)
            {
                db.DiemDens.Add(diemDen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diemDen);
        }

        // GET: Admin/DiemDens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemDen diemDen = db.DiemDens.Find(id);
            if (diemDen == null)
            {
                return HttpNotFound();
            }
            return View(diemDen);
        }

        // POST: Admin/DiemDens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDiemDen,TenDiemDen,MoTa,ViTri,LinkAnh")] DiemDen diemDen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemDen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diemDen);
        }

        // GET: Admin/DiemDens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemDen diemDen = db.DiemDens.Find(id);
            if (diemDen == null)
            {
                return HttpNotFound();
            }
            return View(diemDen);
        }

        // POST: Admin/DiemDens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiemDen diemDen = db.DiemDens.Find(id);
            db.DiemDens.Remove(diemDen);
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
