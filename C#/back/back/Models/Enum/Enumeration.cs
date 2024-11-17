using System.Text.Json.Serialization;

namespace back.Models.Enum
{
    public class Enumeration
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Sexe_enum
        {
            Masculin,
            Féminin
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Statut_enum
        {
            Nouveau,
            Passant,
            Redoublant
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Nationalite_enum
        {
            Malagasy,
            Entranger
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Designationenum
        {
            S1,
            S2,
            S3,
            S4,
            S5,
            S6,
            S7,
            S8,
            Bacc,
            Diplôme_License,
            Diplôme_Maitrise,
            Cv

        }
    }
}
