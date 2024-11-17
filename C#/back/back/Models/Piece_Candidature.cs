using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static back.Models.Enum.Enumeration;

namespace back.Models
{

  
    public class Piece_Candidature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFichier_Candidature { get; set; }
        public required string IdCandidat { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Designationenum Designation { get; set; }
        public required string UrlValeur { get; set; }

        [ForeignKey("IdCandidat")]
        [JsonIgnore]
        public Candidat? Candidats { get; set; }
    }
}
