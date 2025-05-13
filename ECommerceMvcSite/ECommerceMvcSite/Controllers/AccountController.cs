using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ECommerceMvcSite.Models;

namespace ECommerceMvcSite.Controllers
{
    public class AccountController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // Giriş Sayfası (GET)
        public ActionResult Login()
        {
            return View();
        }

        // Giriş Sayfası (POST)
        [HttpPost]
        public ActionResult Login(string email, string password, bool? isAdmin)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Geçersiz giriş bilgileri!";
                return View();
            }

            Session["UserId"] = user.Id;
            Session["Username"] = user.Username;
            Session["IsAdmin"] = user.IsAdmin;
            Session["UserEmail"] = user.Email;
            Session["UserFirstName"] = user.FirstName;
            Session["UserLastName"] = user.LastName;

            if (user.IsAdmin)
            {
                return RedirectToAction("AdminPanel", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        // Kayıt Sayfası (GET)
        public ActionResult Register()
        {
            return View();
        }

        // Kayıt Sayfası (POST)
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.Email == user.Email))
                {
                    ViewBag.Error = "Bu e-posta adresi ile daha önce kayıt olunmuş.";
                    return View(user);
                }

                if (db.Users.Any(u => u.Username == user.Username))
                {
                    ViewBag.Error = "Bu kullanıcı adı ile daha önce kayıt olunmuş.";
                    return View(user);
                }

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Profile()
        {
            string email = Session["UserEmail"]?.ToString();
            if (email == null) return RedirectToAction("Login");

            var user = db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return RedirectToAction("Login");

            return View(user);
        }

        public ActionResult MyOrders()
        {
            if (Session["UserEmail"] == null)
                return RedirectToAction("Login");

            string email = Session["UserEmail"].ToString();

            var orders = db.Orders
                           .Where(o => o.UserEmail == email && !o.IsCancelled)
                           .Include(o => o.Items.Select(i => i.Product))
                           .ToList();

            return View("ConfirmedOrders", orders);
        }

        public ActionResult AdminPanel()
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
            {
                return RedirectToAction("Login");
            }

            var products = db.Products.ToList();
            return View(products);
        }
    }
}
