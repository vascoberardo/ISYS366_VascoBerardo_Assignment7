using ISYS366_VascoBerardo_Assignment3.Models;

namespace ISYS366_VascoBerardo_Assignment3.Data
{
    public interface IMovieRepo
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie?> GetByIdAsync(int id);
        Task<IEnumerable<string>> GetAllGenresAsync();
        Task<IList<Movie>> SearchMoviesAsync(string? searchString, string? genre);
        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);
    }
}
