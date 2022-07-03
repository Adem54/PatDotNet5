
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities{

    public class User {
    
            public int Id {get; set;}
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }   
            public string Password { get; set; }    
            public string RefreshToken {get; set;}
            public DateTime? RefreshTokenExpireDate {get; set;}
            /*
            AccessToken bizim asil authenticate olmak icin kullandgimiz tokendir,life time i bizim icin onemli olan
            ve ona belirlenen expiretime sone erince gecerliligini yitirecek olan token dir
            RefreshToken eger accesstoken suresi doldugunda kullaniciyi logout istemiyor isek
            Bakildiginda cok kotu bir kullanici deneyimi oluyor, kullanicinn 15 dakka da bir login den dusuyor ve surekli login
            olmasi gerekiyor. Ama kendimiz bir e-ticaret sitesinde uzun uzun islem yapiyoruz ama logout olmuyoruz token suremiz
            hic dolmyor nerdeyse, ama orda surekli devam eden aslinda access token degil, token i yeniden alabilmek icin
            size bir access token verildigidne, clientta islem yaparken bu sizin, client login olmayi basardginda yani
            daha onceden register oldugu icin back-end onun loginine izin verip ona bir token veriyor, client login
            islemini basari ile gerceklestirdiginde client a bir access token geliyor, bu accesstoken yaninda bir
            refresh token ile gonderiliyor.Bu refresh token browserlarinizin cash inde biryerinde saklaniyor bu degisiklik gosteriyor
            Ve clientin accesstoken i expire oldugudna, suresi doldugunda, kaynaklara erisilemez duruma geldigidne refresh token ile birlikte, bu token saglayicisina,server a kimlik altyapisi olan uygulamaya bir istek de bunlunuyor ve bu refeesth token ile
            yeniden bir accesstoken aliniyor,eger configurasyonda oyle verildi ise refreash token da yenileniyor
            Yani ne olmus oluyor sanki kullanici loginden yeniden gecmis gibi, refresh token login bilgilieri yerine kullanilabiliyor
            Iste bunun boyle guzel bir avantaji var...
            AccessToken e verilen sure bittignde ne yapmamiz gerekiyor,refresh token dan yeni bir access token istemem gerekiyor
            Yani user uzerinde benim refresh token imi tutuyor olmam gerekiyor
            RefreshTokenExpireDate-Refresh tokenin da kullanilma suresi vardir, kullanilma suresi dolunca o da artik, 
            gecersiz olacaktir...
            Bu arada kimlik altyapilarinin normal applicaton icinde konumlandirilmasi dogru degildir
            Cunku uygulama ile kimlik altyapisi birbirinden ayri islemlerdir dolayisi ile mumkun oldugu kadar bu concern lerin
            projeler olarak ayrilmasi, ayri olmasi gerekir
            Token saglamak sadece bir application in isi olmasi gerekir,webapi icerisinde bir controller olmasina gerek yok aslinda
            Database in ayrildigi noktada uygulamayi ayirmak cok saglikli bir yaklasim oluyor
            */
    }
} 