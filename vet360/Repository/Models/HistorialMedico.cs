using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vet360.Models
{
    public class HistorialMedico
    {
        public int Id { get; set; }
        public int MascotaId { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int UsuarioId { get; set; } // Veterinario
        public int? CitaId { get; set; }

        public virtual Mascota Mascota { get; set; }
        public virtual Usuario Veterinario { get; set; }
        public virtual Cita Cita { get; set; }
    }
}