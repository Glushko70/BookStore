using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.DAL.Repository;
using BookStore.DAL.Models;

namespace BookStore.App_Start
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork<Book>>().To<BookUnitOfWork>();
            Bind<IUnitOfWork<BookCategory>>().To<CategoryUnitOfWork>();
        }
    }
}