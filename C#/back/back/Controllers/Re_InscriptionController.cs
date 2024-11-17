using back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.Controller 
{
    [Route("api/[controller]")]
    [ApiController]
    public class Re_InscriptionController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public Re_InscriptionController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Re_Inscription>>> GetRe_Inscriptions()
        {
            return await _context.Re_Inscription
                .Include(ri => ri.Etudiants)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Re_Inscription>> GetRe_Inscription(int id)
        {
            var re_inscription = await _context.Re_Inscription
                .Include(ri => ri.Etudiants)
                .FirstOrDefaultAsync(ri => ri.IdRe_Inscription == id);

            if (re_inscription == null)
            {
                return NotFound();
            }

            return re_inscription;
        }

 
        [HttpPost]
        public async Task<ActionResult<Re_Inscription>> PostRe_Inscription(Re_Inscription re_inscription)
        {
            _context.Re_Inscription.Add(re_inscription);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRe_Inscription", new { id = re_inscription.IdRe_Inscription }, re_inscription);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutRe_Inscription(int id, Re_Inscription re_inscription)
        {
            if (id != re_inscription.IdRe_Inscription)
            {
                return BadRequest();
            }

            _context.Entry(re_inscription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Re_InscriptionExists(id))
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
        public async Task<IActionResult> DeleteRe_Inscription(int id)
        {
            var re_inscription = await _context.Re_Inscription.FindAsync(id);
            if (re_inscription == null)
            {
                return NotFound();
            }
            var pieceId = re_inscription.IdPiece_A_Fournir;

            _context.Re_Inscription.Remove(re_inscription);
            await _context.SaveChangesAsync();

            var piece_a_fournir = await _context.Piece_A_Fournir.FindAsync(pieceId);

            if (piece_a_fournir == null)
            {
                return NotFound();
            }

            _context.Piece_A_Fournir.Remove(piece_a_fournir);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Re_InscriptionExists(int id)
        {
            return _context.Re_Inscription.Any(ri => ri.IdRe_Inscription == id);
        }
    }
}
