using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.DAL.Repository
{
    public interface IUnitOfWork<T> where T : class
    {
        IBookStoreRepository<T> GetRepository();
    }
}
