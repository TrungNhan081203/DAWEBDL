using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDL.Context;

namespace WebDL.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        WebDLEntities objWebDLEntities = new WebDLEntities();
        public ActionResult Contact()
        {
            var lstDiemDen = objWebDLEntities.DiemDens.ToList();
            return View(lstDiemDen);
        }
    }
}