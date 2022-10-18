using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.DataLayer
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
