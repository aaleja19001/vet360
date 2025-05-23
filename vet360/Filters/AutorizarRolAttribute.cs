using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace vet360.Filters
{
    public class AutorizarRolAttribute : AuthorizeAttribute
    {
        private readonly string[] _rolesPermitidos;

        public AutorizarRolAttribute(params string[] rolesPermitidos)
        {
            _rolesPermitidos = rolesPermitidos;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
                return false;

            var identity = (FormsIdentity)user.Identity;
            var rol = identity.Ticket.UserData;

            return _rolesPermitidos.Contains(rol);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Cuenta/Login");
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Cuenta/AccesoDenegado");
            }
        }
    }
}