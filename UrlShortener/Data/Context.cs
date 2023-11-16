using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public class Context : DbContext
    {
        public DbSet<UrlModel> Urls { get; set; }

        public Context(DbContextOptions<Context> options)
       : base(options)
        {
        }
    }
}
