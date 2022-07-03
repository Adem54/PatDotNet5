using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.DbOperations;
using WebApi.Middlewares;
using WebApi.Services;
using System.Text;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //Configuration=>Uygulama icerisindeki servislerin,bilesenlerin uygulamaya gosterildigi yerdir
        /*
        Database bizim icin bir, servistir, bir bilesendir.Bizim onu burda kullanabilmemiz icin onu 
        buraya inject ediyor olmamiz gerekiyor
        Biryerlerden dependency injection ile gectigimiz verileri kullanabilmek icin onlari burda 
        inject ediyor olmamiz gerekiyor, benim anladgim biz dependency injection mantigi ile veya
        iste parametreye direk interface i .NetCore dan alarak kullandigmiz bazi servisleri,
        kaynaklari new lenmesi icin, bizim onlari burda injeckt etmemiz gerekiyor..
        */
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
          //Startup.cs içerisinde ConfigureServices() içerisinde DbContext'in servis olarak eklenmesi
            //DbContext olarak BookStoreDbContext i ekle ve ben bunu inmemory kullanmak istiyrouz...
            //BookStoreDbContext imizin uygulama icerisinde gorunebilmesi icin, kullanabilmemiz icin servislere DbContext olarak gidip eklemesini soyledik ve artik bunu yaptigm icin ben uygulama icinde herhangi biryerde BookStoreDbContext i alip constructor aracgilig ile enjekte edip istedgim yerde kullanabilirim...
            //   services.AddDbContext<BookStoreDbContext>(options =>
            //     options.UseInMemoryDatabase(databaseName:"BookStoreDB"));
            // services.AddAutoMapper(Assembly.GetExecutingAssembly()); 
            //Bizim icin burda uygulama baslarken bir kez olusturup uygulama sonlana kadar calismasini 
            //istiyorum ben dependency injection ile verdigmiz dependency lerin
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>
             {
                opt.TokenValidationParameters=new TokenValidationParameters()
                {
                                       
                    ValidateAudience=true,//Benim token imiz kimler kullanabilir, audience client im kimler se bunlari validate et diyor, su an kurallari veriyoruz sadece, henuz audience i soylemedik   
                    ValidateIssuer=true,
                    //Bu token in dagiticisi,saglayicisi kim,bunun da validasyonunu yap
                    ValidateLifetime=true,
                    //Mutlaka lifetime i kontrol et diyoruz,lifetime tamamlandiysa token expirse olsun ve token kullanilamasin
                    //yetkiloendirmeyi kapat, erisilemesin o token uzerinden artik
                    ValidateIssuerSigningKey=true,
                    //Burasi da token i imzalayacagimiz, kriptolayacagimiz,anahtar key, bunu da kontrol et diyoruz
                    //Issuer demek sertifikayi veren, cikaran demek
                    ValidIssuer=Configuration["Token:Issuer"],
                    //Bu token in olusturulurken ki issuer lari sunlardir
                    //Token IConfiguration dan geliyor
                    ValidAudience=Configuration["Token:Audience"],
                     IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
                     ClockSkew=TimeSpan.Zero,
                     //Token i ureten sunucunun var oldugu timezone ile token sagladgi client in timezonu birbirinden farkli
                     //oldugu durumda,token in tum zone larda adil birsekilde kullanilabilmesi icin ClockSkew parametresini
                     //kullaniriz,Token in expiration time i uzerine ne kadar time koyacagimizi beliritiyor, biz su an boyle bir
                     //concern ilgimiz olmadigi icin zero verdik yani dogrudan expire time nekadar ise o kadar sure sonunca token
                     //invalid olsun demis oluyoruz

                  
                };
             });    
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
            services.AddDbContext<BookStoreDbContext>(options =>
            options.UseInMemoryDatabase(databaseName:"BookStoreDB"));
            services.AddScoped<IBookStoreDbContext>(provider=>provider.GetService<BookStoreDbContext>());    
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); 
           // services.AddSingleton<ILoggerService,ConsoleLogger>();   
            services.AddSingleton<ILoggerService,DBLogger>();   
            //DbContext i biz servis olarak eklemisiz burda, ok ama biz IBookStoreDbContext i 
            //Scope olarak ekleyecegiz,Scope oalrak ekelmek demek, inject edilen servisin sadece
            //request lifetime icerisinde yasiyor olmasidir,Bir request geldiginde
            //IBookStoreDbContext i implente eden ve bizim burdas ekleyecegizm somut siniftan
            //bir instance olusturyor ve o request te istinaden bir response donene kadar yasiyor
            //response donup bittigi anda olusturulan instance yok ediliyor ve tekrar yeni bir 
            //request geldginde tekrar yeniden IBookStoreDbContext e karsilik gelen instance olusturuluyor
            //ve o request-response islemi sona erene kadar yasamini surduruyor
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

             app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
//Custom middleware imz dir burasi
            app.UseCustomExceptionMiddle();
//Burasir request end-pointe dusme yeridir, end-pointlerin calisma yeri...
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
