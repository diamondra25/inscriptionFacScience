using back.Models.Dto;
using Microsoft.EntityFrameworkCore;

using System.Text.Json.Serialization;
using static back.Models.Enum.Enumeration;

namespace back.Models.Services
{
    public class CandidatService
    {
        private readonly DatabaseContext _context;

        public CandidatService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Candidat> CreerCandidatAsync(Candidat candidat)
        {

            var parcours = await _context.Parcours.FirstOrDefaultAsync(p=>p.id_parcours==candidat.id_parcours);
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

            mention.DernierIdCandidat += 1; 
            candidat.IdCandidat = $"{mention.code_mention}{mention.DernierIdCandidat}{"/301/25"}";

            _context.Candidat.Add(candidat);
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
            return candidat;
        }

         public async Task<UploadCandidatFileRequestDto> GetUrl(string idCandidat, UploadCandidatFileRequestDto request)
         {


             var folderPath = "DownloadFile/CandidatFile";
             var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
             if (!Directory.Exists(uploadPath))
             {
                 Directory.CreateDirectory(uploadPath);
             }

             var sanitizedIdCandidat =idCandidat.Replace("/", "_");

             if (request.ActeFile != null && request.ActeFile.Length > 0)
             {
                 var fileName = $"{sanitizedIdCandidat}_Acte_{Path.GetExtension(request.ActeFile.FileName)}";

                 var actePath = Path.Combine(uploadPath, fileName);
                 using (var stream = new FileStream(actePath, FileMode.Create))
                 {
                     await request.ActeFile.CopyToAsync(stream);
                };
                _context.Candidat
                .Where(c => c.IdCandidat == idCandidat)
                .ExecuteUpdate(setters => setters
                .SetProperty(c => c.UrlActeNaissance, actePath));
             }

             if (request.PhotoFile != null && request.PhotoFile.Length > 0)
             {
                 var fileName = $"{sanitizedIdCandidat}_PhotoId_{Path.GetExtension(request.PhotoFile.FileName)}";

                 var photoPath = Path.Combine(uploadPath, fileName);
                 using (var stream = new FileStream(photoPath, FileMode.Create))
                 {
                     await request.PhotoFile.CopyToAsync(stream);
                 }
                _context.Candidat
              .Where(c => c.IdCandidat == idCandidat)
              .ExecuteUpdate(setters => setters
              .SetProperty(c => c.UrlPhotoIdentite, photoPath));
            }

             if (request.BordereauFile != null && request.BordereauFile.Length > 0)
             {
                 var fileName = $"{sanitizedIdCandidat}_Bordereau_{Path.GetExtension(request.BordereauFile.FileName)}";

                 var bordereauPath = Path.Combine(uploadPath, fileName);
                 using (var stream = new FileStream(bordereauPath, FileMode.Create))
                 {
                     await request.BordereauFile.CopyToAsync(stream);
                 }
                _context.Candidat
               .Where(c => c.IdCandidat == idCandidat)
               .ExecuteUpdate(setters => setters
               .SetProperty(c => c.UrlBordereau, bordereauPath));
            }

            return request;
        }

        public List<Piece_CandidatureDto> GetPiece_CandidatureByIdCandidat(string id)
        {
            var result = (from pc in _context.Piece_Candidature
                          join c in _context.Candidat on pc.IdCandidat equals c.IdCandidat
                          where c.IdCandidat == id
                          select new Piece_CandidatureDto
                          {
                              designation = pc.Designation,
                              urlValeur = pc.UrlValeur
                          }).Distinct();

            return result.ToList();
        }

        public class Piece_CandidatureDto
        {
            [JsonConverter(typeof(JsonStringEnumConverter))]
            public Designationenum designation { get; set; }
            public required string urlValeur { get; set; }
        }
    }
 
}
