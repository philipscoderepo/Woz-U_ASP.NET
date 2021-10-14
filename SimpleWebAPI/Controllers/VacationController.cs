using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleWebAPI.Models;

namespace SimpleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly VacationContext _context;

        public VacationController(VacationContext context)
        {
            _context = context;
        }

        [Route("/api/vacation/init/{number}")][HttpGet]
        public async Task<bool> Init(int number){
            if(number < 1) return false;
            if(number > 10) return false;
            
            for(int i=0; i<number; i++){
                _context.Vacations.Add(new Vacation(){Name = $"Vacation item {i}", Visited = 0==i%2});
            }
            await _context.SaveChangesAsync();
            return true;
        }

        // GET: api/Vacation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vacation>>> GetVacations()
        {
            return await _context.Vacations.ToListAsync();
        }

        // GET: api/Vacation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vacation>> GetVacation(long id)
        {
            var vacation = await _context.Vacations.FindAsync(id);

            if (vacation == null)
            {
                return NotFound();
            }

            return vacation;
        }

        // PUT: api/Vacation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacation(long id, Vacation vacation)
        {
            if (id != vacation.Id)
            {
                return BadRequest();
            }

            _context.Entry(vacation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacationExists(id))
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

        // POST: api/Vacation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vacation>> PostVacation(Vacation vacation)
        {
            _context.Vacations.Add(vacation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVacation), new { id = vacation.Id }, vacation);
        }

        // DELETE: api/Vacation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacation(long id)
        {
            var vacation = await _context.Vacations.FindAsync(id);
            if (vacation == null)
            {
                return NotFound();
            }

            _context.Vacations.Remove(vacation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VacationExists(long id)
        {
            return _context.Vacations.Any(e => e.Id == id);
        }
    }
}
