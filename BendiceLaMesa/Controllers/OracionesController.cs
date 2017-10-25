﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BendiceLaMesa.DAL;
using BendiceLaMesa.Models;

namespace BendiceLaMesa.Controllers
{
    public class OracionesController : Controller
    {
        private BendiceMesaContext db = new BendiceMesaContext();

        // GET: Oraciones
        public async Task<ActionResult> Index()
        {
            return View(await db.Oraciones.ToListAsync());
        }

        public async Task<ActionResult> Random()
        {
            int numeroOraciones = db.Oraciones.Count();

            //return  Oracion await db.Oraciones.OrderBy(s => Guid.NewGuid()).Take(1));
            var rand = new Random();
            int indice = rand.Next(db.Oraciones.Count());

            //Oracion oracion = db.Oraciones.ElementAt(indice);
            //Oracion oracion = await db.Oraciones.Skip(indice).FirstOrDefaultAsync();
            var oracionRandom = (from ora in db.Oraciones
                                 orderby ora.ID descending
                                 select ora).Skip(indice).Take(1).First();

            return View(oracionRandom);
        }

        // GET: Oraciones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oracion oracion = await db.Oraciones.FindAsync(id);
            if (oracion == null)
            {
                return HttpNotFound();
            }
            return View(oracion);
        }

        // GET: Oraciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Oraciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Puntuacion,Texto,Autor,AutorMail")] Oracion oracion)
        {
            if (ModelState.IsValid)
            {
                db.Oraciones.Add(oracion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(oracion);
        }

        // GET: Oraciones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oracion oracion = await db.Oraciones.FindAsync(id);
            if (oracion == null)
            {
                return HttpNotFound();
            }
            return View(oracion);
        }

        // POST: Oraciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Puntuacion,Texto,Autor,AutorMail")] Oracion oracion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oracion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(oracion);
        }

        // GET: Oraciones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oracion oracion = await db.Oraciones.FindAsync(id);
            if (oracion == null)
            {
                return HttpNotFound();
            }
            return View(oracion);
        }

        // POST: Oraciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var propuestasAsociada = await db.Propuestas.Where(x => x.OracionID == id).ToListAsync();

            foreach (Propuesta p in propuestasAsociada)
            {
                p.OracionID = null;
            }
            await db.SaveChangesAsync();
            

            Oracion oracion = await db.Oraciones.FindAsync(id);
            db.Oraciones.Remove(oracion);
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
