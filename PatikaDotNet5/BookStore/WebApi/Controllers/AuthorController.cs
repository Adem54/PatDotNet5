
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;
using static WebApi.Application.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;
using static WebApi.Application.AuthorOperations.Queries.GetAuthors.GetAuthorsQuery;

namespace WebApi.Controllers {

    [ApiController]
    [Route("api/[controller]s")]

    public class AuthorController:ControllerBase 
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper=mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors(){
            GetAuthorsQuery query=new GetAuthorsQuery(_dbContext,_mapper);
            List<AuthorsViewModel> result=new List<AuthorsViewModel>();
            result=query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorDetail(int id){
            GetAuthorDetailQuery query=new GetAuthorDetailQuery(_dbContext,_mapper);
            query.AuthorId=id;
            GetAuthorDetailQueryValidator validator=new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            AuthorDetailViewModel result=new AuthorDetailViewModel();
            result=query.Handle();
            return Ok(result);
        }

        [HttpPost]

        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor){
            CreateAuthorCommand command=new CreateAuthorCommand(_dbContext,_mapper);
            command.Model=newAuthor;
            CreateAuthorCommandValidator validator=new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
            
        }

        [HttpPut("{id}")]

        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand command=new UpdateAuthorCommand(_dbContext);
            command.AuthorId=id;
            command.Model=updatedAuthor;
            UpdateAuthorCommandValidator validator=new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
              DeleteAuthorCommand command=new DeleteAuthorCommand(_dbContext);
              command.AuthorId=id;
              DeleteAuthorCommandValidator validator=new DeleteAuthorCommandValidator();
              validator.ValidateAndThrow(command);
              command.Handle();
              return Ok();  
        }
    




    }
}