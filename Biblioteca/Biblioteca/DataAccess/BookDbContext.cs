namespace Biblioteca.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BookDBContext : DbContext
    {
        public BookDBContext()
            : base("name=Biblioteca1")
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Shelf> Shelfs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(e => e.BookAuthors)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.author_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.BookAuthors)
                .WithRequired(e => e.Book)
                .HasForeignKey(e => e.book_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shelf>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Shelf)
                .HasForeignKey(e => e.shelf_id);
        }

        public System.Data.Entity.DbSet<Biblioteca.Models.AuthorModel> AuthorModels { get; set; }
    }
}
