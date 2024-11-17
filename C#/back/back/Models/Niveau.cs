using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace back.Models
{
    public class Niveau
    {
        [Key]
        public required string  id_niveau { get; set; }

        [JsonIgnore]
        public ICollection<Candidat>? Candidats { get; set; }

        [JsonIgnore]
        public ICollection<Etudiant>? Etudiants { get; set; }

        [JsonIgnore]
        public ICollection<Niveau_Parcours>? Niveau_Parcours { get; set; }

    }
}
