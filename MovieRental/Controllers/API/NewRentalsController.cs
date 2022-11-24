using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRental.Data;
using MovieRental.Dtos;
using MovieRental.Models;

namespace MovieRental.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public NewRentalsController(ApplicationDbContext db)
        {
            _db= db;
        }

        [HttpPost]
        public IActionResult CreateNewRentals(NewRentalDto newRental)
        {
            if (newRental.MovieIds.Count == 0)
            {
                return BadRequest("No Movie Ids have been given.");
            }

            var customer = _db.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
            if(customer == null)
            {
                return BadRequest("Customer Id is not Valid.");
            }

            var movies = _db.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();
            if (movies.Count != newRental.MovieIds.Count)
            {
                return BadRequest("One or more Movie Ids are invalid.");
            }

            foreach(var movie in movies)
            {
                if(movie.NumberAvailable == 0)
                {
                    return BadRequest("Movie is not available.");
                }
                movie.NumberAvailable--;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now,
                };

                _db.Rentals.Add(rental);
            }
            _db.SaveChanges();

            return Ok();
        }
    }
}
