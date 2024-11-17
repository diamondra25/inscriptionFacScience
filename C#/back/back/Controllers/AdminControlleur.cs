using back.Models;
using back.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminControlleur : ControllerBase
    {
        private readonly DatabaseContext _context;
        public AdminControlleur(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>>GetAdmins()
        { 
            return await _context.Admin.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admin.FirstOrDefaultAsync(a=>a.id == id);
            if (admin==null)
            {
                return NotFound();
            }

            return admin;
        }

        [HttpPost]
        public async Task<ActionResult<Admin>>PostAdmin([FromBody] AdminDto adminDto)
        {
            if (adminDto == null)
            {
                return BadRequest("L'administrateur ne peut pas être nul.");
            }

            var newAdmin = new Admin
           {
                nom = adminDto.Nom,
                prenom = adminDto.Prenom,
                role = adminDto.Role,
            };
            newAdmin.SetPassword(adminDto.Password);


            _context.Admin.Add(newAdmin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmin", new { id = newAdmin.id }, newAdmin);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            if (id != admin.id)
            {
                return BadRequest();
            }
            _context.Entry(admin).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            _context.Admin.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool AdminExists(int id)
        {
            return _context.Admin.Any(a => a.id == id);
        }

    }
}
