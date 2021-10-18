using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lesson9_HandsOn.Models;

namespace Lesson9_HandsOn.Controllers
{
    [Route("api")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly CarContext _context;
        private readonly LastDeletedContext _lastDeletedContext;

        public CarController(CarContext context, LastDeletedContext lastDeletedContext)
        {
            _context = context;
            _lastDeletedContext = lastDeletedContext;
        }

        // GET: api/LastDeleted
        [HttpGet("LastDeleted")]
        public async Task<ActionResult<LastDeleted>> GetLastDeleted()
        {
            //get the last deleted car
            var deletedCar = await _lastDeletedContext.LastDeleted.FindAsync(1);
            var carList = await _context.Car.ToListAsync();
            //if it doesn't exist return 404
            if (deletedCar == null)
            {
                return NotFound();
            }
            //create a new car to add to the cars table
            Car newCar = new();
            newCar.Id = carList.Count + 1;
            newCar.Make = deletedCar.Make;
            newCar.Model = deletedCar.Model;
            newCar.Year = deletedCar.Year;

            //add the car back to the cars table
            _context.Car.Add(newCar);
            await _context.SaveChangesAsync();
            //remove the car from the undo list
            _lastDeletedContext.LastDeleted.Remove(deletedCar);
            await _lastDeletedContext.SaveChangesAsync();
            //return the lastdeleted car
            return deletedCar;
        }

        // PUT: api/LastDeleted
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("LastDeleted")]
        public async Task<IActionResult> PutCar(LastDeleted car)
        {
            Console.WriteLine("Updating last deleted entry");
            var check = await _lastDeletedContext.LastDeleted.ToListAsync();
            Console.WriteLine(check.Count);
            //check to see if there are any entries
            if(check.Count == 0)
            {
                _lastDeletedContext.LastDeleted.Add(car);
            }
            else
            {
                //if one already exists, replace it with the most recently deleted car
                _lastDeletedContext.LastDeleted.Remove(check[0]);
                await _lastDeletedContext.SaveChangesAsync();
                _lastDeletedContext.LastDeleted.Add(car);
            }

            await _lastDeletedContext.SaveChangesAsync();
            Console.WriteLine("LastDeleted updated");

            return NoContent();
        }

        // GET: api/Car
        [HttpGet("Car")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            return await _context.Car.ToListAsync();
        }

        // GET: api/Car/5
        [HttpGet("Car/{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Car.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Car/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Car/{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Car
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Car")]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            Console.WriteLine("POST car");   
            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Car/5
        [HttpDelete("Car/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            Console.WriteLine("Delete car");
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();

            //fix the id's that way they are in ascending order
            Console.WriteLine("Updating ID's");
            var carList = await _context.Car.ToListAsync();
            int Id = 1;
            foreach (var c in carList)
            {
                if (c.Id != Id)
                {
                    _context.Car.Remove(c);
                    await _context.SaveChangesAsync();
                    c.Id = Id;
                    _context.Add(c);
                    await _context.SaveChangesAsync();
                }
                Id++;
            }
            Console.WriteLine("Car list updated");

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }
    }
}
