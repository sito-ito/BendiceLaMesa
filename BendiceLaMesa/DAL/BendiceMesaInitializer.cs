using BendiceLaMesa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BendiceLaMesa.DAL
{
    public class BendiceMesaInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BendiceMesaContext>
    {
        protected override void Seed(BendiceMesaContext context)
        {
            var oraciones = new List<Oracion>
            {
                new Oracion{Texto="Bendice estos alimentos",Autor="Anonimo" , AutorMail="" },

            };

            oraciones.ForEach(s => context.Oraciones.Add(s));
            context.SaveChanges();

            var Propuestas = new List<Propuesta>
            {
                new Propuesta{Texto="BEndice estos alimentos", Autor= "Anonimo", AutorMail="", OracionID=1 },
                new Propuesta{Texto="Propuesta 2", Autor= "Anonimo", AutorMail="" },
            };

            Propuestas.ForEach(s => context.Propuestas.Add(s));
            context.SaveChanges();


        }
    }
}