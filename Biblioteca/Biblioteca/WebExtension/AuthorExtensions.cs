using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.WebExtension
{
    public static class AuthorExtensions
    {
        public static Biblioteca.Models.AuthorModel toModel(this Biblioteca.DataAccess.BookAuthor bookAuthor)
        {
            if (bookAuthor == null)
            {
                return null;
            }

            Biblioteca.Models.AuthorModel author = new Biblioteca.Models.AuthorModel
            {
                FirstName = bookAuthor.Author.first_name,
                LastName = bookAuthor.Author.last_name,
                ID = bookAuthor.Author.id
            };

            return author;
        }

        public static List<Biblioteca.Models.AuthorModel> toModel(this ICollection<Biblioteca.DataAccess.BookAuthor> authors)
        {
            List<Biblioteca.Models.AuthorModel> ModelAuthors = new List<Biblioteca.Models.AuthorModel>();
            foreach(Biblioteca.DataAccess.BookAuthor BA in authors)
            {
                ModelAuthors.Add(BA.toModel());
            }
            return ModelAuthors;
        }
    }
}