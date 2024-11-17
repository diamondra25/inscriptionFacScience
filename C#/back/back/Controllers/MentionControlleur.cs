using back.Models;
using back.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentionControlleur : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly MentionService _mentionService;

        public MentionControlleur( MentionService mentionService , DatabaseContext context)
        {
            _context = context; _mentionService = mentionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mention>>> GetMentions()
        {
            return await _context.Mention.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mention>> GetMention(int id)
        {
            var mention = await _context.Mention.FirstOrDefaultAsync(m => m.id_mention == id);

            if (mention == null)
            {
                return NotFound();
            }

            return mention;
        }


        [HttpGet("GetMentionsByNiveau/{niveau}")]
        public IActionResult GetMentionsByNiveau(string niveau)
        {
            try
            {
                var mentions = _mentionService.GetMentionsByNiveau(niveau);

                return Ok(mentions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Mention>> PostMention(Mention mention)
        {
            _context.Mention.Add(mention);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMention), new { id = mention.id_mention }, mention);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMention(int id, Mention mention)
        {
            if (id != mention.id_mention)
            {
                return BadRequest();
            }

            _context.Entry(mention).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MentionExists(id))
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
        public async Task<IActionResult> DeleteMention(int id)
        {
            var mention = await _context.Mention.FindAsync(id);
            if (mention == null)
            {
                return NotFound();
            }

            _context.Mention.Remove(mention);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MentionExists(int id)
        {
            return _context.Mention.Any(e => e.id_mention == id);
        }
    }
}
