using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.Models;
using System.IO;

namespace TReEDeCor.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DatabaseDataContext db = new DatabaseDataContext();
        public ActionResult Index()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            
            return View();
        }
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["pass"];

            
                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {

                    Session["TKAdmin"] = ad;
                    return RedirectToAction("Index", "Admin");

                }
                else
                    ViewBag.ThongBao = "Tên Đăng Nhập Hoặc Tài Khoản Không Đúng";
            
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //////////////////////////////////
        public ActionResult Isslider()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.SLIDERs.ToList());
        }
        public ActionResult Slider_Details(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var s = from SLIDER in db.SLIDERs where SLIDER.MaSlider == id select SLIDER;
                return View(s.SingleOrDefault());
            }
        }
        [HttpGet]
        public ActionResult Slider_Edit(int id)
        {
            SLIDER slr = db.SLIDERs.SingleOrDefault(n => n.MaSlider == id);
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else if (slr == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                return View(slr);
            }
        }

        [HttpPost, ActionName("Slider_Edit")]
        [ValidateInput(false)]
        public ActionResult capnhatSlider(SLIDER slr, HttpPostedFileBase fileUp)
        {
            SLIDER spUpdate = db.SLIDERs.SingleOrDefault(p => p.MaSlider == slr.MaSlider);
            if (spUpdate != null)
            {
                if (fileUp != null)
                {
                    var fileName = Path.GetFileName(fileUp.FileName);
                    var path = Path.Combine(Server.MapPath("~/Image/Tinmain1/"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        TempData["Error"] = "Hình Đã Tồn Tại!";
                        return RedirectToAction("Edit", new { id = slr.MaSlider });
                    }
                    else
                    {
                        fileUp.SaveAs(path);
                        spUpdate.AnhSP = fileName;
                    }
                }
                spUpdate.LoaiSlider = slr.LoaiSlider;
                spUpdate.TenSP = slr.TenSP;
                spUpdate.CapSP = slr.CapSP;
                spUpdate.Vitri = slr.Vitri;
                spUpdate.Noidung1 = slr.Noidung1;
                spUpdate.Noidung2 = slr.Noidung2;
                spUpdate.Noidung3 = slr.Noidung3;
                UpdateModel(slr);
                db.SubmitChanges();
            }
            return RedirectToAction("Isslider", "Admin");
        }
    }
}