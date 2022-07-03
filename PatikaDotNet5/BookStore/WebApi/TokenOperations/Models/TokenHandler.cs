//Burasi hem accesstoken hem de refresh token olusturacak olan class dir...
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations {
    public class TokenHandler 
    {
        //Burda appsettings den config okuyacagimiz icin IConfiguration a ihtiyacimiz var
        //Servis olarak eklerken bazi configler oraya yazmistik eklerken
        // O configlere bagli olarak token olusturacgiz ki cozerken de o configlere 
        //bagli olarak kolay bir sekilde cozulebilsin
       public IConfiguration Configuration {get; set;}
        public TokenHandler(IConfiguration configuration)
        {
            Configuration=configuration;
            
        }
        //User a gore geriye token donen bir method olusturuyoruz
        public Token CreateAccessToken(User user)
        {
            Token tokenModel=new Token();
               //Bu securitykey i kullanarak bir token olusturabilmek icin
            // securityKey in simetrigini almamiz gerekiyor
            //Hatirlarsak biz token protokollerini Startup.cs de belirtirken de
            //IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
            //Token hazirlanyaanin securitykey i bir symetriSecurityKey prosessinden gecirmesi gerekiyor seklinde
            //belirlemistik, yani parametre icinde encode edilecek , kullanici bilgilerinden jwt yapisi oyel calisiyor zaten
            //compoiler in bizim kodlarimiza
            //yaptigi gibi onu sifreli uzun bir string cumlecigine ceviriyor
            //Sifrelenmis bir kimlik olusturuyoruz aslinda
            //Encoding=>using System.Text den gelyor
        SymmetricSecurityKey key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
        //Token:SecurityKey burasi appsettings.json dan geliyor
        //SymmetricSecurityKey=> using Microsoft.IdentityModel.Tokens; burdan geliyor
        //Simdi signinCredential olusturabiliriz bundan
        //singingCredential benden simetricsecurity key i istiyor
        //Sifrelenmis bir kimlik olusturuyoruz aslinda
        //Signingcredentials olusturalim
        //SigninCredentials bizden bir encode edilmis securitykey, bir de  string algoritmasi istiyor
        //algoritma nasil sifreleyecegimizi soyleyecegiz, hangi algoritma kullanilarak key sifrelenecek
        //Burda hazir sifreleme algoritmalarindan kullanialacak
        //SecurityAlgorithms.HmacSha256 bu algoritmayi kullan diyoruz...
        SigningCredentials credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        //sifrelenmis kimligi de burda olusturmus olduk
        //Simdi de expiration i belirleyelim
        //Biz 15 dakika gecerli olacak bir accesstoken olusturmak istiyoruz
        tokenModel.Expiration=DateTime.Now.AddMinutes(15);
        //expiretime i da olusturduk
        //Simdi de tum bunlari kullanarak JwtSecurityToken, bu token in tipidir
        //Bu da bizden tek tek token in issuer,audience, varsa claims(rol),expire time,
        //notBefore ne zaman kullanilmaya baslansin
        //signingCredentials i parametreye istiyor
        //Biz burda securityToken olusturyoruz cunku token i olusturmamiza yardim eden
        //asagidaki jwtSecurityTokenHandler parametre olarak bizden securityToken istior
        //Token i son asamada ortya cikaran, ureten kisim jwtSecurityToken kismidr
        JwtSecurityToken securityToken=new JwtSecurityToken(
            issuer:Configuration["Token:Issuer"],
            audience:Configuration["Token:Audience"],
            expires:tokenModel.Expiration,
            notBefore:DateTime.Now,//uretildigi anda kullanilabilsin diyoruz biz
            //token uretildikten ne kadar sonra devreye girecegini soyluyoruz,yani su an olusturuluyor
            //ama ornegin 5 dakka sonra kullanilabilsin diyebiliyorsunuz
            signingCredentials:credentials
            //Criptoladgimiz credentials yani kullanici bilgilerini veriyoruz burda da
        );
         //JwtSecurityToken=>using System.IdentityModel.Tokens.Jwt;
         //Buraya kadar token in ayarlarini olusturmus olduk
         //ARTIK TOKEN IN OLUSTURULMA ASAMASI BURASIDIR, TOKEN OLUSTURMAK IICIN GEREKLI
         //ISLEMLER BURYA KADAR YAPILDI VE securityToken da hazirlandigi icin artik token i
         //olusturuyoruz
         //Artik token imizin olusturulma asamasindayiz burda tokenimiz olusturulacak bunun icinde
         //yine jwt icindeki jwtSecurityTokenHandler ile token olusturucuyu kullanacagiz
         //Bana bir tane jwtSecurityTokenHanlder in instancesi lazim, ki tokenhandler ile
         //biz token i olusturacagiz, yani hazir JwtSecurityTokenHandler demek token olusturuc demektir
         JwtSecurityTokenHandler tokenHandler=new JwtSecurityTokenHandler();
         tokenModel.AccessToken=tokenHandler.WriteToken(securityToken);
         //REFRESH TOKEN DA ACCESSTOKEN OLUSTURULDUGUNDA OLUSTURULUYORDU
         //SIMDI REFRESH TOKEN I DA OLUSTURALIM
         //Refresh token ne yapiyordu bize yeni bir token uretiyordu yani sanki login islemi nde token
         //uretir gibi,kullanici bir kez daha login olmus gibi, yeni bir token uretiyordu
         //Asagida bize refresh token donen bir method yazariz...
         tokenModel.RefreshToken=CreateRefreshToken();
         return tokenModel;
        }
        public string CreateRefreshToken (){
            return  Guid.NewGuid().ToString();
            //Refresh token icin Guid ile biz uniq bir string olusturuyoruz bu bize
            //uzun bir string donecek ki zaten sonunda ToString ile stringe ceviriyoruz
        }
        //Token uretme ve refresh token uretme olayi buraya kadar
        //Buralar hep configurasyondur olayin mantigini anlamaya calisalim configurasyon
        //kismi dokumana bakarak yapilabilir...Meselede nerde ne yapacagimizi bilirsek 
        //detaylar icin dokumandan yardim alabiliriz....
    }
}