

namespace back.Models.Dto
{
    public class Piece_CandidatureDto
    {
        public required string idCandidat { get; set; }
        public required IFormFile fichierAjouter {  get; set; }
        public required string designation { get; set; }

    }
}
