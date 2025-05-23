using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vet360.Models;
using vet360.Services;

namespace vet360.Controllers
{
    public class RolController : Controller
    {
        private readonly IRolService _rolService;
        public RolController()
        {
            _rolService = Global.ServiceProvider.GetService(typeof(IRolService)) as IRolService;
        }

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        public ActionResult Index()
        {
            var roles = _rolService.ObtenerTodos();
            return View(roles);
        }

        public ActionResult Details(int id)
        {
            var rol = _rolService.ObtenerPorId(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _rolService.Crear(rol);
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        public ActionResult Edit(int id)
        {
            var rol = _rolService.ObtenerPorId(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _rolService.Actualizar(rol);
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        public ActionResult Delete(int id)
        {
            var rol = _rolService.ObtenerPorId(id);
            if (rol == null)
            {
                return HttpNotFound();
            }
            return View(rol);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _rolService.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}