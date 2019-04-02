﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VLO.Models;

namespace VLO.Controllers
{
    public class PrestamosController : Controller
    {
        private Context db = new Context();

        // GET: Prestamos
        public ActionResult Index()
        {
            var prestamos = db.Prestamos.Include(p => p.Usuarios).Include(p => p.Productos);
            return View(prestamos.ToList());
        }

        // GET: Prestamos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = db.Prestamos.Find(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            return View(prestamos);
        }

        // GET: Prestamos/Create
        public ActionResult Create()
        {
            var TodosUsers = db.Usuarios.ToList();
            var Empleados = (from a in TodosUsers where a.IdRol == 1 select a).ToList();
            var EmpleadosOnly = TodosUsers.Except(Empleados);
            ViewBag.IdUser = new SelectList(EmpleadosOnly, "IdUser", "Nombre");

            var TodosProd = db.Productos.ToList();
            var Cat = db.Categoria.ToList();
            var Productos = (from p in TodosProd where p.IdCategoria==1 select p).ToList();
            var HotelOnly = TodosProd.Except(Productos);


            ViewBag.IdProducto = new SelectList(HotelOnly, "IdProducto", "Nombre");
            return View();
        }

        public ActionResult Solicitar()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult DevolverProd(int idprod)
        //{
        //    Productos d = db.Productos.Find(idprod);
        //    var Aumento = (from a in db.Prestamos where a.IdProducto == idprod select a).FirstOrDefault();
        //    d.Cantidad = d.Cantidad + Aumento.Cantidad;

        //    db.Entry(d).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return Redirect("Create");
        //}

        // POST: Prestamos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrestamo,IdProducto,IdUser, Cantidad")] Prestamos prestamos)
        {
            if (ModelState.IsValid)
            {
                db.Prestamos.Add(prestamos);
                db.SaveChanges();

                //Busqueda los Id de los productos que este en ambas tablas para luego aumentar
                var Existencias = (from p in db.Productos
                                   where p.IdProducto == prestamos.IdProducto
                                   select p).FirstOrDefault();
                //Aumenta el stock
                var Disminuye = prestamos.Cantidad;
                double cantidad = Existencias.Cantidad;
                Existencias.Cantidad = cantidad - Disminuye;
                db.Entry(Existencias).State = EntityState.Modified;
                db.SaveChanges();


                return RedirectToAction("Index");
            }

            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre", prestamos.IdUser);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", prestamos.IdProducto);
            return View(prestamos);
        }

        // GET: Prestamos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = db.Prestamos.Find(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre", prestamos.IdUser);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", prestamos.IdProducto);
            return View(prestamos);
        }

        // POST: Prestamos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPrestamo,IdProducto,IdUser, Cantidad")] Prestamos prestamos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.Usuarios, "IdUser", "Nombre", prestamos.IdUser);
            ViewBag.IdProducto = new SelectList(db.Productos, "IdProducto", "Nombre", prestamos.IdProducto);
            return View(prestamos);
        }

        // GET: Prestamos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = db.Prestamos.Find(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            return View(prestamos);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamos prestamos = db.Prestamos.Find(id);
            db.Prestamos.Remove(prestamos);
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
