using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biblioteca.Models;

namespace Biblioteca.ViewModels
{
    public class BookAuthorViewModel
    {
        List<AuthorModel> authors { get; set; }
        public int ID { get; set; }
        public String Name { get; set; }
        public int ISBN { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? OnLoan { get; set; }
        public int? NrCopies { get; set; }
        public int ShelfID { get; set; }

    }
}