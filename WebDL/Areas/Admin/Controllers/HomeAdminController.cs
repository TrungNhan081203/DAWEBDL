using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDL.Context;


namespace WebDL.Areas.Admin.Controllers
{

    public class HomeAdminController : Controller
    {
        private WebDLEntities   objWebDLEntities = new WebDLEntities();

        // GET: Admin/Home/Admin
        public ActionResult Admin()
        {
          
           // int totalDatCho = objWebDLEntities.DatChoes.Count();
            int totalDiemDen = objWebDLEntities.DiemDens.Count();
            int totalTourDuLich = objWebDLEntities.TourDuLiches.Count();
            int totalReservations = objWebDLEntities.Reservations.Count();
          

           // ViewBag.TotalDatCho = totalDatCho;
            ViewBag.TotalDiemDen = totalDiemDen;
            ViewBag.TotalTourDuLich = totalTourDuLich;
            ViewBag.TotalReservations = totalReservations;
      

            return View();
        }
    }

}