using Microsoft.EntityFrameworkCore;
using GamesStore.Api.Data;
using GameStore.Api.Models;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbConntext = scope.ServiceProvider
                                            .GetRequiredService<GameStoreContext>();
        dbConntext.Database.Migrate();
    }
    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("GameStore");

        // DbContext has a scoped service lifetime because
        // 1. it insure that the new instance of dbcontext created per request
        // 2. DB connections are limited and expensive resources
        // 3. DbContext is not a thread safe DbContext avoids the concurrency
        // 4. makes it easier to manage transaction and ensure data consistency  
        //5. reusing DbContext instance can lead to incresed memor usage
        
        builder.Services.AddSqlite<GameStoreContext>(
                connString,
                optionsAction: options => options.UseSeeding((context, _) =>
{
    if (!context.Set<Genre>().Any())
    {
        context.Set<Genre>().AddRange(
            new Genre { Name = "Action" },
            new Genre { Name = "Adventure" },
            new Genre { Name = "RPG" },
            new Genre { Name = "Simulation" },
            new Genre { Name = "Strategy" }
        );
        context.SaveChanges();
    }
})
);
}}