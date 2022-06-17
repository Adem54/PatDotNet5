
using System;
namespace EnumYapilar{
internal class Program
{
    private static void Main(string[] args)
    {
        //Enum-Enumeration in kisaltmasidir, siralamalar demektir
        //Ardisik index lerde calismak zorunda oldugunuzda cok kolaylastiriyorlar
        //Birden fazla sabite ayni anda ihtiyaciniz varsa yani degeri belli olan ifadeler
        //Haftanin gunleri gibi
        //Kodun okunabilirligini arttirmak
        //Tek tek degisken olusturup deger atamaktansa enum uzerinden direk verilebilir
        //Birde eger bizim elimizde ne oldugu belli olan ozellikle string degerler var ve biz kullanicinin bu degerler icinde bir deger girmesini beklersek o zaman kullanici gidip de en ufak bir harf vs hata durumunda yanlis girecektir bunu onloemek icin de ozellikle biz degiskeni field veya property yi eger enum dan alinmasi gereken bir deger olarak ayarlarsak kullanicida o enum degerleri icindeki degerlerden birini girmek zorunda kalir ve bu sekilde de kullanicinin basit yazim hatalarindan vs yanlis bir deger girmesini engellemmis oluruz...
        //Index inin ardisik olarak arttigi ve hic degismeyecegini dusunudugmuz degerler neler ise onlari enum olarak kullanabilirz
        //Ornegin haftanin gunleri
        Console.WriteLine(Days.Søndag);//Sondag yazacaktir 
        //Bizim enum icinde string olarak yazmamiz ciktiyi string olarak verdigi anlamnina gelmiyor
        //Enum un elemaninin adini belirliyoruz ve index ler 0 dan basliyor aslinda numeric olan veriyi anlamli bir sekilde tutmus oluyoruz...
        //Eger hicbirsey yapmazsak index olarak default ta Mandag-0-Tirsdga-1-Onsdag-2,Torsdag-3,Fridag-4,Lørdag-5,Søndag-6
        //Ama biz index leri de degistirmek istersek degistirebilriz
        //Mandag=1,Tirsdag=2,Onsdag=3,Torsdag=4,Fridag=5,Lørdag=6,Søndag=7
        //Eger biz numeric deger ihtiyacimiz olursa nasil aliriz.Ornegin cumartesi hangi index e denk geliyor onu nasil aliriz
        Console.WriteLine((int)Days.Lørdag);//24- casting ile bu islemi yapabiliriz.
        //Normalde her zaman bir ardisik gider ancak mesela biz gittik Mandag=1 ve  Cuma ya 23 verdik o zaman da 
        //Mandag=1,Tirsdag=2,Onsdag=3,Torsdag=4,Fridag=23,Lørdag=24,Søndag=25 seklinde gidecektir
        Console.WriteLine((int)Days.Søndag);//25- casting ile bu islemi yapabiliriz.
        //Peki bunu biz sabit degerleri tutmak icin tutmak istersek, anlamli oludgunu dusunudugjmuz degerlerde bize cok yardimici oluyor
        //Console dan readLine veya kullanicidan form dan aldgmiz sicaklik degeri ni alip kullaniciya bilgi verelim
        int temp=32;
        if(temp<=(int)WeatherCondition.normal){
            Console.WriteLine("Disariya cikmak icin havanin biraz daha isinmasini bekleyelim");
        }
        else if(temp>=(int)WeatherCondition.warm){
            Console.WriteLine("Disariya cikmak icin cok sicak bir gun");

        }else if((temp>=(int)WeatherCondition.normal) && (temp>=(int)WeatherCondition.hot) ){
            Console.WriteLine("Disariya cikmak icin gayet uygun bir hava var");
        }
        //Burdaki puf nokta biz disardan bakilinca sayilarla ugrasmamiza ragmmen kod blogunun ne is yaptigni cok kolay bir sekilde anlayabiliyoruz
    }
}
enum Days{
    Mandag=1,Tirsdag,Onsdag,Torsdag,Fridag=23,Lørdag,Søndag
}

//Uygulama icerisinde kullanacagimiz normlarini, kritetleri belirliyoruz aslinda
enum WeatherCondition{
    cold=5,
    normal=20,
    warm=25,
    hot=30
}
}
