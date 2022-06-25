
using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail {
    public class GetGenreDetailQuery{
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int GenreId {get; set;}


        public GetGenreDetailQuery(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper=mapper;
        }

        public GenreDetailViewModel Handle(){
            var genre=_dbContext.Genres.SingleOrDefault(genre=>genre.IsActive && genre.Id==GenreId);
            if(genre is null)throw new InvalidOperationException("Kitap turu bulunamadi");
            GenreDetailViewModel genreDetailVModel=_mapper.Map<GenreDetailViewModel>(genre);
            return genreDetailVModel;
        }


        public class GenreDetailViewModel {
            public int Id {get; set;}
            public string Name {get; set;}
        }

    }
}