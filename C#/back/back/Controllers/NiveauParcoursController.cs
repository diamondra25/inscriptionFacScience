using back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NiveauParcoursController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public NiveauParcoursController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Niveau_Parcours>>>GetNiveauParcours()
        {
            return await _context.Niveau_Parcours.ToListAsync();
        }

        [HttpGet("{id_niveau}/{id_parcours}")]
        public async Task<ActionResult<Niveau_Parcours>> GetNiveauParcours(string id_niveau, int id_parcours)
        {
            var niveauMention = await _context.Niveau_Parcours.FirstOrDefaultAsync(nm => nm.id_niveau == id_niveau && nm.id_parcours == id_parcours);

            if (niveauMention == null)
            {
                return NotFound();
            }

            return niveauMention;
        }


        [HttpPut("{id_niveau}/{id_parcours}")]
        public async Task<IActionResult> PutNiveauMention(string id_niveau, int id_parcours, Niveau_Parcours niveauMention)
        {
            if (id_niveau != niveauMention.id_niveau || id_parcours != niveauMention.id_parcours)
            {
                return BadRequest();
            }

            _context.Entry(niveauMention).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NiveauMentionExists(id_niveau, id_parcours))
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
        public async Task<ActionResult<Niveau_Parcours>> PostNiveauParcours(Niveau_Parcours niveauParcours)
        {
            _context.Niveau_Parcours.Add(niveauParcours);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNiveauParcours), new { id_niveau = niveauParcours.id_niveau, id_parcours = niveauParcours.id_parcours }, niveauParcours);
        }


        [HttpDelete("{id_niveau}/{id_parcours}")]
        public async Task<IActionResult> DeleteNiveauParcours(string id_niveau, int id_parcours)
        {
            var niveauParcours = await _context.Niveau_Parcours.FindAsync(id_niveau, id_parcours);
            if (niveauParcours == null)
            {
                return NotFound();
            }

            _context.Niveau_Parcours.Remove(niveauParcours);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NiveauMentionExists(string id_niveau, int id_parcours)
        {
            return _context.Niveau_Parcours.Any(nm => nm.id_niveau == id_niveau && nm.id_parcours == id_parcours);
        }
    }
}
