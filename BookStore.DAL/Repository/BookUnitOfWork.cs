using BookStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.DAL.Repository
{
    public class BookUnitOfWork : IUnitOfWork<Book>
    {
        private IBookStoreRepository<Book> bookRepository;
        public IBookStoreRepository<Book> GetRepository()
        {
            if(bookRepository == null)
            {
                bookRepository = new BookRepository();
            }
            return bookRepository;
        }

    }
}
