using System;
namespace hataYonetimi{
internal class Program
{
    private static void Main(string[] args)
    {
        //Burda hataya neden olacak bir senaryo olusturalim
       //      Console.WriteLine("Bir sayi giriniz");
     //   string? text=Console.ReadLine();//Burda kullanici hicbirdeger girmeme durumu da soz konusu oldugu icin C# Console.ReadLine() dan donen degeri nullable yapmistir
        //Ama biz nullable olan degeri sadece string olarak almaya calisirsak tabi ki bir hata soz konusu oluyor ondan dolayi biz de string i nullable yaptik...
        //text i integer a cevirirkende text null gelirse cevirme islemi hata verir o zaman da bu isi kontrollu yapmak gerekir ki uygulamamiz kirilmasin....,
     
        //Hata yonetimini bu sekilde de yapabiliriz ama biz im if else degil de try-catch kullanmamiz burda daha dogru olur cunku, uygulamamiz kirilma durumu var, eger uygulamamizda kirilma durumu var ise try-catch kullaniriz oyle bir durum yok ise o zaman if-else kullaniriz...
        // if(text is not null){
        //       int number1=Int32.Parse(text);  
        // }else{
        //     Console.WriteLine($"{text} null  oldugu icin int e cevrilemez.");
        // }
         
        try
        {
             Console.WriteLine("Bir sayi giriniz");
            string? text2=Console.ReadLine();
            int number2=Int32.Parse(text2);  
             Console.WriteLine($"Girmis oldugunuz sayi: {number2}");

        }
        catch (Exception ex)//Boyle genel hata mesaji verilemez...
        {
           // Console.WriteLine($"{text2} null  oldugu icin int e cevrilemez.");
           //Burda text2 kullanilmaz cunku buraya dusme sebebi zaten text2 yi alamamasi, text2 nin farkli formatta yazilmasi sebebi ile problem yasanmasi
           // throw new NullReferenceException(text);
            Console.WriteLine("Hata: "+ ex.Message.ToString());
        }finally{//Burasi her harukarda calisacak...Bazi kodlar vardir biz hem try durumunda hem de catch durumunda calistirmamiz gerekir o zaman ayri ayri hem try tarafinda hem de catch tarafinda yazmak gerekecekti onun yerine finally ile bir kez yazmis oluruz ve do not repeat your self...
            Console.WriteLine("Islem tamamlandi");
        }

        Console.WriteLine("----------------");
        try
        {
          //  int a=int.Parse(null);//Bu ArgumanNullException firlatmasinin isteyebilirz cunku bizim hatamiza tam olarak uyuyor...
           //  int a=int.Parse("test");//Bu deger de int a cevrilecek deger degildir dolayisi ile formatexception hatasi verecektir     
            //girilen int degerinin min ve max degeri uzerinde olmasi-overflow hatasi...   
            int a=int.Parse("-20000000000000000000000");
        }
       catch (ArgumentNullException ex){
            Console.WriteLine("Hicbir deger girmediniz");
            Console.WriteLine(ex);
       }
        catch (FormatException ex)
        {
            Console.WriteLine("girilen deger dogru formatta degildir...");
            Console.WriteLine(ex);//Value cannot be null. (Parameter 's')
            //Hatalari yazdirmak cook onemlidir bunlari hicbirsekilde loglayip biryere yazdirmazsak file olur, database olur nereye ise artik yazdirmayip handle etmez isek kodlarimiz arttikca projemiz buyukce artik ilk basta kucuk olan o hata problemlerini ayirt edemez duruma gelebiliriz dolaysi ile de biz projemizi buyuturken sorunun nerden kaynaklandigini bilmek icin, bunlar herzaman bize bizim anlayacagimiz sekilde hata mesaji verecek sekilde handle etmeliyiz ki hep o hatalar kontrolumuzde olsun ve bizde proje buyudukce hic aklimiza gelemyecek yerden cikan hatalari bulmmak icin bir suru vaktimizi harcamayalim....
            //throw new ArgumentNullException();
            //Hatamizin kacinci satirdan kaynaklandigini da alabiliyoruz bu sekilde
        }catch(OverflowException ex)
        {
            Console.WriteLine("girilen deger int in min ve max imium degeri disindadir");
            Console.WriteLine(ex); // Value was either too large or too small for an Int32.Bize bunu soyeleyen logdur bu hata kaydediliyor ki loglama islemi 
            //programlamada cook onemlidir, hayati oneme sahiptir...
        }finally{
            Console.WriteLine("Islem basari ile tamamlandi");
        }
    }
}
}
/*
Firstly, you are seeing this message because you have the C# 8 Nullable reference type feature enabled in your project. Console.ReadLine() returns a value that can be either a string or null but you are using it to set a variable of type string. Instead either use the var keyword which will implicitly set your variable to the same type as the return value or manually set your variable as type nullable string string?. the ? denotes a nullable reference type.
You may then want to check that your variable does infact hold a string before you use it:
string? NumInput = Console.ReadLine();
if (NumInput == null)
{
    // ...
}
Bu arada biz back-end de calisiyoruz onu unutmayalim ve biz ui da front-end cinin ne tur bir validation yaptigini bilmiyoruz ama biz hicbiryere guvenmeden kendi
alanimzda olmasi gereken tum tedbirleri alarak, uygulamaizi kullanacak olan front-end ciyi cok guzel ve dogru yonlendirecek sekilde hayat yonetimlerini yapmali ve geri donus hata mesajlari vermeliyiz ki front-end ci de ona gore handle edip kullaniciya bizden gelen verileri guzel birsekilde sunarak uygulamayi handle edebilsin,,,
*/