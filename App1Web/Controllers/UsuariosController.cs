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
    public class UsuariosController : Controller
    {
        private bibliotecaEntities db = new bibliotecaEntities();

        // GET: Usuarios
        public ActionResult Index(string searchString)
        {
            var usuarios = db.Usuarios.Include(u => u.Prestamo);
            if (!String.IsNullOrEmpty(searchString))
            {
                usuarios = usuarios.Where(u => u.nombre.Contains(searchString));
            }
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cod_socio,usuario,contraseña,nombre,apellido,dni,telefono")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cod_socio,usuario,contraseña,nombre,apellido,dni,telefono")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
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

        // GET: Usuarios/Login
        public ActionResult Login()
        {
            return View();
        }


        // POST: Usuarios/Login/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "cod_socio,usuario,contraseña,nombre,apellido,dni,telefono,rol")] Usuarios usuarios)
        {
            foreach(Usuarios usu in db.Usuarios)
            {
                if(usu.usuario.Equals(usuarios.usuario) & usu.contraseña.Equals(usuarios.contraseña))
                {
                    System.Web.HttpContext.Current.Session["rol"] = usu.rol;
                    System.Web.HttpContext.Current.Session["nombre"] = usu.nombre;
                    System.Web.HttpContext.Current.Session["cod_socio"] = usu.cod_socio;
                    return RedirectToAction("Index", "Obras");
                }
            }
            return View();
        }

        // GET: Usuarios/Salir
        public ActionResult Salir()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

    }
}
