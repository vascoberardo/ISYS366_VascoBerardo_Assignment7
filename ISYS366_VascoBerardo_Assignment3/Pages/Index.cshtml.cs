using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ISYS366_VascoBerardo_Assignment3.Data;
using ISYS366_VascoBerardo_Assignment3.Models;

namespace ISYS366_VascoBerardo_Assignment3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MovieRepoEf _repo;

        public IndexModel(ISYS366_VascoBerardo_Assignment3Context context)
        {
            _repo = new MovieRepoEf(context);
        }

        public IEnumerable<ISYS366_VascoBerardo_Assignment3.Models.Movie> Movie { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _repo.GetAllMoviesAsync();
        }
    }
}
