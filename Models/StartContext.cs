using starteAlkemy.Models.ViewModels;
using System.Data.Entity;

namespace starteAlkemy.Models
{
    public class StartContext : DbContext

    {
        #region Constructor 
        public StartContext() : base("localhost")
        {

        }
        #endregion

        #region Tables
        public DbSet<Admin> Admin { get; set; }
        
        public DbSet<Home> Home { get; set; }
        public DbSet<LinksMm> LinksMm { get; set; }


        public DbSet<Category> Categories { get; set; }

        public DbSet<Project> Project { get; set; }
        public DbSet<HomeImages> HomeImages { get; set; }

        #endregion

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    Database.SetInitializer<StartContext>(null);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}