using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CampusNews.Models;

namespace CampusNews.Data
{
    public class CampusNewsContext : DbContext
    {
        public CampusNewsContext (DbContextOptions<CampusNewsContext> options)
            : base(options)
        {
        }

        public DbSet<CampusNews.Models.Newws> Newws { get; set; } = default!;
        public DbSet<CampusNews.Models.User> User { get; set; } = default!;
    }
}
