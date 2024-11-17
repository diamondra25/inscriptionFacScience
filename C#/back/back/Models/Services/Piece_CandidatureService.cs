using static back.Models.Enum.Enumeration;

namespace back.Models.Services
{
    public class Piece_CandidatureService
    {
        private readonly DatabaseContext _context;
        public Piece_CandidatureService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Piece_Candidature> UploadCandidatFileAsync(string idCandidat,IFormFile file,string designation, string folderPath )
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Aucun fichier sélectionné.");
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var sanitizedIdCandidat = idCandidat.Replace("/", "_");

            var fileName = $"{sanitizedIdCandidat}_{designation}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativePath = Path.Combine(folderPath, fileName);
            var newFile = new Piece_Candidature
            {
                IdCandidat =idCandidat,
                Designation = (Designationenum)System.Enum.Parse(typeof(Designationenum), designation, true),
                UrlValeur = relativePath
            };

            _context.Piece_Candidature.Add(newFile);
            await _context.SaveChangesAsync();
            return newFile;
        }
    }
}
