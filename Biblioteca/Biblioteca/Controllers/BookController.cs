using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca.DataAccess;
using System.Data.Entity;

namespace Biblioteca.Controllers
{
    public class BookController : Controller
    {
        // GET: Book

        private BookDbContext db = new BookDbContext();
        private AuthorDbContext adb = new AuthorDbContext();
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

        public ActionResult AddBook(AddBook model)
        {
            var book = new Biblioteca.Models.Book
            {
                Name = model.Name,
                ISBN = model.ISBN,
                ReleaseDate = model.ReleaseDate,
                OnLoan = model.OnLoan,
                NrCopies = model.NrCopies,
                ShelfID=model.ShelfID
                
                
            };
            Book Book = new Biblioteca.Book
            {
                name = book.Name,
                ISBN = book.ISBN,
                release_date = book.ReleaseDate,
                nr_copies=book.NrCopies,
                on_loan=book.OnLoan,
                shelf_id=book.ShelfID
                
            };

            var author = new Biblioteca.Models.Author
            {
                
                 FirstName= model.FirstName,
                LastName = model.LastName
                
            };
            Author auth = new Author
            {
                first_name = author.FirstName,
                last_name = author.LastName

            };

            using (var aut = new AuthorDbContext())
            {
                aut.authors.Add(auth);

            }

            using (var bk = new BookDbContext())
            {
                bk.Books.Add(Book);
            }

            if (ModelState.IsValid)
            {
                db.Books.Add(Book);
                adb.authors.Add(auth);
                adb.SaveChanges();
                db.SaveChanges();
                    return RedirectToAction("Index");
            }

            return View();
        }
        
        public ActionResult Edit(int id=1 ,int aid=1)
        {
            Book book = db.Books.Find(id);
            Author auth = adb.authors.Find(aid);
            if (book == null)
            {
                return HttpNotFound();
            }

            AddBook edit = new Biblioteca.Models.AddBook
            {
                ISBN = book.ISBN,
                Name = book.name,
                ReleaseDate = book.release_date,
                OnLoan = book.on_loan,
                NrCopies = book.nr_copies
            };
                
                         
            return View(edit);
           
        }
        [HttpPost]
        public ActionResult Edit (Book book,Author auth)
        {
            
            db.Books.
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.Entry(auth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //AddBook edit = new AddBook
            //{
            //    FirstName=auth.first_name,
            //    LastName = auth.last_name,

            //    ISBN = book.ISBN,
            //    Name= book.name,
            //    ReleaseDate = book.release_date,
            //    OnLoan = book.on_loan,
            //    NrCopies = book.nr_copies
            //};
            return View();
        }
    }
}