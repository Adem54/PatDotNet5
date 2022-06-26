

using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.GenreOperations.Commands.DeleteGenre{
    public class DeleteGenreCommand {
        private readonly IBookStoreDbContext _dbContext;
        public int GenreId {get; set;}

        public DeleteGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public void Handle (){
            var genre=_dbContext.Genres.SingleOrDefault(genre=>genre.Id==GenreId);
            if(genre is  null)throw new InvalidOperationException("Silinecek kitap turu bulunamadi");
            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }


        
    }
}