using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Models;
using vet360.Repository;

namespace vet360.Services.Impl
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public List<Rol> ObtenerTodos() => _rolRepository.GetAll().ToList();

        public Rol ObtenerPorId(int id) => _rolRepository.GetById(id);

        public void Crear(Rol rol)
        {
            _rolRepository.Add(rol);
            _rolRepository.Save();
        }

        public void Actualizar(Rol rol)
        {
            _rolRepository.Update(rol);
            _rolRepository.Save();
        }

        public void Eliminar(int id)
        {
            _rolRepository.Delete(id);
            _rolRepository.Save();
        }
    }
}