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
using vet360.Services.Impl;

namespace vet360.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        //private readonly Vet360Context _context;
        private readonly IRolService _rolService;

        public UsuarioController()
        {
            _usuarioService = Global.ServiceProvider.GetService(typeof(IUsuarioService)) as IUsuarioService;
            _rolService = Global.ServiceProvider.GetService(typeof(IRolService)) as IRolService;
        }
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: Usuario
        public ActionResult Index()
        {
            var usuarios = _usuarioService.ListarTodos();
            return View(usuarios);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            var usuario = _usuarioService.ObtenerPorId(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            var roles = _rolService.ObtenerTodos();
            ViewBag.RolId = new SelectList(roles, "RolId", "Nombre");
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioService.Crear(usuario);
                return RedirectToAction("Index");
            }
            var roles = _rolService.ObtenerTodos();
            ViewBag.RolId = new SelectList(roles, "RolId", "Nombre", usuario.RolId);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            var usuario = _usuarioService.ObtenerPorId(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            var roles = _rolService.ObtenerTodos();
            ViewBag.RolId = new SelectList(roles, "RolId", "Nombre", usuario.RolId);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioService.Actualizar(usuario);
                return RedirectToAction("Index");
            }
            var roles = _rolService.ObtenerTodos();
            ViewBag.RolId = new SelectList(roles, "RolId", "Nombre", usuario.RolId);
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            var usuario = _usuarioService.ObtenerPorId(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _usuarioService.Eliminar(id);
            return RedirectToAction("Index");
        }

        // GET: Usuario/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Usuario/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string contraseña)
        {
            if (_usuarioService.ValidarCredenciales(email, contraseña))
            {
                // Lógica de autenticación
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Credenciales inválidas");
            return View();
        }
    }
}
