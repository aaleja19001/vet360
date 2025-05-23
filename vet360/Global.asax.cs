using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Data.Entity;
using vet360.Data;
using Microsoft.Extensions.DependencyInjection;
using vet360.App_Start;

namespace vet360
{
    public class Global : HttpApplication
    {
        public static ServiceProvider ServiceProvider;
        void Application_Start(object sender, EventArgs e)
        {
            // Inyección de dependencias
            ServiceProvider = DependencyConfig.Configure();

            var serviceProvider = DependencyConfig.Configure();
            DependencyResolver.SetResolver(new DefaultDependencyResolver(serviceProvider));

            // Código que se ejecuta al iniciar la aplicación
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Inicializador de base de datos (crea tablas si no existen)
            Database.SetInitializer(new CreateDatabaseIfNotExists<Vet360Context>());

            using (var context = new Vet360Context())
            {
                try
                {
                    context.Database.Initialize(force: true);
                    var conn = context.Database.Connection;
                    conn.Open();
                    System.Diagnostics.Debug.WriteLine("✅ Conexión exitosa a la base de datos.");
                    conn.Close();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("❌ Error al conectar con la BD: " + ex.Message);
                }
            }
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] roles = authTicket.UserData.Split(',');

                var identity = new FormsIdentity(authTicket);
                var principal = new System.Security.Principal.GenericPrincipal(identity, roles);

                HttpContext.Current.User = principal;
            }
        }
    }
}