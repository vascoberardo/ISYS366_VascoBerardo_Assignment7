using ISYS366_VascoBerardo_Assignment3.Models;
using Microsoft.EntityFrameworkCore;

namespace ISYS366_VascoBerardo_Assignment3.Data
{
    public class MovieRepoEf
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

        //public ISYS366_VascoBerardo_Assignment3.Models.Movie? GetById(int id);
    }
}
