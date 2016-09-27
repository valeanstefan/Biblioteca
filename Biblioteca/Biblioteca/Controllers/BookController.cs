using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca.DataAccess;

namespace Biblioteca.Controllers
{
    public class BookController : Controller
    {
        // GET: Book

        private BookDbContext db = new BookDbContext();
        public ActionResult Index()
        {
            var BooksFromContext = db.Books.ToList();

            var Books = new List<Biblioteca.Models.Book>();

            BooksFromContext.ForEach(b => Books.Add(
                new Biblioteca.Models.Book
                {
                ID = b.id,
                Name=b.name,
                ISBN=b.ISBN,
                ReleaseDate=b.release_date,
                ShelfID=b.shelf_id,
                OnLoan=b.on_loan,
                NrCopies=b.nr_copies
                }
                ));

            return View(Books);
        }

        public ActionResult Details(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult AddBook()
        {
           var book = new Biblioteca.Models.AddBook();
            return View(book);
        }
    }
}