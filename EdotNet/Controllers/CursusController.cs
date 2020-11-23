using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Copie0Web.Models;
using System.Windows.Forms;
using System.Web;
using System.IO;
using AutoMapper;
using System;
using Microsoft.AspNet.Identity;

namespace Copie0Web.Controllers
{
    public class CursusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private IList<Cursus> Parcours = new List<Cursus>();
        

        ////Get: Listes de parcours 
        public ActionResult ListeParcours()
        {
            if (db.Cursus == null)
            {
                return HttpNotFound();
            }
            else
            {
              
                var list1 = db.Cursus.GroupBy(x => x.Titre).Select(y => y.FirstOrDefault()).ToList();
                return PartialView("ListeParcours", list1);
            }

        }
        public ActionResult ListeModulesParcours(int id)
        {
            List<Module> _list = db.Modules.Where(x => x.IdCursus == id).ToList();

            return PartialView("ListeModulesParcours", _list);
        }

        //Inscription
        //Get :Inscription 
        //private IList<Module> ModulePanier;
        //private IList<Module> SessiCursusPanier;
    //    public ActionResult AjoutModule(int? id )
    //    {
    //        if (id == null)
    //        {
    //            return RedirectToAction("ListeParcoursSessions", "Sessions");
    //        }
    //        Cursus cursus = db.Cursus.Find(id);

    //        if (Session["cursus"] == null)
    //        {
    //            CursusPanier = new List<Cursus>();
    //            Session["cursus"] = CursusPanier;
    //        }

    //        SessiCursusPanier = (List<Cursus>)Session["cursus"];
    //        SessiCursusPanier.Add(cursus);
    //        Session["cursus"] = SessiCursusPanier;

    //        List<Cursus> _list = (List<Cursus>)Session["cursus"];

    //        return PartialView("AjoutCursus", _list);

    //    }


    //}
        // GET: Cursus
        public async Task<ActionResult> Index()
        {
            return View(await db.Cursus.ToListAsync());
        }

        // GET: Cursus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cursus cursus = await db.Cursus.FindAsync(id);
            if (cursus == null)
            {
                return HttpNotFound();
            }
            return View(cursus);
        }

        // GET: Cursus/Create
        public ActionResult Create()
        {
            ViewBag.IdSession = new SelectList(db.Sessions, "Id", "Titre");
            return View();
        }

        // POST: Cursus/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdSession,Titre ,DateDebut,DateFin")]Cursus cursus , HttpPostedFileBase Image)
        {
           if (ModelState.IsValid)
            {

                if (Image != null && Image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var path = Path.Combine(("~/images/cursus/"), fileName);
                    //Image.SaveAs(path);
                
                    cursus.Image = path;
                }


                if (cursus.Image == null) cursus.Image = ".net.png";
                db.Cursus.Add(cursus);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cursus);
        }
      
       
        // GET: Cursus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cursus cursus = await db.Cursus.FindAsync(id);
            if (cursus == null)
            {
                return HttpNotFound();
            }
            return View(cursus);
        }

        // POST: Cursus/Edit/
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdSession,Titre,,DateDebut,DateFin")] Cursus cursus , HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var path = Path.Combine(("~/images/cursus/"), fileName);
                    //Image.SaveAs(path);
                    cursus.Image = path;
                }


                if (cursus.Image == null) cursus.Image = ".net.png";
                db.Entry(cursus).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cursus);
        }

        // GET: Cursus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cursus cursus = await db.Cursus.FindAsync(id);
            if (cursus == null)
            {
                return HttpNotFound();
            }
            return View(cursus);
        }

        // POST: Cursus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cursus cursus = await db.Cursus.FindAsync(id);
            db.Cursus.Remove(cursus);
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
