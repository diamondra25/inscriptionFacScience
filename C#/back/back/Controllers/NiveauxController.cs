using back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NiveauxController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public NiveauxController (DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Niveau>>>GetNiveaux()
        {
            return await _context.Niveau.Include(n => n.Candidats).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Niveau>>GetNiveau(string id)
        {
            var niveau= await _context.Niveau.Include(n => n.Candidats).FirstOrDefaultAsync(n=>n.id_niveau==id);
            if (niveau == null)
            {
                return NotFound();
            }
            return niveau;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNiveau(string id, Niveau niveau)
        {
            if (id != niveau.id_niveau)
            {
                return BadRequest();
            }

            _context.Entry(niveau).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NiveauExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Niveau>> PostNiveau(Niveau niveau)
        {
            _context.Niveau.Add(niveau);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNiveau), new { id = niveau.id_niveau }, niveau);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNiveau(string id)
        {
            var niveau = await _context.Niveau.FindAsync(id);
            if (niveau == null)
            {
                return NotFound();
            }

            _context.Niveau.Remove(niveau);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool NiveauExists(string id)
        {
            return _context.Niveau.Any(e => e.id_niveau == id);
        }
    }
}
