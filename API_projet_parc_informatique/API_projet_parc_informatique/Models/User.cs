using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_projet_parc_informatique.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

       

        [JsonIgnore]
        public ICollection<Ticket>? Tickets { get; set; }

       
    }
}
