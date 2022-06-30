
using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre {
    public class CreateGenreCommand {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateGenreModel Model {get; set;}

        public CreateGenreCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper=mapper;
        }

        public void Handle()
        {
            var genre=_dbContext.Genres.SingleOrDefault(genre=>genre.Name==Model.Name);
            if(genre is not null) throw new InvalidOperationException("Kitap turu zaten mevcut");
            //  genre=_mapper.Map<Genre>(Model);
            genre=new Genre();
            genre.Name=Model.Name;
            //genre.isActive e default olarak true degeri old icin ona tekrr birsey atamaya gerek yok
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();

        }

        public class CreateGenreModel {
                public string Name { get; set; }
    
        }
    }
}