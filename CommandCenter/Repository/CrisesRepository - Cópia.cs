using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandCenter.Configurations;
using CommandCenter.Models;  // ✅ Garantindo a referência correta ao model

namespace CommandCenter.Repository
{
    public class CrisesRepository
    {
        private readonly IMongoCollection<CrisesModel> _crises;

        public CrisesRepository(IDatabaseConfig config) // ✅ Agora usa a interface correta
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);
            _crises = database.GetCollection<CrisesModel>("Crises");
        }

        // ✅ Retorna todas as crises registradas no MongoDB
        public async Task<List<CrisesModel>> GetAllAsync() =>
            await _crises.Find(crise => true).ToListAsync();

        // ✅ Busca uma crise pelo ID
        public async Task<CrisesModel> GetByIdAsync(string id) =>
            await _crises.Find(crise => crise.Id == id).FirstOrDefaultAsync();

        // ✅ Insere uma nova crise no banco
        public async Task CreateAsync(CrisesModel crise) =>
            await _crises.InsertOneAsync(crise);

        // ✅ Atualiza uma crise existente pelo ID
        public async Task<bool> UpdateAsync(string id, CrisesModel crise)
        {
            var resultado = await _crises.ReplaceOneAsync(c => c.Id == id, crise);
            return resultado.ModifiedCount > 0;  // Retorna `true` se houve modificação
        }

        // ✅ Remove uma crise pelo ID
        public async Task<bool> DeleteAsync(string id)
        {
            var resultado = await _crises.DeleteOneAsync(c => c.Id == id);
            return resultado.DeletedCount > 0;  // Retorna `true` se houve exclusão
        }
    }
}
