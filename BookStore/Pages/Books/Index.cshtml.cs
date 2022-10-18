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
    public class IndexModel : PageModel
    {
        private readonly BookStore.DataLayer.BookDbContext _context;

        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }

        public IndexModel(BookStore.DataLayer.BookDbContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            if (_context.Books != null)
            {
                TitleSort = sortOrder == "title_asc_sort" ? "title_desc_sort" : "title_asc_sort";
                AuthorSort = sortOrder == "author_asc_sort" ? "author_desc_sort" : "author_asc_sort";

                if(!String.IsNullOrEmpty(searchString))
                {
                    Book = await _context.Books.Where(b => b.Title.Contains(searchString) 
                        || b.Author.Contains(searchString)
                        || b.Category.Name.Contains(searchString)).ToListAsync();
                }
                else
                {
                    Book = await _context.Books.ToListAsync();
                }

                foreach (var book in Book)
                {
                    book.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == book.CategoryId);
                }
            }

            switch (sortOrder)
            {
                case "title_asc_sort":
                    Book = Book.OrderBy(b => b.Title).ToList();
                    break;
                case "title_desc_sort":
                    Book = Book.OrderByDescending(b => b.Title).ToList();
                    break;
                case "author_asc_sort":
                    Book = Book.OrderBy(b => b.Author).ToList();
                    break;
                case "author_desc_sort":
                    Book = Book.OrderByDescending(b => b.Author).ToList();
                    break;
                default:
                    Book = Book.OrderBy(b => b.Title).ToList();
                    break;
            }
        }
    }
}
