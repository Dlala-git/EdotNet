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
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Copie0Web.Controllers
{
    public class UtilisateursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<string> SessionCours { get; private set; }


        // GET: Utilisateurs
        public async Task<ActionResult> Index()
        {
            return View(await db.Utilisateurs.ToListAsync());
        }

        // GET: Utilisateurs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = await db.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }
        //Espace Formateur Get 
        public ActionResult EspaceFormateur()
        {
            string IdIdentityFramework = System.Web.HttpContext.Current.User.Identity.GetUserId();
            Utilisateur utilisateur = db.Utilisateurs.Where(u => u.IdIdentityFramework == IdIdentityFramework).First();
            Session["formateur"] = utilisateur;
            List<Module> _list = new List<Module>();
            
            var result = (from elt in db.Modules

                          where elt.NomFormateur == utilisateur.Prenom
                          select elt
                        ).ToList();
            List<Module> _list1 = result.ToList();
            Session["module"] = _list1;

            List<string> items = GetFiles();

            return View(items);
        }

        // Espace Formateur :
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EspaceFormateur(HttpPostedFileBase file)
        {
            //telecharger les documents
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Cours"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            }
            else
            {
                ViewBag.Message = "Aucun fichier telechargé ";
            }

            var items = GetFiles();
         
      
            return View("EspaceFormateur",items);
          
        }

        public FileResult Download(string DocName)
        {
            var FileVirtualPath = "~/Cours/" + DocName;
            return File(FileVirtualPath, "application/force- download", Path.GetFileName(FileVirtualPath));
        }

        private List<string> GetFiles()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Cours"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");

            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }

            return items;
        }




        //GET
        //espace Stagiaire 
        public ActionResult EspaceStagiaire()
        {
            if (Request.IsAuthenticated)
            {
                string IdIdentityFramework = System.Web.HttpContext.Current.User.Identity.GetUserId();
                Utilisateur utilisateur = db.Utilisateurs.Where(u => u.IdIdentityFramework == IdIdentityFramework).First();

                //IList<Inscription> inscription = db.Inscriptions.Where(p => p.Utilisateur.Id == utilisateur.Id).ToList();
                Session["Stagiaire"] = utilisateur;
                //var rm = new Microsoft.AspNet.Identity.RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                //var um = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())) ;
               
                IList<Inscription> list1 = new List<Inscription>();

                List<Inscription> result = (from elt in db.Inscriptions
                                            where (elt.Utilisateur.Id == utilisateur.Id)
                                            select elt).ToList();
                list1 = result;


                Session["inscription"] = list1;

                    // return RedirectToAction("Details", "Inscriptions",inscription);
                   return View(list1);
               }
            //else

                

            //if (inscription != null)
            //{
            //    return View(inscription);

            //}

            return View();
        }
       
        //Get Candidature 
        public ActionResult Candidature()
        {
          

            if (Request.IsAuthenticated)
            {
               
                string IdIdentityFramework = System.Web.HttpContext.Current.User.Identity.GetUserId();
                Utilisateur utilisateur = db.Utilisateurs.Where(u => u.IdIdentityFramework == IdIdentityFramework).First();
                Inscription inscription = db.Inscriptions.Where(p => p.Utilisateur.Id == utilisateur.Id).FirstOrDefault();
                List<Cursus> _list = (List<Cursus>)Session["cursus"];

                if (_list != null)
                {

                    inscription.Parcours = new List<Cursus>();
                    for (int i = 0; i < _list.Count(); i++)
                    {
                        Cursus cursus = new Cursus
                        {
                            Id = _list[i].Id,
                            Titre = _list[i].Titre,
                            IdSession = _list[i].IdSession,
                            Image = _list[i].Image,
                            DateDebut = _list[i].DateDebut,
                            DateFin = _list[i].DateFin
                        };
                        inscription.Parcours.Add(cursus);
                        inscription.Utilisateur = utilisateur;
                    }


                    inscription.DateInscription = DateTime.Now;
                    db.Inscriptions.Add(inscription);
                    db.SaveChanges();


                    return RedirectToAction("Details", "Inscriptions",inscription);
                }
               
            }
            return RedirectToAction("Index","Home");
        }
        // GET: Utilisateurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utilisateurs/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,Prenom,DateNaissance,LieuNaissance,Nationalité,NiveauEtude,StatutActuel,Diplome,Catégorie,Adresse,ComplementAdresse,CodePostal,Ville,TelephoneMobile")] Utilisateur utilisateur, HttpPostedFileBase CV)
        {
            if (ModelState.IsValid)
            {
                if (CV.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(CV.FileName);
                        string filePath = Path.Combine(Server.MapPath("/CV/"), fileName);
                        CV.SaveAs(filePath);
                         utilisateur.CV = filePath;
                    }
              
                utilisateur.IdIdentityFramework = System.Web.HttpContext.Current.User.Identity.GetUserId();
                db.Utilisateurs.Add(utilisateur);

                db.SaveChanges();
                Session["Utilisateur"] = utilisateur;
                List<Cursus> _list = (List<Cursus>)Session["cursus"];

                if (_list != null)
                {
                    Inscription inscription = new Inscription();
                    inscription.Parcours = new List<Cursus>();
                    for (int i = 0; i < _list.Count(); i++)
                    {
                        Cursus cursus = new Cursus();

                        cursus.Id = _list[i].Id;
                        cursus.Titre = _list[i].Titre;
                        cursus.IdSession = _list[i].IdSession;
                        cursus.Image = _list[i].Image;
                        cursus.DateDebut = _list[i].DateDebut;
                        cursus.DateFin = _list[i].DateFin;
                        inscription.Parcours.Add(cursus);
                        inscription.Utilisateur = utilisateur;
                    }


                    inscription.DateInscription = DateTime.Now;
                    db.Inscriptions.Add(inscription);

                    Session["InscriptionCursus"] = inscription;
                    db.SaveChanges();
                
       
                    return RedirectToAction("Details" ,"Inscriptions", inscription);
                }
                List<Module> _list1 = (List<Module>)Session["module"];
                if (_list1 != null)
                {
                    Inscription inscription = new Inscription();
                    inscription.Modules = new List<Module>();
                    for (int i = 0; i < _list1.Count(); i++)
                    {
                        Module module = new Module();

                        module.Id = _list1[i].Id;
                        module.Nom = _list1[i].Nom;
                        module.NombreHeures = _list1[i].NombreHeures;
                        module.NomFormateur = _list1[i].NomFormateur;
                        module.LogoModule = _list1[i].LogoModule;
                        module.IdCursus = _list1[i].IdCursus;
                        module.DateDebut = _list1[i].DateDebut;
                        module.DateFin = _list1[i].DateFin;

                        inscription.Modules.Add(module);
                        inscription.Utilisateur = utilisateur;

                    }

                    Session["InscriptionModules"] = inscription;
                    inscription.DateInscription = DateTime.Now;
                    db.Inscriptions.Add(inscription);

                    db.SaveChanges();
                    Session["module"] = null;
                    return RedirectToAction("Details", "Inscriptions" , inscription);
                }
            }


           return RedirectToAction("Index","Utilisateurs");
            //return RedirectToAction("Details", "Inscriptions");

        }

     

         
       
       
        // GET: Utilisateurs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = await db.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nom,Prenom,DateNaissance,LieuNaissance,Nationalité,NiveauEtude,StatutActuel,Diplome,Catégorie,CV,Adresse,ComplementAdresse,CodePostal,Ville,TelephoneMobile")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilisateur).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(utilisateur);
        }

        // GET: Utilisateurs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = await db.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Utilisateur utilisateur = await db.Utilisateurs.FindAsync(id);
            db.Utilisateurs.Remove(utilisateur);
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
