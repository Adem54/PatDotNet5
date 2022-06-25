using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using middleware.Middlewares;

namespace middleware
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "middleware", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "middleware v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            //Middleware ler asenkron calisir
            //Bu middleware lerin calisma mantigni gorebilemk icin console e yazdiralim
            //Run middleware metodu kendinden sonraki middleware calistirmaz kisa devre yaptirir
            //app.Run(async context=>Console.WriteLine("Middleware-1"));
            // app.Run(async context=>Console.WriteLine("Middleware-2"));
        //    app.Use(async (context,next)=>{
        //     Console.WriteLine("Middleware 1 basladi");
        //     await next.Invoke();
            //Bir sonraki middleware i burda tetiklesin diyoruz ve
            //sonrasinda da middleware ler bittikten sonra da gel bana sunu yazdir diyoruz
        //     Console.WriteLine("Middleware 1 sonlandiriliyor");
        //    });
        //     app.Use(async (context,next)=>{
        //     Console.WriteLine("Middleware 2 basladi");
        //     await next.Invoke();
            //Bir sonraki middleware i burda tetiklesin diyoruz ve
            //sonrasinda da middleware ler bittikten sonra da gel bana sunu yazdir diyoruz
        //     Console.WriteLine("Middleware 2 sonlandiriliyor");
        //    });
        //     app.Use(async (context,next)=>{
        //     Console.WriteLine("Middleware 3 basladi");
        //     await next.Invoke();
            //Bir sonraki middleware i burda tetiklesin diyoruz ve
            //sonrasinda da middleware ler bittikten sonra da gel bana sunu yazdir diyoruz
        //     Console.WriteLine("Middleware 3 sonlandiriliyor");
        //    });
           /*
            app.Use LIFO OLARAK CALISIYOR LAST-IN-FIRST-OUT
            Yani ilk once kendisi calisiyor sonra Invoke diye bir sonraki middleware i
            tetikliyor ama kendisi sonlanmiyor hala ve tum middleware leri calistirdiktan sonra
            gelip kendisinin next.Invoke tan sonraki kodlarini calistirip, sonlandiracak...
            Middleware 1 basladi
            Middleware 2 basladi
            Middleware 3 basladi
            Middleware 3 sonlandiriliyor
            Middleware 2 sonlandiriliyor
            Middleware 1 sonlandiriliyor
           */
           /*
           ASENKRON ISLEM....
           next.invoke() dedigmz zamn bir sonraki middleware i calistirir, next.invoke() sonucunu 
           beklemeden gider bir alttakini calistirir ki iste biz de await diyerek sen bunun sonucunu bekle
           sonucu aldiktan sonra asagi in yani esasinda, ssen bir asenkron sun eyvallah ama bu satirda
           senin senkron calismana ihtiyacim var ondan dolay await , yani wait diyoruz....:).
           Burda suna karar vermek gerekiyor benim yapacagimz bir islemimin sonucuna bir alttaki 
           islem bagimli mi yani eger bir alttaki islem benden burdan gelecek dataya bagimli ve 
           o data olmadan calisamiyor ise o zaman async-await ile calistirarak benim asenkron
           islemimden gelecek data min, asagiya ulasmasini garantei etmek icin awatit ile calistiririm
           kendi kodumu...
           Asenkron demek zamandan bagimsiz calisir demek, yani sira ile kendinden bir sonraki
           diyelim bekliyorsa onu beklemden devam eder calisir
           Asenkron kullanmak bize farkli pipe lar olusturmaya ve her bir pipe da da bir den fazla
           thred islem gormesini saglar, nromalde senkron calisir sistem ve bir pipe da bir tane islem
           bitmeden digeri baslamaz, single thread calisir ama multi thread calisma var onda da farkli pipe
           lar acilmasini saglar ve ilk pipe dolu ise diger pipe da yeni thread isleme alinir
           Ama asenkron sayesinde hem multithrea yapisi hem de ayni pipeline da birden fazla thread calisabilir
           Genel olarak dis sistemlere ciktimgiz noktalar asenkron olarak claisir
           orne 1-database bizim icin dis sistemdir(Bize gelen request islemlerinin uzerine bize database e gidiyoruz
           yani, gelen request ve response islemleri bizim icin asenkrondur bunlar veritabai islemleridir
           )
           2-mail servisleri dis sistemdir bizim icin...
           Database e bir veri yazdigmiz zaman, onun donusunu genel olarak bekliyor olmamiz gerekyor
           Cunku orda bir problem olursa, musteriye veya kullaniciya ok yerine hata mesaji donmem lazm
           dolayisi ile ben database e yazma isleminin sonucu ile ilgileniyorum, yani, o asenkron olan islemi
           await leyip bekliyuor olmam gerekiyor 
           Ben onu asenkron yaptimigz zaman arka tarafata o isletim sistemi single tthread mi claisacak yoksa
           multi thread mi calisacak buna kendisi karar veriyor...
           await leyip senkron mehtod kullanmak ile senkron method kullanmak her zaman arka tarafta ayni seye esit olmayabiliyor
           Biz bunlari yine kafamizda sira ile calisiyor gibi dusuneibliriz ama bu iki yontem senkron-asenkron 
           isletim sistemi icin birbirinden cok farklidir
           Bilmemiz gereken nokta sonucu bekleyip aldiktan sonra bir sonraki satir calismasi gerekiyorsa 
           o satiri await leyerek, sonucu alinmasini bekledikten sonra bir alt satira inmesini saglamak
           ya da bir alttaki satir benim bu satirimda yapacagim isle ilgilenmiyorsa da await islemine ihtiyaciimz yok
           beklememize de gerek yoktur...asenkron olarak calisabilir
           Mail gonderme servisi benim icn asenkron calisabilir, cunku bir islemi yaptiktan sonra o emaili gonderme
           methodunu awaitlememe gerek yok cunku onunla bir ilgim yok bir alt satirda ve onun gonderme hizi, ulasip ulasmamsi
           bir alttaki satir ile bir baglantisi yok ve thread lerin, islemlerin akisinda bir tikanmaya yol acacak birsey degil
           o zamnan asenkron calisabilir await ile durdurup sen burda senkron calis dememe gerek yok
           GEnellikle anenkron calisabilir dedigmiz yerler, notificatin firlattimgz mesaj gonderdigmiz sistemler asenkron calisabilir
           Ama veritabani gibi bagimliligmizin oludugu dis sistemler e yaptimiz cagrilari awaitliyor olmamiz gerekiyor
           Ve middleware lerde de sira ile calismak arka arka dogru sirada calismak onemli oldugu icin ordalarda senkron 
           hareket etmesini istedigmz icin, oralarda awaitliyor olmak onemlidir...
           */


              app.UseHello();  

            app.Use(async (context,next)=>{
            Console.WriteLine("Use Middleware tetiklendi");
            await next.Invoke();
           });
        //1-matchPath,1 de IApplicationBuilder istiyor
           app.Map("/example",internalApp=>
           internalApp.Run(async context=>{
            Console.WriteLine("/example middleware tetiklendi");
            await context.Response.WriteAsync("/example middleware tetiklendi");
            //Asp.NetCoreHttp den geliyr WriteAsync
            //Bu WriteAsync ile context icindeki response a mesaj yazmamizi saglar
           //  Eger expample endpointine gidersek yukarda yazdgimiz mesaji response yazacak
           }));

      
      //Sadece routea gore degilde, request icindeki herhangi bir parametreye gore bir middleware olusturmak istersem
      //app.MapWhen-icine 2 tane hazir delge aliyor-Func-predicate-boolean donduren -birde Action-void donduren
    //Get isleminden once calissin diyebiliyoruz...ornegin..querystring den gelen bir parametreyi kontrol de edebliriz
    //Ornegin id geliyorsa eger Request ten 
    //app.MapWhen(x=>x.Request.Method=="GET" && x.Request.QueryString=="",internalApp=>{
        //Pathbase, header vs bircok seyi kontrol edebilriz,controller a gitmeden once
      app.MapWhen(x=>x.Request.Method=="GET" ,internalApp=>{
        internalApp.Run(async context=>{
            Console.WriteLine("MapWhen middleware tetiklendi");
            await context.Response.WriteAsync("MapWhen middleware tetiklendi");
        });
      });

           /*
           Map middleware methodu roota gore middleware leri yontememizi sagliyor
           /example rootuna bir istek gelirse o zaman bu middleware i calistir...yoksa bunu 
           atla
           Eger expample endpointine gidersek yukarda yazdgimiz mesaji response yazacak
           Swagger daki url  olan https://localhost:5001/swagger/index.html onun yerine 
           localhost:5001/example boyle bir url ile request gonderir isek eger
           response da /example middleware tetiklendi yazisini goruruz
           Console dda da bu sekilde yazacak
           Use Middleware tetiklendi
          /example middleware tetiklendi
          Bunlari niye yazdi cunku biz root da yani url de example olarak girdik...
           */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
