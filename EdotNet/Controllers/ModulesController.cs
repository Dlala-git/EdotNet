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
using System.IO;
using System.Web.Helpers;

namespace Copie0Web.Controllers
{
    public class ModulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Modules
        public async Task<ActionResult> Index()
        {
            return View(await db.Modules.ToListAsync());
        }

        // GET: Modules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = await db.Modules.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        public ActionResult ListeModules()
        {
            if (db.Modules == null)
            {
                return HttpNotFound();
            }
            else
            {
                return PartialView("ListeModules", db.Modules.ToList());
            }

        }
        // GET: Modules/Create
        public ActionResult Create()
        {
            ViewBag.IdCursus = new SelectList(db.Cursus, "Id", "Titre");
            return View();
            
        }

        // POST: Modules/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,IdCursus,Nom,NombreHeures,DateDebut,DateFin,NomFormateur")] Module module, HttpPostedFileBase LogoModule)
        {
            if (ModelState.IsValid)
            {
                if (LogoModule != null && LogoModule.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(LogoModule.FileName);
                    string path = Path.Combine(("~/images/modules/"), fileName);

                    //LogoModule.SaveAs(path);
                    module.LogoModule = path;
                }
                if (module.LogoModule == null) module.LogoModule = "agile.png";
                db.Modules.Add(module);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(module);
        }

        // GET: Modules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = await db.Modules.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,IdCursus,Nom,NombreHeures,DateDebut,DateFin,NomFormateur")] Module module, HttpPostedFileBase LogoModule)
        {
            if (ModelState.IsValid)
            {
                if (module.LogoModule != null && module.LogoModule.Length > 0)
                {
                    string fileName = Path.GetFileName(LogoModule.FileName);
                    string path = Path.Combine("~/images/modules/", fileName);

                    //LogoModule.SaveAs(path);
                    module.LogoModule = path;
                }
                if (module.LogoModule == null) module.LogoModule = "aspNet.png";
                db.Entry(module).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(module);
        }

        // GET: Modules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = await db.Modules.FindAsync(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Module module = await db.Modules.FindAsync(id);
            db.Modules.Remove(module);
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
