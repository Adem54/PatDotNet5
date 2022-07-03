using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations {
    public class TokenHandler 
    {
         public IConfiguration Configuration  {get; set;}   

         public TokenHandler(IConfiguration configuration)
         {
                Configuration=configuration;  
         }

         public Token CreateAccessToken()
         {
            Token tokenModel=new Token();
            SymmetricSecurityKey key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SigningCredentials credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            tokenModel.Expiration=DateTime.Now.AddMinutes(15);
            JwtSecurityToken securityToken=new JwtSecurityToken(
                issuer:Configuration["Token:Issuer"],
                audience:Configuration["Token:Audience"],
                notBefore:DateTime.Now,
                expires:tokenModel.Expiration,
                signingCredentials:credentials//credentials i da olusturmamiz gerekiyor
                //Criptoladgimiz credentials yani kullanici bilgilerini veriyoruz burda da
            );

            //Token olusturucu class i JwtSecurityTokenHandler class idir
            JwtSecurityTokenHandler tokenHandler=new JwtSecurityTokenHandler();
            tokenModel.AccessToken=tokenHandler.WriteToken(securityToken);
            //token olusturabilmek icin parametre olarak securitytoken vermemiz gerekiyor
            //Dolaysi ile gidip onu olusturalim
            //Securitytoken, bizim Startup.cs de services ile validation kurallarini eklerken
            //ayarladigmz kurallara gore burda olusturacagiz
            tokenModel.RefreshToken=this.CreateRefreshToken();
            return tokenModel;
         }

         public string CreateRefreshToken(){
            return Guid.NewGuid().ToString();
         }
    }
}