using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebDL.Context;

namespace WebDL.Areas.Admin.Controllers
{
    public class TourDuLichesController : Controller
    {
        private WebDLEntities db = new WebDLEntities();

        // GET: Admin/TourDuLiches
        public ActionResult Index(string searchString, int? page)
        {
            var tourDuLiches = db.TourDuLiches.Include(t => t.DiemDen);

            if (!String.IsNullOrEmpty(searchString))
            {
                tourDuLiches = tourDuLiches.Where(t => t.TenTour.Contains(searchString));
            }

            int pageSize = 6; 
            int pageNumber = (page ?? 1); 

            return View(tourDuLiches.OrderBy(t => t.MaTour).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/TourDuLiches/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourDuLich tourDuLich = await db.TourDuLiches.FindAsync(id);
            if (tourDuLich == null)
            {
                return HttpNotFound();
            }
            return View(tourDuLich);
        }

        // GET: Admin/TourDuLiches/Create
        public ActionResult Create()
        {
            ViewBag.MaDiemDen = new SelectList(db.DiemDens, "MaDiemDen", "TenDiemDen");
            return View();
        }

        // POST: Admin/TourDuLiches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaTour,MaDiemDen,TenTour,MoTa,Gia,NgayBatDau,NgayKetThuc,LinkAnh")] TourDuLich tourDuLich)
        {
            if (ModelState.IsValid)
            {
                db.TourDuLiches.Add(tourDuLich);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaDiemDen = new SelectList(db.DiemDens, "MaDiemDen", "TenDiemDen", tourDuLich.MaDiemDen);
            return View(tourDuLich);
        }

        // GET: Admin/TourDuLiches/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourDuLich tourDuLich = await db.TourDuLiches.FindAsync(id);
            if (tourDuLich == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDiemDen = new SelectList(db.DiemDens, "MaDiemDen", "TenDiemDen", tourDuLich.MaDiemDen);
            return View(tourDuLich);
        }

        // POST: Admin/TourDuLiches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaTour,MaDiemDen,TenTour,MoTa,Gia,NgayBatDau,NgayKetThuc,LinkAnh")] TourDuLich tourDuLich)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tourDuLich).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaDiemDen = new SelectList(db.DiemDens, "MaDiemDen", "TenDiemDen", tourDuLich.MaDiemDen);
            return View(tourDuLich);
        }

        // GET: Admin/TourDuLiches/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourDuLich tourDuLich = await db.TourDuLiches.FindAsync(id);
            if (tourDuLich == null)
            {
                return HttpNotFound();
            }
            return View(tourDuLich);
        }

        // POST: Admin/TourDuLiches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TourDuLich tourDuLich = await db.TourDuLiches.FindAsync(id);
            db.TourDuLiches.Remove(tourDuLich);
            await db.SaveChangesAsync();
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
