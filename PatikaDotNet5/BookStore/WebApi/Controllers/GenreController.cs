
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateGenre;
using WebApi.Application.BookOperations.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;
using static WebApi.Application.BookOperations.Commands.CreateGenre.CreateGenreCommand;

namespace WebApi.Controllers {


    [ApiController]
    [Route("api/[controller]s")]

    public class GenreController:ControllerBase {

     private readonly IBookStoreDbContext _dbContext;
     private readonly IMapper _mapper;   
     public GenreController(IBookStoreDbContext dbContext,IMapper mapper){
        _dbContext=dbContext;
        _mapper=mapper;
     }


     [HttpGet]
     
     public IActionResult GetGenres(){
        GetGenresQuery query=new GetGenresQuery(_dbContext,_mapper);
        var result=query.Handle();
        return Ok(result);
       
     }

     [HttpGet("{id}")]

     public IActionResult GetGenreDetail(int id){
        GetGenreDetailQuery query=new GetGenreDetailQuery(_dbContext,_mapper);
        query.GenreId = id;
        GetGenreDetailQueryValidator validator=new GetGenreDetailQueryValidator();
        validator.ValidateAndThrow(query);
        var result=query.Handle();
        return Ok(result);
        
     }
 
    [HttpPost]

    public IActionResult AddGenre([FromBody] CreateGenreModel genreModel)
    {
        CreateGenreCommand command=new CreateGenreCommand(_dbContext,_mapper);
        command.Model=genreModel;
        CreateGenreCommandValidator validator=new CreateGenreCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
        
    }

   [HttpPut("{id}")]
   public IActionResult UpdateGenre(int id, [FromBody]UpdateGenreModel updatedGenre){
    UpdateGenreCommand command=new UpdateGenreCommand(_dbContext);
    command.GenreId=id;
    command.Model=updatedGenre;
    UpdateGenreCommandValidator validator=new UpdateGenreCommandValidator();
    validator.ValidateAndThrow(command);
    command.Handle();
    return Ok();
   }

   [HttpDelete("{id}")]

   public IActionResult DeleteGenre(int id){
    DeleteGenreCommand command=new DeleteGenreCommand(_dbContext);
    command.GenreId=id;
    DeleteGenreCommandValidator validator=new DeleteGenreCommandValidator();
    validator.ValidateAndThrow(command);
    command.Handle();
    return Ok();
   }
        
    }

}