using BookStore.DAL.Models;
using BookStore.DAL.Repository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private IBookStoreRepository<Book> bookRepo;
        private IBookStoreRepository<BookCategory> categoryRepo;

        public HomeController(IUnitOfWork<Book> bookRepository,
            IUnitOfWork<BookCategory> categoryRepository)
        {        
            bookRepo = bookRepository.GetRepository();
            categoryRepo = categoryRepository.GetRepository();
        }

        public ActionResult Index()
        {
            List<BookCategory> categories = categoryRepo.GetAll().ToList();
            List<Book> books = bookRepo.GetAll().ToList();
            ListBookViewModel model = new ListBookViewModel(books, categories);
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            List<BookCategory> categories = categoryRepo.GetAll().ToList();
            BookViewModel model = new BookViewModel(categories);
            return PartialView("_AddBook", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book
                {
                    Name = model.Name,
                    Athor = model.Athor,
                    BookCategoryId = model.BookCategoryId,
                    Sibscription = model.Description

                };
                int id = bookRepo.Create(book);
                return Json(new { key = true, id = id });
            }
            
            return Json(new { key = false, message = "model is not valid" });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if(id > 0)
            {
                bookRepo.Delete(id);
                return Json(new { key = true });
            }

            return Json(new { key = false });
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}