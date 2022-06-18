using System;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.DeleteBook{
    public class DeleteBookCommand{

        public int BookId {get; set;}
       
        private readonly BookStoreDbContext _dbContext;

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public void Handle()
        {
            var book=_dbContext.Books.SingleOrDefault(book=>book.Id==BookId);
            if(book is null)throw new InvalidOperationException("Silinecek kitap bulunamadi");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
    }