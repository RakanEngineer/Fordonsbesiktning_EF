using Fordonsbesiktning_EF.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fordonsbesiktning_EF.Data
{
    class FordonsbesiktningContext : DbContext
    {
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Inspection> Inspection { get; set; }

        //private ILoggerFactory Logger { get; }
        //public FordonsbesiktningContext(ILoggerFactory logger)
        //{
        //    Logger = logger;
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(Logger);

            optionsBuilder.UseSqlServer("Server=(local);Database=Fordonsbesiktning;Integrated Security=true; Encrypt=True;Trust Server Certificate=True");
        }
        //ctor TAB TAB
        //public FordonsbesiktningContext()
        //{

        //}
    }
}