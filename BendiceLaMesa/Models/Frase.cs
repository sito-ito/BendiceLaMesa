using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BendiceLaMesa.Models
{
    public class Frase
    {
        public int ID { get; set; }

        [StringLength(500, MinimumLength = 3)]
        public string Texto { get; set; }

        public int UsuarioID { get; set; }


        public virtual ICollection<Palabra> Palabras { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}