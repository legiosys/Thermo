using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thermo_Server_Domain.Model
{
    public class DomainContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Temperature> Temperatures { get; set; }

        public DomainContext(DbContextOptions<DomainContext> options)
            : base(options)
        {
        }
    }
}
