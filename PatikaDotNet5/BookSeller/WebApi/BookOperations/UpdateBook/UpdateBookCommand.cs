

using WebApi.DbOperations;

namespace WebApi.BookOperations.UpdateBook{
    public class UpdateBookCommand{

        private readonly BookSellerDbContext _dbContext;

        public UpdateBookCommand(BookSellerDbContext dbContext)
        {
            
        }
    }
}