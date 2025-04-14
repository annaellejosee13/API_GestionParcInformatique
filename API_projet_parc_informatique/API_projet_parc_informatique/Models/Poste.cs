using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_projet_parc_informatique.Models
{
    public class Poste
    {
        public Poste(string marque, string adresseIp, string adresseMac, string systemeExploitation, string processeur, int ram, int disqueDur, string status, int id_salle)
        {
            Marque = marque;
            AdresseIp = adresseIp;
            AdresseMac = adresseMac;
            SystemeExploitation = systemeExploitation;
            Processeur = processeur;
            Ram = ram;
            DisqueDur = disqueDur;
            Status = status;
            Id_salle = id_salle;
        }

        [Key]
        public int Id_poste { get; set; }

        [Required]
        public string Marque { get; set; }

        [Required]
        public string AdresseIp { get; set; }

        [Required]
        public string AdresseMac { get; set; }

        [Required]
        public string SystemeExploitation { get; set; }

        [Required]
        public string Processeur { get; set; }

        [Required]
        public int Ram { get; set; }

        [Required]
        public int DisqueDur { get; set; }

        [Required]
        public string Status { get; set; }

        public int Id_salle { get; set; }

        [JsonIgnore]
        public Salle? Salle { get; set; }
        
        [JsonIgnore]
        public ICollection<Ticket>? Tickets { get; set; }

        public ICollection<Remontees>? Remontees { get; set; }


    }
}
