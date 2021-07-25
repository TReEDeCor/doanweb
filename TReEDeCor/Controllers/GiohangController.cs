using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.assets;
using TReEDeCor.Models;

namespace TReEDeCor.Controllers
{
    public class GiohangController : Controller
    {
        // GET: Giohang
        DatabaseDataContext data = new DatabaseDataContext();
        public List<Giohang> Laygiohang()
        {
            List<Giohang> listgh = Session["Giohang"] as List<Giohang>;
            if (listgh == null)
            {
                listgh = new List<Giohang>();
                Session["Giohang"] = listgh;
            }
            return listgh;
        }
        public ActionResult Themgiohang(int id, String strURL)
        {
            List<Giohang> listgh = Laygiohang();
            Giohang sp = listgh.Find(n => n.idsp == id);
            if (sp == null)
            {
                sp = new Giohang(id);
                listgh.Add(sp);
                return Redirect(strURL);
            }
            else
            {
                sp.soluong++;
                return Redirect(strURL);
            }
        }
        private int Tongsl()
        {
            int sl = 0;
            List<Giohang> list = Session["Giohang"] as List<Giohang>;
            if (list != null)
            {
                sl = list.Sum(n => n.soluong);
            }
            return sl;
        }
        private double Tongtien()
        {
            double s = 0;
            List<Giohang> list = Session["Giohang"] as List<Giohang>;
            if (list != null)
            {
                s = list.Sum(n => n.tongtien);
            }
            return s;
        }
        //view giohang
        public ActionResult Giohang()
        {
            List<Giohang> list = Laygiohang();
            if (list.Count == 0) return RedirectToAction("Index", "User");
            ViewBag.Tongsoluong = Tongsl();
            ViewBag.Tongtien = Tongtien();
            return View(list);

        }
        //hien thi thong tin gio hang
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = Tongsl();
            ViewBag.Tongtien = Tongtien();
            return PartialView();
        }

     
        //xoa sanpham
        public ActionResult Xoagiohang(int id)
        {
            List<Giohang> list = Laygiohang();
            Giohang sp = list.SingleOrDefault(n => n.idsp == id);
            if (sp != null)
            {
                list.RemoveAll(n => n.idsp == id);
                return RedirectToAction("Giohang");
            }
            if (list.Count == 0)
            {
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult Xoatatca()
        {
            List<Giohang> list = Laygiohang();
            list.Clear();
            return RedirectToAction("Index", "User");
        }
        //cap nhap
        public ActionResult Capnhatgiohang(int id, FormCollection f)
        {
            List<Giohang>list = Laygiohang();
            Giohang sp = list.SingleOrDefault(n => n.idsp == id);
            if (sp != null)
            {
                sp.soluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }

        //dat hang
        public ActionResult Dathang()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"] == "")
            {
                return RedirectToAction("Dangnhap", "Nguoidung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "User");
            }
            List<Giohang> list = Laygiohang();
            ViewBag.Tongsoluong = Tongsl();
            ViewBag.Tongtien = Tongtien();
            return View(list);
        }
        [HttpPost]
        public ActionResult Dathang(FormCollection frm)
        {
            DONDATHANG dh = new DONDATHANG();
            NGUOIDUNG kh = (NGUOIDUNG)Session["Taikhoan"];
            List<Giohang> list = Laygiohang();
            dh.MaKH = kh.MaKH;
            dh.Ngaydat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM//dd/yyyy}", frm["Ngaygiao"]);
            dh.Ngaygiao = DateTime.Parse(ngaygiao);
            dh.Sdtnhanhang = Int32.Parse(kh.Dienthoai);
            dh.Diachigiaohang = kh.Diachi;
            data.DONDATHANGs.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach (var i in list)
            {
                CHITIETDATHANG ct = new CHITIETDATHANG();
                ct.MaDH = dh.MaDH;
                ct.MaSP = i.idsp;
                ct.Soluong = i.soluong;   
                ct.Dongia = (decimal)i.dongia;
                ct.Tonggia = (decimal)i.tongtien;
                data.CHITIETDATHANGs.InsertOnSubmit(ct);
            }
            dh.Thanhtien = (decimal?)Tongtien();
            data.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
        public ActionResult Payment()
        {
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMO15JK20210723";
            string accessKey = "m3vHuZl8qZoxniyv";
            string serectkey = "g90eux9ZDPl6uFqKjZNlCTy0yNWwBO2m";
            string orderInfo = "test";
            string returnUrl = "https://localhost:44321/xac-nhan-don-hang";
            string notifyurl = "http://ba1adf48beba.ngrok.io/Home/SavePayment";
     
            string amount = Tongtien().ToString();
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MOMO crypto = new MOMO();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }


    }
}