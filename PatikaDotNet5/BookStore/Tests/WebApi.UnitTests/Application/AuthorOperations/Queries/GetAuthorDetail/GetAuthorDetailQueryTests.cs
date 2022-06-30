
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail {
    public class GetAuthorDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            _mapper=testFixture.Mapper;
        }

//Ilk olarak, database de olmayan bir id gonderildiginde hata verme islmeni bizim uygulamda
//yazdigmiz gibi verip vermedigni test ederiz....
        [Fact]
         public void WhenNonExistentAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
             var author = new Author() { FirstName = "Palermo", LastName="Geovanni", BirthDate = new System.DateTime(1990, 01, 10) };
            _context.Authors.Add(author);
            _context.SaveChanges();
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId = 12;
            FluentActions.Invoking(() => query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadi");

        }

      //Simdi de id database de olan bir id gonderildiginde, ve Handle metodu problemsiz calistginda
      //ne olmasi bekleniyorsa o olmasi beklenenin olup olmadigni test ederiz...  

          [Fact]
        public void When_ExitsAuthorIdIsGiven_AuthorViewModel_ShoulBeReturn()
        {
            var author = new Author() { FirstName = "Palermo", LastName="Geovanni", BirthDate = new System.DateTime(1990, 01, 10) };
            _context.Authors.Add(author);
            _context.SaveChanges();
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId = 1;
            FluentActions.Invoking(() => query.Handle()).Invoke();
            var authorObj = _context.Authors.SingleOrDefault(a => a.Id == query.AuthorId);
            authorObj.Should().NotBeNull();
        }
        
    }
}

