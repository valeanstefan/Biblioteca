using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.WebExtension
{
    public static class AuthorExtensions
    {
        public static Biblioteca.Models.Author toModel(this Biblioteca.DataAccess.BookAuthor bookAuthor)
        {
            if (bookAuthor == null)
            {
                return null;
            }

            Biblioteca.Models.Author author = new Biblioteca.Models.Author
            {
                FirstName = bookAuthor.Author.first_name,
                LastName = bookAuthor.Author.last_name,
                ID = bookAuthor.Author.id
            };

            return author;
        }

        public static List<Biblioteca.Models.Author> toModel(this ICollection<Biblioteca.DataAccess.BookAuthor> authors)
        {
            List<Biblioteca.Models.Author> ModelAuthors = new List<Biblioteca.Models.Author>();
            foreach(Biblioteca.DataAccess.BookAuthor BA in authors)
            {
                ModelAuthors.Add(BA.toModel());
            }
            return ModelAuthors;
        }
    }
}