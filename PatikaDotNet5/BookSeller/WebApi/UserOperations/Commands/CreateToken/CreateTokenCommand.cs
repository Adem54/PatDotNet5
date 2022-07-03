
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.UserOperations.Commands.CreateToken {
    public class CreateTokenCommand 
    {
        private readonly BookSellerDbContext _dbContext;
        private readonly IConfiguration _configuration;  
        public CreateTokenModel Model {get;set;} 
        public CreateTokenCommand(BookSellerDbContext dbContext,IConfiguration configuration)
        {
            _dbContext=dbContext;
            _configuration=configuration;
        } 

        public Token Handle(){
            var user=_dbContext.Users.FirstOrDefault(x=>x.Email==Model.Email && x.Password==Model.Password);
            if(user is not null){
                //Burda token i vermemiz lazm kullanciiya cunku kullanici bilgileri databse de var kullanici daha onceden kayitli
                Token token=new Token();
                TokenHandler handler=new TokenHandler(_configuration);
               token= handler.CreateAccessToken();
               user.RefreshToken=token.RefreshToken;
               user.RefreshTokenExpireDate=token.Expiration.AddMinutes(5);
               _dbContext.SaveChanges();
               return token;
               
            }else {
                throw new InvalidOperationException("Username - Password is wrong");
            }
        }
        public class CreateTokenModel 
        {
            public string Email  {get; set;}
            public string Password {get;set;}
        }
    }
}