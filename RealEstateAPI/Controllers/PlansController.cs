using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class PlansController : ControllerBase
    {
        private readonly RealEstateAPIContext _context;

        public PlansController(RealEstateAPIContext context)
        {
            _context = context;
        }

        // GET: api/Plans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plan>>> GetPlan()
        {
            return await _context.Plan.Include(p => p.realEstates).ToListAsync();
        }

        // GET: api/Plans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plan>> GetPlan(int id)
        {
            var plan = await _context.Plan.Include(p => p.realEstates).FirstOrDefaultAsync(p => p.id == id);

            if (plan == null)
            {
                return NotFound();
            }

            return plan;
        }

        // PUT: api/Plans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"),Authorize]
        public async Task<IActionResult> PutPlan(int id, Plan plan)
        {
            if (id != plan.id)
            {
                return BadRequest();
            }

            _context.Entry(plan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(id))
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

        // POST: api/Plans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost,Authorize]
        public async Task<ActionResult<Plan>> PostPlan(Plan plan)
        {
            //get the id of the user who is currently logged in
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //find the seller with the same user id
            Seller seller = await _context.Seller.Where(s => s.UserId == Int32.Parse(userId)).Include(s => s.User).Include(s => s.AgencyCompany).FirstOrDefaultAsync();
            //if the seller does not exist, create a new seller
            if (seller == null)
            {
                return NotFound();
            }
            else
            {
                if(seller.AgencyCompanyId != null && seller.User.IsOfficial)
                {
                    plan.SellerId = seller.id;
                    // Load the existing RealEstate entities from the database
                    var realEstates = await _context.RealEstate.Where(r => plan.realEstates.Select(re => re.id).Contains(r.id)).ToListAsync();

                    // Replace the RealEstate objects in the plan with the existing RealEstate entities
                    plan.realEstates = realEstates;
                    foreach (var realEstate in realEstates)
                    {
                        realEstate.PlanId = plan.id;
                    }

                    _context.Plan.Add(plan);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Unauthorized();
                }
            }
            

            return CreatedAtAction("GetPlan", new { id = plan.id }, plan);
        }

        // DELETE: api/Plans/5
        [HttpDelete("{id}"),Authorize]
        public async Task<IActionResult> DeletePlan(int id)
        {
            var plan = await _context.Plan.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

            _context.Plan.Remove(plan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanExists(int id)
        {
            return _context.Plan.Any(e => e.id == id);
        }
    }
}
