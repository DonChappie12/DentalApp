using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DentalWebApi.Models
{
    public class DentalContext : IdentityDbContext<User,IdentityRole<int>,int>
    {
        public DentalContext(DbContextOptions<DentalContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Precsription> Precsriptions { get; set; }
        
    }
}