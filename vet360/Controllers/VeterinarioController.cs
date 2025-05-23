using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vet360.Models;
using vet360.Services;
using Microsoft.AspNet.Identity;
using vet360.Filters;

namespace vet360.Controllers
{
    [AutorizarRol("Veterinario", "Admin")]
    //[Authorize(Roles = "Veterinario")] // Solo accesible por veterinarios
    public class VeterinarioController : Controller
    {
        private readonly ICitaService _citaService;
        private readonly IMascotaService _mascotaService;
        private readonly IHistorialMedicoService _historialService;

        public VeterinarioController()
        {
            _citaService = Global.ServiceProvider.GetService(typeof(ICitaService)) as ICitaService;
            _mascotaService = Global.ServiceProvider.GetService(typeof(IMascotaService)) as IMascotaService;
            _historialService = Global.ServiceProvider.GetService(typeof(IHistorialMedicoService)) as IHistorialMedicoService;
        }
        public VeterinarioController(
            ICitaService citaService,
            IMascotaService mascotaService,
            IHistorialMedicoService historialService)
        {
            _citaService = citaService;
            _mascotaService = mascotaService;
            _historialService = historialService;
        }

        // GET: Veterinario
        public ActionResult Index()
        {
            var hoy = DateTime.Today;
            var citasHoy = _citaService.ObtenerCitasPorVeterinario(Convert.ToInt32(User.Identity.Name), hoy, hoy.AddDays(1));
            System.Diagnostics.Debug.WriteLine("Hoy: " + hoy + " ... longi:: " + citasHoy.Count() + " ... ID: " + User.Identity.Name);
            return View(citasHoy);
        }

        // GET: Veterinario/Citas
        public ActionResult Citas(DateTime? fecha)
        {
            var fechaConsulta = fecha ?? DateTime.Today;
            var citas = _citaService.ObtenerCitasPorVeterinario(Convert.ToInt32(User.Identity.Name), fechaConsulta, fechaConsulta.AddDays(7));
            ViewBag.FechaConsulta = fechaConsulta;
            return View(citas);
        }

        // GET: Veterinario/AtenderCita/5
        public ActionResult AtenderCita(int id)
        {
            var cita = _citaService.ObtenerPorId(id);
            if (cita == null || cita.Estado != "Confirmada")
            {
                System.Diagnostics.Debug.WriteLine("ya se atiende ");
                return HttpNotFound();
            }
            System.Diagnostics.Debug.WriteLine("id mascota: " + cita.MascotaId);
            System.Diagnostics.Debug.WriteLine("id cita: " + cita.Id);
            var mascota = _mascotaService.ObtenerPorId(cita.MascotaId);
            var historial = _historialService.ObtenerPorMascota(cita.MascotaId);

            var viewModel = new AtenderCitaViewModel
            {
                Cita = cita,
                Mascota = mascota,
                Historial = historial,
                NuevoHistorial = new HistorialMedico
                {
                    MascotaId = cita.MascotaId,
                    UsuarioId = Convert.ToInt32(User.Identity.Name),
                    CitaId = cita.Id,
                    FechaRegistro = DateTime.Now
                }
            };

            return View(viewModel);
        }

        // POST: Veterinario/AtenderCita
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AtenderCita(AtenderCitaViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Registrar el historial médico
                _historialService.Crear(model.NuevoHistorial);

                // Actualizar estado de la cita
                var cita = _citaService.ObtenerPorId(model.Cita.Id);
                cita.Estado = "Completada";
                _citaService.Actualizar(cita);

                return RedirectToAction("Index");
            }

            // Si hay errores, recargar datos
            model.Cita = _citaService.ObtenerPorId(model.Cita.Id);
            model.Mascota = _mascotaService.ObtenerPorId(model.Cita.MascotaId);
            model.Historial = _historialService.ObtenerPorMascota(model.Cita.MascotaId);

            return View(model);
        }

        // GET: Veterinario/HistorialMascota/5
        public ActionResult HistorialMascota(int mascotaId)
        {
            var historiales = _historialService.ObtenerPorMascota(mascotaId);
            var mascota = _mascotaService.ObtenerPorId(mascotaId);

            ViewBag.MascotaNombre = mascota?.Nombre ?? "Mascota no encontrada";
            return View(historiales);
        }
    }
}