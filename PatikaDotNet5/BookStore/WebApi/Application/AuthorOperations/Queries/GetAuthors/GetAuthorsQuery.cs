


using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors {
    public class GetAuthorsQuery {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext=dbContext;
            _mapper = mapper;
            
        }

        public List<AuthorsViewModel> Handle(){
            var authorList=_dbContext.Authors.OrderBy(x=>x.Id).ToList();
            //Bu listeyi yani Autor entithy sini AuthorViewModel e mapleyecegiz
            List<AuthorsViewModel> authorVModel=_mapper.Map<List<AuthorsViewModel>>(authorList);
            return authorVModel;

        }


        public class AuthorsViewModel {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string BirthDate {get; set;}
        
    }

    }
}