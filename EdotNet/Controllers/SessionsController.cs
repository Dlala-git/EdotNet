using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Copie0Web.Models;
using System.Windows.Forms;
using Microsoft.AspNet.Identity;
using System.Net.Sockets;

namespace Copie0Web.Controllers
{
    public class SessionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
     
        //Get:Liste des sessions
        public ActionResult ListeSessions()
        {
            Session["cursus"] = null;
            if (db.Sessions == null)
            {
                return HttpNotFound();
            }
            else
            {
                return PartialView("ListeSessions", db.Sessions.ToList());
            }


        }
        //liste des modules d'un parcours appartient à une session définie
        public ActionResult ListeModulesParcoursSessions(int?id)
        {

           List<Module> _list1 = new List<Module>();

           
            var result = (from m in db.Modules
                          where m.IdCursus == id
                          select m).ToList();
            _list1 = result.ToList();
        
            return PartialView("ListeModulesParcoursSessions", _list1);
        }
        //Get :Choisir un cursus  
        public IList<Cursus> CursusPanier;
        public IList<Cursus> SessionCursusPanier;

        public ActionResult AjoutCursus(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ListeParcoursSessions", "Sessions");
            }
            Cursus cursus = db.Cursus.Find(id); 

            if (Session["cursus"] == null)
            {
                CursusPanier = new List<Cursus>();
                Session["cursus"] = CursusPanier;
            }
         
           
            SessionCursusPanier = (List<Cursus>)Session["cursus"];
          

            SessionCursusPanier.Add(cursus);
            Session["cursus"] = SessionCursusPanier.Distinct().ToList();
            List<Cursus> _list = new List<Cursus>();
            if (Session["cursus"] != null)
            {
              _list = (List<Cursus>)Session["cursus"];
                while (index < _list.Count - 1)
                {
                    if (_list[index].Id == _list[index + 1].Id)
                        _list.RemoveAt(index);
                    else
                        index++;
                }
                
            }
           
            List<Cursus> list1 = _list.Distinct().ToList();

            return PartialView("AjoutCursus", list1);
        
        }
        

        //Choisir les modules 
        private IList<Module> ModulePanier;
        private IList<Module> SessionModulePanier;
        private int index;

        public ActionResult AjoutModule(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ListeModulesParcoursSessions", "Sessions");
            }
            Module module = db.Modules.Find(id);

            if (Session["module"] == null)
            {
                ModulePanier = new List<Module>();
               
            }

            SessionModulePanier = (List<Module>)Session["module"];
            SessionModulePanier.Add(module);
            Session["module"] = SessionModulePanier;

            List<Module> _list = new List<Module>();
            if (Session["module"] != null)
            {
                _list = (List<Module>)Session["module"];
                while (index < _list.Count - 1)
                {
                    if (_list[index].Id == _list[index + 1].Id)
                        _list.RemoveAt(index);
                    else
                        index++;
                }

            }


            List<Module> list1 = _list.Distinct().ToList();

            return PartialView("AjoutModule", list1);

        }

        
        [AllowAnonymous]
        public ActionResult SupprimerCursus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SessionCursusPanier = (List<Cursus>)Session["cursus"];
            Cursus cursus = SessionCursusPanier.Where(p => p.Id == id).SingleOrDefault();
            SessionCursusPanier.Remove(cursus);

            return RedirectToAction("ListeParcours", "Cursus");
        }

        [AllowAnonymous]
        public ActionResult SupprimerModule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SessionModulePanier = (List<Module>)Session["module"];
            Module module = SessionModulePanier.Where(p => p.Id == id).SingleOrDefault();
            SessionModulePanier.Remove(module);


            return View("AjoutModule", SessionModulePanier);
        }
    
        //[Authorize(Roles = "Stagiaire")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //Get
        public ActionResult ListeParcourSessions(int id)
        {
            List<Cursus> list = db.Cursus.Where(x => x.IdSession == id).ToList();
            var list1 = list.GroupBy(x => x.Titre).Select(y => y.FirstOrDefault()).ToList();


            return PartialView("ListeParcourSessions", list1);
            
        }

        // GET: Sessions
        public async Task<ActionResult> Index()
        {
            return View(await db.Sessions.ToListAsync());
        }

        // GET: Sessions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = await db.Sessions.FindAsync(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // GET: Sessions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sessions/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Titre,Description,DateDebut,DateFin")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Sessions.Add(session);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(session);
        }

        // GET: Sessions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = await db.Sessions.FindAsync(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Titre,Description,DateDebut,DateFin")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(session);
        }

        // GET: Sessions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = await db.Sessions.FindAsync(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Session session = await db.Sessions.FindAsync(id);
            db.Sessions.Remove(session);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
