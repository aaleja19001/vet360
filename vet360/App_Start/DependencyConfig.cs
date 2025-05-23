using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using vet360.Repository.Impl;
using vet360.Repository;
using vet360.Services.Impl;
using vet360.Services;
using vet360.Data;
using System.ComponentModel;

namespace vet360.App_Start
{
    public static class  DependencyConfig
    {
        public static ServiceProvider Configure()
        {
            var services = new ServiceCollection();
            // Registrar el contexto de base de datos
            services.AddScoped<Vet360Context>();

            // Aquí registrar repositorios 
            services.AddScoped<IMascotaRepository, MascotaRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICitaRepository, CitaRepository>();
            services.AddScoped<IServicioRepository, ServicioRepository>();
            services.AddScoped<IHistorialMedicoRepository, HistorialMedicoRepository>();
            services.AddScoped<IHorarioRepository, HorarioRepository>();


            // registrar servicios
            services.AddScoped<IMascotaService, MascotaService>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICitaService, CitaService>();
            services.AddScoped<IServicioService, ServicioService>();
            services.AddScoped<IHistorialMedicoService, HistorialMedicoService>();



            return services.BuildServiceProvider();
        }
    }
}