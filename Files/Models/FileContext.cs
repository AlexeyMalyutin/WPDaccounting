using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Files.Models
{
    public class FileContext : DbContext
    {
        public FileContext(DbContextOptions options)
            : base(options) { }

        public DbSet<WPD> WPDs { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
