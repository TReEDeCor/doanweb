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
        public ActionResult Index(String search)
        {
            List<DONDATHANG> list = db.DONDATHANGs.ToList();
            if (!String.IsNullOrEmpty(search))
            {
                list = db.DONDATHANGs.Where(x => x.MaKH.ToString().Contains(search)).ToList();
            }
            return View(list);
        }
    }
}