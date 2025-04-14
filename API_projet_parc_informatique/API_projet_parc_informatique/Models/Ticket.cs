using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_projet_parc_informatique.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date_creation { get; set; }

       

        [Required]
        public string Status { get; set; }

        [Required]
        public string Priorite { get; set; }

       

        public int Id_poste { get; set; }

        [JsonIgnore]
        public Poste? Poste { get; set; }
        
        public int Id_user { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        
    }
}
