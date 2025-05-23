using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using vet360.Data;

namespace vet360.Controllers
{
    public class CuentaController : Controller
    {
        private Vet360Context db = new Vet360Context();

        [AllowAnonymous]
        public ActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string correo, string contraseña)
        {
            var usuario = db.Usuarios
                .Include("Rol")
                .FirstOrDefault(u => u.Correo == correo && u.Contraseña == contraseña);

            if (usuario == null)
            {
                ViewBag.Mensaje = "Correo o contraseña incorrectos.";
                return View();
            }
            if (usuario.Rol == null)
            {
                ViewBag.Mensaje = "El usuario no tiene un rol asignado.";
                return View();
            }

            // Crear cookie de autenticación
            FormsAuthentication.SetAuthCookie(usuario.Id.ToString(), false);

            // Guardar rol en cookie personalizada (opcional)
            var authTicket = new FormsAuthenticationTicket(
                1,
                usuario.Id.ToString(),
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                usuario.Rol.Nombre
            );

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);

            // Redirigir según rol
            if (usuario.Rol.Nombre == "Veterinario")
                return RedirectToAction("Index", "Veterinario");
            else if (usuario.Rol.Nombre == "Cliente")
                return RedirectToAction("Index", "Cliente");

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult AccesoDenegado()
        {
            return View();
        }
    }
}