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
            }

        public System.Data.Entity.DbSet<MIS4200_Team10.Models.UserDetails> UserDetails { get; set; }
        // Include each object here. The value inside <> is the name of the class,
        // the value outside should generally be the plural of the class name
        // and is the name used to reference the entity in code
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<OrderDetail> OrderDetails { get; set; }
    }
    }
   