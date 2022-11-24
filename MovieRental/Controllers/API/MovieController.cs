using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.Dtos;
using MovieRental.Models;

namespace MovieRental.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public MovieController(IMapper mapper, ApplicationDbContext db)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<MovieDto> GetMovies(string? query = null)
        {
            var moviesQuery = _db.Movies.Include(g => g.Genre).Where(n => n.NumberAvailable > 0).AsQueryable();
            if(!String.IsNullOrEmpty(query) )
            {
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
            }

            return moviesQuery.ToList().Select(_mapper.Map<Movie, MovieDto>);

            //return _db.Movies.Include(g => g.Genre).ToList().Select(_mapper.Map<Movie, MovieDto>);
        }

        [HttpGet("{id:int}", Name ="GetMovie")]
        public IActionResult GetMovie(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var movie = _db.Movies.Include(g => g.Genre).FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(_mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPost]
        public IActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = _mapper.Map<MovieDto, Movie>(movieDto);
            _db.Movies.Add(movie);
            _db.SaveChanges();

            return CreatedAtRoute("GetMovie", new { id = movieDto.Id }, movieDto);
        }

        [HttpPut("{id:int}", Name ="UpdateMovie")]
        public ActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _db.Movies.FirstOrDefault(m => m.Id==id);
            if (movie == null)
            {
                return NotFound();
            }
            _mapper.Map(movieDto, movie);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}", Name ="DeleteMovie")]
        public ActionResult DeleteMovie(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var movie = _db.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            _db.Movies.Remove(movie);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
