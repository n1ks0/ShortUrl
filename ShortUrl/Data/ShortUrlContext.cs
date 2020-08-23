using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShortUrl.Models;

namespace ShortUrl.Data
{
    public class ShortUrlContext : DbContext
    {
        public ShortUrlContext (DbContextOptions<ShortUrlContext> options)
            : base(options)
        {
        }

        public DbSet<ShortUrl.Models.Url> Url { get; set; }
    }
}
