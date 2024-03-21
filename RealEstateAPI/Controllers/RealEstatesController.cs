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
    public class RealEstatesController : ControllerBase
    {
        private readonly RealEstateAPIContext _context;

        public RealEstatesController(RealEstateAPIContext context)
        {
            _context = context;
        }

        // GET: api/RealEstates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RealEstate>>> GetRealEstate()
        {
            return await _context.RealEstate.Include(r=>r.Prices).ToListAsync();
        }

        // GET: api/RealEstates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RealEstate>> GetRealEstate(int id)
        {
            var realEstate = await _context.RealEstate.Include(r=>r.Prices).Include(r => r.Seller.User.Profile).FirstOrDefaultAsync(r=>r.id == id);

            if (realEstate == null)
            {
                return NotFound();
            }

            return realEstate;
        }

        // PUT: api/RealEstates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRealEstate(int id, RealEstate realEstate)
        {
            if (id != realEstate.id)
            {
                return BadRequest();
            }
            // Fetch the current state of the RealEstate entity from the database
            var currentRealEstate = await _context.RealEstate.Include(r => r.Prices).FirstOrDefaultAsync(r => r.id == id);
            if (currentRealEstate == null)
            {
                return NotFound();
            }

            // Update the properties that are present in the request
            currentRealEstate.Name = realEstate.Name;
            currentRealEstate.Area = realEstate.Area;
            currentRealEstate.Address = realEstate.Address;
            currentRealEstate.Link = realEstate.Link;
            currentRealEstate.Imageurl = realEstate.Imageurl;
            currentRealEstate.Description = realEstate.Description;
            currentRealEstate.Design = realEstate.Design;
            currentRealEstate.Legality = realEstate.Legality;
            currentRealEstate.Type = realEstate.Type;
            currentRealEstate.DateExprired = realEstate.DateExprired;
            currentRealEstate.Status = realEstate.Status;

            //check if the prices are provided in the request
            if (realEstate.Prices != null && realEstate.Prices.Count > 0)
                {
                    foreach(var price in realEstate.Prices)
                    {
                        // Add each Price object to the context so they get created in the database
                        currentRealEstate.Prices.Add(price);
                    }
                }   

                try
                {
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealEstateExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            
        }

        // POST: api/RealEstates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost,Authorize]
        public async Task<ActionResult<RealEstate>> PostRealEstate(RealEstate realEstate)
        {
            //get the id of the user who is currently logged in
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //find the seller with the same user id
            Seller seller = await _context.Seller.Where(s => s.UserId == Int32.Parse(userId)).FirstOrDefaultAsync();
            //if the seller does not exist, create a new seller
            if(seller == null)
            {
                seller = new Seller { UserId = Int32.Parse(userId) };
                _context.Seller.Add(seller);
                await _context.SaveChangesAsync();
            }
            //set the seller id of the real estate to the id of the seller
            realEstate.SellerId = seller.id;

            // Check if prices are provided in the request
            if (realEstate.Prices != null && realEstate.Prices.Count > 0)
            {
                foreach (var price in realEstate.Prices)
                {
                    // Add each Price object to the context so they get created in the database
                    _context.Price.Add(price);
                }
            }

            _context.RealEstate.Add(realEstate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRealEstate", new { id = realEstate.id }, realEstate);
        }

        // DELETE: api/RealEstates/5
        [HttpDelete("{id}"),Authorize]
        public async Task<IActionResult> DeleteRealEstate(int id)
        {
            var realEstate = await _context.RealEstate.FindAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }

            _context.RealEstate.Remove(realEstate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RealEstateExists(int id)
        {
            return _context.RealEstate.Any(e => e.id == id);
        }
    }
}
