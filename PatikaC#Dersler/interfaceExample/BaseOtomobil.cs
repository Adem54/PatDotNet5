namespace interfaceExample {
    public abstract class BaseOtomobil : IOtomobil
    {
        //Bunun abstract class olmasina soyle karar veriyoruz.BaseOtomobil abstract class ini inherit edecek olan tum, class lar AracMarka methodunu kullanacagini biliyoruz ayrica da tum araclarda Marka ozelligi kendilerine has olacak yani ortak bir arac marka yok onu da biliyoruz o zaman yazacagimiz bir default deger yok bu methodda o zamanda bosuna govdesini yazmanin anlami yok ondan dolyai abstract class yaz direk....
        public abstract Marka AracMarka();
      
       //Abstract class in bize sagladigi harika fleksibilitet ve polimorfizm...
       //Burda default olarak kullanilacak rengi giriyoruz sonra da detayli implementasyonuna Araclar karar verecek, default rengi kullanacak olanlar direk burdan geldgi gibi base den geleni kullanabilecek, yok bambaska renk girecek olanlarda default rengi kaldirip kendi istedigi rengi girebilecek, bu ornekte ihtiyacimiz yok ama isteyen de hem default u hem de kendi girecegi degeri kullanabilir bu bazi spesifik durumlarda gerekebilir....
        public virtual Renk StandartRenk()
        {
            return Renk.Gri;
        }

        public int TekerlekSayisi()
        {
            return 4;
        }
    }
}