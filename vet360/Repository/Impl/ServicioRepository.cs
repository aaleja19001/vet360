using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Data;
using vet360.Models;


namespace vet360.Repository.Impl
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly Vet360Context _context;

        public ServicioRepository(Vet360Context context)
        {
            _context = context;
        }

        public IEnumerable<Servicio> GetAll()
        {
            return _context.Servicios.ToList();
        }

        public Servicio GetById(int id)
        {
            return _context.Servicios.Find(id);
        }

        public IEnumerable<Servicio> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _context.Servicios
                .Where(s => s.Precio >= minPrice && s.Precio <= maxPrice)
                .ToList();
        }

        public void Add(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
        }

        public void Update(Servicio servicio)
        {
            var existing = _context.Servicios.Find(servicio.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(servicio);
            }
        }

        public void Delete(int id)
        {
            var servicio = _context.Servicios.Find(id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}