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
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI;
using PusherServer;

namespace Copie0Web.Controllers
{
    public class CommentairesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Commentaires
        public async Task<ActionResult> Index()
        {
            return View(await db.Commentaires.ToListAsync());
        }




        // GET: Commentaires/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commentaires commentaires = await db.Commentaires.FindAsync(id);
            if (commentaires == null)
            {
                return HttpNotFound();
            }
            return View(commentaires);
        }
        //[Authorize(Roles = "Stagiaire")]
        //[Authorize(Roles = "Formateur")]
        // GET: Commentaires/Create
        public ActionResult Create()
        {
            var result = db.Commentaires.ToList();
            //_list = result;
            Session["commentaire"] = result;
            return View();
        }

        // POST: Commentaires/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        public IList<Commentaires> Témoignages;
        public IList<Commentaires> SessionTémoignages;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nom,Comments")] Commentaires commentaires)

        { 
            if (ModelState.IsValid)
            {
                db.Commentaires.Add(commentaires);
                await db.SaveChangesAsync();

                //return RedirectToAction("Index");
            }
           
            List<Commentaires> _list = db.Commentaires.ToList();
            Session["commentaire"] = _list;
                return View();
        }

        // GET: Commentaires/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commentaires commentaires = await db.Commentaires.FindAsync(id);
            if (commentaires == null)
            {
                return HttpNotFound();
            }
            return View(commentaires);
        }

        // POST: Commentaires/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nom,Comments")] Commentaires commentaires)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentaires).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(commentaires);
        }

        // GET: Commentaires/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commentaires commentaires = await db.Commentaires.FindAsync(id);
            if (commentaires == null)
            {
                return HttpNotFound();
            }
            return View(commentaires);
        }

        // POST: Commentaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Commentaires commentaires = await db.Commentaires.FindAsync(id);
            db.Commentaires.Remove(commentaires);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
