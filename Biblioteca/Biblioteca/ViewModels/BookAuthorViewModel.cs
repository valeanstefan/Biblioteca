using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biblioteca.Models;
using System.Web.Mvc;
using Biblioteca.DataAccess;

namespace Biblioteca.ViewModels
{
    public class BookAuthorViewModel
    {
        public List<Author> Authors;
        public Book Book { get; set; }
        public List<int> SelectedAuthors { get; set; }
        public List<int> DeletedAuthors { get; set; }
   

    }
}