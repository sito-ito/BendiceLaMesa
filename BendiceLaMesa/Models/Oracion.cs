using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BendiceLaMesa.Models
{
    public class Oracion : Frase
    {
        [Range(0,5)]
        public decimal Puntuacion { get; set; }

        public virtual ICollection<Palabra> Palabras { get; set; }

    }
}