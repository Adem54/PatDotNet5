
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace middleware.Middlewares
{
    public class HelloMiddleware{
        //CUSTOM MIDDLEWARE OLUSTURMAK...
        //Dikkat edersek, middleware ler sira ile calistgii icin kendisi calistiktan 
        //sonra kendinden sonraki middleware ler devam etmesi icn, bunlari constructor
        //parametresindeki requestDelegate tipinde bir class la  yapiyor, ve de invoke methodu
        //da aliyordu, next.Invoke kullaniliyordu,INVOKE dedigmiz de bir, middleware e ait
        //bir methoddur
        //Middleware ler request delege aliyor cunku next dedgimzde bir sonraki evente, 
        //birsonraki  middleware e islemi delege etmesi icin-using Microsoft.AspNetCore.Http;
        // dan gelen bir RequestDelegate tipi aliyor constructor parametresine
        private readonly RequestDelegate _next;
        public HelloMiddleware(RequestDelegate next)
        {
            _next=next;
        }

//Her bir middleware in next.Invoke denildiginde cagrilmasi gereken bir methodunun olmsi
//gerekiyor, ayni dili konusabilmek icin
//Invoke da parametre olarak httpcontext aliyordu hatirlayalim, hazir middleware ler da hep bu sekidle calisiyordu..
//Asenkron kullanirarak kullandigmiz methodlar Task tipindedir, geri donus tipleri Task tipinde oluyor
        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("HelloMiddleware World");
            //_next.Invoke diyerek kendi Invoke methodu icerisinde bir
            //sonraki methodun invoke unu cagirdimm
            await _next.Invoke(context);//bir sonrakinin invoke ugun u cagiriyoruz
            //Middleware genel olarak ler birbrilerine islemleri bu sekilde delege ediyorlar
            //isimiz bittikten sonra da console a mesaj yazdiriyoruz
            //Burasi diger alttaki tum middleware ler bittikten sonra asagidan yukari dogru
            //tum middleware lerin Invoke dan sonra kodlarini calistirarak o 
            //middleware leri sonlandirir response dasn once....
            Console.WriteLine("Bye World");
        }
       
    }    

        //Bunun bir exhtension method olarak cagiriliyor olabilmesi gerekiyor
        // Middleware methdlara bakacak olursak app.UseAuthorization(); application
        //builder aracilgi ile cagrilabilen mehtodlar bunlar.Dolayisi ile bende
        //applicationbuilder tarafindan tetiklenebilecek olan,AppBuilder alan 
        //1 tane exthension method yazmam gerekiyor...
        //Icinde 1 tane method olacagini bildigmiz icin static olarak tanimliyoruz
    public static class HelloMiddlewareExtension
        {

            //using Microsoft.AspNetCore.Builder;
            //IApplicationBuilder type larinda gecerli olacak olan extension methoddur
            public static IApplicationBuilder UseHello(this IApplicationBuilder builder){
                return builder.UseMiddleware<HelloMiddleware>();
            }
        }
        //Burayi da hazirladiktan sonra bu extension class in static methodunu 
        //Startup icerisinde cagiracagiz, pipeline a baglayacagiz
        //Startup icerisindeki configure class i art arda middleware leri cagirarak 
        //bir tane pipeline olusturuyor...
        //Gidelim bu extension methodumuz authorization dan sonra calistiralim
        //  app.UseHello();  
}