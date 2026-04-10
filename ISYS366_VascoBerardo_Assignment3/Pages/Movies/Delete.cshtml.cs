using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ISYS366_VascoBerardo_Assignment3.Data;
using ISYS366_VascoBerardo_Assignment3.Models;

namespace ISYS366_VascoBerardo_Assignment3.Pages.Movies
{
    public class DeleteModel : PageModel
    {
        private readonly IMovieRepo _repo;

        public DeleteModel(IMovieRepo repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _repo.GetByIdAsync(id.Value);

            if (movie is not null)
            {
                Movie = movie;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _repo.GetByIdAsync(id.Value);
            if (movie != null)
            {
                await _repo.DeleteMovieAsync(movie.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
