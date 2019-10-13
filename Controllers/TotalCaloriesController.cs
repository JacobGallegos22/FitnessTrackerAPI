using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFitnessTrackerAPI.Model;

namespace MyFitnessTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalCaloriesController : ControllerBase
    {
        private readonly TotalCaloriesContext _context;

        public TotalCaloriesController(TotalCaloriesContext context)
        {
            _context = context;
        }

        // GET: api/TotalCalories
        [HttpGet]
        public ActionResult<int> GetTotalCalories(String time)
        {
            DateTime dateStart = DateTime.Now;
            DateTime dateEnd = DateTime.Now;
            DateTime date = DateTime.Now;

            switch (time)
            {
                case "day":
                    dateStart = DateTime.Today;
                    dateEnd = DateTime.Today.AddHours(23);
                    break;
                case "week":
                    dateStart = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));
                    dateEnd = DateTime.Today.AddDays((int)DateTime.Today.DayOfWeek);
                    break;
                case "month":
                    dateStart = new DateTime(date.Year, date.Month, 1);
                    dateEnd = dateStart.AddMonths(1).AddDays(-1);
                    break;
                case "Year":
                    dateStart = new DateTime(date.Year, 1, 1);
                    dateEnd = new DateTime(date.Year, 12, 31);
                    break;
                default:
                    Console.WriteLine($"An unexpected value ({time})");
                    break;
            }
            var totalCalories = (from calorie in _context.TotalCalories
                                 where calorie.CreatedAt > dateStart && calorie.CreatedAt < dateEnd
                                 select calorie).Count();
            Console.WriteLine(totalCalories);



            return totalCalories;
        }

        // GET: api/TotalCalories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TotalCalories>> GetTotalCalories(long id)
        {
            var totalCalories = await _context.TotalCalories.FindAsync(id);

            if (totalCalories == null)
            {
                return NotFound();
            }

            return totalCalories;
        }

        // PUT: api/TotalCalories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTotalCalories(long id, TotalCalories totalCalories)
        {
            if (id != totalCalories.Id)
            {
                return BadRequest();
            }

            _context.Entry(totalCalories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TotalCaloriesExists(id))
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

        // POST: api/TotalCalories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult<TotalCalories>> PostTotalCalories(TotalCalories totalCalories)
        {
            _context.TotalCalories.Add(totalCalories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTotalCalories", new { id = totalCalories.Id }, totalCalories);
        }

        // DELETE: api/TotalCalories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TotalCalories>> DeleteTotalCalories(long id)
        {
            var totalCalories = await _context.TotalCalories.FindAsync(id);
            if (totalCalories == null)
            {
                return NotFound();
            }

            _context.TotalCalories.Remove(totalCalories);
            await _context.SaveChangesAsync();

            return totalCalories;
        }

        private bool TotalCaloriesExists(long id)
        {
            return _context.TotalCalories.Any(e => e.Id == id);
        }
    }
}
