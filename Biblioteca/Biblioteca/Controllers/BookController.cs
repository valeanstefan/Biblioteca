using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca.DataAccess;
using System.Data.Entity;
using Biblioteca.WebExtension;
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
                    Name = b.name,
                    ISBN = b.ISBN,
                    ReleaseDate = b.release_date,
                    ShelfID = b.shelf_id,
                    OnLoan = b.on_loan,
                    NrCopies = b.nr_copies,
                    authors = b.Authors.toModel()
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

                db.Books.Add(Book);
                db.authors.Add(auth);
            
            if (ModelState.IsValid)
            {
                db.Books.Add(Book);
                db.authors.Add(auth);
                db.SaveChanges();
                    return RedirectToAction("Index");
            }

            return View();
        }
        
        public ActionResult Edit(int id=0 ,List<Author> aut=null, int count = 0)
        {
            Book book = db.Books.Find(id);

           // Author auth = db.authors.Find(aid);
            
            if (book == null)
            {
                return HttpNotFound();
            }


           // AddBook edit = new Biblioteca.Models.AddBook
           //{
           //     AID = aut.ElementAt(0).id,
           //     ID = book.id,
           //     FirstName = auth.first_name,
           //     LastName = auth.last_name,
           //     Name = book.name,
           //     ReleaseDate = book.release_date,
           //     OnLoan = book.on_loan,
           //     ISBN = book.ISBN,
           //     NrCopies = book.nr_copies

           // };



            return View();
           
        }
        [HttpPost]
        public ActionResult Edit (AddBook edited)
        {

            Book book = new Book
            {
                ISBN = edited.ISBN,
                name = edited.Name,
                nr_copies = edited.NrCopies,
                on_loan = edited.OnLoan,
                release_date = edited.ReleaseDate
            };
            Author auth = new Author
            {
                first_name = edited.FirstName,
                last_name = edited.LastName
            };
            db.authors.Add(auth);
            db.Books.Add(book);
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