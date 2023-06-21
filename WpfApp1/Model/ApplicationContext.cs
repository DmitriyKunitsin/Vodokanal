using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using WpfApp1.Model.Date;

namespace WpfApp1.Model
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Device> device { get; set; } = null!;
        public DbSet<Result> result { get; set; } = null!;
        public DbSet<USL> usl { get; set; } = null!;
        public DbSet<VVOD> vvod { get; set; } = null!;
        public DbSet<Result_uslugi> uslugi { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Vodoconal;Trusted_Connection=True;");
            
        }


    }
}
