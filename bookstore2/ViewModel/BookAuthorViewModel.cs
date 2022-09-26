using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bookstore2.Models;
using Microsoft.AspNetCore.Http;

namespace bookstore2.ViewModel
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }
        [Required]
        [MaxLength(255)]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(3)]    
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
        public IFormFile File { get; set; }
        public string ImgUrl { get; set; }
    }
}
