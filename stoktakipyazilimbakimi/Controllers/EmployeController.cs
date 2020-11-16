using stoktakipyazilimbakimi.filters;
using stoktakipyazilimbakimi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace stoktakipyazilimbakimi.Controllers
{
    [UserAuthenticationFilter]
    public class EmployeController : Controller
    {
        public ActionResult Index()
        {
            List<Personel> personeller;
            using (StokContext ctx = new StokContext())
            {
                personeller = ctx.Personel.ToList();
            }
            return View(personeller);
        }

        public ActionResult Edit(string tcno)
        {
            Personel personel;
            using (StokContext stok = new StokContext())
            {
                personel = stok.Personel.Find(tcno);
            }
            return View(personel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Personel personel)
        {
            if (!ModelState.IsValidField("sifre"))
            {
                StokContext stok = new StokContext();
                Personel old = stok.Personel.Find(personel.tcno);
                ModelState.SetModelValue("sifre", new ValueProviderResult(old.sifre, "", CultureInfo.InvariantCulture));
            }

            if (ModelState.IsValid)
            {
                using (StokContext stok = new StokContext())
                {
                    personel = stok.Personel.Add(personel);
                    stok.Entry(personel).State = System.Data.Entity.EntityState.Modified;
                    stok.SaveChanges();
                }
                TempData["message"] = "Personel güncellendi";
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit", new { tcno = personel.tcno });
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(Personel personel)
        {
            using (StokContext stok = new StokContext())
            {
                stok.Personel.Add(personel);
                stok.SaveChanges();
            }
            TempData["message"] = "Yeni personel eklendi";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string tcno)
        {
            using (StokContext stok = new StokContext())
            {
                var personel = stok.Personel.Find(tcno);
                stok.Personel.Remove(personel);
                stok.SaveChanges();
            }
            TempData["message"] = "Başarıyla Silindi";
            return RedirectToAction("Index");
        }
    }
}