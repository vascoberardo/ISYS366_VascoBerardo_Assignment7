using ISYS366_VascoBerardo_Assignment3.Models;
using Microsoft.EntityFrameworkCore;

namespace ISYS366_VascoBerardo_Assignment3.Data
{
    public class MovieRepoList : IMovieRepo
    {
        private List<Movie> _movies;

        public MovieRepoList()
        {
            _movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "When Harry Met Sally", ReleaseDate = DateTime.Parse("1989-2-12"), Genre = "Romantic Comedy", Price = 7.99M, Rank = 1 },
                new Movie { Id = 2, Title = "Ghostbusters", ReleaseDate = DateTime.Parse("1984-3-13"), Genre = "Comedy", Price = 8.99M, Rank = 2 },
                new Movie { Id = 3, Title = "Ghostbusters 2", ReleaseDate = DateTime.Parse("1986-2-23"), Genre = "Comedy", Price = 9.99M, Rank = 3 },
                new Movie { Id = 4, Title = "Rio Bravo", ReleaseDate = DateTime.Parse("1959-4-15"), Genre = "Western", Price = 3.99M, Rank = 4 }
            };
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await Task.FromResult(_movies.OrderBy(m => m.Rank).ThenBy(m => m.Title).ToList());
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_movies.FirstOrDefault(m => m.Id == id));
        }

        public async Task<IEnumerable<string>> GetAllGenresAsync()
        {
            return await Task.FromResult(_movies.Select(m => m.Genre).Distinct().OrderBy(g => g).ToList());
        }

        public async Task<IList<Movie>> SearchMoviesAsync(string? searchString, string? genre)
        {
            var movies = _movies.AsEnumerable();

            movies = movies.OrderBy(m => m.Rank);

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                movies = movies.Where(x => x.Genre == genre);
            }

            return await Task.FromResult(movies.ToList());
        }

        public async Task AddMovieAsync(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.Id = _movies.Max(m => m.Id) + 1;
            }
            _movies.Add(movie);
            await Task.CompletedTask;
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            var existingMovie = _movies.FirstOrDefault(m => m.Id == movie.Id);
            if (existingMovie != null)
            {
                existingMovie.Title = movie.Title;
                existingMovie.ReleaseDate = movie.ReleaseDate;
                existingMovie.Genre = movie.Genre;
                existingMovie.Price = movie.Price;
                existingMovie.Rank = movie.Rank;
                existingMovie.PictureUri = movie.PictureUri;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = _movies.FirstOrDefault(m => m.Id == id);
            if (movie != null)
            {
                _movies.Remove(movie);
            }
            await Task.CompletedTask;
        }
    }
}