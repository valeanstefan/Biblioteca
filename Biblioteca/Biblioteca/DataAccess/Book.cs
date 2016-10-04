namespace Biblioteca.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int ISBN { get; set; }

        public DateTime release_date { get; set; }

        public int? on_loan { get; set; }

        public int? nr_copies { get; set; }

        public int shelf_id { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }

        public virtual Shelf Shelf { get; set; }
    }
}
