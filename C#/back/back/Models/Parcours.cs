using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace back.Models
{
    public class Parcours
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_parcours { get; set; }
        public required string nom_parcours { get; set; }
        public required string code_parcours { get; set; }

        public int id_mention { get; set; }

        [ForeignKey("id_mention")]
        [JsonIgnore]
        public Mention? Mentions { get; set; }

        [JsonIgnore]
        public ICollection<Candidat>? Candidats { get; set; }

        [JsonIgnore]
        public ICollection<Etudiant>? Etudiants { get; set; }

        [JsonIgnore]
        public ICollection<Niveau_Parcours>? Niveau_Parcours { get; set; }
    }
}
