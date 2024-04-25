using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DentalWebApi.Models
{
    public class DentalContext : DbContext
    {
        public DentalContext(DbContextOptions<DentalContext> options)
            : base(options)
        {
            
        }
    }
}