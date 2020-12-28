using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stoktakipyazilimbakimi.Controllers
{
    public class ResetController : Controller
    {
        // GET: Reset
        public ActionResult Password(string token)
        {
            ViewData["token"] = token;
            return View();
        }

        [HttpPost]
        public ActionResult Password(string token, string password)
        {
            using(StokContext stokContext = new StokContext())
            {
                var personel = stokContext.Personel.SingleOrDefault(perso => perso.token == token);
                if(personel!=null)
                {
                    personel.sifre = password;
                    personel.token = null;
                    stokContext.SaveChanges();
                    TempData["message"] = "Şifreniz Güncellendi";
                }
                else
                    TempData["message"] = "Tek kullanımlık linkin geçerliliği kalmamıştır";

            }
            return RedirectToAction("Index", "Login");
        }
    }
}