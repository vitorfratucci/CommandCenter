using System.Collections.Generic;
using System.Threading.Tasks;
using CommandCenter.Models;

namespace CommandCenter.Repository
{
    public interface ICrisesRepository
    {
        Task<List<CrisesModel>> GetAllAsync();
        Task<CrisesModel> GetByIdAsync(Guid id);
        Task CreateAsync(CrisesModel crise);
        Task<bool> UpdateAsync(Guid id, CrisesModel crise);
        Task<bool> DeleteAsync(Guid id);
        Task AtualizarHistoricoAsync(Guid id, string novoHistorico);
        Task EncerrarCrise(Guid id, string novoHistorico);

        // Método adicionado para testar conexão com o MongoDB
        Task<bool> TestConnectionAsync();
    }
}
