using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace API_projet_parc_informatique.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosteController : ControllerBase
    {
        private readonly ParcInfoBdContext _context;

        public PosteController(ParcInfoBdContext posteService)
        {
            _context = posteService;
        }

        /// <summary>
        /// Récupère la liste de tous les postes informatiques.
        /// </summary>
        /// <returns>Une liste de postes au format DTO.</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Liste tous les postes",
            Description = "Récupère tous les postes informatiques présents en base de données."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PosteDTO>>> GetPostes()
        {
            var postes = await _context.Postes.ToListAsync();
            var dto = postes.Select(post => new PosteDTO(
                post.Id_poste,
                post.Marque,
                post.AdresseIp,
                post.AdresseMac,
                post.SystemeExploitation,
                post.Processeur,
                post.Ram,
                post.DisqueDur,
                post.Status,
                post.Id_salle
            )).ToList();

            return Ok(dto);
        }

        /// <summary>
        /// Récupère un poste spécifique selon son ID.
        /// </summary>
        /// <param name="id">L'identifiant du poste.</param>
        /// <returns>Le poste correspondant sous forme de DTO.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Récupère un poste par ID",
            Description = "Récupère un poste informatique spécifique en fonction de son identifiant."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PosteDTO>> GetPoste(int id)
        {
            var poste = await _context.Postes.FindAsync(id);

            if (poste == null)
            {
                return NotFound();
            }

            var dto = new PosteDTO(
                poste.Id_poste,
                poste.Marque,
                poste.AdresseIp,
                poste.AdresseMac,
                poste.SystemeExploitation,
                poste.Processeur,
                poste.Ram,
                poste.DisqueDur,
                poste.Status,
                poste.Id_salle
            );

            return Ok(dto);
        }

        /// <summary>
        /// Crée un nouveau poste informatique.
        /// </summary>
        /// <param name="poste">Les informations du poste à créer.</param>
        /// <returns>Le poste créé.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Crée un nouveau poste",
            Description = "Ajoute un nouveau poste informatique à la base de données."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Poste>> CreatePostPoste(Poste poste)
        {
            _context.Postes.Add(poste);
            await _context.SaveChangesAsync();
            return Ok(poste);
        }

        /// <summary>
        /// Met à jour un poste existant.
        /// </summary>
        /// <param name="id">L'identifiant du poste à mettre à jour.</param>
        /// <param name="poste">Les nouvelles données du poste.</param>
        /// <returns>Statut de l'opération.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Met à jour un poste",
            Description = "Modifie les données d’un poste existant selon son identifiant."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePutPoste(int id, PosteDTO poste)
        {
            if (id != poste.Id_poste)
            {
                return BadRequest();
            }

            var po = await _context.Postes.FindAsync(id);
            if (po == null)
            {
                return NotFound();
            }

            po.Marque = poste.Marque;
            po.AdresseIp = poste.AdresseIp;
            po.AdresseMac = poste.AdresseMac;
            po.SystemeExploitation = poste.SystemeExploitation;
            po.Processeur = poste.Processeur;
            po.Ram = poste.Ram;
            po.DisqueDur = poste.DisqueDur;
            po.Status = poste.Status;
            po.Id_salle = poste.Id_salle;

            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Supprime un poste selon son ID.
        /// </summary>
        /// <param name="id">L'identifiant du poste à supprimer.</param>
        /// <returns>Le poste supprimé.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprime un poste",
            Description = "Supprime un poste informatique en fonction de son identifiant."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePoste(int id)
        {
            var poste = await _context.Postes.FindAsync(id);
            if (poste == null)
            {
                return NotFound();
            }

            _context.Remove(poste);
            await _context.SaveChangesAsync();
            return Ok(poste);
        }
    }
}
