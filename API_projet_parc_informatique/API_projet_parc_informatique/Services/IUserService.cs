using API_projet_parc_informatique.Controllers;
using API_projet_parc_informatique.Models;

namespace API_projet_parc_informatique.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUtilisateursAsync();
        Task<User> GetUtilisateurByIdAsync(int id);
        Task<User> CreateUtilisateurAsync(User utilisateur);
        Task UpdateUtilisateurAsync(User utilisateur);
        Task DeleteUtilisateurAsync(int id);
    }
}
