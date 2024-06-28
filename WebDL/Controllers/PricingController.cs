using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDL.Context;

namespace WebDL.Controllers
{
    public class PricingController : Controller
    {
        // GET: Pricing
        
        WebDLEntities objWebDLEntities = new WebDLEntities();
        public ActionResult Pricing()
        {
            var lstDiemDen = objWebDLEntities.DiemDens.ToList();
            return View(lstDiemDen);
        }

        public ActionResult PricingDetails()
        {
            var lstDiemDen = objWebDLEntities.DiemDens.ToList();
            return View(lstDiemDen);
        }
        public ActionResult PricingDetailsHanoi()
        {
            var lstDiemDen = objWebDLEntities.DiemDens.ToList();
            return View(lstDiemDen);
        }
        public ActionResult PricingDetailsKorea()
        {
            var lstDiemDen = objWebDLEntities.DiemDens.ToList();
            return View(lstDiemDen);
        }
    }
}