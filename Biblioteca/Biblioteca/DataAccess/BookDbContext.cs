using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteca.DataAccess
{
    public class BookDbContext:DbContext
    {
        
            public DbSet<Book> Books { get; set; }
            public BookDbContext() : base("name=LibraryEntities")
            { }
        
    }
}