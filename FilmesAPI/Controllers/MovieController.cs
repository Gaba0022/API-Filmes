
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieController.Controllers;


[ApiController]
[Route("[Controller]")]
public class MovieController : ControllerBase
{
    private static List<Movie> movies = new List<Movie>();
    private static int id = 0;

    [HttpPost]
    public IActionResult AddMove([FromBody] Movie _movie)
    {
        _movie.Id = id++;
        movies.Add(_movie);

        return CreatedAtAction(nameof(RecoverMovieId), 
            new {id = _movie.Id}, 
            _movie);

    }

    [HttpGet]
    public IEnumerable<Movie> RecoverMovie([FromQuery] int skip = 0, 
        [FromQuery] int take = 1000) 
    {
        return movies.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RecoverMovieId(int id)
    {
        var movie = movies.FirstOrDefault(_movie => _movie.Id == id);
        if(movie == null) 
            return NotFound();
        return Ok(movie);
    }


}

