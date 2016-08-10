using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using book.Models;

namespace book.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();

        public async Task<ActionResult> Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = await db.Books.ToListAsync();
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View();
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!";
        }

        public FileResult File()
        {
            string path = Server.MapPath("~/Other/newgame.png");
            byte[] mas = System.IO.File.ReadAllBytes(path);                
            string type = "image";
            string name = "img.png";
            return File(mas, type, name);
        }

        public string Info()
        {
            string browser = HttpContext.Request.Browser.Browser;
            string userAgent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null
                ? ""
                : HttpContext.Request.UrlReferrer.AbsolutePath;
            return "<p>Browser: " + browser + "</p><p>User agent: " + userAgent + "</p>Url: " +
                url + "</p><p>Ip: " + ip + "</p><p>Referrer: " + referrer + "</p>";
        }
    }
}