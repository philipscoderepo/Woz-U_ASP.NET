using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lesson7_HandsOn.Models;
using Microsoft.Data.Sqlite;

namespace Lesson7_HandsOn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarContext _context;

        public CarController(CarContext context)
        {
            _context = context;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.OrderBy(Car => Car.Year).ToListAsync();
        }

        // GET: api/Car
        [HttpGet("three-pass-or-less")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCarsWithThreePassOrLess()
        {
            var list = await _context.Cars.OrderBy(Car => Car.Year).ToListAsync();
            List<Car> newList = new List<Car>();
            int i = 0;
            foreach (var car in list)
            {
                if(car.NumberOfPassengers < 3){
                    newList.Add(car);
                }
                i++;
            }

            return newList;
        }

        // GET: api/Car/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(long id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Car/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(long id, Car car)
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
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        [HttpPost("new-car")]
        public async Task<ActionResult<Car>> PostNewCar(int id, int year, string make, string model, int numberOfPassengers)
        {
            Car car = new Car(){
                Id = id,
                Year = year,
                Make = make,
                Model = model,
                NumberOfPassengers = numberOfPassengers
            };
            if(CarExists(id)){
                await PutCar(id, car);
            }else{
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Car/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(long id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(long id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
