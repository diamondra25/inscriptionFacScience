namespace back.Models.Dto
{
    public class CombinedInscriptionDto
    {
        public required Piece_A_Fournir PieceAFournir { get; set; }
        public required InscriptionDto Inscription { get; set; }
    }
}
