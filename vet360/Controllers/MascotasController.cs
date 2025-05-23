using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNetCore.Http;
using vet360.Data;
using vet360.Filters;
using vet360.Models;
using vet360.Services;
using vet360.Services.Impl;

namespace vet360.Controllers
{
    public class MascotasController : Controller
    {
        private readonly IMascotaService _mascotaService;
        private readonly IUsuarioService _usuarioService;

        public MascotasController()
        {
            _mascotaService = Global.ServiceProvider.GetService(typeof(IMascotaService)) as IMascotaService;
            _usuarioService = Global.ServiceProvider.GetService(typeof(IUsuarioService)) as IUsuarioService;
        }

        // GET: Mascotas
        [AutorizarRol("Veterinario", "Admin")]
        public ActionResult Index()
        {
            var mascotas = _mascotaService.ObtenerTodas();
            return View(mascotas);
        }

        // GET: Mascotas/MisMascotas
        [AutorizarRol("Cliente")]
        public ActionResult MisMascotas()
        {
            var ticket = (FormsIdentity)User.Identity;
            var username = ticket.Ticket.Name; // o Ticket.UserData si tenés el ID
            /*var rol = ticket.UserData;
            if (rol != "Veterinario")
            {
                return RedirectToAction("AccesoDenegado", "Error"); // o mostrar mensaje
            }*/
            var mascotasDelUsuario = _mascotaService.ObtenerPorUsuario(Convert.ToInt32(username));
            return View("Index", mascotasDelUsuario); // 
        }

        [HttpGet]
        public JsonResult ObtenerMisMascotasJson()
        {
            var ticket = (FormsIdentity)User.Identity;
            var username = ticket.Ticket.Name;
            var mascotasDelUsuario = _mascotaService.ObtenerPorUsuario(Convert.ToInt32(username));

            var resultado = mascotasDelUsuario.Select(m => new {
                id = m.Id,
                nombre = m.Nombre
            });

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        // GET: Mascotas/Details/5
        [AutorizarRol("Cliente", "Veterinario", "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var mascota = _mascotaService.ObtenerPorId(id.Value);
            if (mascota == null)
                return HttpNotFound();

            return View(mascota);
        }

        // GET: Mascotas/Create
        [AutorizarRol("Cliente", "Veterinario", "Admin")]
        public ActionResult Create()
        {
           ViewBag.UsuarioId = new SelectList(_usuarioService.ListarTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Mascotas/Create
        [AutorizarRol("Cliente", "Veterinario", "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Especie,Raza,Edad,UsuarioId")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                _mascotaService.Crear(mascota);

                var user = HttpContext.User;

                var identity = (FormsIdentity)user.Identity;
                var rol = identity.Ticket.UserData;
                if(rol != "Cliente")
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("MisMascotas");

            }

            ViewBag.UsuarioId = new SelectList(_usuarioService.ListarTodos(), "Id", "Nombre");
            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        [AutorizarRol("Cliente", "Veterinario", "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var mascota = _mascotaService.ObtenerPorId(id.Value);
            System.Diagnostics.Debug.WriteLine("usuario controlador mascota: " + mascota.UsuarioId);
            if (mascota == null)
                return HttpNotFound();

            ViewBag.UsuarioId = new SelectList(_usuarioService.ListarTodos(), "Id", "Nombre", mascota.UsuarioId);
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        [AutorizarRol("Cliente", "Veterinario", "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Especie,Raza,Edad,UsuarioId")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("usuario procesado: " + mascota.UsuarioId);
                _mascotaService.Editar(mascota);
                var user = HttpContext.User;

                var identity = (FormsIdentity)user.Identity;
                var rol = identity.Ticket.UserData;
                if (rol != "Cliente")
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("MisMascotas");
            }

            ViewBag.UsuarioId = new SelectList(_usuarioService.ListarTodos(), "Id", "Nombre", mascota.UsuarioId);
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        [AutorizarRol("Cliente", "Veterinario", "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var mascota = _mascotaService.ObtenerPorId(id.Value);
            if (mascota == null)
                return HttpNotFound();

            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [AutorizarRol("Cliente", "Veterinario", "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _mascotaService.Eliminar(id);

            var user = HttpContext.User;

            var identity = (FormsIdentity)user.Identity;
            var rol = identity.Ticket.UserData;
            if (rol != "Cliente")
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("MisMascotas");
        }
    }
}
