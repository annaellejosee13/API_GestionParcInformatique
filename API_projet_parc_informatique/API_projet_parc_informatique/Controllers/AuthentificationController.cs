using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Swashbuckle.AspNetCore.Annotations;
using API_projet_parc_informatique.BD;
using API_projet_parc_informatique.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using API_projet_parc_informatique.Models;

namespace API_projet_parc_informatique.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        ParcInfoBdContext _context;

        public AuthentificationController(ParcInfoBdContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Authentifie un utilisateur avec ses identifiants (email et mot de passe).
        /// </summary>
        /// <param name="loginDTO">DTO contenant l'email et le mot de passe de l'utilisateur</param>
        /// <returns>Retourne un token JWT si l'authentification est réussie.</returns>
        /// <response code="200">Retourne un token JWT</response>
        /// <response code="400">Si l'email ou le mot de passe est manquant</response>
        /// <response code="401">Si l'authentification échoue (email ou mot de passe incorrect)</response>
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Authentifie un utilisateur et génère un token JWT", Description = "Authentifie un utilisateur avec son email et son mot de passe, et retourne un token JWT.")]



        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            Console.WriteLine("lancement login");
            if (loginDTO == null || string.IsNullOrWhiteSpace(loginDTO.Email) || string.IsNullOrWhiteSpace(loginDTO.MotDePasse))
                return BadRequest(new { Message = "Email et mot de passe sont requis." });

            if (!await Authenticate(loginDTO))
                return Unauthorized(new { Message = "Email ou mot de passe incorrect." });

            var utilisateur = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);
            string token = this.GenerateJwtToken(utilisateur);

            var obj = new
            {
                token
            };
            return Ok(obj);
        }

        /// <summary>
        /// Authentification basique avec comparaison de mot de passe (mieux de passer par un hachage).
        /// </summary>
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> Authenticate(LoginDTO loginDto)
        {
            var utilisateur = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (utilisateur == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(loginDto.MotDePasse, utilisateur.Password); // Comparer avec mot de passe haché
        }

        /// <summary>
        /// Génère un token JWT sécurisé pour un utilisateur authentifié.
        /// </summary>
        /// <param name="utilisateur">Utilisateur pour lequel le token est généré</param>
        /// <returns>Le token JWT sous forme de chaîne</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GenerateJwtToken(User utilisateur)
        {
            // Définir les claims à inclure dans le JWT
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, utilisateur.Email), // Sujet du token (email)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Identifiant unique
                new Claim(ClaimTypes.Email, utilisateur.Email), // Email
                new Claim(ClaimTypes.Role, utilisateur.Role) // Rôle
            };

            // Récupérer la clé secrète depuis la configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Créer les credentials pour signer le token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Configurer le token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims, // Ajouter les claims
                expires: DateTime.UtcNow.AddDays(2), // Expiration après 1 jour
                signingCredentials: creds
            );

            // Générer le token sous forme de chaîne
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    /// <summary>
    /// DTO pour gérer les données de connexion utilisateur.
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Email de l'utilisateur.
        /// </summary>
        [Required(ErrorMessage = "Le champ Email est requis.")]
        [EmailAddress(ErrorMessage = "Le format de l'Email est invalide.")]
        public string Email { get; set; }

        /// <summary>
        /// Mot de passe de l'utilisateur.
        /// </summary>
        [Required(ErrorMessage = "Le champ Mot de passe est requis.")]
        public string MotDePasse { get; set; }
    }
}

