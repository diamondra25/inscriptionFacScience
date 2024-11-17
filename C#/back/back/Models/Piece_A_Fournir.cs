using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static back.Models.Enum.Enumeration;


namespace back.Models
{
    public enum Designation_enum
    {
        Inscription,
        Réinscription
    }
    public class Piece_A_Fournir
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPiece_A_Fournir { get; set; }
        public Designation_enum Designation { get; set; }
        public Nationalite_enum Nationalite { get; set; }
        public required string Telephone { get; set; }
        public required string Email { get; set; }
        public string? NumeroRecuDroit { get; set; }
        public string? UrlBordereau { get; set; }
        public required string UrlPhotoIdentite { get; set; }
        public string? UrlCin { get; set; }
        public required string UrlCertificatResidence { get; set; }
        public required string UrlCharte { get; set; }
        public required string UrlReglement { get; set; }
        public required string UrlLivret { get; set; }
        public DateTime Date { get; set; }

        public Statut_enum Statut { get; set; }
        public required string Niveau { get; set; }
        public required string AnneeAcademique { get; set; }

        [JsonIgnore]
        public Pre_Inscription? Pre_Inscriptions { get; set; }

        [JsonIgnore]
        public Re_Inscription? Re_Inscriptions { get; set; }
    }
}
