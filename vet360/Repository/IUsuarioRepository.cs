using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vet360.Models;

namespace vet360.Repository
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> GetAll();
        Usuario GetById(int id);
        Usuario GetByEmail(string email);
        void Add(Usuario usuario);
        void Update(Usuario usuario);
        void Delete(int id);
        void Save();

        IEnumerable<Usuario> GetVeterinarios();
    }
}
