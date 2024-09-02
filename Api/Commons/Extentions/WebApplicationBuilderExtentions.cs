using Microsoft.EntityFrameworkCore;
using Core.DAL;

namespace Api.Commons.Extentions
{
    public static class WebApplicationBuilderExtentions
    {

        public static WebApplication UpdateMigration(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<DatabaseContext>();
            if (context is not null && context.Database.IsRelational())
                context.Database.Migrate();

            var x = context.Database.GetConnectionString();

            return app;
        }
    }
}
