using back.Models;
using back.Models.Dto;
using back.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatControlleur : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly CandidatService _candidatService;
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

        public CandidatControlleur(CandidatService candidatService, DatabaseContext context)
        {
            _candidatService = candidatService;
            _context = context;
            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(_filePath);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidat>>> GetCandidats()
        {
            return await _context.Candidat.ToListAsync();
        }


        [HttpGet("GetCandidat")]
        public async Task<ActionResult<Candidat>> GetCandidat([FromQuery] string id)
        {
            var candidat = await _context.Candidat.FirstOrDefaultAsync(c => c.IdCandidat == id);

            if (candidat == null)
            {
                return NotFound();
            }

            return candidat;
        }

        [HttpPost]
        public async Task<ActionResult<Candidat>> PostCandidat(Candidat candidat)
        {
            if (candidat == null)
            {
                return BadRequest("Les données du candidat sont invalides.");
            }
            if (!candidat.IsValidBacc())
            {
                return BadRequest("Le candidat n'a pas un baccalauréat valide.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var newCandidat = await _candidatService.CreerCandidatAsync(candidat);
                return CreatedAtAction("GetCandidat", new { id = newCandidat.IdCandidat }, newCandidat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateCandidat")]
        public async Task<IActionResult> PutCandidat([FromQuery] string id, Candidat candidat)
        {
            if (id != candidat.IdCandidat)
            {
                return BadRequest();
            }

            _context.Entry(candidat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidatExists(id))
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
        [HttpDelete("DeleteCandidat")]
        public async Task<IActionResult> DeleteCandidat([FromQuery] string id)
        {

            var candidat = await _context.Candidat.FindAsync(id);
            if (candidat == null)
            {
                return NotFound();
            }

            _context.Candidat.Remove(candidat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CandidatExists(string id)
        {
            return _context.Candidat.Any(e => e.IdCandidat == id);
        }

        [HttpGet("download-CandidatPhoto")]
        public IActionResult GetFile ([FromForm]string fileName)
        {
            var filePath = fileName;

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileExtension = Path.GetExtension(filePath).ToLower();
            var mimeType = fileExtension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".svg" => "image/svg+xml",
                _ => "application/octet-stream" 
            };

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, mimeType);
        }


        [HttpPut("uploadCandidatFile")]
        public async Task<IActionResult> UploadCandidatFile([FromForm] string idCandidat, UploadCandidatFileRequestDto request)
        {
            try
            {
                var uploadFile = await _candidatService.GetUrl(idCandidat, request);
                await _context.SaveChangesAsync();

                return Ok(new { message = " Votre candiature a été ajouté avec succès." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidatExists(idCandidat))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur est survenue lors du traitement de votre demande.");
            }

        }

        [HttpGet("GetPiece_CandidatureByIdCandidat")]
        public IActionResult GetPiece_CandidatureByIdCandidat([FromQuery] string id)
        {
            try
            {
                var piece_C = _candidatService.GetPiece_CandidatureByIdCandidat(id);

                return Ok(piece_C);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
