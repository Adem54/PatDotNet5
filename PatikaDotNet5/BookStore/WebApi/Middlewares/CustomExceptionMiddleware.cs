
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Services;

 //controller dan try-catch kaldirdik, ve controller da bir hata firlatildigi
            //zaman, kod, await _next(context) den sonra kirilacak ben de try-catch i burda
            //kullanarak hata yi burda yaklayacagim ve controller da try-catch
            //kullanmak zorunda kalmamis olacagiz...

             // await _next(context);
            //Tam bu noktada kod kirilirsa hatadan dolayi, firlatilan exception dan dolayi
            //watch.Stop() calisamayacak ve duramayacak ve dolasyi ile ben dogru bir calisma
            //zamanina erisemeyecegim ondan dolayi, eger hata alirsa da watch.Stop et diyecegiz
namespace WebApi.Middlewares {

    public class CustomeExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
       public CustomeExceptionMiddleware(RequestDelegate next,ILoggerService loggerService)
       {
                 _next=next;
                 _loggerService=loggerService;
       }
      
       public async Task Invoke (HttpContext context){
             var watch=Stopwatch.StartNew(); 
            try
            {
            string message="[Request] HTTP "+context.Request.Method+ " - "+ context.Request.Path;
            //Console.WriteLine(message);
            _loggerService.Write(message);
            await _next(context);
            watch.Stop();
            message="[Request] HTTP" + context.Request.Method + " - "+ context.Request.Path+ " responded "+ 
            context.Response.StatusCode+ " in " + watch.Elapsed.TotalMilliseconds;
            //Console.WriteLine(message);
            _loggerService.Write(message);

            }
            catch (Exception ex)
            {
                
            watch.Stop();
            await HandleException(context,ex,watch);
            }
           
       }

        
        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            //Burd ahata durmunda loga yazacagim ve geriye donecegim mesajo olusturacagim
            string message="[Error] HTTP" + context.Request.Method + 
            context.Response.StatusCode+ " Error Message "+ ex.Message
            +" in "+ watch.Elapsed.TotalMilliseconds+ " ms";
            Console.WriteLine(message);
            context.Response.ContentType="application/json";//bu genel olarak application/json dir
            //Mumkun oldugunca bir starndart ortaya koymaya calisiyoruz ki hepsi ayni sekilde donsun
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            var result=JsonConvert.SerializeObject(new {error=ex.Message},Formatting.None);
            //VE bunu da context icerisine yazip geri donmem gerekiyor
            return context.Response.WriteAsync(result);
         
        }
    }
   
       public static class CustomExceptionMiddlewareExtension{
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder){

            return builder.UseMiddleware<CustomeExceptionMiddleware>();
        }
       }
       /*
       Extension methodlarda hangi class icinde olusturuldugu class ismi vs onemli degildir
       Dogrudan, method ismi hangi tip icin yaziliyorsa bu method oo tipten olsuturlmus bir instace
       degiskeninin .dot notasyonu ile method ismi yanyana direk kullanilabilir...
       */

        /*
       return context.Response.WriteAsync(result);
            Git Response a yaz diyoruz
            context icindeki Response objesini ezerek api uzerinden hata durumunda hangi mesajin donecegini
            soylemis olduk..
            Sonra gidip controller da bir validasyon hatasi firlatacak bir post
            gireriz...,add islemindeki try-catch i kaldirdiktan sonra tabi ki...
    */
    //Bu nokta da biz bir de extension method yazacaktik ki app.use prefix i ile birlikte
       //kullanabilelim...
       //Startup icerisinde csalistirmak icin yaptimiz yer burasi ve hangi isimle erisecegiz
       //Startup icerisinde onu ayarliyoruz...Ayrica, IApplicationBuilder bu middleware 
       //deki ozel tanimlar...ve middleware de bu tip  ile calisiyor
       //Biz burda hem loglama hem de exception i bir arada yapacagiz
       //Extensin metodumuzu da yazdiktan sonra Startup icerisinde bunu yazmamiz gerekiyor
       //VE bunu tam olarka authorization dan sonra ve endpointler calismadan once calistaicagiz...
       //Pipeline e dogru noktadan sokacagiz...
       //Extension methodu biz bize hazir gelen type lar ornegin int,string, IApplicationBuilder gibi
       //type lar icinde kendime ati kullanmam gereken method kullanmam gerektigi zaman olusturuyoruz
       //Yani adam bir sistem kurmus middleware lar IAppBuilder tipinden olusturulan ornekj
       //uzerinden method larla caliisyor biz de ne yapiyoruz disardan ona method eklemis oluyoruz diyebiiliriz
       //bu da extension metod oluyor
}

 //Exception larimizi mantikli bir sekilde loglamak icinde bir Exception method calistiracagiz
            //burda, yani bir tane de Exception lari yazabilmek icin bir method olustururuz
            //direk buraya da yazabilrdik ama buray cok da buyutmek istemiyoruz
           // await HandleException(context,ex,watch);
        //CUSTOMEXCEPTIONMIDDLEWARE HANDLEEXCEPTION METHODUNDA PARAMETREYE ALDGIMIZ
        //CLASS LAR ILE METHOD DEPENDENCY MIZ VAR
        /*
        Biz burda mesajimizii gidip Console a yazdirdik ve su an Console  class ina bagimliyiz
        ama biz ornegin ya logu database e yazmak istersek veya dosyaya yazmak istersek su anda
        yazamiyoruz cunku biz Console a bir bagimliligimiz var ve de burdaki Invoke methodu
        icinde direk Console.WriteLine ile log un console a yazildiginin bilinmesine gerek yok
        biz nereye istersek oraya yazabilmeliyiz normalde
        Biz hicbiryere dokunmadan, ekstra ornegin database e yazacaksam database e yazacak class ekleyp
        database e de yazabilmeliyim veya yani sekilde ornegin bir file e yazacak class i ekleyip
        ora ya da yazabilmeliyim mevcut kodlari bozmadan oralara dokunmadana
        Sadece bir config degistirerek tek hamle ile uygulama icindeki tum loglari baska 
        bir yere yazabilmeliyim...
        Bunu da zaten servis gibi yapmak istiyoruz, istedigim yere implemente edebileyim bu servisi
        cagirarak istegimi yerede log yazabileyim, belki ben BookOPerations klasoru altindaki 
        operasyonlar icinde log yazmak isteyecegim, belki dis sisteme ciktik ve ordan gelecek olan
        responsu loglamak istiyozdur, dis sistem demek disardan hazir bir servisden bir data
        veya mehtod vs kullanmak .
        Su an ki sistemde ornegin biz console.log yerine googlce cloud log kullanmak istersek
        ne yapaagiz gidip console.log yazdigmiz heryerden console.log lari silip yerine
        cloud log yazmamiz gerekecek ki bizim projemiz cok buyur ve biz 100 yerde conosle log
        yazmissak ne olacak bu surdurulebilir degil kesinlikle... 

        */

        //Hata mesajini hala ekrana donemedik ve de geriye sttus code olarak da 200 degil 
            //500 server error donmemiz gerekiyor..Burda elimde context olduguna gore
            //Bu hata mesajini anlamli bir sekilde donuyor olmam gerekiyor ve ben bu noktada
            //500 server error donecegim ve burda hata mesajii istedigmz gibi yonetebiliriz elimizde
            //Exception objesi oldugu icin, istersek uygulama icinde custom Exception tanimlayip onu da
            //donebilirz
            //ben nerdeyse her turlu bilgiye erisebiliyorum dolaysi ile de
            //context ile yazarak, geriye mesaj donebilirim, eger hata oldu ise zaten 
            //ben geriye birsey donmeyecgim sadece hata mesajo donecegiz demektir...
           // context.Response.ContentType="application/json";//bu genel olarak application/json dir
            //Mumkun oldugunca bir starndart ortaya koymaya calisiyoruz ki hepsi ayni sekilde donsun
           // context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            //HttpStatusCode.InternalServerError bu hata kodunu donuyor 500 olarak 
            //ama type i integer degil int e cast ederiz..
            //enum System.Net.HttpStatusCode enum dan int e cast ederiz...
            //Bir de Exception objemin bir json olarak donmesini istiyorum cunku bunun bir ui a 
            //dondugunu dusunursek, ui icerisinde ne kadar json ile tasirsam  o kadar ui icerisinde
            //cozumlenmesi kolay olacaktir 
            //ex.message objesini bir serialize etmem gerekiyor, json objesine cevirebilmem icin
            //Ondn dolayi da bir json paketini uygulamamiza eklememiz gerekiyor
            //dotnet add package Newtonsoft.Json
            //Gidp dosyamizda kontrol edersek -   <PackageReference Include="Newtonsoft.Json" Version="13.0.1" /> 
            //yuklendigin goruruz
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
    
    Ondan dolayi biz try-catch i Custom middleware imiz iceriisnde kullanacagiz, cunku
    sistem controller da bir hata firlattig zaman bu next dedikten sonraki alt satir da yani response 
    beklenen satirda kod kirilmis olacak cunku middleware lerin hepsi calisip en alttaki 
    end-poiinte dusuren middleware ile beraber controller a giriyor ve sonra da response gelmeye
    baslayacak ama tabi bir hata firlatilirsa d ao hata yi alacak ve de kod kirilmis olacak bu da middle
    ware ler in sonlandirmak iicn oraya gitmeye calistiginda zaten kod kirilmis olacak.... yani bu islem
    await next() den sonra olacak....

    Custome middlware icersindeki Invoke isleminde exception i handle ettikten sonra
    gidip bir post islemi ne validation kurallrina uymayan datlar girerek exception
    firlatmasina neden olup exception larin middleware araciligi ile nasil ele alindigni gorebiliriz    
    500 ERROR STATUS CODE GELIR VE DE HATA MESAJINI DA GERIYE DONDURUYOR RESPONSE OLARAK JSON BICIMINDE

    {
  "error": "Validation failed: \r\n -- Model.GenreId: 'Model Genre Id' kan ikke være lik med '0'.\r\n -- Model.GenreId: 
  'Model Genre Id' skal være større enn '0'.\r\n -- Model.PageCount: 'Model Page Count' skal være større enn '0'.\r\n -- Model.PublishDate.Date:
   'Model Publish Date Date' skal være mindre enn '20.06.2022 00:00:00'.\r\n -- Model.Title:
    'Model Title' kan ikke være tom.\r\n -- Model.Title: 'Model Title' skal være større enn eller lik 4 tegn. Du tastet inn 0 tegn."
}

Biz bunu Json a donduruduk ve dogrudan yazdirdik ama bunlari istedigmiz sekidle de dondurebilrdik
Ornegin biz bunlari, exception mesajlari bir diziye atip anlamli bir sekilde de dondurebilirdik...
UI yin nasil bir hata bicimine ihtiyaci var ise veya UI nasil bir hata formati bekliyor ise oyle 
donebiliriz cunku hata kaynagi elimzde var Exception objesi...
Onemli olan bu yapiyi kurabilmektir...

Biz resmen request in ilk baslama ani ile response verildikten sonra ki ana kadar olan 
dilimi middleware lerimiz le kontrol edebiliyoruz...
Artik controller daki tum try-catch leri kaldirabiliriz....

Ayni kitabi eklemeye calisinca hata firlatmistik BookOperations altindaki CreateBookCommand dan
Bizm CustomExcaption middleware imiz bu hatayi yakaladi ve error objesi icerisine koydu
Error umz su an cok okunabilir durumda, bunu simdi istedigm gibi front-end e gosterebilirim
Error objesinin dolu olup olmadigini kontrol edebilirim, eger bu obje dolu ise undefined degil
ise demekki hata almisim diyerek bu mesaji gosterebilirim

*/