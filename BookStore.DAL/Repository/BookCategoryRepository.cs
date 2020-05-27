using BookStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace BookStore.DAL.Repository
{
    public class BookCategoryRepository : IBookStoreRepository<BookCategory>
    {

        private MemoryCache memoryCache;
        private DateTimeOffset offset;

        public BookCategoryRepository()
        {
            memoryCache = MemoryCache.Default;
            offset = DateTime.Now.AddMinutes(10);
            InitialazeCategories();
        }

        private void InitialazeCategories()
        {
            List<BookCategory> items = GetAll().ToList();
            if(items.Count == 0)
            {
                List<BookCategory> bookCategories = new List<BookCategory>
                {
                    new BookCategory
                    {
                        Name = "Проза"
                    },
                    new BookCategory
                    {
                        Name = "Фентезі"
                    },
                    new BookCategory
                    {
                        Name = "Фантастика"
                    },
                    new BookCategory
                    {
                        Name = "Детектив"
                    }
                };

                foreach (BookCategory item in bookCategories)
                {
                    AddBookCategory(item);
                }
            }            
        }

        public int Create(BookCategory item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public BookCategory Get(int id)
        {
            return GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<BookCategory> GetAll()
        {
            List<BookCategory> items = new List<BookCategory> ();

            if (memoryCache.Contains("BooksCategory"))
            {
                items = memoryCache.Get("BooksCategory") as List<BookCategory>;
            }
            return items;
        }

        private void AddBookCategory(BookCategory item)
        {
            List<BookCategory> items = GetAll().ToList();
            if (items.Count == 0)
            {
                item.Id = 1;
            }
            else
            {
                BookCategory lastBook = items[items.Count - 1];
                item.Id = lastBook.Id + 1;
            }

            items.Add(item);
            memoryCache.Set("BooksCategory", items, offset);
        }
    }
}
