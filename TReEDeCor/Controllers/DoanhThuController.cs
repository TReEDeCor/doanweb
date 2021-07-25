using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;

namespace TReEDeCor.Controllers
{
    public class DoanhThuController : Controller
    {
        DatabaseDataContext db = new DatabaseDataContext();
        // GET: DoanhThu
        public ActionResult Index()
        {
            return View(db.DONDATHANGs.ToList());
        }
    }
}