using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.DAL.Repository
{
    public interface IBookStoreRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        int Create(T item);       
        void Delete(int id);
    }
}
