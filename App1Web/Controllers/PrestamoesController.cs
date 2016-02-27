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
    public class PrestamoesController : Controller
    {
        private bibliotecaEntities db = new bibliotecaEntities();

        // GET: Prestamoes
        public ActionResult Index()
        {   //Añadidodo p.Copias.Obra -Obra solo
            if ((String)Session["rol"] == "user")
            {
                String nom = Session["nombre"].ToString();
                var prestamo = db.Prestamo.Include(p => p.Copias.Obra).Include(p => p.Usuarios);
                prestamo = prestamo.Where(p => p.Usuarios.nombre.Contains(nom));
                return View(prestamo.ToList());
            }
            else {
                var prestamo = db.Prestamo.Include(p => p.Copias.Obra).Include(p => p.Usuarios);
                return View(prestamo.ToList());
            }
        }

        // GET: Prestamoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // GET: Prestamoes/Create
        public ActionResult Create()
        {
            ViewBag.id_obra = new SelectList(db.Obra, "id_obra", "nombre");
            ViewBag.cod_socio = new SelectList(db.Usuarios, "cod_socio", "usuario");
            return View();
        }

        // POST: Prestamoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_prestamo,cod_socio,n_copia,tipo_prestamo,fecha_prestamo,fecha_devolucion,fecha_tope")] Prestamo prestamo,
            [Bind(Include = "n_copia,id_obra,comentarios")] Copias copia )
        {
            if (ModelState.IsValid)
            {
                foreach(Copias elemnt in db.Copias)
                {
                    if(elemnt.id_obra == copia.id_obra)
                    {
                        prestamo.n_copia = elemnt.n_copia;
                    }
                }
                db.Prestamo.Add(prestamo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.n_copia = new SelectList(db.Copias, "n_copia", "comentarios", prestamo.n_copia);
            ViewBag.cod_socio = new SelectList(db.Usuarios, "cod_socio", "usuario", prestamo.cod_socio);
            return View(prestamo);
        }

        // GET: Prestamoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.n_copia = new SelectList(db.Copias, "n_copia", "comentarios", prestamo.n_copia);
            ViewBag.cod_socio = new SelectList(db.Usuarios, "cod_socio", "usuario", prestamo.cod_socio);
            return View(prestamo);
        }

        // POST: Prestamoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_prestamo,cod_socio,n_copia,tipo_prestamo,fecha_prestamo,fecha_devolucion,fecha_tope")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.n_copia = new SelectList(db.Copias, "n_copia", "comentarios", prestamo.n_copia);
            ViewBag.cod_socio = new SelectList(db.Usuarios, "cod_socio", "usuario", prestamo.cod_socio);
            return View(prestamo);
        }

        // GET: Prestamoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // POST: Prestamoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamo prestamo = db.Prestamo.Find(id);
            db.Prestamo.Remove(prestamo);
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


        //GET: Prestamoes/Reserva/
        public ActionResult Reserva([Bind(Include = "id_obra,nombre,fecha_publi,categoria,n_ejemplares, Copias, Autores")] Obra obra,
            [Bind(Include = "id_obra,comentarios")] Copias copias,
            [Bind(Include = "id_prestamo,cod_socio,")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {

                Prestamo prest = new Prestamo();
                String nom = Session["nombre"].ToString();
                var usu = db.Usuarios.Include(u => u.Prestamo);
                usu = usu.Where(u => u.nombre.Contains(nom));
                var n_copia = db.Copias.Include(c => c.Obra);
                n_copia = n_copia.Where(c => c.id_obra.Equals(obra.id_obra));

                prest.cod_socio = Convert.ToInt32(usu);
                prest.n_copia = Convert.ToInt32(n_copia);
                db.Prestamo.Add(prest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_obra = new SelectList(db.Cd_Dvd, "id_obra", "id_obra", obra.id_obra);
            ViewBag.id_obra = new SelectList(db.Libro, "id_obra", "id_obra", obra.id_obra);
            return View(obra);
        }
    }
}
