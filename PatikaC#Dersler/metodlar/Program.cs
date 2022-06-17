using System;

namespace metodlar
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //Simdi normalde bir methodun parametresine ornegin primitive type yani value type dedgiz integer gibi deger verdgimiz zaman yapilan is tam olarak sudur
        //ornegin parametreye verilen degisken in degerleri normalde, bellekte bit bit kopyalaniyor ve o kopyalar veriliyor, yoksa paramtrede verilen degerlerin o degiskenlerle hic alakasi yoktur zaten birbirlerini etkilemeyeceklerdir de iste bu islem e aslinda immutable olma diyoruz yani degismeme, primitive type lar 
        //birbirlerine deger leri aktarir, yani bellekte o degeri ni kopyalar gecer kendisinde hicbir degisiklik olmaz, verdigi deger ise bellekte onun yeniden olusturulmus kopyasidir esasinda.....
            Console.WriteLine("Hello, World!");
            int number1 = 12;
            int number2 = 6;
            Person person = new Person("Memet");
            person.Topla(ref number1, number2);
            Console.WriteLine("number1: "+number1);//number1 ref keywordunden dolayi, artik referans bir deger olmustur dolayisi ile method icinde yapilan degiisiklik
            //disarda parametre olarak verddimiz degeri de etkilemistir 
            Console.WriteLine("number2: "+number2);

            //Out keywordu nu kullanmak-Out keywordune initial deger atamak zorunda degiliz ancak, ref keywordune initial deger atamak zorundayiz...
            //Ve normalde biz bir degiskene deger atamadan kullanamayiz iste istisnasi out keywordu ile paramtereye verilecekse pek ala ornekte oldugu gibi kullanilabilir
            int number3=15;
            int number4;
            person.CheckOut(ref number3, out number4);
            Console.WriteLine($"number3: {number3}");
            Console.WriteLine($"number4: {number4}");
            //Out parametresi..
            //Bir fonksiyona bir is yaptirip ve onun sonucunda bir degeri set etmesini istiyorsak ve o setelenen degeri de fonksiyon disinda kullanmak istiyorsak
            //o zaman bu isi iste out parametresi veya ref parametresi ile yapiyoruz...

            Console.WriteLine("Bir sayi giriniz: ");
            //TryParse kullanimi
            string? number=Console.ReadLine();
            //Sayi olarak kullanabilmem icin kulanicinin sayi girmesi gerekiyor eger kullanici null veya direk hello gibi bir text veya bool true degeri girerse o zaman da degerimiz hataya dusecektir....Iste boyle durumlar icin biz TryParse kullanirz. Yani eger biz girilecek degeri parse edecek isek ama girilecek degerin durumuna gore parse islemi basarili veya basarisiz olacak ise boyle durumlarda uzun uzn kendimiz kontrol etmek yerne tryParse kullanabiilirz
            //Yapacagimiz islemi garanti altina almak icin tryParse methodunu kullaniriz  
            var result=int.TryParse(number, out int outNumber); //git sayiyi cevirip ceviremedigine bak ve sonucunda da cevirebiliyorsa cevirdigi sayiyi bana bu sekilde don diye paramtreye veririz
            //out ile verilen paramtreyi biz onden disarda da tanimlayabiliriz ya da direk paramtrede invoke ederken de tanimlayabiliriz
            int outNumber2;
            var result2=int.TryParse(number, out  outNumber2); //git sayiyi cevirip ceviremedigine bak ve sonucunda da cevirebiliyorsa cevirdigi sayiyi bana bu sekilde don diye paramtreye veririz
            //Disardan girilen eger sayi olursa da onu 2.paramtreye atiyor ve disarda kullanabiliyoruz. 1. paramterede ise kullaniciin girdigi sayidir
            //Eger kullanicii sayi girmez ise o zaman default olarak verilen integer degeri olan 0 u outNumber a atayacaktir
            
            Console.WriteLine("result: "+result);//Kullaniici sayi girer ise basarili birsekilde parse edilecektir ve kullanicinin girdigi deger de outNumber a atanacaktir
            Console.WriteLine("outNumber: "+outNumber);
            Console.WriteLine("result2: "+result2);
            Console.WriteLine("outNumber2: "+outNumber2);

            Customer customer=new Customer();
            int a=3;
            int b=8;
            customer.Topla(a,b, out int toplam);
            Console.WriteLine("toplam: "+toplam);
            //Gordgumuz gibi int ile out keywordunu kullanarak bir metodu invoke ederken o method paramtresinde yeni bir int out keywordu ile degisken olusturup method icinde set edip o degiskeni disarda da kullanabilmis olduk....

            customer.ShowMyValue("Hello");
            customer.ShowMyValue(45);
            //Method imzasi birbirinden farkli oldugu surecek sorun yasamiyoruz
            //Method imzasi =>Method adi+parametre sayisi+paramtre tipi=>Bunlarin ucu de ayni olursa o zaman compile hatasi aliriz, degilse hicbirsorun yasamayiz...
        }
    }

public class Customer{
    public void Topla(int a , int b, out int toplam){
        toplam=a+b;
    }
//Overloading...Bizim methodlari cok efektif birsekilde kullanabilmemizi sagliyor
    public void ShowMyValue(string message){
        Console.WriteLine("message: "+ message);
    }
    public void ShowMyValue(int message){
        Console.WriteLine("message: "+ message);
    }
}


}


