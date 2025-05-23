using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Models;
using vet360.Repository;

namespace vet360.Services.Impl
{
    public class HistorialMedicoService : IHistorialMedicoService
    {
        private readonly IHistorialMedicoRepository _repository;

        public HistorialMedicoService(IHistorialMedicoRepository repository)
        {
            _repository = repository;
        }

        public List<HistorialMedico> ListarTodos()
        {
            return _repository.GetAll().ToList();
        }

        public HistorialMedico ObtenerPorId(int id)
        {
            return _repository.GetById(id);
        }

        public List<HistorialMedico> ObtenerPorMascota(int mascotaId)
        {
            return _repository.GetByMascota(mascotaId).ToList();
        }

        public List<HistorialMedico> ObtenerPorVeterinario(int veterinarioId)
        {
            return _repository.GetByVeterinario(veterinarioId).ToList();
        }

        public void Crear(HistorialMedico historial)
        {
            _repository.Add(historial);
            _repository.Save();
        }

        public void Actualizar(HistorialMedico historial)
        {
            _repository.Update(historial);
            _repository.Save();
        }

        public void Eliminar(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }
    }
}