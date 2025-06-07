using CommandCenter.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommandCenter.Repository
{
    public interface IInformativosRepository
    {
        Task<List<InformativosModel>> GetAllAsync();
        Task<InformativosModel> GetByIdAsync(Guid id);
        Task CreateAsync(InformativosModel informativo);
        Task<bool> UpdateAsync(Guid id, InformativosModel informativo);
        Task<bool> DeleteAsync(Guid id);
    }
}
