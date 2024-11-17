using back.Models.Services;
using back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtudiantController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public EtudiantController( DatabaseContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Etudiant>>> GetEtudiants()
        {

            return await _context.Etudiant.ToListAsync();
        }

        [HttpGet("GetEtudiant")]
        public async Task<ActionResult<Etudiant>> GetEtudiant( [FromQuery] string id)
        {

            var etudiant = await _context.Etudiant
                .FirstOrDefaultAsync(e => e.matricule == id);

            if (etudiant == null)
            {
                return NotFound();
            }

            return etudiant;
        }

        [HttpPost]
        public async Task<ActionResult<Etudiant>> PostEtudiant(Etudiant etudiant)
        {
            try
            {
                _context.Etudiant.Add(etudiant);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEtudiant", new { id = etudiant.matricule }, etudiant);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("DeleteEtudiant")]
        public async Task<IActionResult> DeleteEtudiant([FromQuery] string id)
        {
            if (!EtudiantExists(id))
            {
                return NotFound();
            }

            try
            {
                var etudiant = await _context.Etudiant.FindAsync(id);
                if (etudiant == null)
                {
                    return NotFound();
                }

                _context.Etudiant.Remove(etudiant);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Une erreur est survenue lors de la suppression de l'étudiant.", detail = ex.Message });
            }
        }


        private bool EtudiantExists(string id)
        {
            return _context.Etudiant.Any(e => e.matricule == id);
        }
    }
}
