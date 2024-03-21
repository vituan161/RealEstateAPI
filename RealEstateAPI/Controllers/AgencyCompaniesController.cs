using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Data;
using RealEstateAPI.Models;

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyCompaniesController : ControllerBase
    {
        private readonly RealEstateAPIContext _context;

        public AgencyCompaniesController(RealEstateAPIContext context)
        {
            _context = context;
        }

        // GET: api/AgencyCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgencyCompany>>> GetAgencyCompany()
        {
            return await _context.AgencyCompany.ToListAsync();
        }

        // GET: api/AgencyCompanies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgencyCompany>> GetAgencyCompany(int id)
        {
            var agencyCompany = await _context.AgencyCompany.FindAsync(id);

            if (agencyCompany == null)
            {
                return NotFound();
            }

            return agencyCompany;
        }

        // PUT: api/AgencyCompanies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"),Authorize]
        public async Task<IActionResult> PutAgencyCompany(int id, AgencyCompany agencyCompany)
        {
            if (id != agencyCompany.id)
            {
                return BadRequest();
            }

            _context.Entry(agencyCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgencyCompanyExists(id))
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

        // POST: api/AgencyCompanies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost,Authorize]
        public async Task<ActionResult<AgencyCompany>> PostAgencyCompany(AgencyCompany agencyCompany)
        {
            _context.AgencyCompany.Add(agencyCompany);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgencyCompany", new { id = agencyCompany.id }, agencyCompany);
        }

        // DELETE: api/AgencyCompanies/5
        [HttpDelete("{id}"),Authorize]
        public async Task<IActionResult> DeleteAgencyCompany(int id)
        {
            var agencyCompany = await _context.AgencyCompany.FindAsync(id);
            if (agencyCompany == null)
            {
                return NotFound();
            }

            _context.AgencyCompany.Remove(agencyCompany);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgencyCompanyExists(int id)
        {
            return _context.AgencyCompany.Any(e => e.id == id);
        }
    }
}
