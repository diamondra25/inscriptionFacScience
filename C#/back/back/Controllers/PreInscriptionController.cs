using Microsoft.AspNetCore.Mvc;
using back.Models;
using back.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreInscriptionController : ControllerBase
    {
        private readonly DatabaseContext _context;
        
        public PreInscriptionController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pre_Inscription>>> GetPreInscriptions()
        {
            return await _context.Pre_Inscription.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pre_Inscription>> GetPreInscription(int id)
        {
            var pre_inscription = await _context.Pre_Inscription.FindAsync(id);

            if (pre_inscription == null)
            {
                return NotFound();
            }

            return pre_inscription;
        }

        [HttpPost]
        public async Task<ActionResult<Pre_Inscription>> PostPreInscription(Pre_Inscription pre_inscription)
        {
            try
            {
                _context.Pre_Inscription.Add(pre_inscription);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetPreInscription", new { id = pre_inscription.IdPre_Inscription }, pre_inscription);

            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An internal server error occurred. Please try again later." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreInscription(int id, Pre_Inscription pre_Inscription)
        {
            if (id != pre_Inscription.IdPre_Inscription)
            {
                return BadRequest();
            }

            _context.Entry(pre_Inscription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreInscriptionExists(id))
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
        public async Task<IActionResult> DeletePreInscription(int id)
        {
            var pre_inscription = await _context.Pre_Inscription.FindAsync(id);
            if (pre_inscription == null)
            {
                return NotFound();
            }


            var pieceId = pre_inscription.IdPiece_A_Fournir;

            _context.Pre_Inscription.Remove(pre_inscription);
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

        private bool PreInscriptionExists(int id)
        {
            return _context.Pre_Inscription.Any(pi => pi.IdPre_Inscription == id);
        }
    }
}
