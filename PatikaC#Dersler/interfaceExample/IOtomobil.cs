using System;
namespace interfaceExample {
    public interface IOtomobil{
        int TekerlekSayisi();
        Marka AracMarka();
        Renk StandartRenk();
    }
}
//Araba nin starndart renkleri bellidir sonradan eklenmesi cok zor ihtimaldir
//Markalar da bellidir bu da sonradan eklenmesi zor bir ihtimaldir
//Bu tarz durumlarda enum lara basvurmaliyiz....Yani elimizde sabit belli degerler var ve bizim olusturacagimz nesneler sadece o listede bulunan renklerden veya markalardan kullanacak ise o zaman bu isi enum ile cozmeliyiz....
//Ve interface lerde methodlarimiz enum dondurecek sekilde ayarliyoruz