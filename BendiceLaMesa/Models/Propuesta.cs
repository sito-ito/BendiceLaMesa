using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BendiceLaMesa.Models
{
    public class Propuesta: Frase
    {
        public int? OracionID { get; set; }

        public virtual Oracion Oracion { get; set; }

    }
}