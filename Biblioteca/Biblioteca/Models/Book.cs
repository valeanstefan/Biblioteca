using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Book
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int ISBN { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? OnLoan { get; set; }
        public int? NrCopies { get; set; }
        public int ShelfID { get; set; }
        public List<Author> authors { get; set; }
    }
    public class Author
    {
        public int ID { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
    }

    public class AddBook
    {
        
        public int ID { get; set; }
        public int AID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "ISBN")]
        public int ISBN { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "ShelfID")]
        public int ShelfID { get; set; }

        [Required]
        [Display(Name = "OnLoan")]
        public int? OnLoan { get; set; }

        [Required]
        [Display(Name = "Nr. Copies")]
        public int? NrCopies { get; set; }
    }
 
} 