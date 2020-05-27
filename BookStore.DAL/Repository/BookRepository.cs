using BookStore.DAL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace BookStore.DAL.Repository
{
    public class BookRepository : IBookStoreRepository<Book>
    {
        private MemoryCache memoryCache;
        private DateTimeOffset offset;

        public BookRepository()
        {
            memoryCache = MemoryCache.Default;
            offset = DateTime.Now.AddDays(1);
        }

        public int Create(Book item)
        {
            return AddBook(item);
        }

        public void Delete(int id)
        {
            List<Book> books = GetAll().ToList();
            Book book = books.Where(x => x.Id == id).FirstOrDefault();
            if(book != null)
            {
                books.Remove(book);
            }

            memoryCache.Set("Books", books, offset);
        }

        public Book Get(int id)
        {
            List<Book> books = GetAll().ToList();
            
            return books.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Book> GetAll()
        {
            List<Book> books = new List<Book>();
            
            if (memoryCache.Contains("Books"))
            {
                books = memoryCache.Get("Books") as List<Book>;
            }
            return books;
        }

        private int AddBook(Book book)
        {
            List<Book> books = GetAll().ToList(); 
            if(books.Count == 0)
            {
                book.Id = 1;
            }
            else
            {
                Book lastBook = books[books.Count - 1];
                book.Id = lastBook.Id + 1;
            }

            books.Add(book);
            memoryCache.Set("Books", books, offset);
            return book.Id;
        }
    }
}
