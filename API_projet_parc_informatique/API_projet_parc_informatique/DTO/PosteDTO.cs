public class PosteDTO
{
    public int Id_poste { get; set; }
    public string Marque { get; set; }
    public string AdresseIp { get; set; }
    public string AdresseMac { get; set; }
    public string SystemeExploitation { get; set; }
    public string Processeur { get; set; }
    public int Ram { get; set; }
    public int DisqueDur { get; set; }
    public string Status { get; set; }
    public int Id_salle { get; set; }

    public PosteDTO(int id_poste, string marque, string adresseIp, string adresseMac, string systemeExploitation, string processeur, int ram, int disqueDur, string status, int id_salle)
    {
        Id_poste = id_poste;
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
}