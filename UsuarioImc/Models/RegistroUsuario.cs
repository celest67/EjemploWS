using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsuarioImc.Models
{
    public class RegistroUsuario
    {
        public string IdUsuario { get; set; } 
        public decimal peso { get; set; }
        public decimal altura { get; set; }
    }
}