using System;
namespace DateTimeAndMath{
internal class Program
{
    private static void Main(string[] args)
    {
     //  UsageOfDateTime.ShowDateTimeExamples();
     
     //Math kutuphanesi-
     //Cok fazla ihityac duydugumuz ve Math kutuphanesi fonksiyonlarini kullandigmiz zaman bizi cok ciddi rahatlatan bir kutuphaendir..Gunluk hayatta cok ihtiaycimiz olacak, ondan dolayi bu kutuphaneyi de iyi bilmemiz gerekir
     Console.WriteLine(Math.Abs(-24));//+24 tur cevabimiz...Abs mutlak demektir biz - de versek, onu + kabul eder. Ornegin karsimiza gelecek olan algoritmalarda eger kullanicinin girecegi deger - de olsa onu +  kabul etmemiz gerekirse iste Math.Abs metodu bu is icin kullanilabilir ve bunun gibi isler icin
    Console.WriteLine(Math.Sin(10));//10 un sinus karsiligini verir
    Console.WriteLine(Math.Cos(10));//10 un cosinus karsiligini verir
    Console.WriteLine(Math.Tan(10));//10 un tanjant karsiligini verir
    
    //Kusuratli verilen sayiyi bize verilen algoritmadaki talebe gore asagi veya yukari yuvarlayabilmemizi saglayacaktir....
    //double,float,decimal gibi degerler aliyor paramtresine
    Console.WriteLine(Math.Ceiling(10.1));//10 un sinus karsiligini verir-11-Herzaman bir ustteki tam sayiyi verir
    Console.WriteLine(Math.Round(10.6));//10.6 ve ustu olunca bir ustteki sayiya tamamlar, 10.5 ve alti olunca da bir alt sayiya tamamlar
    Console.WriteLine(Math.Round(10.4));//10.6 ve ustu olunca bir ustteki sayiya tamamlar, 10.5 ve alti olunca da bir alt sayiya tamamlar
    Console.WriteLine(Math.Floor(10.8));//Bu her harukarda alttaki tam sayiyi veriyor

    //Bir sayinin Min va max i bulma
    Console.WriteLine(Math.Min(14,76));//14
    Console.WriteLine(Math.Max(14,76));//76

    //Pow ile us islemi yapma
    Console.WriteLine(Math.Pow(3,4));//3^4 u verir. 3*3*3*3
    //Sqrt ile karekok islemi yapma
    Console.WriteLine(Math.Sqrt(81));//81 in karekoku 9 dur
    Console.WriteLine(Math.Log(9));//9 un e tabanindaki logaritmik karsiligi
    Console.WriteLine(Math.Log10(10));//10 un log10 tabanindaki logaritmik karsiligi-1
    Console.WriteLine(Math.Exp(3));// e^3 u verir
    }
}

public static class UsageOfDateTime{

    public static void ShowDateTimeExamples(){
         //DateTime class ina ait methodlari cok fazla kullanacagiz ve real hayatta cok fazla ihtiyacimiz olan bir class tir...Ondan dolayi bunun nasil kullanildigini bilmek, methdlarina hakim olmak bizim isimizi cok ciddi  kolaylastiriyor. Ondan dolayi bunlari iyi bilmemiz gerekiyor...
        //Burda saati getirirken bilgisayar hangi dile gore ayarli ise o saate gore kendisini ayarlayacaktir..
        Console.WriteLine(DateTime.Now);//28.05.2022 22:36:16
        Console.WriteLine(DateTime.Now.Date);//28.05.2022 00:00:00
        Console.WriteLine(DateTime.Now.Day);//28
        Console.WriteLine(DateTime.Now.Month);//5
        Console.WriteLine(DateTime.Now.Year);//2022
        Console.WriteLine(DateTime.Now.Hour);//22
        Console.WriteLine(DateTime.Now.Minute);//36
        Console.WriteLine(DateTime.Now.Second);//16

        //Haftanin gununu string olarak almak
        Console.WriteLine(DateTime.Now.DayOfWeek);//haftanin hangi gunundeyiz onu getirir.Saturday
        Console.WriteLine(DateTime.Now.DayOfYear);//Yilin kacinci gununde isek o sayiyi getirir.148.gunu
        Console.WriteLine(DateTime.Now.ToLongDateString());//lordag 28. mai 2022
        Console.WriteLine(DateTime.Now.ToShortDateString());//28.05.202 diye getirir
        Console.WriteLine(DateTime.Now.ToLongTimeString());//22:45:50
        Console.WriteLine(DateTime.Now.ToShortTimeString());//22:45
        //Add Methodlari
        Console.WriteLine("Add metotlari");
        Console.WriteLine(DateTime.Now.AddDays(2));
        Console.WriteLine(DateTime.Now.AddHours(3));
        Console.WriteLine(DateTime.Now.AddSeconds(30));
        Console.WriteLine(DateTime.Now.AddMonths(5));
        Console.WriteLine(DateTime.Now.AddYears(10));
        Console.WriteLine(DateTime.Now.Millisecond);
        Console.WriteLine(DateTime.Now.AddMilliseconds(50));//100 milisecond 1 saniyedir

        Console.WriteLine("Data Format: ");
        //COOK ONEMLI-BESTPRACTSE
        //Bu arada yine cok fazla ihtiyac duyacagimiz Tarih formatlari bolumudur.Ornegin dd.mm.yy seklinde almak isteyebilirz ve aralarda nokta ., tire -, veya da / yan cizgi olsun isteyebilirz veya ornegin format icinde ay kisaltilmis olarak gelsin vs gibi bircok istegimiz olabilir...
        //ToString i formatlamak icin kullaniyoruz...dikkat edelim..
        Console.WriteLine(DateTime.Now.ToString("dd"));//dd-Bugun ayin gunleri ne ise onu getirir- 28 mayisin 28 ini getirir
        Console.WriteLine(DateTime.Now.ToString("ddd"));//lor.
        Console.WriteLine(DateTime.Now.ToString("dddd"));//lordag

        Console.WriteLine(DateTime.Now.ToString("MM"));//05
        Console.WriteLine(DateTime.Now.ToString("MMM"));//mai- ayni kisa yazilisini getirir
        Console.WriteLine(DateTime.Now.ToString("MMMM"));//mai- ayni uzun yazilisini getirir

        Console.WriteLine(DateTime.Now.ToString("yy"));//22- year kisa halini getirir
        Console.WriteLine(DateTime.Now.ToString("yyyy"));//2022-year uzun halini getirir
        
        Console.WriteLine("Spesifik date");
        //DateTime e belirli tarih atayarak o tarihi alalim
        DateTime dt1=new DateTime();
        Console.WriteLine("dt1: "+dt1);
        //assigns year, month, day
        DateTime dt2=new DateTime(2015, 12,24);
        Console.WriteLine("dt2: "+dt2);
        //assigns year, month, day, hour, min, seconds
         DateTime dt3 = new DateTime(2015, 12, 31, 5, 10, 20);
        Console.WriteLine("dt3: "+dt3);
        //assigns year, month, day, hour, min, seconds, UTC timezone
        DateTime dt4 = new DateTime(2015, 12, 31, 5, 10, 20, DateTimeKind.Utc);
        Console.WriteLine("dt4: "+dt4);
        //DateTime Static Fields
        //The DateTime struct includes static fields, properties, and methods. The following example demonstrates important static fields and properties.
        DateTime currentDateTime = DateTime.Now;  //returns current date and time
        DateTime todaysDate = DateTime.Today; // returns today's date
        DateTime currentDateTimeUTC = DateTime.UtcNow;// returns current UTC date and time

        DateTime maxDateTimeValue = DateTime.MaxValue; // returns max value of DateTime
        DateTime minDateTimeValue = DateTime.MinValue; // returns min value of DateTime

        //TimeSpan
        //TimeSpan is a struct that is used to represent time in days, hour, minutes, seconds, and milliseconds.
        //Bir tarihe baska bir tarih ekleme....
        DateTime dt = new DateTime(2015, 12, 31);
           
        TimeSpan ts = new TimeSpan(25,20,55);//25 saat, 20 dakika ve 55saniye demektir...
        
        DateTime newDate = dt.Add(ts);//Bunu dt tarihine ekliyor....

        Console.WriteLine(newDate);//1/1/2016 1:20:55 AM
      //  Subtraction of two dates results in TimeSpan.
      //Bir tarihten baska bir tarih cikarma....
      DateTime dt5 = new DateTime(2015, 12, 31); 
    DateTime dt6 = new DateTime(2016, 2, 2);
    TimeSpan result = dt6.Subtract(dt5);//33.00:00:00
    //Operators
    //Tarihler arasi cikarma, ekleme karsilastirma vs islemleri
    //The DateTime struct overloads +, -, ==, !=, >, <, <=, >= operators to ease out addition, subtraction, and comparison of dates. 
    //These make it easy to work with dates.
    DateTime dt7 = new DateTime(2015, 12, 20);
    DateTime dt8 = new DateTime(2016, 12, 31, 5, 10, 20); 
    TimeSpan time = new TimeSpan(10, 5, 25, 50);//10 gun, 5saat 25 dakka, 50 saniye
    //Belli bir tarihe bellli bir sureyi ekleyebiliyoruz asgidaki gibi
    Console.WriteLine(dt8 + time); // 1/10/2017 10:36:10 AM
    Console.WriteLine(dt8 - dt7); //377.05:10:20
    Console.WriteLine(dt7 == dt8); //False
    Console.WriteLine(dt7 != dt8); //True
    Console.WriteLine(dt7 > dt8); //False
    Console.WriteLine(dt7 < dt8); //True
    Console.WriteLine(dt7 >= dt8); //False
    Console.WriteLine(dt7 <= dt8);//True
    //Convert String to DateTime
    //A valid date and time string can be converted to a DateTime object using Parse(), ParseExact(), TryParse() and TryParseExact() methods.
    //The Parse() and ParseExact() methods will throw an exception if the specified string is not a valid representation of a date and time.
    // So, it's recommended to use TryParse() or TryParseExact() method because they return false if a string is not valid. 
        var str = "5/12/2020";
        DateTime dt9;           
        var isValidDate = DateTime.TryParse(str, out dt9);
        if(isValidDate)
            Console.WriteLine(dt9);
        else
            Console.WriteLine($"{str} is not a valid date string");
    }
}
}
