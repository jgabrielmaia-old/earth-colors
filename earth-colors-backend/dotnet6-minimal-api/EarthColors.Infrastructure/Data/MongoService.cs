namespace EarthColors.Infrastructure.Data;

using System;
using MongoDB.Driver;

public class MongoService<T>
{
    private readonly IMongoCollection<T> _collection;

    public MongoService(
        IOptions<MongoConnectionSettings> mongoConnectionSettings)
    {
        var mongoClient = new MongoClient(
            mongoConnectionSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoConnectionSettings.Value.DatabaseName);

        _collection = mongoDatabase.GetCollection<T>(
            mongoConnectionSettings.Value.TsCollectionName);
    }

    public async Task<List<T>> GetAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<T?> GetAsync(Guid id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(T newEntity) =>
        await _collection.InsertOneAsync(newEntity);

    public async Task UpdateAsync(Guid id, T updatedEntity) =>
        await _collection.ReplaceOneAsync(x => x.Id == id, updatedEntity);

    public async Task RemoveAsync(Guid id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);
}