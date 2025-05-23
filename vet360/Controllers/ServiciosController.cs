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
using vet360.Services;

namespace vet360.Controllers
{
    public class ServicioController : Controller
    {
        private readonly IServicioService _servicioService;

        public ServicioController()
        {
            _servicioService = Global.ServiceProvider.GetService(typeof(IServicioService)) as IServicioService;
        }
        public ServicioController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        // GET: Servicio
        public ActionResult Index()
        {
            var servicios = _servicioService.ListarTodos();
            return View(servicios);
        }

        // GET: Servicio/Details/5
        public ActionResult Details(int id)
        {
            var servicio = _servicioService.ObtenerPorId(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // GET: Servicio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicioService.Crear(servicio);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear el servicio: " + ex.Message);
                }
            }
            return View(servicio);
        }

        // GET: Servicio/Edit/5
        public ActionResult Edit(int id)
        {
            var servicio = _servicioService.ObtenerPorId(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // POST: Servicio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servicioService.Actualizar(servicio);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar el servicio: " + ex.Message);
                }
            }
            return View(servicio);
        }

        // GET: Servicio/Delete/5
        public ActionResult Delete(int id)
        {
            var servicio = _servicioService.ObtenerPorId(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // POST: Servicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _servicioService.Eliminar(id);
            return RedirectToAction("Index");
        }

        // GET: Servicio/PorPrecio
        public ActionResult PorPrecio(decimal? minPrice, decimal? maxPrice)
        {
            if (!minPrice.HasValue) minPrice = 0;
            if (!maxPrice.HasValue) maxPrice = decimal.MaxValue;

            var servicios = _servicioService.BuscarPorRangoPrecio(minPrice.Value, maxPrice.Value);
            return View("Index", servicios);
        }
    }
}
