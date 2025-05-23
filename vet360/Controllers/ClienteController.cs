using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vet360.Models;
using vet360.Services;
using vet360.Services.Impl;

namespace vet360.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class ClienteController : Controller
    {
        private readonly IMascotaService _mascotaService;
        private readonly ICitaService _citaService;

        public ClienteController()
        {

            _citaService = Global.ServiceProvider.GetService(typeof(ICitaService)) as ICitaService;
            _mascotaService = Global.ServiceProvider.GetService(typeof(IMascotaService)) as IMascotaService;
    
        }

        public ClienteController(IMascotaService mascotaService, ICitaService citaService)
        {
            _mascotaService = mascotaService;
            _citaService = citaService;
        }

        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cliente/RegistrarMascota
        public ActionResult RegistrarMascota()
        {
            return View();
        }

        // POST: Cliente/RegistrarMascota
        [HttpPost]
        public ActionResult RegistrarMascota(Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                mascota.UsuarioId = Convert.ToInt32(User.Identity.Name);
                _mascotaService.Crear(mascota);
                return RedirectToAction("MisMascotas");
            }

            return View(mascota);
        }

        // GET: Cliente/MisMascotas
        public ActionResult MisMascotas()
        {
            var usuarioId = Convert.ToInt32(User.Identity.Name);
            var mascotas = _mascotaService.ObtenerPorUsuario(usuarioId);
            return View(mascotas);
        }

        // GET: Cliente/AgendarCita
        public ActionResult AgendarCita()
        {
            var usuarioId = Convert.ToInt32(User.Identity.Name);
            var mascotas = _mascotaService.ObtenerPorUsuario(usuarioId);

            ViewBag.Mascotas = new SelectList(mascotas, "Id", "Nombre");
            return View();
        }

        // POST: Cliente/AgendarCita
        [HttpPost]
        public ActionResult AgendarCita(Cita cita)
        {
            if (ModelState.IsValid)
            {
                cita.Estado = "Confirmada";
                System.Diagnostics.Debug.WriteLine("c " + cita.Estado);
                _citaService.Crear(cita);
                return RedirectToAction("MisCitas");
            }

            var usuarioId = Convert.ToInt32(User.Identity.Name);
            var mascotas = _mascotaService.ObtenerPorUsuario(usuarioId);
            ViewBag.Mascotas = new SelectList(mascotas, "Id", "Nombre");

            return View(cita);
        }

        // GET: Cliente/MisCitas
        public ActionResult MisCitas()
        {
            var usuarioId = Convert.ToInt32(User.Identity.Name);
            var citas = _citaService.ObtenerPorUsuario(usuarioId);
            return View(citas);
        }
    }
}