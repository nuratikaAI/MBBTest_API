using MBBTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MBBTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancersController : ControllerBase
    {

        private readonly FreelancerContext _dbContext;

        public FreelancersController(FreelancerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Freelancers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Freelancer>>> GetFreelancers()
        {
            if (_dbContext.Freelancers == null)
            {
                return NotFound();
            }

            return await _dbContext.Freelancers.ToListAsync();
        }

        // GET: api/Freelancers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Freelancer>> GetFreelancers(int id)
        {
            if (_dbContext.Freelancers == null)
            {
                return NotFound();
            }

            var freelancer = await _dbContext.Freelancers.FindAsync(id);

            if (freelancer == null)
            {
                return NotFound();
            }

            return freelancer;
        }

        // POST: api/Freelancers
        [HttpPost]
        public async Task<ActionResult<Freelancer>> PostFreelancer(Freelancer freelancer)
        {
            _dbContext.Freelancers.Add(freelancer);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFreelancers), new { id = freelancer.Id }, freelancer);
        }

        // PUT: api/Freelancers/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutFreelancer(int id, Freelancer freelancer)
        {
            if (id != freelancer.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(freelancer).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FreelanceExists(id))
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

        // DELETE: api/Freelancers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFreelancer(int id)
        {
            if (_dbContext.Freelancers == null)
            {
                return NotFound();
            }

            var freelance = await _dbContext.Freelancers.FindAsync(id);

            if (freelance == null)
            {
                return NotFound();
            }

            _dbContext.Freelancers.Remove(freelance);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool FreelanceExists(int id)
        {
            return (_dbContext.Freelancers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
