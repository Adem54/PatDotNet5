
using linqDemo;
using Microsoft.EntityFrameworkCore;

namespace linqDemo {

public class LqDbContext:DbContext {
 public DbSet<Student> Students {get; set;}
        //bu aslinda bir property dir, DbSet tipinde yani
        // IQuarayble tipinde yani IEnumerable tipinde yani linq ile islem yapma turundedir...


        //Biz burda Entityframeworkcore un inmemory database ini kullanacagiz
    //WebApi projemizde Startup dosyasi icerisinde server ayaga kalkarken olustururken inmemory database in uygulanmasi
    //gerektigini belirtmis idik...ama bu projede console projesinde onu burda verecegiz, burda soyleyecegiz...

    //DbContext icerisinden gelen bazi override methodlar var, onlardan bazilarini override ederek biz burda
    //uygulama calisirken inmemory database kullanacagimizi soyleyebilriz..
    //Inherit ettgimiz bu tarz class lar icindeki virtual methodlari , normal methodlari,prpertiesleri hepsinden faydalanabiliriz 
    //Bir console uygulamasina bu sekilde, entity frameworku projemize dahil etmeyi ogreniyoruz...
    //Bu konfigurasyonu biz, database first mantiginda yaptigmiz projemizde kullanmistik...
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            optionsBuilder.UseInMemoryDatabase(databaseName:"LinqDatabase");
    }
    /*Biz burda kullanacagimiz bir bileseni kutupaheneyi, normalde 
    Startup.cs de
      services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName:"BookStoreDB")); 
      bu sekilde configure etmis idik iste o isi bu uygulamada direk DbContext icindeki bir override methodu araciligi ile yapyoruz
      Bu yontemi biz database first mantiginda da normal database Mssql kullanirken de yapabiliyoruz asagidaki da bir ornegi

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectsV13;Database=ReCap;Trusted_Connection=True");
        }

        public DbSet<Car> Cars { get; set; }
      */
    //Su an bizim contextimiz LinqDatabase ismi ile inmemory olacak sekilde hazirdir kullanabilirz.Ama su an bizim data miz eksik
    //Bir data generate etmemiz gerekiyor...Onn icin bir de DataGenerator.cs dosyasi olusturacgiz
}

}