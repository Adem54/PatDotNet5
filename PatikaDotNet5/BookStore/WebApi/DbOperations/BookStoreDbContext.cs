using Microsoft.EntityFrameworkCore;
using System;
namespace WebApi.DbOperations{
    /*Bu class in bir DbContext olabilmesi icin EntityFrameWorkCore dan DbContext i
     inherit etmesi ve onun ozelliklerini alabilmesi gerekir ve de new lendigi zaman 
     da DbContext e bir parametre gondermesi ve onu da new lemesi gerekiyor ki Database
      baglantisi vs islemleri gerceklestirilmis olsun
  */
    public class BookStoreDbContext:DbContext
    {
        //DbContextOptions<> bu da yine Microsoft.EntityFrameworkCore dan geliyor
            public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options):base(options)
            {
                Console.WriteLine("Burasi da DbContext constructor");
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
          public  DbSet<Book> Books {get; set;}
    }
}
/*
Burda inMemory database kullanacagiimz icin ve get yaptigmizda tamamen bos gelmemsi icin 
simdi icine bir data ekleyecegiz, goredebilmek icin ozellikle
Controllerda datamizi tamamen satatic kullanmistik, burda ise bunu kendimiz kod la yapacagiz,
Bunu yapabilmek icin 1 tane DbOperations altina datagenerator class i yazacagiz
*/