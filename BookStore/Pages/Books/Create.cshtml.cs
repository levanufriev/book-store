using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStore.DataLayer;
using BookStore.Models;

namespace BookStore.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly BookStore.DataLayer.BookDbContext _context;

        public List<SelectListItem> Categories { get; set; }

        public CreateModel(BookStore.DataLayer.BookDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Categories = _context.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            }).ToList();

            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
