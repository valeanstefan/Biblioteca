namespace Biblioteca.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BookAuthor
    {
        public int author_id { get; set; }

        public int book_id { get; set; }

        [Key]
        public int BookAuthorsID { get; set; }

        public virtual Author Author { get; set; }

        public virtual Book Book { get; set; }
    }
}
