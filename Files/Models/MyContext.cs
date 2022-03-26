using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Files.Models
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions options)
            : base(options) { }

        public DbSet<FileModel> Files { get; set; }
    }
}
