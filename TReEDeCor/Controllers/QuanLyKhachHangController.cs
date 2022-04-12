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
    public class QuanLyKhachHangController : Controller
    {
        DatabaseDataContext db = new DatabaseDataContext();
        // GET: QuanLyKhachHang
        public ActionResult Index(int? page, string search)
        {
            List<NGUOIDUNG> list = db.NGUOIDUNGs.ToList();
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                int pageNumber = (page ?? 1);
                int pageSize = 4;
                list = (db.NGUOIDUNGs.ToList().OrderBy(n => n.MaKH).ToList());
                if (!String.IsNullOrEmpty(search))
                {
                    list = (db.NGUOIDUNGs.Where(x => x.HoTen.Contains(search)).ToList());
                }
                return View(list.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult Details(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var user = from NGUOIDUNG in db.NGUOIDUNGs where NGUOIDUNG.MaKH == id select NGUOIDUNG;
                return View(user.SingleOrDefault());
            }
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var user = from NGUOIDUNG in db.NGUOIDUNGs where NGUOIDUNG.MaKH == id select NGUOIDUNG;
                return View(user.SingleOrDefault());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult xoasp(int id)
        {
            NGUOIDUNG user = db.NGUOIDUNGs.Where(n => n.MaKH == id).SingleOrDefault();
            db.NGUOIDUNGs.DeleteOnSubmit(user);
            db.SubmitChanges();
            return RedirectToAction("Index", "QuanLyNguoiDung");
        }
    }
}