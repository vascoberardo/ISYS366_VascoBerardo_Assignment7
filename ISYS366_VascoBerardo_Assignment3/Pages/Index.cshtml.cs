using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ISYS366_VascoBerardo_Assignment3.Data;
using ISYS366_VascoBerardo_Assignment3.Models;

namespace ISYS366_VascoBerardo_Assignment3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ISYS366_VascoBerardo_Assignment3Context _context;

        public IndexModel(ISYS366_VascoBerardo_Assignment3Context context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie
                .OrderBy(m => m.Rank)
                .ThenBy(m => m.Title)
                .ToListAsync();
        }
    }
}
