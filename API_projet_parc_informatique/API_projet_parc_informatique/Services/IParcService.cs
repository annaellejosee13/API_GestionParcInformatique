using API_projet_parc_informatique.Controllers;
using API_projet_parc_informatique.Models;

namespace API_projet_parc_informatique.Services
{
    public interface IParcService
    {
        Task<IEnumerable<Parc>> GetAllParcsAsync();
        Task<Parc> GetParcByIdAsync(int id);
        Task<Parc> CreateParcAsync(Parc parc);
        Task UpdateParcAsync(Parc parc);
        Task DeleteParcAsync(int id);
    }
}
