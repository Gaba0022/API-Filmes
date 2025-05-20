using AutoMapper;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;

namespace FilmesApi.Profiles;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<CreateMovieDTOs, Movie>();
        CreateMap<UpdateMovieDTOs , Movie>();
        CreateMap<Movie, UpdateMovieDTOs>();
        CreateMap<Movie, ReadMovieDTOs>();
    }
}