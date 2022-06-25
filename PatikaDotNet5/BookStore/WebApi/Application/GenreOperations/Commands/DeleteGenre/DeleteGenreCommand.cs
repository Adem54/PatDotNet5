

using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.GenreOperations.Commands.DeleteGenre{
    public class DeleteGenreCommand {
        private readonly BookStoreDbContext _dbContext;
        public int GenreId {get; set;}

        public DeleteGenreCommand(BookStoreDbContext dbContext)
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