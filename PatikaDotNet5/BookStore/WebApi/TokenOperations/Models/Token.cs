using System;
//Bu connect-token end-pointi yani, conncecttoken endpointi cagrildiginda
//geriye donulecek olan verileri tutan obje olacak bu
//Bu da su demek geriye bir accesstoken donecek, bir expirationtime donecek
//ne zaman expire olacagini verecek olan data
//Birde refresh token donecekki bir sonraki cagrimlarda refresh token kullanilarak
//access token uretilebilsin
namespace WebApi.TokenOperations.Models {
    public class Token {
        public string AccessToken { get; set; } 
        public DateTime Expiration {get; set;}
        public string RefreshToken {get; set;}
        //RefreshToken de bir string key dir 
    }
    //Token class ini da olusturduktan sonra artik TokenHandler i olusturabiliriz
}