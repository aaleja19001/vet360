using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vet360.Models;

namespace vet360.Repository
{
    public interface IHistorialMedicoRepository
    {
        IEnumerable<HistorialMedico> GetAll();
        HistorialMedico GetById(int id);
        IEnumerable<HistorialMedico> GetByMascota(int mascotaId);
        IEnumerable<HistorialMedico> GetByVeterinario(int veterinarioId);
        void Add(HistorialMedico historial);
        void Update(HistorialMedico historial);
        void Delete(int id);
        void Save();
    }
}
