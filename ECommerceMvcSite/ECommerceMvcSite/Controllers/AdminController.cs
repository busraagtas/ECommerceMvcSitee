using ECommerceMvcSite.Models;
using System.Linq;
using System.Web.Mvc;

public class AdminController : Controller
{
    private MyDbContext db = new MyDbContext();

    // Login sayfası
    public ActionResult Login()
    {
        return View();
    }

    // Giriş işlemi
    [HttpPost]
    public ActionResult Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var admin = db.Admins.FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);
            if (admin != null)
            {
                Session["AdminId"] = admin.Id;
                return RedirectToAction("AdminPanel");
            }
            else
            {
                ViewBag.ErrorMessage = "Hatalı email veya şifre!";
            }
        }
        return View(model);
    }
    // Admin Dashboard
    public ActionResult AdminPanel()
    {
        // Admin girişi kontrolü
        if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
        {
            return RedirectToAction("Login", "Admin");
        }

        // Ürünler ve admin işlemleri
        var products = db.Products.ToList();
        return View(products);
    }
}
