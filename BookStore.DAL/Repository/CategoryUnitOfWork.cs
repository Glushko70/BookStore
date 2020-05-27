using BookStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.DAL.Repository
{
    public class CategoryUnitOfWork : IUnitOfWork<BookCategory>
    {
        private IBookStoreRepository<BookCategory> categoryRepository;
        public IBookStoreRepository<BookCategory> GetRepository()
        {
            if(categoryRepository == null)
            {
                categoryRepository = new BookCategoryRepository();
            }

            return categoryRepository;
        }
    }
}
