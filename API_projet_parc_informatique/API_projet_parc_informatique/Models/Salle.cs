using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_projet_parc_informatique.Models
{
    public class Salle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        public int Id_parc { get; set; }

        [JsonIgnore]
        public Parc? Parc { get; set; }

        [JsonIgnore]
        public ICollection<Poste>? Postes { get; set; }

        
    }
}
