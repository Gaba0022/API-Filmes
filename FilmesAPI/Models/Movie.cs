using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models;
public class Movie
{

    //[Required(ErrorMessage = "The id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "The title is required")]
    [StringLength(100, ErrorMessage = "The title cannot be longer than 100 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "The Genre is required")]
    [StringLength(50, ErrorMessage = "The genre cannot be longer than 50 characters")]
    public string Genre { get; set; }
    
    [Required]
    [Range(70,600, ErrorMessage = "The duration must be between 70m to 600m")]
    public int Duration { get; set; }

    public string Director { get; set; }

}
