using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Models;
using Biblioteca.DataAccess;
using System.Net;

namespace Biblioteca.Controllers
{
    public class AuthorController : Controller
    {

        BookDBContext db = new BookDBContext();
        // GET: Author/Create
        public ActionResult Index()
        {
            var AuthorsFromContext = db.Authors.ToList();
            var Authors = new List<Biblioteca.Models.AuthorModel>();
            AuthorsFromContext.ForEach(a => Authors.Add(
                new Biblioteca.Models.AuthorModel
                {
                    ID=a.id,
                    FirstName=a.first_name,
                    LastName = a.last_name

                }
                ));

            return View(Authors);
        }
        public ActionResult AddAuthor()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public ActionResult AddAuthor(AuthorModel author)
        {
            Biblioteca.DataAccess.Author auth = new Author
            {
                first_name=author.FirstName,
                last_name = author.LastName
            };
            try
            {
                if (ModelState.IsValid)
                {
                   
                    db.Authors.Add(auth);
                    db.SaveChanges();
                }
                return Redirect("/Book/");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int ? id)
        {

            Biblioteca.DataAccess.Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }

            AuthorModel edit = new AuthorModel
            {
                ID = author.id,
                FirstName = author.first_name,
                LastName = author.last_name
            };
            
            return View(edit);
        }

        [HttpPost]
        public ActionResult Edit(AuthorModel edited)
        {
            try{
            Author auth = new Author
            {
                id=edited.ID,
                first_name = edited.FirstName,
                last_name = edited.LastName
            };
            db.Authors.Add(auth);
            if (ModelState.IsValid)
            {
                db.Entry(auth).State = System.Data.Entity.EntityState.Modified;
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
            Biblioteca.DataAccess.Author author = db.Authors.Find(id);
            if(author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Biblioteca.DataAccess.Author author = db.Authors.Find(id);
                db.Authors.Remove(author);
                db.SaveChanges();
                
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
