using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_projet_parc_informatique.Models
{
    public class Parc
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Localisation { get; set; }

        [JsonIgnore]
        public ICollection<Salle>? Salles { get; set; }

       
    }
}
