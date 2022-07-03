

using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;

using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken {
    public class RefreshTokenCommand
    { 
        private readonly IBookStoreDbContext _dbContext;
       //token uretme islem i old icin IConfiguration a da ihtiyacimz var
        private readonly IConfiguration _configuration;

        public string RefreshToken {get; set;}    
        public RefreshTokenCommand(IBookStoreDbContext dbContext,IConfiguration configuration)
        {
            _dbContext=dbContext;
            _configuration=configuration;
        }

        public Token Handle()
        {
            //Email ve password yerine sanki login den gecmis gibi refreshtoken gelecek burda
            //Bizim ayrica bir de refreshtoken in expiration time ini da kontrol etmemiz gerekiyor
            //user.RefreshTokenExpireDate=token.Expiration.AddMinutes(5);burasi da accesstoken dan sonra 5 dakka olacak
            //Yani ilk accesstoken uretildikten 20 dakika sonra bu accesstoken kulllanilamiyor olmasi gerekiyor
            //u.RefreshTokenExpireDate > DateTime.Now zamaninin gecmemis olmasini bu sekilde kontrol ederiz
            var user=_dbContext.Users.FirstOrDefault(u=>u.RefreshToken==RefreshToken && u.RefreshTokenExpireDate > DateTime.Now);
            if(user is not null)
            {
               TokenHandler handler=new TokenHandler(_configuration);
               Token token=handler.CreateAccessToken(user);
               //Token i da tekrardan guncellememiz gerekiyor bu onemli guncellemezsek
               //refreshtoken in expire time i sona erer ve kullanicinin gonderdigi refreshtoken
               //artik bir ise yaramaz...
               //20 dakikalik sure icinde accesstoken+refreshtoken expiretime lari suresi boyunca yeni 
               //bir accesstoken alinabilir,dolayisi ile bu 20dakka gecmedgidi surece gelip yeni accesstoken alinabilir
               //Kullanici 15 dakka boyunca browser da hicbiryere tiklamadi ise hicbir hareket yapmadi ise
               //browser tabi ki herhangi bir hareket algilayip da refreshtoken requesti gonderip yeni bir accesstoken alayim demez
               //Kullanici ornegin 30 dakka hicbirsey yapmmadi sonra tekrar geldi ve tikladi islem yapmaya calisiyor
               //refreshtoken ile accesstoken alinmaya calisilacak ama refreshtoken expire time i da bittigi icin
               //accesstoken da refresthtoken uzerinden alinamamis oluyor, bu durumda da kullanici yeniden login olmasi gerekir
               //Kullanici uzun sure ekranini acik birakip islem yapmadigi zaman gerceklesen bir olaydir bu
                user.RefreshToken=token.RefreshToken;
                user.RefreshTokenExpireDate=token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;

            }else {
                  //Degil ise 
                throw new InvalidOperationException("Valid bir refresh token bulunamadi!");
            }
           
        }
    }
}