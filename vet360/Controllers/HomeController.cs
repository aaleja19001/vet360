using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using vet360.Data;
using vet360.Models;

namespace vet360.Controllers
{
   
    public class HomeController : Controller
    {
        private Vet360Context db = new Vet360Context();

        [AllowAnonymous]
        public ActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Registrarse(Usuario model)
        {
            if (ModelState.IsValid)
            {
                // Verificamos si ya existe el correo
                if (db.Usuarios.Any(u => u.Correo == model.Correo))
                {
                    ViewBag.Mensaje = "Ya existe un usuario con ese correo.";
                    return View(model);
                }

                 var rolCliente = db.Roles.FirstOrDefault(r => r.Nombre == "Cliente");
                if (rolCliente == null)
                {
                    ViewBag.Mensaje = "No se pudo asignar el rol Cliente.";
                    return View(model);
                }

                model.RolId = rolCliente.RolId;
                db.Usuarios.Add(model);
                db.SaveChanges();

                FormsAuthentication.SetAuthCookie(model.Id.ToString(), false);
                return RedirectToAction("Index", "Cliente");
            }

            return View(model);
        }
    }
}