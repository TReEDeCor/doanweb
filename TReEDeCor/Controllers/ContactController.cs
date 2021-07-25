using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;

namespace TReEDeCor.Controllers
{
    public class ContactController : Controller
    {
        DatabaseDataContext db = new DatabaseDataContext();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(FormCollection collection, LIENHE cont)
        {
            var HoTen = collection["name"];

            var Email = collection["Email"];

            var Phone = collection["phone"];

            var Ghichu = collection["message"];

            if (String.IsNullOrEmpty(HoTen))
            {
                ViewData["loi1"] = "Không được để trống";
            }

            else if (String.IsNullOrEmpty(Email))
            {
                ViewData["loi2"] = "Không được để trống";
            }
            else if (String.IsNullOrEmpty(Phone))
            {
                ViewData["loi3"] = "Không được để trống";
            }
            else if (String.IsNullOrEmpty(Ghichu))
            {
                ViewData["loi4"] = "Không được để trống";
            }

            else
            {

                cont.HoTen = HoTen;
                cont.Email = Email;
                cont.Phone = Phone;
                cont.Ghichu = Ghichu;
                db.LIENHEs.InsertOnSubmit(cont);
                db.SubmitChanges();


            }

            return this.Contact();
        }

    }
}