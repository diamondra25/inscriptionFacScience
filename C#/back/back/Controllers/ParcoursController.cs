using back.Models;
using back.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcoursController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ParcoursService _parcoursService;

        public ParcoursController(ParcoursService parcoursService, DatabaseContext context)
        {
            _context = context;
            _parcoursService = parcoursService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parcours>>> GetParcours()
        {
            return await _context.Parcours
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Parcours>> GetParcour(int id)
        {
            var parcour = await _context.Parcours.FirstOrDefaultAsync(p => p.id_parcours == id);

            if (parcour == null)
            {
                return NotFound();
            }

            return parcour;
        }

        [HttpGet("GetParcoursByMention/{IdMention}")]
        public IActionResult GetParcoursByMention(int IdMention)
        {
            try
            {
                var parcours = _parcoursService.GetParcoursByMention(IdMention);

                return Ok(parcours);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Parcours>> PostParcour(Parcours parcour)
        {
            _context.Parcours.Add(parcour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParcour", new { id = parcour.id_parcours }, parcour);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutParcour(int id, Parcours parcour)
        {
            if (id != parcour.id_parcours)
            {
                return BadRequest();
            }

            _context.Entry(parcour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParcourExists(id))
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
        public async Task<IActionResult> DeleteParcour(int id)
        {
            var parcour = await _context.Parcours.FindAsync(id);
            if (parcour == null)
            {
                return NotFound();
            }

            _context.Parcours.Remove(parcour);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParcourExists(int id)
        {
            return _context.Parcours.Any(p => p.id_parcours == id);
        }
    }
}
