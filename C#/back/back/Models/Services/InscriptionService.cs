using back.Models;
using Microsoft.EntityFrameworkCore;

namespace back.Models.Services
{
    public class InscriptionService
    {
        private readonly DatabaseContext _context;

        public InscriptionService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Inscription> CreerInscriptionAsync(Inscription inscription)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var pre_inscription = await _context.Pre_Inscription.FirstOrDefaultAsync(pi => pi.IdPre_Inscription == inscription.IdPre_Inscription);

                if (pre_inscription == null)
                {
                    throw new Exception("Pré-Inscription introuvable !");
                }

                var pre_selection = await _context.Pre_Selection.FirstOrDefaultAsync(ps => ps.IdCandidat == pre_inscription.IdCandidat);
                if (pre_selection == null)
                {
                    throw new Exception("Pré-sélection introuvable !");
                }

                var candidat = await _context.Candidat.FirstOrDefaultAsync(c => c.IdCandidat == pre_selection.IdCandidat);
                if (candidat == null)
                {
                    throw new Exception("Candidat introuvable !");
                }

                var parcours = await _context.Parcours.FirstOrDefaultAsync(p => p.id_parcours == candidat.id_parcours);
                if (parcours == null)
                {
                    throw new Exception("Parcours introuvable !");
                }

                var mention = await _context.Mention
                    .FirstOrDefaultAsync(m => m.id_mention == parcours.id_mention);
                if (mention == null)
                {
                    throw new Exception("Mention introuvable !");
                }

                mention.DernierMatricule += 1;

                inscription.matricule = $"{mention.code_mention}{mention.DernierMatricule.ToString("D6")}";


                _context.Inscription.Add(inscription);

                _context.Update(mention);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return inscription;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}