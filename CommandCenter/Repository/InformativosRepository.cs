using CommandCenter.Configurations;
using CommandCenter.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommandCenter.Repository
{
    public class InformativosRepository : IInformativosRepository
    {
        private readonly IMongoCollection<InformativosModel> _informativosCollection;

        public InformativosRepository(IDatabaseConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);
            _informativosCollection = database.GetCollection<InformativosModel>(config.InformativosCollectionName);
        }

        public async Task<List<InformativosModel>> GetAllAsync()
        {
            return await _informativosCollection.Find(_ => true).ToListAsync();
        }

        public async Task<InformativosModel> GetByIdAsync(Guid id)
        {
            return await _informativosCollection.Find(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(InformativosModel informativo)
        {
            await _informativosCollection.InsertOneAsync(informativo);
        }

        public async Task<bool> UpdateAsync(Guid id, InformativosModel informativo)
        {
            var result = await _informativosCollection.ReplaceOneAsync(i => i.Id == id, informativo);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _informativosCollection.DeleteOneAsync(i => i.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
