using back.Models;
using back.Models.Dto;
using back.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static back.Models.Enum.Enumeration;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Piece_A_FournirController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly Piece_A_FournirService _A_FournirService;

        public Piece_A_FournirController(Piece_A_FournirService A_FournirService, DatabaseContext context)
        {
            _context = context;
            _A_FournirService = A_FournirService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Piece_A_Fournir>>>GetPieceAFournirs()
        {
            return await _context.Piece_A_Fournir.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Piece_A_Fournir>> GetPieceAFournir(int id)
        {
            var piece_a_fournir= await _context.Piece_A_Fournir.FindAsync(id);

            if(piece_a_fournir==null)
            {
                return NotFound();
            }
            return piece_a_fournir;
        }

        [HttpPost]
        public async Task<ActionResult<Piece_A_Fournir>> PostPieceAFournir([FromBody] CombinedInscriptionDto combinedInscriptionDto)
        {
            try
            {
                var createdFichier = await _A_FournirService.AssignFichierToInscriptionOrReinscription(combinedInscriptionDto);
                return CreatedAtAction(nameof(GetPieceAFournir), new { id = createdFichier.IdPiece_A_Fournir }, createdFichier);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPieceAFournir(int id, Piece_A_Fournir piece_A_Fournir)
        {
            if (id != piece_A_Fournir.IdPiece_A_Fournir)
            {
                return BadRequest();
            }

            _context.Entry(piece_A_Fournir).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PieceExists(id))
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
        public async Task<IActionResult> DeletePieceAFournir(int id)
        {
            var piece_A_Fournir = await _context.Piece_A_Fournir.FindAsync(id);
            if (piece_A_Fournir == null)
            {
                return NotFound();
            }

            _context.Piece_A_Fournir.Remove(piece_A_Fournir);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool PieceExists(int pieceId)
        {
            return _context.Piece_A_Fournir.Any(pf => pf.IdPiece_A_Fournir == pieceId);
        }
    }
}
