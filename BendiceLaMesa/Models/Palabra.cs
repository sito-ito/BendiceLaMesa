using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BendiceLaMesa.Models
{
    public class Palabra
    {
        public int ID { get; set; }
        public string Valor { get; set; }

        public virtual ICollection<Oracion> Oraciones { get; set; }
    }
}