using MongoDB.Driver;
using CommandCenter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandCenter.Configurations;
using MongoDB.Bson;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CommandCenter.Repository
{
    public class CrisesRepository : ICrisesRepository
    {
        private readonly IMongoCollection<CrisesModel> _crisesCollection;

        public CrisesRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);
            _crisesCollection = database.GetCollection<CrisesModel>(databaseConfig.CrisesCollectionName);
        }

        public async Task<List<CrisesModel>> GetAllAsync()
        {
            return await _crisesCollection.Find(_ => true).ToListAsync();
        }

        public async Task<CrisesModel> GetByIdAsync(Guid id)
        {
            return await _crisesCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(CrisesModel crise)
        {
            await _crisesCollection.InsertOneAsync(crise);
        }

        public async Task<bool> UpdateAsync(Guid id, CrisesModel crise)
        {
            var result = await _crisesCollection.ReplaceOneAsync(c => c.Id == id, crise);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _crisesCollection.DeleteOneAsync(c => c.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task AtualizarHistoricoAsync(Guid id, string novoHistorico) { 
            var filtro = Builders<CrisesModel>.Filter.Eq(r => r.Id, id); 
            var update = Builders<CrisesModel>.Update.Push(r => r.AcoesRealizadas, novoHistorico); 
            await _crisesCollection.UpdateOneAsync(filtro, update);
        }

        // CRIAR VARIAVEL DATAENCERRAMENTO NA MODELCRISES 

        public async Task EncerrarCrise(Guid id, string novoHistorico, string solucaoAplicada, string causaRaizResolvida)
        {
            var filtro = Builders<CrisesModel>.Filter.Eq(r => r.Id, id);

            var updates = new List<UpdateDefinition<CrisesModel>>();

            // Atualiza DataEncerramento
            updates.Add(Builders<CrisesModel>.Update.Set(r => r.DataEncerramento, DateTime.Now));

            // Atualiza SolucaoAplicada
            updates.Add(Builders<CrisesModel>.Update.Set(r => r.SolucaoAplicada, solucaoAplicada));

            // Atualiza CausaRaizResolvida
            updates.Add(Builders<CrisesModel>.Update.Set(r => r.CausaRaizResolvida, causaRaizResolvida));

            // Se houver novoHistorico, adiciona no AcoesRealizadas
            if (!string.IsNullOrWhiteSpace(novoHistorico))
            {
                updates.Add(Builders<CrisesModel>.Update.Push(r => r.AcoesRealizadas, novoHistorico));
            }

            // Aplica todas as atualizações
            var updateDefinition = Builders<CrisesModel>.Update.Combine(updates);

            await _crisesCollection.UpdateOneAsync(filtro, updateDefinition);
        }



        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                var count = await _crisesCollection.CountDocumentsAsync(Builders<CrisesModel>.Filter.Empty);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao conectar ao MongoDB: " + ex.Message);
                return false;
            }
        }
    }
}
