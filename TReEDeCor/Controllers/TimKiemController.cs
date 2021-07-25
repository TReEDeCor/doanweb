using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;

namespace TReEDeCor.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        DatabaseDataContext data = new DatabaseDataContext();
        public ActionResult TimKiemSP(string tk)
        {
            var timkiem = data.SANPHAMs.Where(x => x.TenSP.Contains(tk));
            return View(timkiem);
        }
    }
}