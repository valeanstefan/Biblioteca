using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Models;
using Biblioteca.DataAccess;

namespace Biblioteca.Controllers
{
    public class AuthorController : Controller
    {

        BookDBContext db = new BookDBContext();
        // GET: Author/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public ActionResult Create(AuthorModel author)
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
                    // TODO: Add insert logic here
                    db.Authors.Add(auth);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Author/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, AuthorModel author)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
