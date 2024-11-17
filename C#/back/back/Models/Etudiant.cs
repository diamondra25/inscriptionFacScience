using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static back.Models.Enum.Enumeration;

namespace back.Models
{
    public class Etudiant
    {
        [Key]
        public required string matricule { get; set; }
        public required string nom { get; set; }
        public string? prenom { get; set; }

        public Sexe_enum sexe { get; set; }
        public required string UrlPhotoIdentite { get; set; }
        public required string adresse { get; set; }
        public required string telephone { get; set; }
        public required string email { get; set; }

        public required string id_niveau { get; set; }
        public required int id_parcours { get; set; }
        public Statut_enum statut { get; set; }
        public required string resultat { get; set; }


        [ForeignKey("id_niveau")]
        [JsonIgnore]
        public Niveau? Niveaux{ get; set; }

        [ForeignKey("matricule")]
        [JsonIgnore]
        public Inscription? Inscriptions { get; set; }

        [ForeignKey("id_parcours")]
        [JsonIgnore]
        public Parcours? Parcours { get; set; }

        [JsonIgnore]
        public ICollection<Re_Inscription>? Re_Inscriptions { get; set; }

    }
}
