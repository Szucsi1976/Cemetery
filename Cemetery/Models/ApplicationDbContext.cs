using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cemetery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cemetery.Models
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Grave> Graves { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<Burial> Burials { get; set; }
        public DbSet<Redemption> Redemptions { get; set; }
        public DbSet<Renter> Renters { get; set; }
        public DbSet<Dead> Deads { get; set; }
        public DbSet<Undertaker> Undertakers { get; set; }
        public DbSet<Religion> Religions { get; set; }
    }
}
