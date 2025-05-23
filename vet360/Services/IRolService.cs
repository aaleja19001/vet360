using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vet360.Models;

namespace vet360.Services
{
    public interface IRolService
    {
        List<Rol> ObtenerTodos();
        Rol ObtenerPorId(int id);
        void Crear(Rol rol);
        void Actualizar(Rol rol);
        void Eliminar(int id);
    }
}
