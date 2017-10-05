using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BendiceLaMesa.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BendiceLaMesa.DAL
{
    public class BendiceMesaContext: DbContext
    {
        public BendiceMesaContext() : base("BendiceMesaContext")
        {
        }


        public DbSet<Oracion> Oraciones { get; set; }
        public DbSet<Propuesta> Propuestas { get; set; }
        public DbSet<Palabra> Palabras { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}