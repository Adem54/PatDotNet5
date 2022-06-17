using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.DbOperations;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
              /*
            Biz program her ayaga kalktiginda DataGenerator un de calismasini istiyorum,
             veritabanim default datalar la karsima cikmasi icin ondan dolayi da program.cs 
             run etmeden once DataGenerator u devereye sokmam gerekiyor
             CreateHostBuilder(args).Build().Run(); bunu bir parcalayalim, 
             Buil() e kadar olan kisim host diye bir degiskene atayalim
            Burda kisaca sunu yapiyoruz bizim eger uygulama basladiginda
             tetiklenmesini istedgimiz methodlarimiz olursa o methoda paramtre olarak
              IServiceProvider aliriz ki onun araciligi ile biz Program.cs de 
              uygulamamiz ayaga kalkarken bizim istedigmiz methodumuzu tetikleyerek
               ayaga kalkar ve de Program.cs de de yine Services uzerinden gideriz..
               Bir scope olustururuz ve o scope dan da ServiceProvider olustururz
                ve ardin da artik parametresine IServiceProvider alan static methodun
                 class i uzerinden o methodu cagiragbiliriz....
            Artik uygulama her ayaga kalkarken context icerisine 3 tane kitap verimiz yazilacak
          
            Uygulama ayağa kalktığından initial datanın in memory DB'ye yazılması 
            için Program.cs içerisinde configurasyon yapılması
            1. Get the IWebHost which will host this application.
            var host = CreateHostBuilder(args).Build();
            2. Find the service layer within our scope.
            using (var scope = host.Services.CreateScope())
            3. Get the instance of BoardGamesDBContext in our services layer
                var services = scope.ServiceProvider;
                4. Call the DataGenerator to create sample data
                DataGenerator.Initialize(services);
            Continue to run the application
             */
            var host=CreateHostBuilder(args).Build();
            using(var scope=host.Services.CreateScope())
            {
                var services=scope.ServiceProvider;
                Datagenerator.Initialize(services);
            }

            host.Run();
            //Burasi server i ayaga kaldiran yer burasi calismazsa controller
            //dosyaslarini calistirip server i ayaga kaldirmaz ve 
            //Waiting for a file to change before restarting dotnet error boyle bir uyari verir... 
           
             
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                 

                    webBuilder.UseStartup<Startup>();
                });
    }
}
