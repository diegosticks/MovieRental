using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.Models;
using MovieRental.ViewModels;

namespace MovieRental.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Movies.Include(m=>m.Genre).ToList());
        }

        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var movie = _db.Movies.Include(c => c.Genre).SingleOrDefault(x => x.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var genre = _db.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = genre,
            };

            return View("Upsert", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Movie movie)
        {
            if (movie.Id == 0)
                _db.Movies.Add(movie);
            else
            {
                var movieInDb = _db.Movies.Single(c => c.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _db.SaveChanges();

            return RedirectToAction("Index", "Movie");
        }

        public IActionResult Edit(int id)
        {
            var movie = _db.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _db.Genres.ToList()
            };

            return View("Upsert", viewModel);
        }
    }
}
