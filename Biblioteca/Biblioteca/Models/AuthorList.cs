using Biblioteca.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Models
{
    public class AuthorList
    {
        BookDBContext db = new BookDBContext();
        private MultiSelectList GetAuthors(string[] selectedValues)
        {

            List<Author> Authors = db.Authors.ToList();
            Authors.ForEach(a => a.first_name = a.first_name + " " + a.last_name);


            return new MultiSelectList(Authors, "id", "first_name", selectedValues);

        }
    }
}