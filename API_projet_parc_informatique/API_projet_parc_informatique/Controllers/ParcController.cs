using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace API_projet_parc_informatique.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ParcController : ControllerBase
    {
        private readonly ParcInfoBdContext _context;

        public ParcController(ParcInfoBdContext parcService)
        {
            _context = parcService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Récupère tous les parcs informatiques",
            Description = "Retourne la liste complète des parcs informatiques présents en base de données."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Parc>>> GetParcs()
        {
            var parcs = await _context.Parcs.ToListAsync();
            return Ok(parcs);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Récupère un parc par son ID",
            Description = "Retourne un parc informatique spécifique selon l'identifiant fourni."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Parc>> GetParc(int id)
        {
            var parc = await _context.Parcs.FindAsync(id);
            if (parc == null)
            {
                return NotFound();
            }
            return Ok(parc);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Crée un nouveau parc",
            Description = "Ajoute un nouveau parc informatique avec les données fournies."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Parc>> CreatePostParc(Parc parc)
        {
            _context.Parcs.Add(parc);
            await _context.SaveChangesAsync();
            return Ok(parc);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Met à jour un parc existant",
            Description = "Met à jour les informations d’un parc informatique existant en fonction de l'identifiant fourni."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePutParc(int id, Parc parc)
        {
            if (id != parc.Id)
            {
                return BadRequest();
            }

            var p = await _context.Parcs.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }

            p.Name = parc.Name;
            p.Localisation = parc.Localisation;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprime un parc",
            Description = "Supprime un parc informatique en fonction de son identifiant."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteParc(int id)
        {
            var parc = await _context.Parcs.FindAsync(id);
            if (parc == null)
            {
                return NotFound();
            }

            _context.Remove(parc);
            await _context.SaveChangesAsync();
            return Ok(parc);
        }
    }
}
