
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieController.Controllers;


[ApiController]
[Route("[Controller]")]
public class MovieController : ControllerBase
{
    private static List<Movie> movies = new List<Movie>();

    [HttpPost]
    public void AddMove([FromBody] Movie _movie)
    {
        movies.Add(_movie);
        Console.WriteLine(_movie.Title);
        Console.WriteLine(_movie.Genre);
        Console.WriteLine(_movie.Duration);
    }
}

