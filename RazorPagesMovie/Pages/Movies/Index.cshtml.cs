using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Models.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Models.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }
        public SelectList Genres { get; set; }
        public string MovieGenre { get; set; }

        public async Task OnGetAsync(string q, string movieGenre)
        {
            var genreQuery = _context.Movie
                                     .OrderBy(m => m.Genre)
                                     .Select(m => m.Genre);

            var movies = _context.Movie.Select(m => m);

            if (!String.IsNullOrWhiteSpace(q))
            {
                movies = movies.Where(m => m.Title.Contains(q));
            }

            if (!String.IsNullOrWhiteSpace(movieGenre))
            {
                movies = movies.Where(m => m.Genre == movieGenre);
            }

            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    }
}
