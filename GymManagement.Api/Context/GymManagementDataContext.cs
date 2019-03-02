using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using GymManagement.DataModel;

namespace GymManagement.Api.Context
{
    public class GymManagementDataContext : DbContext
    {
        public GymManagementDataContext() { }
        public GymManagementDataContext(DbContextOptions<GymManagementDataContext> options) : base(options) { }

        public DbSet<Class> Classes { get; set; }
        //public DbSet<ClassSchedule> ClassSechedules { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<GymManagementLog> GymManagementLogs { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<DataModel.Type> Types { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["gym_management"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
