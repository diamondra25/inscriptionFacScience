using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace back.Models
{
    public class Mention
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int  id_mention { get; set; }
        public required string nom_mention { get; set; }
        public required string code_mention { get; set; }
        public int DernierMatricule { get; set; }
        public int DernierIdCandidat { get; set; }

        [JsonIgnore]
        public ICollection<Parcours>? Parcours { get; set; }


    }
}
