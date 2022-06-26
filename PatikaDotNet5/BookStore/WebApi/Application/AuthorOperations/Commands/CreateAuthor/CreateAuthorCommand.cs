

using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor {
        public class CreateAuthorCommand {
            private readonly IBookStoreDbContext _dbContext;
            private readonly IMapper _mapper;
            public CreateAuthorModel Model { get; set; }
            
            
        public CreateAuthorCommand(IBookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper=mapper;
        }

        public void Handle(){
            var author=_dbContext.Authors.SingleOrDefault(a=>a.FirstName==Model.FirstName && a.LastName == Model.LastName);
            if(author!=null)throw new InvalidOperationException("Ayni isimde yazar mevcut");
            Author result=_mapper.Map<Author>(Model);
            _dbContext.Authors.Add(result);
            _dbContext.SaveChanges();
        } 
            
      
        }

        public class CreateAuthorModel 
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate {get; set;}
        
        }
}