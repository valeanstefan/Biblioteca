using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Models
{
    public class BookModel
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int ISBN { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? OnLoan { get; set; }
        public int? NrCopies { get; set; }
        public int ShelfID { get; set; }
        public List<AuthorModel> authors { get; set; }
       
    }
    
    public class AuthorModel
    {
        public int ID { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
    }

    
    public class ShelfModel
    {
        public int ID { get; set; }
        public string Domain { get; set;}
    }

    
 
} 