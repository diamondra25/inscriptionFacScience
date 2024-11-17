using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace back.Models.Services
{
    public class EtudiantService
    {
        private readonly DatabaseContext _context;

        public EtudiantService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Etudiant> CreerEtudiantAsync(Etudiant etudiant)
        {
            /*var candidat = await _context.Candidats
                .Include(c => c.Parcours)
                .ThenInclude(p => p.Mentions)
                .FirstOrDefaultAsync(c => c.IdCandidat == etudiant.IdCandidat);

            if (candidat == null)
            {
                throw new Exception("Candidat introuvable !");
            }


            if(candidat.Parcours == null)
            {
                throw new Exception("Mention introuvable !");
            }
            var mention = candidat.Parcours.Mentions;

            if (mention == null)
            {
                throw new Exception("Mention introuvable !");
            }

            mention.DernierMatricule += 1;
            etudiant.matricule = $"{mention.DernierMatricule}{mention.code_mention}";

            _context.Etudiants.Add(etudiant);

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                _context.Update(mention);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            */
            return etudiant;
        }
    }
}
