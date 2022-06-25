using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DbOperations{
    
    public class Datagenerator{

        /*Icerisinde verileri insert eden initialize methodum olacak
        IServiceProvider aliyoruz InMemory den, ve bu ServiceProvider araciligi 
        ile(biz bunu gidip Program.cs icine baglayacagiz ve program.cs kendi 
        icerisindeki ServicProvider ile buray i cagirarak gidip 1 tane uygulama 
        ilk ayaga kalktiginda hep calisacak bir yapi olusturacagiz burda,
         bu da serviceprovider sayesinde gerceklesiyor )
        */
        public Datagenerator(){
            Console.WriteLine("Datagenerator da cconstrctor da yiz");
        }
        
        public static void Initialize(IServiceProvider serviceProvider){
            //Burda olusturdugmz BookStoreDbContext in bir instancesine ihtiyacimiz var,
            // cunku bilgilerimizi databaseimize kaydeecegiz bunu da BookDbContext araciligi ile yapacagiz

            //using ile sunuu diyoruz bu scope icinde context nesnesini kullanabilirsin demis oluyoruz....
            using (var context=new BookStoreDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
                {
                    //BESTPRACTISE....
                        //Uygulama ilk ayaga kalktiginda database e bir kez veri yazilmasi gerekiyor
                        //Ilk uygulama basladiginda database i bir kontrol edelim veri var mi diye 
                        //bu herzaman ve heryerde bu mantikladir, eger bir veritabani mantiginda dizi,
                        //localStorage veya bu sekilde inmemorydatabase ilk girdigmizde bir kontrol
                        // etmemiz gerekir data var mi diye ona gore data ekleyelim veya iptal edelim
                        //Any() methodu harika bir methoddur C# da kullandigmiz, cok ismize yarayacak iyice hakim olmak da fayda var
                        //Any() methodu javascriptteki some() methodu ile ayni mantikta calisir...
                        if(context.Books.Any()){
                            return;//database icine veri var ise direk return et ve bitir methodu diyoruz...
                        }

                        context.Authors.AddRange(
                              new Author{
                                FirstName="Margaret",
                                LastName="Atwood",
                                BirthDate=new System.DateTime(1972,03,14)
                              },
                              new Author{
                                FirstName="Jokha",
                                LastName="Alharthi",
                                BirthDate=new System.DateTime(1977,07,04)
                              },
                              new Author{
                                FirstName=" Achuthan",
                                LastName="Namboodri",
                                BirthDate=new System.DateTime(1982,11,18)
                              }  

                        );


                        //isActive default oalrak true gelecek
                        context.Genres.AddRange(
                            new Genre{
                                 Name="Personal Growth"   
                            },
                            new Genre{
                                 Name="Science Fiction"   
                            },
                            new Genre{
                                 Name="Romance"   
                            });

                    //Liste icine liste veya dizi eklerken AddRange kullaniriz...
                    // Ayrica biz birden fazla elementi yanyana da ekleyebiliyoruz AddRange ile
                        context.Books.AddRange(
                            //Id leri Book.cs entity frameworkunde auto-increment bir sekilde ayarladigmiz 
                            //icin artik data eklerken id vermemize gerek yok kendisi yeni data geldiginde 
                            //otomatik olarak artacak sekilde id atamasi yapacak
                                 new Book{
                                        // Id=1,
                                        Title="Lean StartUp",
                                        GenreId=1,//Personal Growth,
                                        AuthorId=1,
                                        PageCount=200,
                                        PublishDate=new System.DateTime(2010,05,23)
                                    },
                                    new Book{
                                        // Id=2,
                                        Title="Herland",
                                        GenreId=2,//Science Fiction,
                                        AuthorId=2,
                                        PageCount=250,
                                        PublishDate=new System.DateTime(2011,07,13)
                                    },
                                    new Book{
                                        // Id=3,
                                        Title="Dune",
                                        GenreId=3,//Romance
                                        AuthorId=3,
                                        PageCount=540,
                                        PublishDate=new System.DateTime(2004,02,6)
                                    }   
                                );
                                //EntityframeworkCore Unitof work mantiginda calisiyor tum degisklikler 
                                //bitip tum yapacagimz surecler bittikten sonra database e yaziyor, 
                                //ayni commitlenmek gibi dusunebilirz,
                                /*
                                The unit of work in C# implementation manages in-memory database CRUD operations 
                                on entities as one transaction. So, if one of the operations is failed then the
                                 entire database operations will be rollback. Unit of Work in C# is the 
                                 concept that is related to the effective implementation of the Repository Design Pattern.
                                */
                                //Dolayisi ile de tum islemlerimizi bitirdikten sonra en son SaveChanges islemi yapilmalidir, entityframeworkcore da
                                context.SaveChanges();
                             
                                
                }
        }
    }
}

/*
Biz data generator class imzi ve initialize methodumuz yazdik ancak, bunun 
calismasi icin de biryerden tetiklenmesi, gerekiyor...
Simdi biz yukarda serviceProvider ekledik ve bunu program cs de kullanacagiz
 cunkiu uygulama ilk olarak Program.cs de ayaga kalkiyor ve ilk olarak calisan yer
  Program.cs dir ve ben bu uygulamami da uygulamanin her ayaga kalktiginda 
  veritabanina bu veriyi atmasi icn bu methodu uygulama her ayaga kalktiginda
   calistirmasini istiyoruz o zaman Program.cs icersine datagenerator u eklememiz gerekiyor..Burasi onemli...
*/