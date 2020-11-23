using Copie0Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web.Mvc;

namespace Copie0Web.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private IList<Commentaires> Commentaires = new List<Commentaires>();
      
        public ActionResult Index()
        {
          


            return View();
        }

      
        public ActionResult About()
        {
            //Commentaires commentaire = new Commentaires();
            //  // db.Commentaires.Add(commentaires);
            //     db.SaveChanges();

                //return RedirectToAction("Index");
           

            //List<Commentaires> _list = db.Commentaires.ToList();
            
            return View();
        }
       
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}