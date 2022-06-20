
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middlewares {

    public class CustomeExceptionMiddleware
    {
        private readonly RequestDelegate _next;
       public CustomeExceptionMiddleware(RequestDelegate next)
       {
                 _next=next;
       }

       public async Task Invoke (HttpContext context){
            var watch=Stopwatch.StartNew(); //Biz izleme acar bizim icin...
            //Bir timer mekanizmasidir...
            //Gelen request ve responlsari loglamak istiyoruz hic hata olmadigini
            //dusunelim...request-response arasinda girdigi ve ciktigi noktada loglamak
            //istiyoruz
            //Okumasi kolay ve ayni format
            //Request ilk basladigi andir buraya ilk girdinde, next.Invoke diyene kadar
            //bir sonraki endpoint cagirma islevine girmeyecek, o zaman burda bir request
            //i loglayaalim, bir isaretleyelim attribute aracligi ile request i
            //Dogru end-pointler tasarliyor olmanin faydasini boyle noktalarda cok goruyoruz
            //End-pointleri ne kadar dogru tasarlarsak,ve end-pointler bana ne kadar ilk bakista
            //dogru bilgi verirse biz bu loglari o kadar dogru tasarlayabiliriz...Ic gudusel
            //olarak aradgimiz bilgiyi o kadar kolay buluruz...
            //Okunabilir Api tasarlamak onemli bir konu
            string message="[Request] HTTP "+context.Request.Method+ " - "+ context.Request.Path;
            Console.WriteLine(message);
            await _next(context);//Bir sonraki middleware i cagirmis olduk...
            //Bu use seklinde, use methodu ile calisiyor
            //Bu alt satirda biz bir sonraki middleware ler bittikten sonra devam edebiliyoruz
            //Tum middleware ler bittikten sonra tekrar burya gelecek ve bu middleware in
            //next kodundan sonra,ki satirlari calistirarak, middleware i sonlandiracaktir
            //Burasi da bizim responsumuza denk geliyor
            //Burda da responsu loglayalim
           watch.Stop();//Burda timer i durduruuz cunku tam response geldigi yer burasi.
           //Request geldikten sonra, responsa kadar ne kadar sure gectingi gorebilirz
            message="[Request] HTTP" + context.Request.Method + " - "+ context.Request.Path+ " responded "+ 
            context.Response.StatusCode+ " in " + watch.Elapsed.TotalMilliseconds;
            Console.WriteLine(message);
            //Bunun ne kadar surede calistigini ve http requestine karsilik hangi
            //http post mesajini ve post status codunu dondugunu gostermek istiyorum log icerisinde
            //ve bur requeste girdikten ve bu requestten ciktigi noktaya kadar ki sureyi olcersem
            //servis icerisinde ne kadar zaman harcadigni ogrenmis olurum...Bunun icnde Invoke
            //methodunun en ustunde Stopwatch methodu ile StartNew baslatiriz ve en son response
            //verirken durdurarak ne kadar zaman gectigini olcebiliriz

       }

    }
    //Bu nokta da biz bir de extension method yazacaktik ki app.use prefix i ile birlikte
       //kullanabilelim...
       //Startup icerisinde csalistirmak icin yaptimiz yer burasi ve hangi isimle erisecegiz
       //Startup icerisinde onu ayarliyoruz...Ayrica, IApplicationBuilder bu middleware 
       //deki ozel tanimlar...ve middleware de bu tip  ile calisiyor
       //Biz burda hem loglama hem de exception i bir arada yapacagiz
       public static class CustomExceptionMiddlewareExtension{
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder){

            return builder.UseMiddleware<CustomeExceptionMiddleware>();
        }
       }
       //Extensin metodumuzu da yazdiktan sonra Startup icerisinde bunu yazmamiz gerekiyor
       //VE bunu tam olarka authorization dan sonra ve endpointler calismadan once calistaicagiz...
       //Pipeline e dogru noktadan sokacagiz...
}
/*
            app.UseAuthorization();
Custom middleware imz dir burasi
            app.UseCustomExceptionMiddle();
Burasir request end-pointe dusme yeridir, end-pointlerin calisma yeri...
            app.UseEndpoints(endpoints =>

            WebApi projemizi calistirdiktan sonra api de request ler gonderirsek
            Ornegin bir Get request gonderirsek
            Console da -[Request] HTTP GET - /api/Books bunu yazar
            Post request gonderirsek
            Console da [Request] HTTP POST - /api/Books yazar
            GetById ye request gonderirsek-2 numarasi id ye
            Console da [Request] HTTP GET - /api/Books/2
            Put request ile 2 numarali id ye istek gonderirsek
            [Request] HTTP PUT - /api/Books/2
            Delete request ile 2 numarasi id ye delete istegi gonderirek
            [Request] HTTP DELETE - /api/Books/2 console da bu yazar...

            Requestlerimizi bu sekilde gormus olduk
            Simdi de Requst ile response arasinda ne kadar sure geciyor
            ona bakalim
            
            Get request gonderdgimizde
            [Request] HTTP GET - /api/Books
            [Request] HTTPGET - /api/Books responded 200 in 567,3136
            Post request gonderdigmizde
            [Request] HTTP POST - /api/Books
            [Request] HTTPPOST - /api/Books responded 200 in 442,5289
            Put request gonderdgimizde
            [Request] HTTP PUT - /api/Books/2
            [Request] HTTPPUT - /api/Books/2 responded 200 in 480,8899

            Simdi try-catch lerden de kurtulup bu hata durumlarini middleware ler
            icerisinde yakalamak istiyorum...
            Controller class ina gidelim ve orda AddBook da try-catch i 
            kaldirarak baslayalim ise
            try-catch blogunu biz, kaldirdigmiz zaman, yani tabi onun yerine if blogu da
            kullanmadigmizi varsayarsak biz Validasyon katmani bir hata firlattiginda
            veya benim Hanlde methodumdan birsey throw edildiginde her turlu request ok 
            donecek ve ben bu hata mesajini geriye donemiyor olacagim...
            BadRequst gonderememis olurum...

        TRY-CATCH ILE YAPILMIS HALI BU
        public IActionResult AddBook([FromBody]CreateBookModel newBook){
            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
        try
        {
            command.Model=newBook;
        CreateBookCommandValidator validator=new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);//usingFluentValidation dan geliir...
            command.Handle(); 
        }
        catch (Exception ex)//ex aslinda bizim mesajimiz
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

        TRY-CATCH OLMADAN YAPILMIS HALI BU

      public IActionResult AddBook([FromBody]CreateBookModel newBook){
            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
            command.Model=newBook;
        CreateBookCommandValidator validator=new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);//usingFluentValidation dan geliir...
            command.Handle(); 
        
        return Ok();
    }
*/