using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace API_projet_parc_informatique.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalleController : ControllerBase
    {
        private readonly ParcInfoBdContext _context;

        public SalleController(ParcInfoBdContext salleService)
        {
            _context = salleService;
        }

        /// <summary>
        /// Récupère toutes les salles.
        /// </summary>
        /// <returns>Liste des salles enregistrées.</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Récupère toutes les salles",
            Description = "Retourne la liste complète des salles enregistrées dans la base de données."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Salle>>> GetSalles()
        {
            var salles = await _context.Salles.ToListAsync();
            return Ok(salles);
        }

        /// <summary>
        /// Récupère une salle spécifique par son identifiant.
        /// </summary>
        /// <param name="id">ID de la salle à récupérer.</param>
        /// <returns>Une salle spécifique ou une erreur 404.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Récupère une salle par ID",
            Description = "Retourne une salle spécifique à partir de son identifiant."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Salle>> GetSalle(int id)
        {
            var salle = await _context.Salles.FindAsync(id);
            if (salle == null)
            {
                return NotFound();
            }
            return Ok(salle);
        }

        /// <summary>
        /// Crée une nouvelle salle.
        /// </summary>
        /// <param name="salle">Données de la salle à créer.</param>
        /// <returns>Salle créée avec succès.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Crée une nouvelle salle",
            Description = "Ajoute une nouvelle salle dans la base de données."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Salle>> CreatePostSalle(Salle salle)
        {
            _context.Salles.Add(salle);
            await _context.SaveChangesAsync();
            return Ok(salle);
        }

        /// <summary>
        /// Met à jour une salle existante.
        /// </summary>
        /// <param name="id">Identifiant de la salle à modifier.</param>
        /// <param name="salle">Données mises à jour de la salle.</param>
        /// <returns>Status de l’opération.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Met à jour une salle",
            Description = "Met à jour les informations d'une salle existante."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePutSalle(int id, Salle salle)
        {
            if (id != salle.Id)
            {
                return BadRequest();
            }

            var sa = await _context.Salles.FindAsync(id);
            sa.Nom = salle.Nom;
            sa.Id_parc = salle.Id_parc;

            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Supprime une salle existante.
        /// </summary>
        /// <param name="id">Identifiant de la salle à supprimer.</param>
        /// <returns>Status de la suppression.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprime une salle",
            Description = "Supprime une salle existante selon son identifiant."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSalle(int id)
        {
            var salle = await _context.Salles.FindAsync(id);
            if (salle == null)
            {
                return NotFound();
            }

            _context.Remove(salle);
            await _context.SaveChangesAsync();
            return Ok(salle);
        }
    }
}
