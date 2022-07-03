

using System;

namespace WebApi {
        
        public class User {
        public int Id {get; set;}
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public string Email  {get; set;}
        public string Password {get; set;} 
        public string RefreshToken {get; set;}
        public DateTime? RefreshTokenExpireDate {get; set;}  

        }
        //Burda RefreshToken i da biz Accesstoken olusturulunca olusturacagiz ki, kullanici
        //accesstoken suresi bittiginde her sefreinde gelip de  tekrar login olmaya calismasi yerine
        //onun sayfayi kullanmaya devam etmesi uzerine front-end developer in otomaik refresh-request i 
        //yazmasi ile kullanici login olup sayfay i kullanmmaya devam etmesi halinde, refresh token accesstoken
        //expire time bitmeye yakin refresh token requesti gondererek yeni bir accesstoken alarak 
        //tekrar sureleri bastan baslatmis olyor ve kimlik dogrulama gerektiren sayfalaria ersisime devam etmis oluyor

}