using Microsoft.EntityFrameworkCore;
using NetCoreReact.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreReact.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
     
        public DbSet<Movement> Movement { get; set; }
        
        public DbSet<Category> Category { get; set; }

        public DbSet<User> User { get; set; }

       
    }
}
