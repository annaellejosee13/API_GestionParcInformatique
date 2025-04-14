using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Controllers;
using API_projet_parc_informatique.Models;
using Microsoft.EntityFrameworkCore;

namespace API_projet_parc_informatique.Services
{
    public class TicketService
    {
        private readonly ParcInfoBdContext _context;
        public TicketService(ParcInfoBdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets
                .Include(t => t.Poste)
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _context.Tickets
                .Include(t => t.Poste)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            ticket.Date_creation = DateTime.Now;
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
