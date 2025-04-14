using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Controllers;
using API_projet_parc_informatique.Models;
using Microsoft.EntityFrameworkCore;

namespace API_projet_parc_informatique.Services
{
    public class PosteService : IPosteService
    {
        private readonly ParcInfoBdContext _context;
        public PosteService(ParcInfoBdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Poste>> GetAllPostesAsync()
        {
            return await _context.Postes
                .Include(p => p.Salle)
                //.Include(p => p.User)
                .Include(p => p.Tickets)
                .ToListAsync();
        }

        public async Task<Poste> GetPosteByIdAsync(int id)
        {
            return await _context.Postes
                .Include(p => p.Salle)
                //.Include(p => p.User)
                .Include(p => p.Tickets)
                .FirstOrDefaultAsync(p => p.Id_poste == id);
        }

        public async Task<Poste> CreatePosteAsync(Poste poste)
        {
            _context.Postes.Add(poste);
            await _context.SaveChangesAsync();
            return poste;
        }

        public async Task UpdatePosteAsync(Poste poste)
        {
            _context.Entry(poste).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePosteAsync(int id)
        {
            var poste = await _context.Postes.FindAsync(id);
            if (poste != null)
            {
                _context.Postes.Remove(poste);
                await _context.SaveChangesAsync();
            }
        }
    }
}
