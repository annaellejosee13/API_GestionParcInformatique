using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Controllers;
using API_projet_parc_informatique.Models;
using Microsoft.EntityFrameworkCore;


namespace API_projet_parc_informatique.Services
{
    public class SalleService : ISalleService   
    {
        private readonly ParcInfoBdContext _context;

        public SalleService(ParcInfoBdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Salle>> GetAllSallesAsync()
        {
            return await _context.Salles
                .Include(s => s.Parc)
                .Include(s => s.Postes)
                .ToListAsync();
        }

        public async Task<Salle> GetSalleByIdAsync(int id)
        {
            return await _context.Salles
                .Include(s => s.Parc)
                .Include(s => s.Postes)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Salle> CreateSalleAsync(Salle salle)
        {
            _context.Salles.Add(salle);
            await _context.SaveChangesAsync();
            return salle;
        }

        public async Task UpdateSalleAsync(Salle salle)
        {
            _context.Entry(salle).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSalleAsync(int id)
        {
            var salle = await _context.Salles.FindAsync(id);
            if (salle != null)
            {
                _context.Salles.Remove(salle);
                await _context.SaveChangesAsync();
            }
        }   }
}
