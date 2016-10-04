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

        private BookDBContext db = new BookDBContext();
        
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
                    authors = b.BookAuthors.toModel()
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
            Biblioteca.DataAccess.Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        public ActionResult AddAuthor()
        {
            return View();
        }

        public ActionResult AddBook(AddBook model)
        {


            Biblioteca.DataAccess.Book Book = new Biblioteca.DataAccess.Book
            {
                name = model.Name,
                ISBN = model.ISBN,
                release_date = model.ReleaseDate,
                nr_copies=model.NrCopies,
                on_loan=model.OnLoan,
                shelf_id=model.ShelfID               
                
            };


            Biblioteca.DataAccess.Author auth = new Biblioteca.DataAccess.Author
            {
                first_name = model.FirstName,
                last_name = model.LastName
                

            };

                db.Books.Add(Book);
                db.Authors.Add(auth);
            
            if (ModelState.IsValid)
            {
                db.Books.Add(Book);
                db.Authors.Add(auth);
                db.SaveChanges();
                    return RedirectToAction("Index");
            }

            return View();
        }
        
        public ActionResult Edit(int id=0 ,int aid = 0)
        {
            Biblioteca.DataAccess.Book book = db.Books.Find(id);
            Biblioteca.DataAccess.Author auth = db.Authors.Find(aid);
            
            if (book == null)
            {
                return HttpNotFound();
            }


            AddBook edit = new Biblioteca.Models.AddBook
            {
                AID = auth.id,
                ID = book.id,
                FirstName = auth.first_name,
                LastName = auth.last_name,
                Name = book.name,
                ReleaseDate = book.release_date,
                OnLoan = book.on_loan,
                ISBN = book.ISBN,
                NrCopies = book.nr_copies,
                ShelfID= book.shelf_id

            };



            return View(edit);
           
        }
        [HttpPost]
        public ActionResult Edit (AddBook edited)
        {
            try
            {
                Biblioteca.DataAccess.Book book = new Biblioteca.DataAccess.Book
                {
                    id = edited.ID,
                    ISBN = edited.ISBN,
                    name = edited.Name,
                    nr_copies = edited.NrCopies,
                    on_loan = edited.OnLoan,
                    release_date = edited.ReleaseDate,
                    shelf_id = edited.ShelfID
                };
                Biblioteca.DataAccess.Author auth = new Biblioteca.DataAccess.Author
                {

                    id = edited.AID,
                    first_name = edited.FirstName,
                    last_name = edited.LastName
                };
                db.Authors.Add(auth);
                db.Books.Add(book);
                if (ModelState.IsValid)
                {
                    db.Entry(book).State = EntityState.Modified;
                    db.Entry(auth).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
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