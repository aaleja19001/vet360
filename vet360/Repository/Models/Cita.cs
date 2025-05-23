using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace vet360.Models
{
    public class Cita
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un horario")]
        public int HorarioId { get; set; }
        public string Estado { get; set; }
        public int UsuarioId { get; set; } // cliente
        public int MascotaId { get; set; }
        public int ServicioId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un veterinario")]
        public int VeterinarioId { get; set; }

        public virtual Usuario Usuario { get; set; } // cliente
        public virtual Mascota Mascota { get; set; }
        public virtual Servicio Servicio { get; set; }
        public virtual Horario Horario { get; set; }

        public virtual ICollection<HistorialMedico> HistorialesMedicos { get; set; }
    }
}