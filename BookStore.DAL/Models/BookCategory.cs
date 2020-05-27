

using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Models
{
    public class BookCategory
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }
    }
}
