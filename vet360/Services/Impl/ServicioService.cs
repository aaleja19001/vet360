using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Models;
using vet360.Repository;

namespace vet360.Services.Impl
{
    public class ServicioService : IServicioService
    {
        private readonly IServicioRepository _servicioRepository;

        public ServicioService(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        public List<Servicio> ListarTodos()
        {
            return _servicioRepository.GetAll().ToList();
        }

        public Servicio ObtenerPorId(int id)
        {
            return _servicioRepository.GetById(id);
        }

        public List<Servicio> BuscarPorRangoPrecio(decimal precioMin, decimal precioMax)
        {
            return _servicioRepository.GetByPriceRange(precioMin, precioMax).ToList();
        }

        public void Crear(Servicio servicio)
        {
            _servicioRepository.Add(servicio);
            _servicioRepository.Save();
        }

        public void Actualizar(Servicio servicio)
        {
            _servicioRepository.Update(servicio);
            _servicioRepository.Save();
        }

        public void Eliminar(int id)
        {
            _servicioRepository.Delete(id);
            _servicioRepository.Save();
        }
    }
}