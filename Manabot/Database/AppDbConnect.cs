using Manabot.Database.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using MongoDB.Driver;

namespace Manabot.Database;

/// <summary>
/// AppDbConnect<br/>
/// Over here the connections are made with the Database and allows DbConnect.cs to communicate with the Database.
/// </summary>
/// <param name="options"></param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public static AppDbContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<AppDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<GuildSettings>().ToCollection("GuildSettings");
    }
}