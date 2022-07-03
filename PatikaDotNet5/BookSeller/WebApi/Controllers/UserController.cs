


using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;
using WebApi.UserOperations.Commands.CreateToken;
using WebApi.UserOperations.Commands.CreateUser;
using static WebApi.UserOperations.Commands.CreateToken.CreateTokenCommand;
using static WebApi.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.Controllers{

    [ApiController]//class imzin httpresponse donecegini soyluyoruz burda
    [Route("api/[controller]s")]
    
    public class UserController:ControllerBase {

          private readonly BookSellerDbContext _context;
          private readonly IConfiguration _configuration;
          public UserController(BookSellerDbContext context,IConfiguration configuration)
          {
            _context=context;
            _configuration=configuration;
          }

          [HttpPost]

          public IActionResult Register([FromBody] CreateUserModel newUser){
            CreateUserCommand command=new CreateUserCommand(_context);
            command.Model=newUser;
            // CreateUserCommandValidator validator=new CreateUserCommandValidator();
            // validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
          }

          [HttpPost("login")]

          public ActionResult<Token> Login([FromBody] CreateTokenModel login)
          {
                CreateTokenCommand command=new CreateTokenCommand(_context,_configuration);
                command.Model=login;
                Token token=command.Handle();
                return Ok(token);
          } 

     }
     }