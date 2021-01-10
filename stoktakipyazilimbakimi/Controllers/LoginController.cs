using stoktakipyazilimbakimi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace stoktakipyazilimbakimi.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> Attempt(Personel model)
        {
            if(Session["attemptCount"]!=null && Int32.Parse(Session["attemptCount"].ToString()) > 2)
            {
                using (HttpClient client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        { "secret", "6Lc-tycaAAAAAHKxSYPlSnhonSlsY0lxTJ18qPS5" },
                        { "response", Request.Params["g-recaptcha-response"]},
                        { "remoteip", Request.UserHostAddress},
                    };
                    var content = new FormUrlEncodedContent(values);             
                    var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var parsed = JObject.Parse(responseString);
                    if(!(bool)parsed["success"])
                    {
                        TempData["message"] = "Lütfen robot olmadığınızı kanıtlayınız / Hatalı Girişim: " + Session["attemptCount"];
                        return RedirectToAction("Index", "Login");
                    }
                }

            }

            if (ModelState.IsValidField("tcno") && ModelState.IsValidField("sifre"))
            {
                using (StokContext db = new StokContext())
                {
                    var obj = db.Personel.Where(a => a.tcno.Equals(model.tcno) && a.sifre.Equals(model.sifre)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["attemptCount"] = null;
                        Session["UserID"] = obj.tcno.ToString();
                        Session["UserName"] = obj.ad.ToString();
                        Session["UserLastName"] = obj.soyad.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            Session["attemptCount"] = Session["attemptCount"] != null ? Int32.Parse(Session["attemptCount"].ToString()) + 1 : 1;
            TempData["message"] = "Böyle bir kullanıcı bulunamadı / Hatalı Girişim: " + Session["attemptCount"];
            return RedirectToAction("Index", "Login");
        }

        public RedirectToRouteResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}