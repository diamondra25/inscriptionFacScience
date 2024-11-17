using back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pre_SelectionController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public Pre_SelectionController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pre_Selection>>> GetPreSelections()
        {
            return await _context.Pre_Selection.ToListAsync();
        }

        [HttpGet("GetPreselection")]
        public async Task<ActionResult<Pre_Selection>> GetPreSelection([FromQuery] string id)
        {
            var preSelection = await _context.Pre_Selection.FirstOrDefaultAsync(ps => ps.IdCandidat == id);

            if (preSelection == null)
            {
                return NotFound();
            }

            return preSelection;
        }

        [HttpPut("UpdatePreSelection")]
        public async Task<IActionResult> PutPreSelection([FromQuery] string id, Pre_Selection preSelection)
        {
            if (id != preSelection.IdCandidat)
            {
                return BadRequest();
            }

            _context.Entry(preSelection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreSelectionExists(id))
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
        public async Task<ActionResult<Pre_Selection>> PostPreSelection(Pre_Selection preSelection)
        {
            _context.Pre_Selection.Add(preSelection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPreSelection", new { id = preSelection.IdCandidat }, preSelection);
        }

        [HttpDelete("DeletePreSelection")]
        public async Task<IActionResult> DeletePreSelection([FromQuery] string id)
        {
            var preSelection = await _context.Pre_Selection.FindAsync(id);
            if (preSelection == null)
            {
                return NotFound();
            }

            _context.Pre_Selection.Remove(preSelection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PreSelectionExists(string id)
        {
            return _context.Pre_Selection.Any(ps => ps.IdCandidat == id);
        }
    }
}
