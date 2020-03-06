using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIS4200_Team10.Models;
using System.Data.Entity;


namespace MIS4200_Team10.DAL
{


    public class MIS4200Context : DbContext // inherits from DbContext
    {
        public MIS4200Context() : base("name=DefaultConnection")
        {
            // this method is a 'constructor' and is called when a new context is created
            // the base attribute says which connection string to use

            // add the SetInitializer statement here
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MIS4200Context,
           MIS4200_Team10.Migrations.MISContext.Configuration>("DefaultConnection"));
        }

        public DbSet<UserDetails> userDetails;

        public DbSet<UserDetails> GetUserDetails()
        {
            return userDetails;
        }

        public void SetUserDetails(DbSet<UserDetails> value)
        {
            userDetails = value;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<MIS4200_Team10.Models.UserDetails> UserDetails { get; set; }
        public object Users { get; internal set; }
    }
}
   