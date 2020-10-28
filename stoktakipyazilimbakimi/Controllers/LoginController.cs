using stoktakipyazilimbakimi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public RedirectToRouteResult Attempt(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (StokContext db = new StokContext())
                {
                    var obj = db.Personel.Where(a => a.tcno.Equals(model.tcno) && a.sifre.Equals(model.sifre)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.tcno.ToString();
                        Session["UserName"] = obj.ad.ToString();
                        Session["UserLastName"] = obj.soyad.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {

                    }
                }
            }
            TempData["message"] = "Böyle bir kullanıcı bulunamadı";
            return RedirectToAction("Index", "Login");
        }

        public RedirectToRouteResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}