
using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;

using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken {
    public class CreateTokenCommand
    { 
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        //token uretme islem i old icin IConfiguration a da ihtiyacimz var
        private readonly IConfiguration _configuration;

        public CreateTokenModel Model {get; set;}    
        public CreateTokenCommand(IBookStoreDbContext dbContext,IConfiguration configuration,IMapper mapper)
        {
            _dbContext=dbContext;
            _mapper=mapper;
            _configuration=configuration;
        }

        public Token Handle()
        {
            //Burasi bizim icin create token ureten bir endpoint olacak
            //Ilk once gondeirilen user benim database imde var mi onu bir check etmeliyim
            //Email uniq old icin onun uzerinden cek edebilirim
            //Cunku eger login olmak icin gonderilen user bilgilerine sahip olan
            //bir data veritabaninda yok ise demekki bu kullanici daha once retgister olmamis
            //O zaman token uretemeyiz biz bu kullaniciya, kullanici once register olmalidir...
            var user=_dbContext.Users.FirstOrDefault(u=>u.Email==Model.Email && u.Password == Model.Password);
            if(user is not null)
            {
                //Token uret
                //Burda bizim bir token olusturma methoduna ihtiyacimiz var ondan dolayi ayri bir folder altinda
                //token olusturma islemlerini tutacagimiz bir folder olusturacagiz...WebApi altinda TokenOperations
                //folder i olustururuz ki baska yerlerden de erisilsin,ayri class lar icinde tutarak reusable yapacagiz
                //daha temiz kod yazmis olacagiz...
                //Token imizi TokenOperations klasorunde olusturduk ve artik burda kullanabiliriz...
                TokenHandler handler=new TokenHandler(_configuration);
                Token token=handler.CreateAccessToken(user);
                user.RefreshToken=token.RefreshToken;
                user.RefreshTokenExpireDate=token.Expiration.AddMinutes(5);
                //refreshtokenexpiredate de normal expiration dan 5 dakka daha fazla 
                //sure veriyoruz ki o sure de de, gelip sizden bir accesstoken alabilecek
                //kadar yeterli zamandir aslinda. Cunku bu sure de gectikten sonra bu
                //kullanilan refreshtoken expire olacak,yani gecersiz olacak dolayisi ile
                //refresh token ile accesstoken i almaya geldigm zaman da benim mevcut refresh
                //token i guncellemem gerekiyor ya da bunu cok uzun tutuyor olmam gerekiyor
                //Cunku ayni refresh token ile ben su anda totalde 20dakkadan daha fazla istekde
                //bulunamiyoruz
                //Refreshtoken u yazacagimiz endpointte bunu biraz daha acacagiz
                //Simdi artik yaptiklarimizi database e kaydedelim
                _dbContext.SaveChanges();
                return token;

            }else {
                  //Degil ise 
                throw new InvalidOperationException("Kullanici Adi - Password is wrong");
            }
           
        }

//Biz Login isleminde kullanicidan Email ve Password bekliyoruz...
//Login islemi sonucunda kullanicinin kimsligi dogrulandiginda-Authentication islemi ile
//response olarak bir token gonderiliyor...ve artik kullanici api de giris izni veya kimlik dogrulama
//istenen tum resourches ler veya endpointler uzerinden erisilen datalar a gonderilecek requestler
//token ile olmasi gerekiyor erisebilmek icin
        public class CreateTokenModel {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}