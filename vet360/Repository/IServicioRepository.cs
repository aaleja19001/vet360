using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vet360.Models;

namespace vet360.Repository
{
    public interface IServicioRepository
    {
        IEnumerable<Servicio> GetAll();
        Servicio GetById(int id);
        IEnumerable<Servicio> GetByPriceRange(decimal minPrice, decimal maxPrice);
        void Add(Servicio servicio);
        void Update(Servicio servicio);
        void Delete(int id);
        void Save();
    }
}
