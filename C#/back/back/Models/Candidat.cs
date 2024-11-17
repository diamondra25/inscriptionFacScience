using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static back.Models.Enum.Enumeration;

namespace back.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TypeBaccEnum
    {
        Général,
        [Display(Name = "Technique et Professionnel")]
        Technique_Professionnel
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SerieBaccEnum
    {
        C,    
        D,
        S,
        BTP,
        ELECTHEC,
        MA,
        [Display(Name = "OB-OM")]
        OB_OM,
        FM,
        ELECMECA
    }

    public class Candidat
    {
        [Key]
        public  string IdCandidat { get; set; }
        public required string Nom { get; set; }
        public string? Prenom { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public  Sexe_enum Sexe { get; set; }
        public required string LieuNaissance { get; set; }
        public DateTime DateNaissance { get; set; }
        public Nationalite_enum Nationalite { get; set; }
        public required string SituationMatrimoniale { get; set; }

        public required string Adresse { get; set; }
        public required string Telephone { get; set; }
        public required string Email { get; set; }

        public string? NumeroCin { get; set; }
        public DateTime? DateDelivreCin { get; set; }
        public string? LieuDelivreCin { get; set; }

        public string? NomPere { get; set; }
        public required string NomMere { get; set; }
        public string? ProfessionPere { get; set; }
        public string? ProfessionMere { get; set; }
        public string? AdresseParents { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required TypeBaccEnum TypeBacc { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SerieBaccEnum? SerieBacc { get; set; }

        public required string NumeroBacc { get; set; }
        public required string CentreBacc { get; set; }
        public required string MentionBacc { get; set; }
        public float MoyenneBacc { get; set; }
        public int SessionBacc { get; set; }

        public required string id_niveau { get; set; }
        public required int id_parcours { get; set; }
        public required string CentreUniversitaire { get; set; }
        public string? NumeroRecu { get; set; }

        public  string? UrlBordereau { get; set; }
        public  string? UrlPhotoIdentite { get; set; }
        public  string? UrlActeNaissance { get; set; }

        [ForeignKey("id_niveau")]
        [JsonIgnore]
        public Niveau? Niveaux { get; set; }

        [ForeignKey("id_parcours")]
        [JsonIgnore]
        public Parcours? Parcours { get; set; }

        [JsonIgnore]
        public Pre_Selection? Pre_Selections { get; set; }

        [JsonIgnore]
        public List<Piece_Candidature>? Piece_Candidatures { get; set; } = new List<Piece_Candidature>();

        public bool IsValidBacc()
        {
            if (TypeBacc == TypeBaccEnum.Général)
            {

                return SerieBacc == SerieBaccEnum.C ||
                       SerieBacc == SerieBaccEnum.D ||
                       SerieBacc == SerieBaccEnum.S;
            }
            else if (TypeBacc == TypeBaccEnum.Technique_Professionnel)
            {

                return SerieBacc == SerieBaccEnum.BTP ||
                       SerieBacc == SerieBaccEnum.ELECTHEC ||
                       SerieBacc == SerieBaccEnum.MA ||
                       SerieBacc == SerieBaccEnum.OB_OM ||
                       SerieBacc == SerieBaccEnum.FM ||
                       SerieBacc == SerieBaccEnum.ELECMECA;
            }

            return false; 
        }
    }
}
