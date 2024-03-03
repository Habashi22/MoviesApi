using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOS;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _context.Genre.ToListAsync();
            return Ok(genres);

        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
        {


            var genre = new genre
            {
                Id = dto.Id,
                Name = dto.Name
            };

            await _context.Genre.AddAsync(genre);
            _context.SaveChanges();
            return Ok(genre);

        }


        [HttpPut("{id}")]

        public async Task<IActionResult> updateAsync(int id, [FromBody] CreateGenreDto dto)
        {

            var genre = await _context.Genre.SingleOrDefaultAsync(s => s.Id == id);

            if (genre == null)
                return NotFound($"no genre found{id}");

            genre.Name = dto.Name;
            _context.SaveChanges();
            return Ok(genre);



        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {


            var genre = await _context.Genre.SingleOrDefaultAsync(s => s.Id == id);

            if (genre == null)
                return NotFound($"no genre found{id}");
            _context.Genre.Remove(genre);
            _context.SaveChanges();
            return Ok(genre);

        }


    }
}
