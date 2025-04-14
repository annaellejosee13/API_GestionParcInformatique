using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace API_projet_parc_informatique.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ParcInfoBdContext _context;

        public UserController(ParcInfoBdContext userService)
        {
            _context = userService;
        }

        /// <summary>
        /// Récupère la liste de tous les utilisateurs.
        /// </summary>
        /// <returns>Liste des utilisateurs enregistrés dans la base de données.</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Récupère tous les utilisateurs",
            Description = "Retourne la liste complète de tous les utilisateurs enregistrés dans la base de données."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetUtilisateurs()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        /// <summary>
        /// Récupère un utilisateur spécifique par son identifiant.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur à récupérer.</param>
        /// <returns>Un utilisateur spécifique ou une erreur 404 si l'utilisateur n'existe pas.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Récupère un utilisateur par ID",
            Description = "Retourne un utilisateur spécifique à partir de son identifiant."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetUtilisateur(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        /// <summary>
        /// Crée un nouvel utilisateur.
        /// </summary>
        /// <param name="user">Données de l'utilisateur à créer.</param>
        /// <returns>L'utilisateur créé avec succès.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Crée un nouvel utilisateur",
            Description = "Ajoute un nouvel utilisateur dans la base de données avec un mot de passe haché."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> CreatePostUtilisateur(User user)
        {
            string motDePasseHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var utilisateur = new User
            {
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email,
                Password = motDePasseHash,
                Role = user.Role,
            };

            _context.Users.Add(utilisateur);
            await _context.SaveChangesAsync();
            return Ok(utilisateur);
        }

        /// <summary>
        /// Met à jour un utilisateur existant.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur à modifier.</param>
        /// <param name="user">Données mises à jour de l'utilisateur.</param>
        /// <returns>Status de l'opération de mise à jour.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Met à jour un utilisateur",
            Description = "Met à jour les informations d'un utilisateur existant dans la base de données."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePutUtilisateur(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            var u = await _context.Users.FindAsync(id);
            u.Nom = user.Nom;
            u.Prenom = user.Prenom;
            u.Email = user.Email;
            u.Password = user.Password;
            u.Role = user.Role;

            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Supprime un utilisateur existant.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur à supprimer.</param>
        /// <returns>Status de la suppression.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Supprime un utilisateur",
            Description = "Supprime un utilisateur existant selon son identifiant."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
    }
}
