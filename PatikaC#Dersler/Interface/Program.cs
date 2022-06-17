using System;
namespace Interface {
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        ILogger fileLogger=new FileLogger();
        ILogger smsLogger=new SmsLogger();
        ILogger databaseLogger=new DatabaseLogger();
        //If yapmaktan kurtuluyoruz burda
        List<ILogger> loggers=new List<ILogger>{fileLogger,smsLogger,databaseLogger};
        foreach (var item in loggers)
        {
            item.WriteLogg();
        }
        //Biz bir tane LogManager yazmak istiyoruz...ve icerisine hangi log islemi verilirse o log islemini yapacagimiz logManager sinifi yzmak istioruz
        //Biz artik hangi loglama islemini istersek onu yapabiliriz...ayni parametreye yapmak istedigimz loglama isleminin instancesini yazmamiz yeterlidir...Bizim if lerle vs ugrasmaya da ihtiaycimiz kalmamis oldu....
        LoggManager loggManager=new LoggManager(smsLogger);
        loggManager.ApllyLogManager();
        loggManager.WriteLogg();
        //LoggManager sadece bir sistemi yurutuyor bizim hangi log islemini yazdigimz la ilgilenmez o sadece kendi WriteLogg veya ApplyManager icini calistirir arka planda ise, biz paramreye hangi loglama isleminin isntancesini verirsek  o calisacaktir....
        
    }
}
}
/*
Interface leri neden kullaniriz
1-Interface ler ile biz onu implemente edecek class larin sorumluluklairnin cercevesini cizmis oluyoruz
2-En onemlsi de interface ler sayesinde biz uygulamamizin mevcut kodlarina dokunmadan yeni ozlellikler ekleyerek uygulamamizi genisletebiliyoruz..
3-Genel cercevesi ayni olan daha dogrusu fonksiyonel olarak ayni isi yapacak olan class lar icerisiklerinde farkli isleri yapsalar bile ayni basliktaki ayi imzaadaki methjodlari kullanacaklari icin aslinda ayni interface leri de kullanabilirler.
Esas ana noktamiz uygulamamizi SOLID prensiplerine uygun bir sekilde daha kolay kontrol edebilmemizi sagliyor... ve de uygulamamizi buyuturken bize cok ciddi kolaylik sagliyor
4.Ozellikle bizi if bloklari arasindas bogulup kalmaktan, sipagetti kod yazmaktan kurtarir...Yani if kullanimi sadece belli sayida condition larin oldugu ve yeni condition eklenmeyecegini bildgimiz yerlerde kullanilabilir ama yeni ozelliiklerin gelme durumu var ise o zaman ne yapacagiz her yen i ozellikte if conditiona a yeni bir tane else if mi ekleyecegiz...onun yerin if condition icin yazdigmiz mehtodlari class haline getir...Sonra bunlarin hepsini ayni interface i implement ettir ve de bu methjodlarini hepsinin islemlerini bir tane class ta topla ve o class i methodlarin hepsine dependency injection ile gonder ve her birisi ayni metod ismi altinda ortak interfacee den gelen, kendi methodlarini kendi islemlerini invok etsinler ve  bu sekilde if condition larinin durumnuna gore kullandigimnz tum condition durumlarini da bu  method class larinin icinde property haline donustur..Ve de son olarak, da ayni interface i  implemente etmis class lar bir nevi ayni tip te olmus olurlar ve o interface tipi kullanilarak ayni liste icinde toplayabiliriz....Ve de bir dongu ile oncew kullanicidan glecek deger alinir sonra liste icinden bu deger hangi metodda var ona bakilir ve ve o class in instancesi alinir ve de hepsi de zaten ayni isimde method u claistiridigi icin ayni isimde methodu otomatik calisitirizi ve bu sekilde if condition innda kurtulmjus oluruz...
-Yeni ozellik eklenecegi zmaan yani bir isletmede farkli musteriler var bir de yeni bir musteri turu cikti ogrenciMusteri peki ben  nerden bilecegim musteri min ogrenci musteeri  mi oldugunu, gidip Musteri class i icide isStuden diye bool bir property yazmak akillica degil cunku yarin oburgun birde ogretmen cikarsa ona da gidip isTeacher mi yazacagiz hayir bu surdurulebilir degil o zaman iste interfaca bizi bu tarz bool yazip if kullanmaktan da kurtariyor onun yerine gid yeni bir Student class i olustur ayni interfadce i implemente ettir...
-Ayrica interface bir tiptir is ile kontrol edilebilir  bir tipdir ve de bir class a birden fazla interface implemente edebiliyor....Yani biz bu ozellikl uzerinden de muthis fleksibel isler yapabiliuyoruz...
-Birde interface leri biz isaretleme tekniginde de kullanabiliriz yani sirf ornegin bizim veritabani islerinde kulllandacagimz entity lere bir IEntiy vererek onu implemente eden tum class lariin Entity classs i oldugunu anlariz ve de GEneric Repostory deisgn pattern generic constrainterler de bu ozellik sayesinde sadece veritabanina muhatap olan class lar o tipler kullanilabilsin diye where <IEntity> seklinde constraint yazabiliriz....MUthis ozelliklerdir...
*/
