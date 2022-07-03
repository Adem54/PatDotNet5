using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace WebApi
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
            //JwtBearer-Jwt tasiyici,Jwt yi eliinde bulunduran
            //Jwt-Json Web token
            //Burda biz JsonWebToken elinde bulunduran tasiyiciyi ekle diyoruz ve 
            //Eklerken de nasil olacagini ne sartlara sahip olacagini, hangi validasyon 
            //parametreleri olmasi gerektiggini default olarak belirliyoruz ki, jwt token 
            //uretilgidi zaman burdaki validasyon parametreleri ile kiyaslanacak uretilen
            //token burda verilen validasyon parametrelerinie uyuyor mu diye....
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt=>{
                opt.TokenValidationParameters=new TokenValidationParameters()
                {
                    //Diyor ki bunun bir clienti olacak,true
                    //Burda tabirici caiz se jwt token in kurallari yaziliyor...default olarak
                    //Bunu tabi, gidip jwt dokumanindan da bulabilirz
                    ValidateAudience=true,
                    //Bu token in bir dagiticisi saglayicisi olacak diyoruz
                    ValidateIssuer=true,
                    //Lifetime i var baslama ve bitis suresi-expiration time
                    ValidateLifetime=true,
                    //Burda da kullanici bilgileri kriptolanarak, bir key olusturulacak
                    //Bu da kontrol edilmeli diyoruz
                    ValidateIssuerSigningKey=true,
                    //Simdi burda token ile ilgili valid degerler, icinde olmasi gereken valid 
                    //degerler yaziliyor, bunlari saglayacak olan, Configuration class idir ve bunlar da appsettings.json dan alacak
                    //Oraya da tabi bu datalari eklememiz gerekecek
                    ValidIssuer=Configuration["Token:Issuer"],
                    ValidAudience=Configuration["Token:Audience"],
                    //Burda saglayicinin kullanici bilgilerini bir SimetreikGuvenlik imzasi ile encode etmesi gerekecek
                    //Encode etme islemini de standart encode etme islemlerinden biri ile yapacak
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
                    ClockSkew=TimeSpan.Zero,//Bu da token saglayan server timezonu ile client timezonu farkli oldugu durumda, token
                    //in tum zone larda adil bir sekilde kullanlabilmesi icin kullanilir
                };
            });    
            //Kisacasi token uretilme ile ilgili class ve mehtod yazarken iste burdaki protokoller izlenecek 
            //burdaki validation ve valid degerlerine uyacak sekilde olusturulmalidir....
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            //Startup.cs içerisinde ConfigureServices() içerisinde DbContext'in servis olarak eklenmesi
            //DbContext olarak BookStoreDbContext i ekle ve ben bunu inmemory kullanmak istiyrouz...
            //BookStoreDbContext imizin uygulama icerisinde gorunebilmesi icin, kullanabilmemiz icin servislere DbContext olarak gidip eklemesini soyledik ve artik bunu yaptigm icin ben uygulama icinde herhangi biryerde BookStoreDbContext i alip constructor aracgilig ile enjekte edip istedgim yerde kullanabilirim...
            services.AddDbContext<BookSellerDbContext>(options =>
            options.UseInMemoryDatabase(databaseName:"BookSellerDB"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Alttaki middleware lerin hepsi IApplicationBuilder in bir extension methodudur
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
