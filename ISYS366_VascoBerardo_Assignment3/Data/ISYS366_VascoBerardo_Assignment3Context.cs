using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ISYS366_VascoBerardo_Assignment3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ISYS366_VascoBerardo_Assignment3.Data
{
    public class ISYS366_VascoBerardo_Assignment3Context : IdentityDbContext
    {
        public ISYS366_VascoBerardo_Assignment3Context (DbContextOptions<ISYS366_VascoBerardo_Assignment3Context> options)
            : base(options)
        {
        }

        public DbSet<ISYS366_VascoBerardo_Assignment3.Models.Movie> Movie { get; set; } = default!;
    }
}
