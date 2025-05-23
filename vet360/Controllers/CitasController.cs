using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using vet360.Data;
using vet360.Models;
using vet360.Services;

namespace vet360.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaService _citaService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMascotaService _mascotaService;
        private readonly IServicioService _servicioService;

        public CitaController()
        {
            _citaService = Global.ServiceProvider.GetService(typeof(ICitaService)) as ICitaService;
            _usuarioService = Global.ServiceProvider.GetService(typeof(IUsuarioService)) as IUsuarioService;
            _mascotaService = Global.ServiceProvider.GetService(typeof(IMascotaService)) as IMascotaService;
            _servicioService = Global.ServiceProvider.GetService(typeof(IServicioService)) as IServicioService;
        }

        // GET: Cita
        public ActionResult Index()
        {
            var citas = _citaService.ListarTodas();
            System.Diagnostics.Debug.WriteLine("citas ---: " + citas.Count); 
            return View(citas);
        }

        // GET: Cita/Details/5
        public ActionResult Details(int id)
        {
            var cita = _citaService.ObtenerPorId(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: Cita/Create
        public ActionResult Create()
        {
            CargarListasDesplegables();
            return View();
        }

        // POST: Cita/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cita cita)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _citaService.Crear(cita);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear la cita: " + ex.Message);
                }
            }
            CargarListasDesplegables();
            return View(cita);
        }

        // GET: Cita/Edit/5
        public ActionResult Edit(int id)
        {
            var cita = _citaService.ObtenerPorId(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            CargarListasDesplegables();
            return View(cita);
        }

        // POST: Cita/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cita cita)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _citaService.Actualizar(cita);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar la cita: " + ex.Message);
                }
            }
            CargarListasDesplegables();
            return View(cita);
        }

        // GET: Cita/Delete/5
        public ActionResult Delete(int id)
        {
            var cita = _citaService.ObtenerPorId(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: Cita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _citaService.Cancelar(id);
            return RedirectToAction("Index");
        }

        // GET: Cita/Confirmar/5
        public ActionResult Confirmar(int id)
        {
            _citaService.Confirmar(id);
            return RedirectToAction("Index");
        }

        private void CargarListasDesplegables()
        {
            ViewBag.UsuarioId = new SelectList(_usuarioService.ListarTodos(), "Id", "Nombre");
            ViewBag.MascotaId = new SelectList(_mascotaService.ObtenerTodas(), "Id", "Nombre");
            ViewBag.ServicioId = new SelectList(_servicioService.ListarTodos(), "Id", "NombreServicioVet");
            ViewBag.VeterinarioId = new SelectList(_usuarioService.ObtenerVeterinarios(), "Id", "Nombre");
            var horarios = _citaService.ObtenerHorariosDisponibles()
            .Select(h => new
            {
                Id = h.Id,
                Fecha = h.Fecha.ToString("yyyy-MM-dd HH:mm") // O el formato que necesités
            });

            ViewBag.HorarioId = new SelectList(horarios, "Id", "Fecha");
        }

        public ActionResult Agendar()
        {
            CargarListasDesplegables();
            var usuarioActualId = Convert.ToInt32(User.Identity.Name);
            System.Diagnostics.Debug.WriteLine("veterinarios---: " + _usuarioService.ObtenerVeterinarios().Count());
           

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerFechas(int veterinarioId)
        {
            var fechas = _citaService.ObtenerFechasDisponibles(veterinarioId)
                .Select(f => f.ToString("yyyy-MM-dd"));
            return Json(fechas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerHorarios(int veterinarioId, string fecha)
        {
            var horarios = _citaService.ObtenerHorariosDisponibles()
                .Select(h => new {
                    id = h.Id,
                    hora = $"{h.HoraInicio:hh\\:mm} - {h.HoraFin:hh\\:mm}"
                });
            return Json(horarios, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Agendar(Cita cita)
        {
            System.Diagnostics.Debug.WriteLine($"VeterinarioId: {cita.VeterinarioId}");
            System.Diagnostics.Debug.WriteLine($"Fecha: {cita.HorarioId}");
            System.Diagnostics.Debug.WriteLine("Agendaaa");
            CargarListasDesplegables();
            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("Agendaaa error");
                // Esto imprime los errores uno por uno en la consola del servidor (Output de Visual Studio)
                foreach (var error in ModelState)
                {
                    string key = error.Key;
                    foreach (var subError in error.Value.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"[ERROR] Campo---: {key} - Mensaje: {subError.ErrorMessage}");
                    }
                }
            }
            var usuarioActualId = Convert.ToInt32(User.Identity.Name);
            cita.UsuarioId = usuarioActualId;
            if (_citaService.AgendarCita(cita))
            {

                // return RedirectToAction("Confirmacion");
                // return RedirectToAction("index");

                TempData["MensajeExito"] = "¡Cita agendada con éxito!";

                return RedirectToAction("Index", "Cliente");
            }

            ModelState.AddModelError("", "El horario seleccionado ya no está disponible");
            ViewBag.Veterinarios = new SelectList(_usuarioService.ObtenerVeterinarios(), "Id", "NombreCompleto");
            return View();
        }
    }
}
