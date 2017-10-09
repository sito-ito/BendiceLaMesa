using System;
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
    public class OracionController : Controller
    {
        private BendiceMesaContext db = new BendiceMesaContext();

        // GET: Oracion
        public async Task<ActionResult> Index()
        {
            var oraciones = db.Oraciones.Include(o => o.Usuario);
            return View(await oraciones.ToListAsync());
        }

        // GET: Oracion/Details/5
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

        // GET: Oracion/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "ID", "Nombre");
            return View();
        }

        // POST: Oracion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Puntuacion,Texto,UsuarioID")] Oracion oracion)
        {
            if (ModelState.IsValid)
            {
                db.Oraciones.Add(oracion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioID = new SelectList(db.Usuarios, "ID", "Nombre", oracion.UsuarioID);
            return View(oracion);
        }

        // GET: Oracion/Edit/5
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
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "ID", "Nombre", oracion.UsuarioID);
            return View(oracion);
        }

        // POST: Oracion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Puntuacion,Texto,UsuarioID")] Oracion oracion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oracion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "ID", "Nombre", oracion.UsuarioID);
            return View(oracion);
        }

        // GET: Oracion/Delete/5
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

        // POST: Oracion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
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
