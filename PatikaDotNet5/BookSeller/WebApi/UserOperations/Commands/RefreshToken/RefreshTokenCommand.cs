
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.UserOperations.Commands.RefreshToken {
    public class RefreshTokenCommand 
    {
        private readonly BookSellerDbContext _dbContext;
        private readonly IConfiguration _configuration;    
        public string RefreshToken {get; set;}

        public RefreshTokenCommand(BookSellerDbContext dbContext,IConfiguration configuration)
        {
            _dbContext=dbContext;
            _configuration=configuration;
        }

        public Token Handle(){
            //Gonderilen refreshtoken eger, bizdeki ile ayni ve refreshtoken suresi dolmamis ise
            User user=_dbContext.Users.SingleOrDefault(x=>x.RefreshToken==RefreshToken && x.RefreshTokenExpireDate > System.DateTime.Now);
            if(user is not null){
                TokenHandler handler=new TokenHandler(_configuration);
                Token newToken=new Token();
                newToken=handler.CreateAccessToken();
                //Refreshtoken i kontrol edip yeni bir token gonderdikten sonra
                //refreshtoken i da user a gondermemiz gerekiyor ki bir sonraki
                //refreshtoken endpointinde yeni uretlen refreshtoken ile
                //gelecek kullanici ve biz de olmasi gerekiyor ki dogru refrehstoken 
                //ile mi geldgni anlayabilelim
                user.RefreshToken=RefreshToken;
                user.RefreshTokenExpireDate=newToken.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                return newToken;
            }else {
                throw new InvalidOperationException("Valid bir Refresh token bulunamadi");
            }
        }
    }
}