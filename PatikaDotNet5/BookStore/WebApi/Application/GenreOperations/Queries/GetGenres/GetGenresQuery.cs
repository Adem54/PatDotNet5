
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenres{

    public class GetGenresQuery{
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext=dbContext;
            _mapper=mapper;
        }

        public List<GenresViewModel> Handle(){
            //isActive i true olan genre leri getirecegiz..
            var genreList= _dbContext.Genres.Where(genre=>genre.IsActive==true).OrderBy(x=>x.Id).ToList();
            List<GenresViewModel> genreVmodel=_mapper.Map<List<GenresViewModel>>(genreList);
            return genreVmodel;
        }

  // foreach (var genre in result)
            // {
            //     vm.Add(
            //         new GenresViewModel()
            //         {
            //             Name=genre.Name,
            //         }
            //     );
            // }

//Hic usenmden cok kucuk view modeller icin bile view modeller olusturmaliyiz
//Burda id yi almamiz cok onemlidir cunku, id uzerinden biz DTO  yapip, o dto
//yu kulaniciya gostermek isteyebiliriz...Book-Genre den olusan bir dto class
//olusturup onu kullaniciya gostermek isteyebilirz

        public class GenresViewModel{
            public int Id {get; set;}
            public string Name {get; set;}
        }

    }
}