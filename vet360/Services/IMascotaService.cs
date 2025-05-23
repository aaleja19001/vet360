using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Models;

namespace vet360.Services
{
    public interface IMascotaService
    {
        IEnumerable<Mascota> ObtenerTodas();
        Mascota ObtenerPorId(int id);
        void Crear(Mascota mascota);
        void Editar(Mascota mascota);
        void Eliminar(int id);

        IEnumerable<Mascota> ObtenerPorUsuario(int usuarioId);
    }
}