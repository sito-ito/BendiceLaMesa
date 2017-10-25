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
    public class PropuestasController : Controller
    {
        private BendiceMesaContext db = new BendiceMesaContext();

        // GET: Propuestas
        public async Task<ActionResult> Index()
        {
            var propuestas = db.Propuestas.Include(p => p.Oracion);
            return View(await propuestas.ToListAsync());
        }

        // GET: Propuestas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propuesta propuesta = await db.Propuestas.FindAsync(id);
            if (propuesta == null)
            {
                return HttpNotFound();
            }
            return View(propuesta);
        }

        // GET: Propuestas/Create
        public ActionResult Create()
        {
            ViewBag.OracionID = new SelectList(db.Oraciones, "ID", "Texto");
            return View();
        }
             
        // GET: Propuestas/Convertir/5
        public async Task<ActionResult> Convertir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propuesta propuesta = await db.Propuestas.FindAsync(id);
            if (propuesta == null)
            {
                return HttpNotFound();
            }
            
            Oracion oracionNueva = new Oracion();
            oracionNueva.Puntuacion = 0;
            oracionNueva.Texto = propuesta.Texto;
            oracionNueva.Autor = propuesta.Autor;
            oracionNueva.AutorMail = propuesta.AutorMail;

            db.Oraciones.Add(oracionNueva);
            await db.SaveChangesAsync();

            propuesta.OracionID = oracionNueva.ID;
            
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
            //return View(propuesta);
        }



        // POST: Propuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,OracionID,Texto,Autor,AutorMail")] Propuesta propuesta)
        {
            if (ModelState.IsValid)
            {
                db.Propuestas.Add(propuesta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OracionID = new SelectList(db.Oraciones, "ID", "Texto", propuesta.OracionID);
            return View(propuesta);
        }

        // GET: Propuestas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propuesta propuesta = await db.Propuestas.FindAsync(id);
            if (propuesta == null)
            {
                return HttpNotFound();
            }
            //ViewBag.OracionID = new SelectList(db.Oraciones, "ID", "Texto", propuesta.OracionID);
            return View(propuesta);
        }

        // POST: Propuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,OracionID,Texto,Autor,AutorMail")] Propuesta propuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propuesta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.OracionID = new SelectList(db.Oraciones, "ID", "Texto", propuesta.OracionID);
            return View(propuesta);
        }

        // GET: Propuestas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propuesta propuesta = await db.Propuestas.FindAsync(id);
            if (propuesta == null)
            {
                return HttpNotFound();
            }
            return View(propuesta);
        }

        // POST: Propuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Propuesta propuesta = await db.Propuestas.FindAsync(id);
            db.Propuestas.Remove(propuesta);
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
