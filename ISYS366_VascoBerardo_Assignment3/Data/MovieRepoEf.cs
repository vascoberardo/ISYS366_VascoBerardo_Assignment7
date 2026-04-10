using ISYS366_VascoBerardo_Assignment3.Models;
using Microsoft.EntityFrameworkCore;

namespace ISYS366_VascoBerardo_Assignment3.Data
{
    public class MovieRepoEf : IMovieRepo
    {
        private readonly ISYS366_VascoBerardo_Assignment3Context _context;

        public MovieRepoEf(ISYS366_VascoBerardo_Assignment3Context context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ISYS366_VascoBerardo_Assignment3.Models.Movie>> GetAllMoviesAsync()
        {
            return await _context.Movie.OrderBy(m => m.Rank).ThenBy(m => m.Title).ToListAsync();
        }
        public async Task<ISYS366_VascoBerardo_Assignment3.Models.Movie?> GetByIdAsync(int id)
        {
            return await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<IEnumerable<string>> GetAllGenresAsync()
        {
            return await _context.Movie.Select(m => m.Genre).Distinct().OrderBy(g => g).ToListAsync();
        }

        public async Task<IList<Movie>> SearchMoviesAsync(string? searchString, string? genre)
        {
            var movies = from m in _context.Movie
                         select m;

            movies = movies.OrderBy(m => m.Rank);

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                movies = movies.Where(x => x.Genre == genre);
            }

            return await movies.ToListAsync();
        }
        public async Task AddMovieAsync(Movie movie)
        {
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Attach(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
