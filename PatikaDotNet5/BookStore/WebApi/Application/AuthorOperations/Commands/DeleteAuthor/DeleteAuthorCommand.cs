
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor {
    public class DeleteAuthorCommand {
        private readonly IBookStoreDbContext _dbContext;
        public int AuthorId {get; set;}
        public DeleteAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            //Gonderilen id ye sahip bir yazar var mi ona bakariz
            var author=_dbContext.Authors.SingleOrDefault(a=>a.Id==AuthorId);
            if(author is null)throw new InvalidOperationException("Silinecek yazar bulunamadi");
            //Sonra gonderilen id deki yazarin kitbi var mi  ona bakariz...
            var book=_dbContext.Books.Include(x=>x.Author).SingleOrDefault(b=>b.AuthorId==AuthorId && b.PublishDate < DateTime.Now);
            if(book is not null)throw new InvalidOperationException("Yayinda kitabi olan bir yazari silemezsiniz...");
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();

        }
    }
}