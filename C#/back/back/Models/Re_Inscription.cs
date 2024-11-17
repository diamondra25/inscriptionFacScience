
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace back.Models
{
    public class Re_Inscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRe_Inscription{ get; set; }
        public required string matricule { get; set; }
        public required int IdPiece_A_Fournir { get; set; }

        [ForeignKey("matricule")]
        [JsonIgnore]
        public Etudiant? Etudiants { get; set; }

        [ForeignKey("IdPiece_A_Fournir")]
        [JsonIgnore]
        public Piece_A_Fournir? Piece_A_Fournirs { get; set; }

    }                                        
}
