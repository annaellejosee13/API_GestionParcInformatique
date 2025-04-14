using API_projet_parc_informatique.Controllers;
using API_projet_parc_informatique.Models;

namespace API_projet_parc_informatique.Services
{
    public interface IPosteService
    {
        Task<IEnumerable<Poste>> GetAllPostesAsync();
        Task<Poste> GetPosteByIdAsync(int id);
        Task<Poste> CreatePosteAsync(Poste poste);
        Task UpdatePosteAsync(Poste poste);
        Task DeletePosteAsync(int id);
    }
}
