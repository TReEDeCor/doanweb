using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;

namespace TReEDeCor.Controllers
{
    public class QuanLyUserController : Controller
    {
        // GET: QuanLyUser
        DatabaseDataContext db = new DatabaseDataContext();
        public ActionResult Index(int? page, string search)
        {
            List<Admin> list = db.Admins.ToList();
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                int pageNumber = (page ?? 1);
                int pageSize = 4;
                list = (db.Admins.ToList().OrderBy(n => n.UserAdmin).ToList());
                if (!String.IsNullOrEmpty(search))
                {
                    list = (db.Admins.Where(x => x.HoTen.Contains(search)).ToList());
                }
                return View(list.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult Details(string id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var admin = from Admin in db.Admins where Admin.UserAdmin == id select Admin;
                return View(admin.SingleOrDefault());
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
                ViewBag.UserAdmin = new SelectList(db.Admins.ToList().OrderBy(n => n.UserAdmin), "UserAdmin","UserAdmin");
           
            return View();
        }
        [HttpPost]
        public ActionResult Create(Admin admin)
        {  
            
            db.Admins.InsertOnSubmit(admin);
            db.SubmitChanges();
            return RedirectToAction("Index", "QuanLyUser");
        }

     
        

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var admin = from Admin in db.Admins where Admin.UserAdmin == id select Admin;
                return View(admin.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult xoasp(string id)
        {
            Admin admin = db.Admins.Where(n => n.UserAdmin == id).SingleOrDefault();
            db.Admins.DeleteOnSubmit(admin);
            db.SubmitChanges();
            return RedirectToAction("Index", "QuanLyUser");
        }
    }
}