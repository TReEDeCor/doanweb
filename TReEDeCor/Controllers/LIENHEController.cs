using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;

namespace TReEDeCor.Controllers
{
    public class LIENHEController : Controller
    {
        // GET: LIENHE
        DatabaseDataContext db = new DatabaseDataContext();

        public ActionResult Index()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
           
            return View(db.LIENHEs.ToList());
        }
       


    }
}