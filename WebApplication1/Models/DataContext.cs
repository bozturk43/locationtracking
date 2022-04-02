using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DataContext:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DataContext() : base("dbConnect")
        {

        }

        public DbSet<Sevkiyat> Sevkiyats { get; set; }
        public DbSet <Arac> Aracs { get; set; }
        public DbSet <Sofor> Sofors { get; set; }
        public DbSet <Koordinat>Koordinats { get; set; }

    }
}