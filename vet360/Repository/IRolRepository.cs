using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vet360.Models;

namespace vet360.Repository
{
    public interface IRolRepository
    {
        IEnumerable<Rol> GetAll();
        Rol GetById(int id);
        void Add(Rol rol);
        void Update(Rol rol);
        void Delete(int id);
        void Save();
    }
}
