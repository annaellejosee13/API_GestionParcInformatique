using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace API_projet_parc_informatique.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ParcInfoBdContext _context;

        public TicketController(ParcInfoBdContext ticketService)
        {
            _context = ticketService;
        }

        /// <summary>
        /// Récupère tous les tickets.
        /// </summary>
        /// <returns>Liste de tous les tickets enregistrés.</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Récupère tous les tickets",
            Description = "Retourne la liste complète des tickets enregistrés dans la base de données."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();
            return Ok(tickets);
        }

        /// <summary>
        /// Récupère un ticket spécifique par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant du ticket à récupérer.</param>
        /// <returns>Un ticket spécifique ou une erreur 404 si le ticket n'existe pas.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Récupère un ticket par ID",
            Description = "Retourne un ticket spécifique à partir de son identifiant."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        /// <summary>
        /// Crée un nouveau ticket.
        /// </summary>
        /// <param name="ticket">Données du ticket à créer.</param>
        /// <returns>Le ticket créé avec succès.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Crée un nouveau ticket",
            Description = "Ajoute un nouveau ticket dans la base de données."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Ticket>> CreatePostTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }

        /// <summary>
        /// Met à jour un ticket existant.
        /// </summary>
        /// <param name="id">Identifiant du ticket à modifier.</param>
        /// <param name="ticket">Données mises à jour du ticket.</param>
        /// <returns>Status de l’opération.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Met à jour un ticket",
            Description = "Met à jour les informations d'un ticket existant."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            var t = await _context.Tickets.FindAsync(id);
            t.Description = ticket.Description;
            t.Date_creation = ticket.Date_creation;
            t.Status = ticket.Status;
            t.Priorite = ticket.Priorite;
            t.Id_poste = ticket.Id_poste;
            t.Id_user = ticket.Id_user;
            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Supprime un ticket existant.
        /// </summary>
        /// <param name="id">Identifiant du ticket à supprimer.</param>
        /// <returns>Status de la suppression.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprime un ticket",
            Description = "Supprime un ticket existant selon son identifiant."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Remove(ticket);
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }
    }
}
