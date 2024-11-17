using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace back.Models
{
    public class Inscription
    {
        [Key]
        public  string? matricule { get; set; }
        public required int IdPre_Inscription { get; set; }

        [ForeignKey("IdPre_Inscription")]
        [JsonIgnore]
        public Pre_Inscription? Pre_Inscription { get; set; }

        [JsonIgnore]
        public Etudiant? Etudiants { get; set; }

    }
}
