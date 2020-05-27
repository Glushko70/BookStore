using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.DAL.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Sibscription { get; set; }

        public string Athor { get; set; }

        public int BookCategoryId { get; set; }
        public BookCategory Category { get; set; }
    }
}
