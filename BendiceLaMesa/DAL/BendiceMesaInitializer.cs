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
            var usuario = new List<Usuario>
            {
                new Usuario{Nombre="Anónimo",Email=""},
                new Usuario{Nombre="Mari Patxi Ayerra", Email="mpatxiayerra@gmail.com"}

            };

            usuario.ForEach(s => context.Usuarios.Add(s));
            context.SaveChanges();


        }
    }
}