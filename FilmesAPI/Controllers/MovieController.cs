using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="createMovieDTOs">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddMove([FromBody] CreateMovieDTOs createMovieDTOs)
    {
        Movie _movie = _mapper.Map<Movie>(createMovieDTOs);
        _context.Movies.Add(_movie);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecoverMovieId),
            new { id = _movie.Id },
            _movie);
    }

    /// <summary>
    /// Recupera uma lista de filmes com paginação
    /// </summary>
    /// <param name="skip">Número de filmes a pular</param>
    /// <param name="take">Número máximo de filmes a retornar</param>
    /// <returns>Lista de filmes</returns>
    /// <response code="200">Filmes recuperados com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadMovieDTOs> RecoverMovie([FromQuery] int skip = 0, [FromQuery] int take = 1000)
    {
        return _mapper.Map<List<ReadMovieDTOs>>(_context.Movies.Skip(skip).Take(take));
    }

    /// <summary>
    /// Recupera um filme pelo seu ID
    /// </summary>
    /// <param name="id">ID do filme a ser recuperado</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Filme encontrado</response>
    /// <response code="404">Filme não encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecoverMovieId(int id)
    {
        var movie = _context.Movies.FirstOrDefault(_movie => _movie.Id == id);
        if (movie == null)
            return NotFound();
        var movieDTO = _mapper.Map<ReadMovieDTOs>(movie);
        return Ok(movieDTO);
    }

    /// <summary>
    /// Atualiza um filme completamente
    /// </summary>
    /// <param name="id">ID do filme a ser atualizado</param>
    /// <param name="updateMovieDTOs">Objeto com os novos dados do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Atualização feita com sucesso</response>
    /// <response code="404">Filme não encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult MovieUpdate(int id, [FromBody] UpdateMovieDTOs updateMovieDTOs)
    {
        var movie = _context.Movies.FirstOrDefault(_movie => _movie.Id == id);
        if (movie == null)
            return NotFound();
        _mapper.Map(updateMovieDTOs, movie);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Atualiza parcialmente um filme
    /// </summary>
    /// <param name="id">ID do filme a ser atualizado</param>
    /// <param name="patch">Operações de modificação no filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Atualização parcial feita com sucesso</response>
    /// <response code="400">Erro na validação dos dados</response>
    /// <response code="404">Filme não encontrado</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Remove um filme do banco de dados
    /// </summary>
    /// <param name="id">ID do filme a ser removido</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Remoção feita com sucesso</response>
    /// <response code="404">Filme não encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
