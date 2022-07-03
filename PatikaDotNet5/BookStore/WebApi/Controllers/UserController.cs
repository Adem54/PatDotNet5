


using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DbOperations;
using WebApi.TokenOperations.Models;
using static WebApi.Application.UserOperations.Commands.CreateToken.CreateTokenCommand;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.Controllers {

    [ApiController]
    [Route("api/[controller]s")]

    public class UserController:ControllerBase 
    { 
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        
        private readonly IConfiguration _configuration;
        //using AutoMapper.Configuration; burdan gelir ve benim config bilgilerine ulasmami saglar
        //Biz uygulama icindeki string olarak tutulan config ayarlarini appsetting de tutuyorduk
        //IConfiguration bizim appsettings altindaki verilere ulasmamizi sagliyor

        public UserController(IBookStoreDbContext context,IConfiguration configuration,IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
            _configuration=configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser){
            CreateUserCommand command=new CreateUserCommand(_context,_mapper);
            command.Model=newUser;
            CreateUserCommandValidator validator=new CreateUserCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

//Ornegin biz token i disardan thirdpart bir kutuphane veya service den aldigmiz zamanlarda
//da bu connect/token cok standart kullanilan bir isimdir....
        [HttpPost("connect/token")]//https://localhost:5001/api/users/connect/token
        //Bu geriye bir token donecek Token isminde bir model olusturacagiz
        //https://localhost:5001/api/users/connect/token bu endpinte istek gonderince body den de
        //login bilgileri bekleyecek bizden,email ve password bilgileri cunku  login islemidir
        //yapilan...
        //Daha once yukardaki Create endpointine yani register islemi yapan bir kullanici ile bu seferde
        //login islemi yapiyoruz-{
    // "email": "zehra5434e@gmail.com",
    // "password": "Zehra5434@"
    //bu datalari girerek ve de bize biz accessToken,expiration ve refreshtoken donuyor...
    /*
    {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2NTY3ODA4NTksImV4cCI6MTY1Njc4MTc1OSwiaXNzIjoid3d3LnRlc3QuY29tIiwiYXVkIjoid3d3LnRlc3QuY29tIn0.b6eJINtgvCo4An-xzo8QQJ8uXBJPy8RPxUD9q1qPOsY",
    "expiration": "2022-07-02T19:09:19.9942345+02:00",
    "refreshToken": "794f5b7f-01f9-4168-94c9-be3b0c9e6af3"

    COOK ONEMLI BESTPRACTISE--URETTGIMIZ TOKEN ILE CONTROLLER LARI KORUMAK
    Biz token imizi urettik ama hala controllerlarimizi korumuyoruz yani token ile giris yapilablir
    diye bir ayarlama yapmadik kullanici direk benim controller larima erisiyor direk Books a ve diger
    Genre,Author a erisebiliyor
    Gidip BookController da namspace den sonra ilk olarak
    [Authorize] diye bir attribute ekliyoruz
        [Authorize]
        using Microsoft.AspNetCore.Authorization;

        https://localhost:5001/api/books buna dogrudan bir istek gonderildiginde
        401Unauthorized responsu alacak istek gonderen kullanici cunku BookController
        a erismek icin, kimlik dogrulamasi yapilmasi gerekiyor, register olup
        sonra da login olup bir token alip o token i expire suresi icinde 
        Header da gondererek giris yapmayi denemek gerekiyor

        1-ONCE SISTEME KAYIT OLUYORUZ-REGISTER 
        https://localhost:5001/api/users-POST REQUEST

         BODY YE ASAGIDAKI BILGILER GIRILIR-REQUESTTE GONDERILIR
            {
        "firstName": "Zehra",
        "lastName": "Erbas",
        "email": "zehra5434e@gmail.com",
        "password": "Zehra5434@"
             }
           RESPONSE:  200OK
       2-SISTEME LOGIN OLARAK TOKEN ALACAGIZ      
       https://localhost:5001/api/users/connect/token-POST REQUEST
       
       BODY YE ASAGIDAKI BILGILER GIRILIR-REQUESTTE GONDERILIR
      {
          "email": "zehra5434e@gmail.com",
          "password": "Zehra5434@"
      }
      RESPONSE:{
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2NTY4MzQ3NjMsImV4cCI6MTY1NjgzNTY2MywiaXNzIjoid3d3LnRlc3QuY29tIiwiYXVkIjoid3d3LnRlc3QuY29tIn0.1EYiciNq4O_hzYsPqvVYg3uO0d3se4ZtMAKl7IO4sfs",
    "expiration": "2022-07-03T10:07:43.3517803+02:00",
    "refreshToken": "c2c835fd-5289-41b5-ad21-bf002a2edbf6"
}
    3-GET BOOKS A TOKEN ILE BIRLIKTE ISTEK GONDER
    AUTHORIZATION MENUSUNDEN TYPE BERARER TOKEN I SEC VE ORADA TOKEN YERINE 
    RESPONSE OLARAK DONEN TOKEN I YAPISTIR
    BU HEADERS ICINDE KEY OLARAK AUTHORIZATION VE KARSISINA DA BEARER ILE BASLAYAN YANINA DA GONDERDIGMIZ TOKEN I YERLESTIRIYOR 
    https://localhost:5001/api/books
    istegini token ile birlikte gonderince artik books datalarimiza token imiz ile birlike erisebiliyoruz
Bizim webapimiz hem bir identityprovider-kimlik saglayici olarak davrandi hem de client olarka davrandi
Token operation islemlerinde bir token olusturma, token provide etme yeri idi, burayi identity-provider olarka yapti
Ama startup icerisinde de diyoruz ki sana bir token geldiginde, bu jwttoken protokolunde olabilir
jwttoken in default semasini kullaniyordur ve genel olarak bunlar kontrol edilir diyoruz, sen onu cozmek icin bunu kullancaksin
diyoruz,Startup da da token i cozmek icin gerekli olan configleri veriyoruz
 yani esasinda hem token i webapimiz uretiyor hem de urettigi token i kendisi cozuyor dogruluyor
 Bunlar aslinda farkli projeler icinde olabilir...,identityprovider ayri bir proje icinde disarda konumlanir
 PEKI REFRESHTOKEN ILE TEKRAR BIR ACCESSTOKEN NASIL ALINIR ONA BAKALIM
}
    */
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command=new CreateTokenCommand(_context,_configuration,_mapper);
            command.Model=login;
            var token=command.Handle();
            return token;
        }
    } 
    
    }
    /*
    appsettings.json da neler var bakalim
    {
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
Biz Startupda Issuer,Audience ve SecurityKey belirlemistik, bunlar olacak diye belirtmistik
Bunlari da karsilarina demisiz ki bir config olarak bunlari git token ve security olarak
token objesi altinda issuer, audience ve security key olarak git bul demisiz..
    Biz Startup.cs dde Authentication ayarlarini yaparken bazi kurallar belirlemistik
    Burda kullanilan Configuration class i bizim appsettings.json imiza bakiyor
    Dolayisi ile bizim bu token a karsilik geleen audience,issuer ve securitykey i appsettings.json
    a eklemem gerekiyor, buraya onlari ekledigmiz zaman cok kolay bir sekilde calisma aninda
    Configuration clasass i appsettings den onlari bulacaktir...Ki bu bir middleware olarak calistigi
    icin gonderilen request ile controller da ki response actionlari arasindaki 
    zaamn diliminde calisacaktir.
    
    Issuer:Token i dagitan server gibi dusunebiliriz, token i veren burasi o yuzden www.test.com su asamada
    saglayici kendimiz old icin ekstra bir confige gerek yok
    Audience:"www.test.com"
    Security key i biraz uzun girmek gerekir yoksa hata alinabilir, korumak icin
    Simdi appsettings.json a Token objesini ekleyecegiz...

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "Token":{
    "Issuer":"www.test.com",
    "Audience":"www.test.com",
    "SecurityKey":"This is my custom secret key for authentication"
  }
}

Token bilgilerini de appsettings.json da verdgimize gore artik Startup.cs icindeki Configuration
classimiz calisma aninda gidip token a karsilik gelen issuer,audience,securitykey i bulabilecek

     ValidIssuer=Configuration["Token:Issuer"],
    Bu token in olusturulurken ki issuer lari sunlardir
    Token IConfiguration dan geliyor
    ValidAudience=Configuration["Token:Auidience"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
    */