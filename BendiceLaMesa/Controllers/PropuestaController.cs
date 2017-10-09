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
    public class PropuestaController : Controller
    {
        private BendiceMesaContext db = new BendiceMesaContext();

        // GET: Propuesta
        public async Task<ActionResult> Index()
        {
            var propuestas = db.Propuestas.Include(p => p.Oracion).Include(p => p.Usuario);
            return View(await propuestas.ToListAsync());
        }

        // GET: Propuesta/Details/5
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

        // GET: Propuesta/Create
        public ActionResult Create()
        {
            ViewBag.OracionID = new SelectList(db.Oraciones, "ID", "Texto");
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "ID", "Nombre");
            return View();
        }

        // POST: Propuesta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,OracionID,Texto,UsuarioID")] Propuesta propuesta)
        {
            if (ModelState.IsValid)
            {
                db.Propuestas.Add(propuesta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OracionID = new SelectList(db.Oraciones, "ID", "Texto", propuesta.OracionID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "ID", "Nombre", propuesta.UsuarioID);
            return View(propuesta);
        }

        // GET: Propuesta/Edit/5
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
            ViewBag.OracionID = new SelectList(db.Oraciones, "ID", "Texto", propuesta.OracionID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "ID", "Nombre", propuesta.UsuarioID);
            return View(propuesta);
        }

        // POST: Propuesta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,OracionID,Texto,UsuarioID")] Propuesta propuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propuesta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OracionID = new SelectList(db.Oraciones, "ID", "Texto", propuesta.OracionID);
            ViewBag.UsuarioID = new SelectList(db.Usuarios, "ID", "Nombre", propuesta.UsuarioID);
            return View(propuesta);
        }

        // GET: Propuesta/Delete/5
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

        // POST: Propuesta/Delete/5
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
