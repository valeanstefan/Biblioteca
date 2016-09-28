using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteca.DataAccess
{
    public class ShelfDbContext:DbContext
    {
        public DbSet<Shelf> Shelfs { get; set; }
        public ShelfDbContext() : base("name=LibraryEntities") { }
    }
}