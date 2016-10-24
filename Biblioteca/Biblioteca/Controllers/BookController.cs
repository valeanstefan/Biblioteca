﻿using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca.DataAccess;
using System.Data.Entity;
using Biblioteca.WebExtension;
using Biblioteca.ViewModels;

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
        
        public ActionResult Create()
        {
            var Book = new Book();
            var AllAuthorsFromContext = from a in db.Authors
                          select a;
            BookAuthorViewModel viewModel = new BookAuthorViewModel
            {
                Authors = AllAuthorsFromContext.ToList(),
                Book = Book,
                SelectedAuthors = new List<int>()

            };

            return View(viewModel);
            
        }
        [HttpPost]
        public ActionResult Create(BookAuthorViewModel model)
        {
            Book book = model.Book;
           

            if (model.SelectedAuthors != null)
            {
                foreach (var AuthorID in model.SelectedAuthors)
                {
                    Author author = db.Authors.Where(t => t.id == AuthorID).First();
                    book.BookAuthors.Add(new BookAuthor { author_id=author.id,book_id=book.id});
                }
            }
            db.Books.Add(book);
            db.SaveChanges();

            return RedirectToAction("Index");
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

        public ActionResult Edit(int id=1)
        {
            Biblioteca.DataAccess.Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            List<Author> auth = new List<Author>();
            foreach(var author in book.BookAuthors)
            {
                auth.Add(author.Author);
            }


            BookAuthorViewModel edit = new BookAuthorViewModel
            {

                Book = book,
                Authors = auth
            };
            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit (BookAuthorViewModel edited)
        {
            try
            {
                var baid = Request.Params.GetValues("id");
                List<BookAuthor> bookAuthors = new List<BookAuthor>();
                //BookAuthor newOne = new BookAuthor
                //{
                //    author_id = int.Parse(baid.First()),book_id=edited.Book.id
                //};
                 
                if(baid!=null)
               foreach(var id in baid)
                {
                        bookAuthors.Add(new BookAuthor {
                            
                        author_id=int.Parse(id),
                         book_id = edited.Book.id

                    });
                }
                Biblioteca.DataAccess.Book book = new Biblioteca.DataAccess.Book
                {
                    id = edited.Book.id,
                    ISBN = edited.Book.ISBN,
                    name = edited.Book.name,
                    nr_copies = edited.Book.nr_copies,
                    on_loan = edited.Book.on_loan,
                    release_date = edited.Book.release_date,
                    shelf_id =edited.Book.shelf_id
                };
               
                db.Books.Add(book);
                
                if (ModelState.IsValid)
                {
                    if(bookAuthors!=null)
                    foreach (var ba in bookAuthors)
                    {
                        db.BookAuthors.Add(ba);
                    }
                   // List<BookAuthor> toDelete = new List<BookAuthor>();
                    //foreach (var id in edited.DeletedAuthors)
                    //{
                    //    BookAuthor newBA = db.BookAuthors.Where(ba=>ba.author_id==id&&ba.book_id==edited.Book.id).First();
                    //    toDelete.Add(newBA);
                    //}
                    //foreach(var ba in toDelete)
                    //{
                    //    db.BookAuthors.Remove(ba);
                    //}
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
   

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var AuthorsFromContext = db.Authors;
            var authors = (from author in AuthorsFromContext
                             where author.first_name.StartsWith(prefix)
                             select new
                             {
                                 label = author.first_name+" "+author.last_name,
                                 val = author.id,
                                 author=author
                             }).ToList();

            return Json(authors);
        }

        [HttpPost]
        public ActionResult DeleteBookAuthor(int bid,List<int> aid)
        {            
            if (ModelState.IsValid)
            {
                Book book = db.Books.Find(bid);
                foreach(var id in aid) { 
                BookAuthor baObj = db.BookAuthors.
                    Where(ba => ba.author_id ==id
                        && ba.book_id == bid
                    ).FirstOrDefault();
                db.BookAuthors.Remove(baObj);
                }
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            return View();

        }
        //public ActionResult PopulateAuthorToDelete(int bid,int aid)
        //{
        //    Book book = db.Books.Find(bid);
        //    List<BookAuthor> baObj = new List<BookAuthor>();

        //    baObj.Add(db.BookAuthors.
        //           Where(ba => ba.author_id == aid
        //               && ba.book_id == bid
        //           ).FirstOrDefault();
                   
            
        //    return View("Edit",baObj);
        //}

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