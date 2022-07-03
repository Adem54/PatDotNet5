using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Entities;

namespace WebApi.DbOperations{
    /*Bu class in bir DbContext olabilmesi icin EntityFrameWorkCore dan DbContext i
     inherit etmesi ve onun ozelliklerini alabilmesi gerekir ve de new lendigi zaman 
     da DbContext e bir parametre gondermesi ve onu da new lemesi gerekiyor ki Database
      baglantisi vs islemleri gerceklestirilmis olsun
  */
    public class BookStoreDbContext:DbContext,IBookStoreDbContext
    {
        //DbContextOptions<> bu da yine Microsoft.EntityFrameworkCore dan geliyor
            public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options):base(options)
            {
                
            }

          public  DbSet<Book> Books {get; set;}
          public  DbSet<Genre> Genres {get; set;}
          public DbSet<Author> Authors {get; set;}
          public DbSet<User> Users {get; set;}

         public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
/*
            DbSet<> Microsoft.Entityframework.Core dan geliyor ve burda entity mizi database de karsiligi 
             olan tablo olan Books a bagliyoruz ve bu yaklasim codefirst yaklasimidir ondan dolayi,
              biz once kodu olusturuyoruz ve bu yazdgimiz kod gidecek bizim veritabanindaki database tablomuzu ousturacaktir
            Isimlendirme standarti olarak genel olarak Entityler tekil, yani entity class larimiz tekil, 
            onlari karsiligi olan veritabani tablolari ise coguldurlar
            Entityframeworkcore burda bizim kod tarafindaki, Book entity imiz ile veritabnindaki
             Books arasinda bir kopru bir baglanti kuruyor
            */
/*
Burda inMemory database kullanacagiimz icin ve get yaptigmizda tamamen bos gelmemsi icin 
simdi icine bir data ekleyecegiz, goredebilmek icin ozellikle
Controllerda datamizi tamamen satatic kullanmistik, burda ise bunu kendimiz kod la yapacagiz,
Bunu yapabilmek icin 1 tane DbOperations altina datagenerator class i yazacagiz
*/