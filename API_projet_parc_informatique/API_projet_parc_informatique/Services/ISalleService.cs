using API_projet_parc_informatique.Controllers;
using API_projet_parc_informatique.Models;

namespace API_projet_parc_informatique.Services
{
    public interface ISalleService
    {
        Task<IEnumerable<Salle>> GetAllSallesAsync();
        Task<Salle> GetSalleByIdAsync(int id);
        Task<Salle> CreateSalleAsync(Salle salle);
        Task UpdateSalleAsync(Salle salle);
        Task DeleteSalleAsync(int id);
    }
}
