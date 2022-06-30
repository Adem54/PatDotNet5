

using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail {
    public class GetGenreDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            _mapper=testFixture.Mapper;
        }

//Ilk olarak, database de olmayan bir id gonderildiginde hata verme islmeni bizim uygulamda
//yazdigmiz gibi verip vermedigni test ederiz....
        [Fact]
         public void WhenNonExistentGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            Genre genre = new Genre() {Name="Real-life" };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId = 12;
            FluentActions.Invoking(() => query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap turu bulunamadi");

        }

      //Simdi de id database de olan bir id gonderildiginde, ve Handle metodu problemsiz calistginda
      //ne olmasi bekleniyorsa o olmasi beklenenin olup olmadigni test ederiz...  

          [Fact]
        public void When_ExitsGenreIdIsGiven_GenreViewModel_ShoulBeReturn()
        {
           GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
           Genre genre=new Genre(){Name="Historical"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            query.GenreId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();
            var genreObj = _context.Genres.SingleOrDefault(g => g.Id == query.GenreId);
            genreObj.Should().NotBeNull();

          
           

        }
        
    }
}

