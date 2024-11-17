using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace back.Models
{
    public class Niveau_Parcours
    {
        [Key, Column(Order = 0)]
        public required string id_niveau { get; set; }

        [Key, Column(Order = 1)]
        public required int id_parcours { get; set; }

       public bool status_selection {  get; set; }

        [ForeignKey("id_parcours")]
        [JsonIgnore]
        public Parcours? Parcours { get; set; }

        [ForeignKey("id_niveau")]
        [JsonIgnore]
        public Niveau? Niveaux { get; set; }
    }
}
