using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOS;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private new List<string> allowedExtensions= new List<string> { ".jpg",".png"};
        private long _maxallowedpostersize = 1048576;

        public MoviesController( ApplicationDbContext context)
        {
            this._context = context;
        }



        [HttpGet]
        public async Task<IActionResult> GetMovieAsync()
        { 
        var movie= await _context.Movie.Include(m=>m.genre).ToListAsync();
            return Ok(movie);
        }




        // get movie by id

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid( int id)
        {


            var movie = await _context.Movie.Include(m=>m.genre).SingleOrDefaultAsync(m=>m.Id==id);
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }


        [HttpGet("getbygenreid")]

        public async Task<IActionResult> GetByGenreid(int genreid)
        {


            var movie = await _context.Movie.Where(m => m.genreId == genreid)
                .OrderByDescending(x=>x.Rate).Include(m=>m.genre).ToListAsync();
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }





        [HttpPost]
        public async Task<IActionResult> createMovieAsync([FromForm]MovieDto dto)
        {

            if(dto.Poster == null)
            {
                return BadRequest("poster is required");
            }

            if (!allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("only jpg is allowed");


            if (dto.Poster.Length > _maxallowedpostersize)
                return BadRequest("max is 1 MB");

            var isvalidGenre= await _context.Genre.AnyAsync(g=> g.Id==dto.genreId);

            if(!isvalidGenre )
                return BadRequest("Invalid genre id");

            using  var datastream= new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);

            var Movie = new Movie
            {

                genreId=dto.genreId,
                Title= dto.Title,
                Poster=datastream.ToArray(),
                Rate=   dto.Rate,
                StoreLine=dto.StoreLine,
                Year=dto.Year


            };



          await  _context.AddAsync(Movie);
            _context.SaveChanges();
            return Ok(Movie);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> updateasync(int id, [FromForm]MovieDto dto)

        {


            var movie=await _context.Movie.FindAsync(id);
            if (movie == null) return BadRequest($"no movies was found by id {id}");

            var isvalidGenre = await _context.Genre.AnyAsync(g => g.Id == dto.genreId);

            if (!isvalidGenre)
                return BadRequest("Invalid genre id");

            //poster
            if (dto.Poster != null) 
            {
                if (!allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("only jpg is allowed");


                if (dto.Poster.Length > _maxallowedpostersize)
                    return BadRequest("max is 1 MB");


                using var datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);

                movie.Poster = datastream.ToArray();


            }

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.genreId = dto.genreId;
            movie.Rate = dto.Rate;
            movie.StoreLine = dto.StoreLine;
            _context.SaveChanges();
            return Ok(movie); 


        }







        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
                return NotFound($"No movie was found with id {id}");

            _context.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok(movie);
        }







    }
}
