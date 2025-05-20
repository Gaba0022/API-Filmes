using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTOs;

public class ReadMovieDTOs
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set;} = DateTime.Now;

}
