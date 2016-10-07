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

            var Books = new List<Biblioteca.Models.BookModel>();

            BooksFromContext.ForEach(b => Books.Add(
                new Biblioteca.Models.BookModel
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

        private MultiSelectList GetAuthors(string[] selectedValues)
        {

            List<Author> Authors = db.Authors.ToList();
            Authors.ForEach(a => a.first_name = a.first_name +" "+ a.last_name);
       

            return new MultiSelectList(Authors, "id", "first_name", selectedValues);

        }

        public ActionResult AddBook(BookModel model)
        {

            ViewBag.Authors = GetAuthors(null);
          //  List<Author> authors = db.Authors.Find()
            Biblioteca.DataAccess.Book Book = new Biblioteca.DataAccess.Book
            {
                name = model.Name,
                ISBN = model.ISBN,
                release_date = model.ReleaseDate,
                nr_copies=model.NrCopies,
                on_loan=model.OnLoan,
                shelf_id=model.ShelfID,

               
                              
                
            };
          
            //var ba = new BookAuthor
            //{
            //    author_id = model.ID,
            //    book_id = model.AID
            //};

            db.Books.Add(Book);

//            db.BookAuthors.Add(ba);
            
            if (ModelState.IsValid)
            {
  //              db.BookAuthors.Add(ba);
                //db.Books.Add(Book);

                //db.SaveChanges();
                  //  return RedirectToAction("Index");
            }

            return View();
        }
        public ActionResult AddAuthorsToBook()
        {
            return View();
        }
        
        public ActionResult Edit(int id=1)
        {
            Biblioteca.DataAccess.Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            BookModel edit = new Biblioteca.Models.BookModel
            {
                
                ID = book.id,
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
        public ActionResult Edit (BookModel edited)
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
                db.Books.Add(book);
                if (ModelState.IsValid)
                {
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
        
            return View();
        }
        
     

        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Biblioteca.DataAccess.Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}