using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vet360.Models
{
    public class AgendarCitaViewModel
    {
        public int VeterinarioId { get; set; }
        public DateTime Fecha { get; set; }
        public int HorarioId { get; set; }
        public int MascotaId { get; set; }
        public int ServicioId { get; set; }
        public string Observaciones { get; set; }

        public List<SelectListItem> Veterinarios { get; set; }
        public List<SelectListItem> Mascotas { get; set; }
        public List<SelectListItem> Servicios { get; set; }
        public List<SelectListItem> HorariosDisponibles { get; set; }
    }
}