using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vet360.Models;

namespace vet360.Repository
{
    public interface ICitaRepository
    {
        IEnumerable<Cita> GetAll();
        Cita GetById(int id);
        IEnumerable<Cita> GetByUsuarioId(int usuarioId);
        IEnumerable<Cita> GetByMascotaId(int mascotaId);
        void Add(Cita cita);
        void Update(Cita cita);
        void Delete(int id);
        void Save();
        IEnumerable<Cita> ObtenerCitasPorVeterinario(int Idveterinario, DateTime desde, DateTime hasta);
    }
}
