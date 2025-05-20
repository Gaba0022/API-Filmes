
using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MovieController.Controllers;


[ApiController]
[Route("[Controller]")]
public class MovieController : ControllerBase
{

    private MovieContext _context;
    private IMapper _mapper;

    public MovieController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddMove([FromBody] CreateMovieDTOs createMovieDTOs)
    {
        Movie _movie = _mapper.Map<Movie>(createMovieDTOs);
        _context.Movies.Add(_movie);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecoverMovieId),
            new { id = _movie.Id },
            _movie);

    }

    [HttpGet]
    public IEnumerable<ReadMovieDTOs> RecoverMovie([FromQuery] int skip = 0,
        [FromQuery] int take = 1000)
    {
        return _mapper.Map<List<ReadMovieDTOs>>(_context.Movies.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecoverMovieId(int id)
    {
        var movie = _context.Movies.FirstOrDefault(_movie => _movie.Id == id);
        if (movie == null)
            return NotFound();
        var movieDTO = _mapper.Map<ReadMovieDTOs>(movie);
        return Ok(movieDTO);
    }

    [HttpPut("{id}")]
    public IActionResult MovieUpdate(int id, [FromBody] UpdateMovieDTOs updateMovieDTOs)
    {
        var movie = _context.Movies.FirstOrDefault(_movie => _movie.Id == id);
        if (movie == null)
            return NotFound();
        _mapper.Map(updateMovieDTOs, movie);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult MoviePath(int id, JsonPatchDocument<UpdateMovieDTOs> patch)
    {
        var movie = _context.Movies.FirstOrDefault(_movie => _movie.Id == id);

        if (movie == null)
            return NotFound();

        var movieToUpdate = _mapper.Map<UpdateMovieDTOs>(movie);
        patch.ApplyTo(movieToUpdate);

        if (!TryValidateModel(movieToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(movieToUpdate, movie);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult MovieDelete(int id)
    {
        var movie = _context.Movies.FirstOrDefault(_movie => _movie.Id == id);
        if (movie == null)
            return NotFound();

        _context.Remove(movie);
        _context.SaveChanges();
        return NoContent();
    }
}

