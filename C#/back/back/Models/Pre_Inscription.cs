using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static back.Models.Enum.Enumeration;
namespace back.Models
{
    public class Pre_Inscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int IdPre_Inscription { get; set; }
        public required string IdCandidat { get; set; }
        public required string Nom { get; set; }
        public string? Prenom { get; set; }
        public int? Sexe { get; set; }
        public string? UrlDiplomeBacc { get; set; }
        public string? UrlReleveBacc { get; set; }
        public required int IdPiece_A_Fournir { get; set; }



        [ForeignKey("IdCandidat")]
        [JsonIgnore]
        public Pre_Selection? Pre_Selections { get; set; }

        [ForeignKey("IdPiece_A_Fournir")]
        [JsonIgnore]
        public Piece_A_Fournir? Piece_A_Fournirs { get; set; }

        [JsonIgnore]
        public Inscription? Inscriptions { get; set; }
    }
}
