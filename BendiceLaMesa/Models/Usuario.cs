using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BendiceLaMesa.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Oracion>  Oraciones { get; set; }
        public virtual ICollection<Propuesta> Propuestas { get; set; }
        
    }
}