

using Microsoft.EntityFrameworkCore;

namespace WebApi.DbOperations{

    public class BookSellerDbContext:DbContext{

        public BookSellerDbContext(DbContextOptions<BookSellerDbContext> options):base(options)
        {

        }

          public  DbSet<Book> Books {get; set;}

    }
}