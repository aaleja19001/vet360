using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Data;
using vet360.Models;
using System.Data.Entity;

namespace vet360.Repository.Impl
{
    public class RolRepository : IRolRepository
    {
        private readonly Vet360Context _context;

        public RolRepository(Vet360Context context)
        {
            _context = context;
        }

        public IEnumerable<Rol> GetAll() => _context.Roles.ToList();

        public Rol GetById(int id) => _context.Roles.Find(id);

        public void Add(Rol rol)
        {
            _context.Roles.Add(rol);
        }

        public void Update(Rol rol)
        {
            var existingRol = _context.Roles.Find(rol.RolId);
            if (existingRol != null)
            {
                _context.Entry(existingRol).CurrentValues.SetValues(rol);
            }else
            {
                 _context.Entry(rol).State = EntityState.Modified;
            }
               
        }

        public void Delete(int id)
        {
            var rol = _context.Roles.Find(id);
            if (rol != null)
            {
                _context.Roles.Remove(rol);
            }
        }

        public void Save() => _context.SaveChanges();
    }
}