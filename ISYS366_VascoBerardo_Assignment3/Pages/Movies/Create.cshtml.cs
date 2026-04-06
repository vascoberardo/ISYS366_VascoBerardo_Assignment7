using ISYS366_VascoBerardo_Assignment3.Data;
using ISYS366_VascoBerardo_Assignment3.Models;
using ISYS366_VascoBerardo_Assignment3.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISYS366_VascoBerardo_Assignment3.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly ISYS366_VascoBerardo_Assignment3.Data.ISYS366_VascoBerardo_Assignment3Context _context;
        private readonly IWebHostEnvironment _env;
        public CreateModel(ISYS366_VascoBerardo_Assignment3.Data.ISYS366_VascoBerardo_Assignment3Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

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
                Movie.PictureUri = string.Empty;
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
