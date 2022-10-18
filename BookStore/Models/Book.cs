using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Title name is too short.")]
        public string Title { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Author name is too short.")]
        public string Author { get; set; }

        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Date should be less than current date.")]
        public DateTime ReleaseDate { get; set; }

        [Range(1, 5, ErrorMessage = "Rating should be from 1 to 5.")]
        public string Rating { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }

    public class DateValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime date = Convert.ToDateTime(value);
            return date.Date <= DateTime.Now;
        }
    }
}
