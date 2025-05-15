
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
    public void AddMove([FromBody] Movie _movie)
    {
        _movie.Id = id++;
        movies.Add(_movie);
        Console.WriteLine(_movie.Title);
        Console.WriteLine(_movie.Genre);
        Console.WriteLine(_movie.Duration);
    }

    [HttpGet]
    public IEnumerable<Movie> RecoverMovie() 
    {
        return movies;
    }

    [HttpGet("{id}")]
    public Movie? RecoverMovieId(int id)
    {
        return movies.FirstOrDefault(_movie => _movie.Id == id);
    }


}

