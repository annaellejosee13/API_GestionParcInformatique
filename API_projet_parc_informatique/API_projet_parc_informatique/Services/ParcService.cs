using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Controllers;
using API_projet_parc_informatique.Models;
using Microsoft.EntityFrameworkCore;

namespace API_projet_parc_informatique.Services
{
    public class ParcService : IParcService
    {
        private readonly ParcInfoBdContext _context;
        public ParcService(ParcInfoBdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Parc>> GetAllParcsAsync()
        {
            return await _context.Parcs.Include(p => p.Salles).ToListAsync();
        }

        public async Task<Parc> GetParcByIdAsync(int id)
        {
            return await _context.Parcs
                .Include(p => p.Salles)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Parc> CreateParcAsync(Parc parc)
        {
            _context.Parcs.Add(parc);
            await _context.SaveChangesAsync();
            return parc;
        }

        public async Task UpdateParcAsync(Parc parc)
        {
            _context.Entry(parc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteParcAsync(int id)
        {
            var parc = await _context.Parcs.FindAsync(id);
            if (parc != null)
            {
                _context.Parcs.Remove(parc);
                await _context.SaveChangesAsync();
            }
        }
    }
}
