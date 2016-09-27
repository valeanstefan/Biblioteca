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
    }

    public class AddBook
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "ISBN")]
        public int ISBN { get; set; }
        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }



    }
 
} 