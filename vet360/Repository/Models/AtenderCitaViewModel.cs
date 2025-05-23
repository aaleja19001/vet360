using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Models;

namespace vet360.Models
{
    public class AtenderCitaViewModel
    {
        public Cita Cita { get; set; }
        public Mascota Mascota { get; set; }
        public IEnumerable<HistorialMedico> Historial { get; set; }
        public HistorialMedico NuevoHistorial { get; set; }
    }
}