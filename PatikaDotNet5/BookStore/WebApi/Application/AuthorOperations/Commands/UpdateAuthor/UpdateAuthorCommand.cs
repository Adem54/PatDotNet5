

using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor{
    public class UpdateAuthorCommand {
        private readonly IBookStoreDbContext _dbContext;
       
        public int AuthorId {get; set;}
        public UpdateAuthorModel Model {get; set;}
        public UpdateAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }

    public void  Handle(){
          var author=_dbContext.Authors.SingleOrDefault(a=>a.Id==AuthorId);
          if(author is null)throw new InvalidOperationException("Guncellenecek kitap bulunamadi");
          Author auth=new Author();
          author.FirstName=string.IsNullOrEmpty(Model.FirstName) ? author.FirstName : Model.FirstName;   
        //  author.LastName=string.IsNullOrEmpty(Model.FirstName) ? author.LastName : Model.LastName;   
          author.LastName=author.LastName != default ? Model.LastName : author.LastName;  
         // author.BirthDate=string.IsNullOrEmpty(Model.BirthDate.ToString()) ? author.BirthDate : Model.BirthDate;
          author.BirthDate=author.BirthDate !=default ? Model.BirthDate : author.BirthDate;
          _dbContext.SaveChanges();
    }
        

       
        
    }

    public class UpdateAuthorModel {
            public string FirstName {get; set;}
            public string LastName {get; set;}
            public DateTime BirthDate {get; set;}
            
        }
}