using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_projet_parc_informatique.Models
{
    public class Remontees
    {
        [Key]
        public int Id { get; set; }
        public int Id_poste { get; set; }
        [ForeignKey("Id_poste")]

        [JsonIgnore]
        public Poste? Poste { get; set; }
        public string Description { get; set; }
        public DateTime DateRemontee { get; set; }
        public string EtatSysteme { get; set; }

        public double CPU { get; set; }
        public double RAM { get; set; }
        public double DisqueDur { get; set; }
        public string ConnectiviteReseau { get; set; }

    }
}
