using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace vet360.Models
{
    [Table("Roles")]
    public class Rol
    {
        public int RolId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }

        public bool Activo { get; set; } = true;

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}