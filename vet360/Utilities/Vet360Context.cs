using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using vet360.Models;

namespace vet360.Data
{
    public class Vet360Context : DbContext
    {
        // DbSets
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<HistorialMedico> HistorialesMedicos { get; set; }
        public DbSet<Horario> Horario { get; set; }

        public Vet360Context() : base("name=Vet360Context")
        {
            // Configuración inicial
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remover convenciones que no necesitamos
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Configuración de Mascota
            modelBuilder.Entity<Mascota>()
                .HasRequired(m => m.Usuario)
                .WithMany(u => u.Mascotas)
                .HasForeignKey(m => m.UsuarioId)
                .WillCascadeOnDelete(false);

            // Configuración de Usuario-Rol
            modelBuilder.Entity<Usuario>()
                .HasOptional(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId)
                .WillCascadeOnDelete(false);

            // Configuración de Roles
            modelBuilder.Entity<Rol>()
                .ToTable("Roles")
                .Property(r => r.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            // Configuración de Historial Médico
            modelBuilder.Entity<HistorialMedico>()
                .ToTable("HistorialesMedicos")
                .HasRequired(hm => hm.Mascota)
                .WithMany(m => m.HistorialesMedicos)
                .HasForeignKey(hm => hm.MascotaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HistorialMedico>()
                .HasRequired(hm => hm.Veterinario)
                .WithMany(u => u.HistorialesMedicosRegistrados)
                .HasForeignKey(hm => hm.UsuarioId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HistorialMedico>()
                .HasOptional(hm => hm.Cita)
                .WithMany(c => c.HistorialesMedicos)
                .HasForeignKey(hm => hm.CitaId)
                .WillCascadeOnDelete(false);

            // Configuraciones adicionales para Historial Médico
            modelBuilder.Entity<HistorialMedico>()
                .Property(hm => hm.Diagnostico)
                .IsRequired()
                .HasMaxLength(2000);

            modelBuilder.Entity<HistorialMedico>()
                .Property(hm => hm.Tratamiento)
                .HasMaxLength(2000);

            modelBuilder.Entity<HistorialMedico>()
                .Property(hm => hm.FechaRegistro)
                .HasColumnType("datetime2");
        }
    }
}