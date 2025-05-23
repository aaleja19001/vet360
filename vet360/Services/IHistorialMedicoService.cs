using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vet360.Models;

namespace vet360.Services
{
    public interface IHistorialMedicoService
    {
        List<HistorialMedico> ListarTodos();
        HistorialMedico ObtenerPorId(int id);
        List<HistorialMedico> ObtenerPorMascota(int mascotaId);
        List<HistorialMedico> ObtenerPorVeterinario(int veterinarioId);
        void Crear(HistorialMedico historial);
        void Actualizar(HistorialMedico historial);
        void Eliminar(int id);
    }
}
