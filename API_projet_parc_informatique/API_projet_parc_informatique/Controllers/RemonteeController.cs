using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace API_projet_parc_informatique.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemonteeController : ControllerBase
    {
        private readonly ParcInfoBdContext _context;

        public RemonteeController(ParcInfoBdContext remonteeService)
        {
            _context = remonteeService;
        }

        /// <summary>
        /// Récupère toutes les remontées d'information.
        /// </summary>
        /// <returns>Liste complète des remontées.</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Liste toutes les remontées",
            Description = "Récupère toutes les remontées enregistrées dans la base de données."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Remontees>>> GetRemontees()
        {
            var remontees = await _context.Remontees.ToListAsync();
            return Ok(remontees);
        }

        /// <summary>
        /// Crée une nouvelle remontée d'information.
        /// </summary>
        /// <param name="remontee">Les données de la remontée à enregistrer.</param>
        /// <returns>La remontée créée.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Crée une nouvelle remontée",
            Description = "Ajoute une nouvelle remontée dans la base de données."
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostRemontee([FromBody] Remontees remontee)
        {
            if (remontee == null)
            {
                return BadRequest("Les données sont invalides.");
            }

            _context.Remontees.Add(remontee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRemontees), new { id = remontee.Id }, remontee);
        }
    }
}
