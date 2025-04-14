using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Controllers;
using API_projet_parc_informatique.Models;
using Microsoft.EntityFrameworkCore;

namespace API_projet_parc_informatique.Services
{
    public class UserService : IUserService 
    {
        private readonly ParcInfoBdContext _context;
        public UserService(ParcInfoBdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUtilisateursAsync()
        {
            return await _context.Users
                //.Include(u => u.Postes)
                .Include(u => u.Tickets)
                .ToListAsync();
        }

        public async Task<User> GetUtilisateurByIdAsync(int id)
        {
            return await _context.Users
               // .Include(u => u.Postes)
                .Include(u => u.Tickets)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> CreateUtilisateurAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUtilisateurAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUtilisateurAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
