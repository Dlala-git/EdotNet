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
using Microsoft.AspNet.Identity;

namespace Copie0Web.Controllers
{
    public class InscriptionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //private Utilisateur utilisateur = new Utilisateur();
        //private Cursus Cursus = new Cursus();
        // GET: Inscriptions
        public async Task<ActionResult> Index()
        {
 
            return View(await db.Inscriptions.ToListAsync());
        }


        // GET: Inscriptions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = await db.Inscriptions.FindAsync(id);
       
            if (inscription == null)
            {
                return HttpNotFound();
            }
            return View(inscription);
        }

        // GET: Inscriptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inscriptions/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DateInscription")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
                db.Inscriptions.Add(inscription);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(inscription);
        }

        // GET: Inscriptions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = await db.Inscriptions.FindAsync(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            return View(inscription);
        }

        // POST: Inscriptions/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DateInscription")] Inscription inscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inscription).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(inscription);
        }

        // GET: Inscriptions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inscription inscription = await db.Inscriptions.FindAsync(id);
            if (inscription == null)
            {
                return HttpNotFound();
            }
            return View(inscription);
        }

        // POST: Inscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Inscription inscription = await db.Inscriptions.FindAsync(id);
            db.Inscriptions.Remove(inscription);
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
