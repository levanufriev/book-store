using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStore.DataLayer;
using BookStore.Models;

namespace BookStore.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly BookStore.DataLayer.BookDbContext _context;

        public DetailsModel(BookStore.DataLayer.BookDbContext context)
        {
            _context = context;
        }

      public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);
            book.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == book.CategoryId);
            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
            }
            return Page();
        }
    }
}
