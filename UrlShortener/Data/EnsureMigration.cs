using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Data
{
    public static class EnsureMigration
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<T>();
                context.Database.Migrate();
            }
           
        }
    }
}
