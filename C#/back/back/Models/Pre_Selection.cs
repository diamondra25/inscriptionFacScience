using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace back.Models
{
    public class Pre_Selection
    {
        [Key]
        public required string IdCandidat { get; set; }
         
        [ForeignKey("IdCandidat")]
        [JsonIgnore]
        public Candidat? Candidats { get; set; }

        [JsonIgnore]
        public Pre_Inscription? Pre_Inscriptions { get; set; }
    }
}
