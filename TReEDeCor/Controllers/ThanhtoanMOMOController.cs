using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TReEDeCor.assets;

namespace TReEDeCor.Controllers
{
    public class ThanhtoanMOMOController : Controller
    {
        // GET: ThanhtoanMOMO
        public ActionResult Payment()
        {
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMO15JK20210723";
            string accessKey = "m3vHuZl8qZoxniyv";
            string serectkey = "g90eux9ZDPl6uFqKjZNlCTy0yNWwBO2m";
            string orderInfo = "test";
            string returnUrl = "https://localhost:44321/ThanhtoanMOMO/thanhtoantructuyen"; //localhost:44321
            string notifyurl = "http://ba1adf48beba.ngrok.io/Home/SavePayment"; 

            string amount = "ViewBag.Tongtien";
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

        public ActionResult thanhtoantructuyen()
        {
            return View();
        }

        [HttpPost]
        public void SavePayment()
        {
            //c?p nh?t d? li?u vào db
        }
    }
}
