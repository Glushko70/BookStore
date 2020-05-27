using BookStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введіть назву книги")]
        [DisplayName("Назва книги")]
        public string Name { get; set; }

        [DisplayName("Короткий опис книги")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введіть автора")]
        [DisplayName("Автор книги")]
        public string Athor { get; set; }

        [Required(ErrorMessage = "Виберіть уатегорію")]
        [DisplayName("Категорія книги")]
        public int BookCategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<SelectListItem> bookCategories;

        public BookViewModel()
        {
            bookCategories = new List<SelectListItem>();
        }

        public BookViewModel(List<BookCategory> bookCategories)
        {
            this.bookCategories = bookCategories.Select(m =>
            new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList();
        }
    }

    public class ListBookViewModel
    {
        public List<BookViewModel> models;

        public ListBookViewModel(List<Book> books, List<BookCategory> bookCategories)
        {
            models = new List<BookViewModel>();
            foreach (Book item in books)
            {
                models.Add(new BookViewModel
                {
                    Name = item.Name,
                    Id = item.Id,
                    Athor = item.Athor,
                    Description = item.Sibscription,
                    CategoryName = bookCategories.Where(x => x.Id == item.BookCategoryId).FirstOrDefault()?.Name
                });
            }
        }


    }
}