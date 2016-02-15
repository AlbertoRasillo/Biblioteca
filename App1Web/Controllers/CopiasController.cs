using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App1Web.Models;

namespace App1Web.Controllers
{
    public class CopiasController : Controller
    {
        private bibliotecaEntities db = new bibliotecaEntities();

        // GET: Copias
        public ActionResult Index()
        {
            var copias = db.Copias.Include(c => c.Obra);
            return View(copias.ToList());
        }

        // GET: Copias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Copias copias = db.Copias.Find(id);
            if (copias == null)
            {
                return HttpNotFound();
            }
            return View(copias);
        }

        // GET: Copias/Create
        public ActionResult Create()
        {
            ViewBag.id_obra = new SelectList(db.Obra, "id_obra", "nombre");
            return View();
        }

        // POST: Copias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "n_copia,id_obra,comentarios")] Copias copias)
        {
            if (ModelState.IsValid)
            {
                db.Copias.Add(copias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_obra = new SelectList(db.Obra, "id_obra", "id_obra", copias.id_obra);
            return View(copias);
        }

        // GET: Copias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Copias copias = db.Copias.Find(id);
            if (copias == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_obra = new SelectList(db.Obra, "id_obra", "nombre", copias.id_obra);
            return View(copias);
        }

        // POST: Copias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "n_copia,id_obra,comentarios")] Copias copias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(copias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_obra = new SelectList(db.Obra, "id_obra", "nombre", copias.id_obra);
            return View(copias);
        }

        // GET: Copias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Copias copias = db.Copias.Find(id);
            if (copias == null)
            {
                return HttpNotFound();
            }
            return View(copias);
        }

        // POST: Copias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Copias copias = db.Copias.Find(id);
            db.Copias.Remove(copias);
            db.SaveChanges();
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
