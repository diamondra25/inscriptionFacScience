using Microsoft.AspNetCore.Mvc;
using back.Models;
using back.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscriptionController : ControllerBase
    {
        private readonly InscriptionService _inscriptionService;
        private readonly DatabaseContext _context;
        private readonly ILogger<InscriptionController> _logger;

        public InscriptionController(InscriptionService inscriptionService, DatabaseContext context, ILogger<InscriptionController> logger)
        {
            _inscriptionService = inscriptionService;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inscription>>> GetInscriptions()
        {
            return await _context.Inscription.ToListAsync();
        }

        [HttpGet("GetInscription")]
        public async Task<ActionResult<Inscription>> GetInscription([FromQuery] string matricule)
        {
            var inscription = await _context.Inscription.FindAsync(matricule);

            if (inscription == null)
            {
                return NotFound();
            }

            return inscription;
        }


        [HttpPost]
        public async Task<ActionResult<Inscription>> PostInscription(Inscription inscription)
        {
            try
            {
                var createdInscription = await _inscriptionService.CreerInscriptionAsync(inscription);
                return CreatedAtAction(nameof(GetInscription), new { matricule = createdInscription.matricule }, createdInscription);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de la création de l'inscription");
                return StatusCode(500, new { message = "An internal server error occurred. Please try again later." });
            }
        }

        [HttpPut("UpdateInsciption")]
        public async Task<IActionResult> PutInscription([FromQuery]string matricule, Inscription inscription)
        {
            if (matricule != inscription.matricule)
            {
                return BadRequest();
            }

            _context.Entry(inscription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscriptionExists(matricule))
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

        [HttpDelete("DeleteInscription")]
        public async Task<IActionResult> DeleteInscription([FromQuery]string matricule)
        {
            var inscription = await _context.Inscription.FindAsync(matricule);
            if (inscription == null)
            {
                return NotFound();
            }

            _context.Inscription.Remove(inscription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InscriptionExists(string matricule)
        {
            return _context.Inscription.Any(e => e.matricule == matricule);
        }
    }
}