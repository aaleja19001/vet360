using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using vet360.Data;
using vet360.Models;

namespace vet360.Repository.Impl
{
    public class HorarioRepository : IHorarioRepository
    {
        private readonly Vet360Context _context;

        public HorarioRepository(Vet360Context context)
        {
            _context = context;
        }

        public IEnumerable<Horario> ObtenerHorariosDisponiblesPorVeterinarioYFecha(int veterinarioId, DateTime fecha)
        {
            var horarios = _context.Horario
                .Where(h => h.UsuarioId == veterinarioId && h.Fecha.Date == fecha.Date)
                .ToList();

            var horariosOcupados = _context.Citas
                .Where(c => c.Horario.UsuarioId == veterinarioId && c.Horario.Fecha.Date == fecha.Date)
                .Select(c => c.HorarioId)
                .ToList();

            return horarios
                .Where(h => !horariosOcupados.Contains(h.Id) &&
                       (h.Fecha > DateTime.Now ||
                       (h.Fecha.Date == DateTime.Now.Date && h.HoraInicio > DateTime.Now.TimeOfDay)))
                .OrderBy(h => h.HoraInicio);
        }

        public IEnumerable<Horario> ObtenerHorariosDisponibles()
        {
            var horarios = _context.Horario
               // .Where(h => h.UsuarioId == veterinarioId && h.Fecha.Date == fecha.Date)
                .ToList();

            var horariosOcupados = _context.Citas
                //.Where(c => c.Horario.UsuarioId == veterinarioId && c.Horario.Fecha.Date == fecha.Date)
                .Select(c => c.HorarioId)
                .ToList();

            return horarios
                .Where(h => !horariosOcupados.Contains(h.Id) &&
                       (h.Fecha > DateTime.Now ||
                       (h.Fecha.Date == DateTime.Now.Date && h.HoraInicio > DateTime.Now.TimeOfDay)))
                .OrderBy(h => h.HoraInicio);
        }

        public IEnumerable<DateTime> ObtenerFechasDisponibles(int veterinarioId, DateTime desde)
        {
            return _context.Horario
                .Where(h => h.UsuarioId == veterinarioId &&
                       h.Fecha >= desde.Date &&
                       !_context.Citas.Any(c => c.HorarioId == h.Id))
                .Select(h => h.Fecha.Date)
                .Distinct()
                .OrderBy(d => d)
                .Where(f => f > DateTime.Now.Date ||
                       (f == DateTime.Now.Date &&
                        _context.Horario.Any(h => h.UsuarioId == veterinarioId &&
                                              h.Fecha.Date == f &&
                                              h.HoraInicio > DateTime.Now.TimeOfDay)))
                .ToList();
        }

        public Horario ObtenerPorId(int id)
        {
            return _context.Horario.Find(id);
        }

        public void GuardarCambios()
        {
            _context.SaveChanges();
        }
    }
}