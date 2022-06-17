using System;
namespace Polimorfism{
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Transport transport=new Transport();
        Car car=new Car();
        Fly fly=new Fly();
        Boat boat=new Boat();
        car.MoveFromNorwayToSweden();
        fly.MoveFromNorwayToSweden();
        boat.MoveFromNorwayToSweden();
    }
}

public  class Transport{
    public  virtual void MoveFromNorwayToSweden(){//DEfault olarak icini doldurduk...Bu sinifi inherit eden class ister default olarak calistirir isterse tamamen kendi iceriklerini doldurur isterse de hem default u hem de kendi iceriklerini doldururlar
            Console.WriteLine("Isvece araba ile gidiyoruz...");
    }
}

public class Car:Transport{

public override void MoveFromNorwayToSweden(){
    base.MoveFromNorwayToSweden();//Default halini calisitir demektir bu....
    Console.WriteLine("Norvecten Isvece kara yolu ile giderken aracla bir feribota binip kisa bir sure feribot ile gidecegiz....");
}
    
}

public class Fly:Transport{
    public override void MoveFromNorwayToSweden(){
        Console.WriteLine("Isvece ucarak gidiyoruz");
    }
}

public class Boat:Transport{
    public override void MoveFromNorwayToSweden(){
        Console.WriteLine("Isvece deniz yolu ile gidiyhoruz...");
    }
}

}
/*
Polimorfism cok bicimciligi virtual anahtar kelimesi ile sagliyoruz...Ana baslikta ayni isi ama farkli sekillerde yaptgimiz durumlarda. Ornegin ucak,araba,gemi ucu de mesela Norvecten, Isvece gidiyor ucu de gitme eylemi methodunu gerceklestiriyor ama metodun iceriginde nasil gerceklestiriyor hepsi bu eylemi kendi yontemi ile gerceklestiriyor birisi hava yolu ile birisi kara yolu ile birisi de deniz yolu ile....
Veya yine ayni mantikta biz bir uygulama yapiyoruz ayni uygulamayi musterilerimizden biri Oracle veritabani ile calisiyor digeri, Mysql, baskasi da Mssql ile calisiyor ornegin her biris i de Add islemi yapiyor ancak her bir veritabani kendi yontemine gore yapiyor bu isi....
Ana tepedeki base class icinde bir tane virtual method yazilir ve bu base class i inherit edecek olan Araba,Gemi,Ucak siniflari icinde bu methodun bicimi degistirilir bu sekilde cok bicimlilik olmus olur
SEALAD ANAHTAR KELIMESI TAM DA POLIMORFISM UYGULARKEN GUNDEMIMIZE GIRIYOR...
CUNKU SEALED ANAHTAR KELIMESINI KULLANARAK BIR CLASS IN INHERIT EDILMESINI ENGELLEYEBILDIGIMIZ GIBI, BIR METHODUN DA OVERRIDE EDILMESINI ENGELLEYEBILIORUZ
Yani biz Transport class inin inherit edilmesini engellemeki icin class basina sealed yazariz ayrica da icindeki metodun, Transport class ini inherit eden class lar icinde override edilmesii engellemek istersek de o zaman da MoveFromNorwayToSweden methodunun basina sealed yazariz.......
Bir class in inherit edilmesini engelledgimiz zaamn o class in icinde ki virtual method larin da dogal olarak override edilmesini ve diger method ve propertiesleriinin kullaniminin engellenmesini saglamis oluruz....
*/