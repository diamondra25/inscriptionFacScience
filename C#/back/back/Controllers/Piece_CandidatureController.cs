using back.Models;
using back.Models.Dto;
using back.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Piece_CandidatureController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly Piece_CandidatureService _piece_CandidatureService;
        public Piece_CandidatureController(Piece_CandidatureService piece_CandidatureService, DatabaseContext context)
        {
            _context = context;
            _piece_CandidatureService = piece_CandidatureService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Piece_Candidature>>> GetPieceCandiatures()
        {
            return await _context.Piece_Candidature.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Piece_Candidature>> GetPiece_Candidature(int id)
        {
            try
            {
                var piece_Candidature = await _context.Piece_Candidature.FindAsync(id);

                return Ok(piece_Candidature);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("/uploadCandidature")]
        public async Task<IActionResult> UploadFile([FromForm] string idCandidat, [FromForm] List<IFormFile> file, [FromForm] List<string> designation)
        {
            if (file == null || file.Count == 0)
            {
                return BadRequest("Aucun fichier sélectionné.");
            }

            if (file.Count != designation.Count)
            {
                return BadRequest("Le nombre de fichiers ne correspond pas au nombre de désignations.");
            }
            try
            {
                for (int i = 0; i < file.Count; i++)
                {
                    var relativePath = await _piece_CandidatureService.UploadCandidatFileAsync(idCandidat, file[i], designation[i], "DownloadFile/CandidatReleveFile");
                }
                return Ok(new { message = "Fichiers ajoutés avec succès." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReleve(int id, Piece_Candidature releve)
        {
            if (id != releve.IdFichier_Candidature)
            {
                return BadRequest();
            }

            _context.Entry(releve).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReleveExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReleve(int id)
        {
            var releve = await _context.Piece_Candidature.FindAsync(id);
            if (releve == null)
            {
                return NotFound();
            }

            _context.Piece_Candidature.Remove(releve);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReleveExists(int id)
        {
            return _context.Piece_Candidature.Any(r => r.IdFichier_Candidature == id);
        }
    }
}
