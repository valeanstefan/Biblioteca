using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.WebExtension
{
    using MA = Biblioteca.Models.Author;
    public static class AuthorExtensions
    {
        public static List<MA> toModel(this ICollection<Author> authors)
        {
            List<MA> ModelAuthors = new List<MA>();
            foreach(var BA in authors)
            {
                ModelAuthors.Add(new MA { FirstName = BA.first_name, LastName = BA.last_name, ID=BA.id });
            }
            return ModelAuthors;
        }
    }
}