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
    public class Cd_DvdController : Controller
    {
        private bibliotecaEntities db = new bibliotecaEntities();

        // GET: Cd_Dvd
        public ActionResult Index()
        {
            var cd_Dvd = db.Cd_Dvd.Include(c => c.Obra);
            return View(cd_Dvd.ToList());
        }

        // GET: Cd_Dvd/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cd_Dvd cd_Dvd = db.Cd_Dvd.Find(id);
            if (cd_Dvd == null)
            {
                return HttpNotFound();
            }
            return View(cd_Dvd);
        }

        // GET: Cd_Dvd/Create
        public ActionResult Create()
        {
            ViewBag.id_obra = new SelectList(db.Obra, "id_obra", "nombre");
            return View();
        }

        // POST: Cd_Dvd/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_obra,duracion")] Cd_Dvd cd_Dvd)
        {
            if (ModelState.IsValid)
            {
                db.Cd_Dvd.Add(cd_Dvd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_obra = new SelectList(db.Obra, "id_obra", "nombre", cd_Dvd.id_obra);
            return View(cd_Dvd);
        }

        // GET: Cd_Dvd/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cd_Dvd cd_Dvd = db.Cd_Dvd.Find(id);
            if (cd_Dvd == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_obra = new SelectList(db.Obra, "id_obra", "nombre", cd_Dvd.id_obra);
            return View(cd_Dvd);
        }

        // POST: Cd_Dvd/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_obra,duracion")] Cd_Dvd cd_Dvd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cd_Dvd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_obra = new SelectList(db.Obra, "id_obra", "nombre", cd_Dvd.id_obra);
            return View(cd_Dvd);
        }

        // GET: Cd_Dvd/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cd_Dvd cd_Dvd = db.Cd_Dvd.Find(id);
            if (cd_Dvd == null)
            {
                return HttpNotFound();
            }
            return View(cd_Dvd);
        }

        // POST: Cd_Dvd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cd_Dvd cd_Dvd = db.Cd_Dvd.Find(id);
            db.Cd_Dvd.Remove(cd_Dvd);
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
