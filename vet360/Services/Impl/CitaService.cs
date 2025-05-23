using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Data;
using vet360.Models;
using vet360.Repository;

namespace vet360.Services.Impl
{
    public class CitaService : ICitaService
    {
        private readonly ICitaRepository _citaRepository;

        private readonly IHorarioRepository _horarioRepo;
        private readonly IMascotaRepository _mascotaRepo;

        public CitaService(ICitaRepository citaRepository,
             IHorarioRepository horarioRepo,
        IMascotaRepository mascotaRepo)
        {
            _citaRepository = citaRepository;
            _horarioRepo = horarioRepo;
            _mascotaRepo = mascotaRepo;
        }

        public List<Cita> ListarTodas()
        {
            return _citaRepository.GetAll().ToList();
        }

        public Cita ObtenerPorId(int id)
        {
            return _citaRepository.GetById(id);
        }

        public List<Cita> ObtenerPorUsuario(int usuarioId)
        {
            return _citaRepository.GetByUsuarioId(usuarioId).ToList();
        }

        public List<Cita> ObtenerPorMascota(int mascotaId)
        {
            return _citaRepository.GetByMascotaId(mascotaId).ToList();
        }

        public void Crear(Cita cita)
        {
            cita.Estado = "Pendiente"; // Estado inicial
            _citaRepository.Add(cita);
            _citaRepository.Save();
        }

        public void Actualizar(Cita cita)
        {
            _citaRepository.Update(cita);
            _citaRepository.Save();
        }

        public void Cancelar(int id)
        {
            var cita = _citaRepository.GetById(id);
            if (cita != null)
            {
                cita.Estado = "Cancelada";
                _citaRepository.Update(cita);
                _citaRepository.Save();
            }
        }

        public void Confirmar(int id)
        {
            var cita = _citaRepository.GetById(id);
            if (cita != null)
            {
                cita.Estado = "Confirmada";
                _citaRepository.Update(cita);
                _citaRepository.Save();
            }
        }

        public IEnumerable<Cita> ObtenerCitasPorVeterinario(int idVeterinario, DateTime desde, DateTime hasta)
        {
            return _citaRepository.ObtenerCitasPorVeterinario(idVeterinario, desde, hasta);
        }

        public IEnumerable<DateTime> ObtenerFechasDisponibles(int veterinarioId)
        {
            return _horarioRepo.ObtenerFechasDisponibles(veterinarioId, DateTime.Today);
        }

        public IEnumerable<Horario> ObtenerHorariosDisponibles()
        {
            return _horarioRepo.ObtenerHorariosDisponibles();
        }

        public bool AgendarCita(Cita cita)
        {
            // Validar disponibilidad
            var horario = _horarioRepo.ObtenerPorId(cita.HorarioId);
            if (horario == null) return false;

            var disponible = _horarioRepo.ObtenerHorariosDisponibles()
                .Any(h => h.Id == cita.HorarioId);

            if (!disponible) return false;

            cita.Estado = "Pendiente";
            Crear(cita);
            //_context.SaveChanges();

            return true;
        }

        public IEnumerable<Mascota> ObtenerMascotasPorCliente(int clienteId)
        {
            return _mascotaRepo.ObtenerPorUsuario(clienteId);
        }
    }
}