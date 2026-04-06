using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ISYS366_VascoBerardo_Assignment3.Data;
using ISYS366_VascoBerardo_Assignment3.Models;
using ISYS366_VascoBerardo_Assignment3.Utils;

namespace ISYS366_VascoBerardo_Assignment3.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly ISYS366_VascoBerardo_Assignment3.Data.ISYS366_VascoBerardo_Assignment3Context _context;
        private readonly IWebHostEnvironment _env;

        public EditModel(ISYS366_VascoBerardo_Assignment3.Data.ISYS366_VascoBerardo_Assignment3Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        [BindProperty]
        public IFormFile? MovieImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            Movie = movie;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (HttpContext.Request.Form.Files.Count > 0)
            {
                Movie.PictureUri = PictureHelper.UploadNewImage(_env,
                    HttpContext.Request.Form.Files[0]);
            }
            else
            {
                var existing = await _context.Movie.AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == Movie.Id);
                Movie.PictureUri = existing?.PictureUri ?? string.Empty;
            }

            _context.Attach(Movie).State = EntityState.Modified; await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
