using stoktakipyazilimbakimi.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace stoktakipyazilimbakimi.Controllers
{
    public class MailController : Controller
    {
        private readonly Random _random = new Random();

        [HttpPost]
        public ActionResult sendNewPassword(string email)
        {
            var fromAddress = new MailAddress("atakannyildirim@gmail.com");
            var toAddress = new MailAddress(email);
            string newToken;
            const string subject = "Stok Takip Sistemi | Şifremi Unuttum";
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "741852Gmail")
            })
            {

                using (StokContext stok = new StokContext())
                {
                    Personel personel = stok.Personel.SingleOrDefault(perso => perso.eposta == email);
                    if(personel!=null)
                    {
                        newToken = RandomString(17);
                        personel.token = newToken;
                        personel.tokenExpiredTime = DateTime.Now.AddMinutes(5);
                        stok.SaveChanges();
                        TempData["message"] = "Şifre değiştirme linkini mail adresinize gönderdik.";
                        string resetPasswordUrl = "http://localhost:57115/Reset/Password?token=" + newToken;
                        using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = "Şifreyi Değiştirmek için: " + resetPasswordUrl })
                        {
                            smtp.Send(message);
                        }
                    }
                    else
                    {
                        TempData["message"] = "Böyle Bir email adresi kayıtlı değil";
                    }
                    
                }
            }
            return RedirectToAction("Index", "Login");
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):   
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}