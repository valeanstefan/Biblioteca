using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteca.DataAccess
{
    public class AuthorDbContext: DbContext
    {
        public DbSet<Author> authors { get; set; }
        public AuthorDbContext() : base("name=LibraryEntities") { }
        
    }
}