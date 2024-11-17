using static back.Models.Enum.Enumeration;


namespace back.Models.Dto
{
    public class InscriptionDto
    {
        public string? IdCandidat { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public int? Sexe { get; set; }
        public string? UrlDiplomeBacc { get; set; }
        public string? UrlReleveBacc { get; set; }
        public int? IdPiece_A_Fournir { get; set; }
        public string? Matricule { get; set; }
    }
}
