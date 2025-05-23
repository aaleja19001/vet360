using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vet360.Models;

namespace vet360.Repository
{
    public interface IMascotaRepository
    {
        IEnumerable<Mascota> GetAll();
        Mascota GetById(int id);
        void Add(Mascota mascota);
        void Update(Mascota mascota);
        void Delete(int id);
        void Save();

        IEnumerable<Mascota> ObtenerPorUsuario(int usuarioId);
    }
}
