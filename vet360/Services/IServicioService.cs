using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vet360.Models;

namespace vet360.Services
{
    public interface IServicioService
    {
        List<Servicio> ListarTodos();
        Servicio ObtenerPorId(int id);
        List<Servicio> BuscarPorRangoPrecio(decimal precioMin, decimal precioMax);
        void Crear(Servicio servicio);
        void Actualizar(Servicio servicio);
        void Eliminar(int id);
    }
}
