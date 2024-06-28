using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDL.Context;

namespace WebDL.Controllers
{
    public class DestinationController : Controller
    {
        // GET: Destination
        WebDLEntities objWebDLEntities = new WebDLEntities();
        public ActionResult Destination()
        {
            var lstDiemDen = objWebDLEntities.DiemDens.ToList();
            return View(lstDiemDen);
        }
    }
}