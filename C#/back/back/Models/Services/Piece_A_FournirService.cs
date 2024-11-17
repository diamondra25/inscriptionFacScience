using back.Models.Dto;
using System;
using static back.Models.Enum.Enumeration;

namespace back.Models.Services
{
    public class Piece_A_FournirService
    {
        private readonly DatabaseContext _context;
        private readonly InscriptionService _inscriptionService;

        public Piece_A_FournirService(InscriptionService inscriptionService, DatabaseContext context)
        {
            _context = context;
            _inscriptionService = inscriptionService;
        }

        public async Task<Piece_A_Fournir> AssignFichierToInscriptionOrReinscription(CombinedInscriptionDto combinedInscriptionDto)
        {
            var pieceAFournir = combinedInscriptionDto.PieceAFournir;
            var inscription = combinedInscriptionDto.Inscription;

            _context.Piece_A_Fournir.Add(pieceAFournir);
            await _context.SaveChangesAsync();



            if (pieceAFournir.Designation == 0 && inscription.IdCandidat!=null && inscription.Nom!=null)
            {
                var pre_inscription = new Pre_Inscription
                {
                    IdCandidat= inscription.IdCandidat,
                    Nom= inscription.Nom,
                    Prenom= inscription.Prenom,
                    Sexe=inscription.Sexe,
                    UrlDiplomeBacc= inscription.UrlDiplomeBacc,
                    UrlReleveBacc=inscription.UrlReleveBacc,
                    IdPiece_A_Fournir= pieceAFournir.IdPiece_A_Fournir
                };
                _context.Pre_Inscription.Add(pre_inscription);

            }
            else
            {
                if (inscription.Matricule != null)
                {
                    var reinscription = new Re_Inscription
                    {
                        matricule = inscription.Matricule,
                        IdPiece_A_Fournir = pieceAFournir.IdPiece_A_Fournir
                    };
                    _context.Re_Inscription.Add(reinscription);
                }
            }
            await _context.SaveChangesAsync();
            return pieceAFournir;
        }
    }
}
