using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
     public class GetAuthorDetailQuery {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public int AuthorId {get; set;}
        public GetAuthorDetailQuery(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper=mapper;
        }

        public AuthorDetailViewModel Handle(){
            var author=_dbContext.Authors.SingleOrDefault(a=>a.Id==AuthorId);
            if(author==null)throw new InvalidOperationException("Yazar bulunamadi");
            AuthorDetailViewModel authorDetailVModel=_mapper.Map<AuthorDetailViewModel>(author);
            return authorDetailVModel;
        }

        public class AuthorDetailViewModel {
                    public string FirstName { get; set; }
                    public string LastName { get; set; }
                    public string BirthDate {get; set;}
                
            }

        

      

    }
}



