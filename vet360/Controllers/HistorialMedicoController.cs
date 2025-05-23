using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using vet360.Data;
using vet360.Models;

namespace vet360.Controllers
{
    public class HistorialMedicoController : Controller
    {
        private Vet360Context db = new Vet360Context();

        // GET: HistorialMedico
        public ActionResult Index()
        {
            var historialesMedicos = db.HistorialesMedicos.Include(h => h.Cita).Include(h => h.Mascota).Include(h => h.Veterinario);
            return View(historialesMedicos.ToList());
        }

        // GET: HistorialMedico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialMedico historialMedico = db.HistorialesMedicos.Find(id);
            if (historialMedico == null)
            {
                return HttpNotFound();
            }
            return View(historialMedico);
        }

        // GET: HistorialMedico/Create
        public ActionResult Create()
        {
            ViewBag.CitaId = new SelectList(db.Citas, "Id", "Estado");
            ViewBag.MascotaId = new SelectList(db.Mascotas, "Id", "Nombre");
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: HistorialMedico/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MascotaId,Diagnostico,Tratamiento,FechaRegistro,UsuarioId,CitaId")] HistorialMedico historialMedico)
        {
            if (ModelState.IsValid)
            {
                db.HistorialesMedicos.Add(historialMedico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CitaId = new SelectList(db.Citas, "Id", "Estado", historialMedico.CitaId);
            ViewBag.MascotaId = new SelectList(db.Mascotas, "Id", "Nombre", historialMedico.MascotaId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", historialMedico.UsuarioId);
            return View(historialMedico);
        }

        // GET: HistorialMedico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialMedico historialMedico = db.HistorialesMedicos.Find(id);
            if (historialMedico == null)
            {
                return HttpNotFound();
            }
            ViewBag.CitaId = new SelectList(db.Citas, "Id", "Estado", historialMedico.CitaId);
            ViewBag.MascotaId = new SelectList(db.Mascotas, "Id", "Nombre", historialMedico.MascotaId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", historialMedico.UsuarioId);
            return View(historialMedico);
        }

        // POST: HistorialMedico/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MascotaId,Diagnostico,Tratamiento,FechaRegistro,UsuarioId,CitaId")] HistorialMedico historialMedico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historialMedico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CitaId = new SelectList(db.Citas, "Id", "Estado", historialMedico.CitaId);
            ViewBag.MascotaId = new SelectList(db.Mascotas, "Id", "Nombre", historialMedico.MascotaId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", historialMedico.UsuarioId);
            return View(historialMedico);
        }

        // GET: HistorialMedico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialMedico historialMedico = db.HistorialesMedicos.Find(id);
            if (historialMedico == null)
            {
                return HttpNotFound();
            }
            return View(historialMedico);
        }

        // POST: HistorialMedico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistorialMedico historialMedico = db.HistorialesMedicos.Find(id);
            db.HistorialesMedicos.Remove(historialMedico);
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
