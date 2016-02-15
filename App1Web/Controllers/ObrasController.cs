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
    public class ObrasController : Controller
    {
        private bibliotecaEntities db = new bibliotecaEntities();

        // GET: Obras
        public ActionResult Index()
        {
            var obra = db.Obra.Include(o => o.Cd_Dvd).Include(o => o.Libro);
            return View(obra.ToList());
        }

        // GET: Obras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            return View(obra);
        }

        // GET: Obras/Create
        public ActionResult Create()
        {
            ViewBag.id_obra = new SelectList(db.Cd_Dvd, "id_obra", "id_obra");
            ViewBag.id_obra = new SelectList(db.Libro, "id_obra", "isbn");
            return View();
        }

        // POST: Obras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_obra,nombre,fecha_publi,categoria,n_ejemplares")] Obra obra,
            [Bind(Include = "id_obra,isbn")] Libro libro, [Bind(Include = "n_copia,id_obra,comentarios")] Copias copias)
        {
            if (ModelState.IsValid)
            {
                db.Obra.Add(obra);
                db.Libro.Add(libro);
                db.Copias.Add(copias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_obra = new SelectList(db.Cd_Dvd, "id_obra", "id_obra", obra.id_obra);
            ViewBag.id_obra = new SelectList(db.Libro, "id_obra", "id_obra", obra.id_obra);
            return View(obra);
        }

        // GET: Obras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_obra = new SelectList(db.Cd_Dvd, "id_obra", "id_obra", obra.id_obra);
            ViewBag.id_obra = new SelectList(db.Libro, "id_obra", "isbn", obra.id_obra);
            return View(obra);
        }

        // POST: Obras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_obra,nombre,fecha_publi,categoria,n_ejemplares")] Obra obra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_obra = new SelectList(db.Cd_Dvd, "id_obra", "id_obra", obra.id_obra);
            ViewBag.id_obra = new SelectList(db.Libro, "id_obra", "isbn", obra.id_obra);
            return View(obra);
        }

        // GET: Obras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            return View(obra);
        }

        // POST: Obras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Obra obra = db.Obra.Find(id);
            db.Obra.Remove(obra);
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
