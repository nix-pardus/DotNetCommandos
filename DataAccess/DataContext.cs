using Domain.Aggregates;
using Infrascructure.DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrascructure.DataAccess
{
    public class DataContext
        :DbContext
    {
        //TODO: нужен набор для заказа и сотрудника
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
        }
        
        public DataContext(DbContextOptions<DataContext> options)
        :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
