using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Data;
using vet360.Models;
using System.Data.Entity;


namespace vet360.Repository.Impl
{
    public class CitaRepository : ICitaRepository
    {
        private readonly Vet360Context _context;

        public CitaRepository(Vet360Context context)
        {
            _context = context;
        }

        public IEnumerable<Cita> GetAll()
        {
            return _context.Citas
                
                .Include(c => c.Usuario)
                .Include(c => c.Mascota)
                .Include(c => c.Servicio)
                .ToList();
        }

        public Cita GetById(int id)
        {
            return _context.Citas
                .Include(c => c.Usuario)
                .Include(c => c.Mascota)
                .Include(c => c.Servicio)
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Cita> GetByUsuarioId(int usuarioId)
        {
            return _context.Citas
                .Where(c => c.UsuarioId == usuarioId)
                .Include(c => c.Mascota)
                .Include(c => c.Servicio)
                .ToList();
        }

        public IEnumerable<Cita> GetByMascotaId(int mascotaId)
        {
            return _context.Citas
                .Where(c => c.MascotaId == mascotaId)
                .Include(c => c.Usuario)
                .Include(c => c.Servicio)
                .ToList();
        }

        public void Add(Cita cita)
        {
            _context.Citas.Add(cita);
        }

        public void Update(Cita cita)
        {
            var existing = _context.Citas.Find(cita.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(cita);
                // Actualizar relaciones
                existing.UsuarioId = cita.UsuarioId;
                existing.MascotaId = cita.MascotaId;
                existing.ServicioId = cita.ServicioId;
            }
        }

        public void Delete(int id)
        {
            var cita = _context.Citas.Find(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Cita> ObtenerCitasPorVeterinario(int idVeterinario, DateTime desde, DateTime hasta)
        {
            return _context.Citas
                .Where(c => !c.Estado.ToLower().Equals("completada"))
                .Include("Mascota")
                .Include("Usuario")
                .Include("Servicio")
                .Include("Horario.Veterinario")
                .Where(c => c.Horario.Veterinario.Id == idVeterinario &&
                            c.Horario.Fecha >= desde &&
                            c.Horario.Fecha <= hasta)
                .OrderBy(c => c.Horario.Fecha)
                .ToList();
        }
    }
}