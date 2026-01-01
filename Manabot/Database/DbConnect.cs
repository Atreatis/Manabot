using Manabot.Database.Models;
using MongoDB.Driver;

namespace Manabot.Database;

/// <summary>
/// DbConnect performs several tasks for CRUD operations within the Discord bot. These CRUD actions are either triggered
/// through `events` or `/commands` on Discord.
/// </summary>
public class DbConnect
{
    private static IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_URI"))!;
        IMongoDatabase database = client.GetDatabase(Environment.GetEnvironmentVariable("DATABASE_NAME"));
        return database.GetCollection<T>(collectionName);   
    }

    // Search Queries
    public async Task<List<T>> FindAsync<T>(string collection, FilterDefinition<T> filter)
    {
        return await GetCollection<T>(collection).Find(filter).ToListAsync();
    }
    
    public async Task<T> FindOneAsync<T>(string collection, FilterDefinition<T> filter)
    {
        return await GetCollection<T>(collection).Find(filter).FirstOrDefaultAsync();
    }
    
    // Insert Queries
    public async Task<T> InsertOneAsync<T>(string collection, T entity)
    {
        await GetCollection<T>(collection).InsertOneAsync(entity);
        return entity;
    }
    
    // Update Queries
    public async Task<UpdateResult> UpdateOneAsync<T>(string collection, FilterDefinition<T> filter, UpdateDefinition<T> update)
    {
        return await GetCollection<T>(collection).UpdateOneAsync(filter, update);
    }
    
    // Delete Queries
    public async Task<DeleteResult> DeleteOneAsync<T>(string collection, FilterDefinition<T> filter)
    {
        return await GetCollection<T>(collection).DeleteOneAsync(filter);
    }

    public async Task<DeleteResult> DeleteManyAsync<T>(string collection, FilterDefinition<T> filter)
    {
        return await GetCollection<T>(collection).DeleteManyAsync(filter);
    }
}