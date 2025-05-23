using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Data;
using vet360.Models;
using System.Data.Entity;

namespace vet360.Repository.Impl
{
    public class HistorialMedicoRepository : IHistorialMedicoRepository
    {
        private readonly Vet360Context _context;

        public HistorialMedicoRepository(Vet360Context context)
        {
            _context = context;
        }

        public IEnumerable<HistorialMedico> GetAll()
        {
            return _context.HistorialesMedicos
                .Include(h => h.Mascota)
                .Include(h => h.Veterinario)
                .Include(h => h.Cita)
                .ToList();
        }

        public HistorialMedico GetById(int id)
        {
            return _context.HistorialesMedicos
                .Include(h => h.Mascota)
                .Include(h => h.Veterinario)
                .Include(h => h.Cita)
                .FirstOrDefault(h => h.Id == id);
        }

        public IEnumerable<HistorialMedico> GetByMascota(int mascotaId)
        {
            return _context.HistorialesMedicos
                .Include(h => h.Veterinario)
                .Include(h => h.Cita)
                .Where(h => h.MascotaId == mascotaId)
                .OrderByDescending(h => h.FechaRegistro)
                .ToList();
        }

        public IEnumerable<HistorialMedico> GetByVeterinario(int veterinarioId)
        {
            return _context.HistorialesMedicos
                .Include(h => h.Mascota)
                .Include(h => h.Cita)
                .Where(h => h.UsuarioId == veterinarioId)
                .OrderByDescending(h => h.FechaRegistro)
                .ToList();
        }

        public void Add(HistorialMedico historial)
        {
            historial.FechaRegistro = DateTime.Now;
            _context.HistorialesMedicos.Add(historial);
        }

        public void Update(HistorialMedico historial)
        {
            var existing = _context.HistorialesMedicos.Find(historial.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(historial);
            }
        }

        public void Delete(int id)
        {
            var historial = _context.HistorialesMedicos.Find(id);
            if (historial != null)
            {
                _context.HistorialesMedicos.Remove(historial);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}