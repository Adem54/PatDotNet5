using System;

namespace tipDonusumleri{
internal class Program
{
    private static void Main(string[] args)
    {
        //Implicit conversion-bilincsiz donusum
        //Dusuk kapasiteli bir degiskeni kendinden daha yuksek kapasiteli baska bir degiskene atanma islemidir aslinda
        //Burda biz herhangib birsey yapmiyoruz bizm yerimze compiler yapacak

        byte a=5;
        sbyte b=30;
        short c=10;
//int kapasitesi a,b ve c den daha yuksektir..Ondan dolayi burda herhangi bir sorun yasanmiyor ama tam tersi durumda, yani d nin kapasitesi eger a,b,c den daha kucuk olasa idi o zaman sorun alirdikk
        int d=a+b+c;
        Console.WriteLine($"d: {d}");
        long h=d;
        Console.WriteLine($"h: {h}");
        float i=h;
        Console.WriteLine($"i: {i}");
        int x=12;
        short y=23;
//Burda y short oldugu ve x int old icin ve de short bir degiskene int degeri atmaya calistigmiz icin burda compiler hata verecektir ve kendisi implicit donusumu gerceklestiremeyecektir, ondan dolayi biz kendimiz expliicit olarak x degerini short a donusturuuz casting yontemi ile
       // y=x;
       y=(short)x;//explicit conversion-casting
        Console.WriteLine($"y: {y}");
        string e="adem";
        char f='k';
        object g=e+f+d;
        Console.WriteLine($"g: {g}");
        //Explicit conversion-bilincli donusum-aciktan donusum-Casting-boxing de diyebiliriz...
        //Compiler kendisi donusumu yapamadigi icin bizim kendimizin Convert ve parse i kullanarak donusumu kendimizin yapmasi islemidir
        int xx=4;
        byte yy=(byte)xx;//Casting
        int z=100;
        byte t=(byte)z;
        float w=10.7f;
        byte v=(byte)w;
        Console.WriteLine("v: "+v);//10
        //Float a int veya long degeri atamasinda problem yasmayiz implicit olarak gerceklesir ancak, tam tersi bir durum da ise explicit donusum yapmamiz gerekir
        int r=15;
        float n=15.6f;
        n=r;
        //Implicit numeric conversions-Explicit numeric conversions diye google de ararask bunlar detayli bir sekilde mevcuttur hangi turu hangi ture ne sekilde donustururuz bunlari bulabiliriz ezbere gerek yoktur...
        //ToString methodu
        int xxx=6;
        string yyy=xxx.ToString();
        Console.WriteLine($"y: {yyy}");
        string zz=12.5f.ToString();
        Console.WriteLine($"zz: {zz}");
        string s1="12", s2="21";
        int sayi1,sayi2;
        int top;
        sayi1=Convert.ToInt32(s1);
        sayi2=Convert.ToInt32(s2);
        top=sayi1+sayi2;
        Console.WriteLine("toplam: "+top);


String word = "123,987324234234";
Console.WriteLine(Convert.ToDouble(word));
        //Parse methodu
        ParseMethod();
    

    }

    public static void ParseMethod(){
          string text1="10";
          string text2="10,25";//10.25 yazarak cevrilmeye calisilirsa hata veriyor...
          int number1;
          double double1;
          number1=Int32.Parse(text1);
          double1=Double.Parse(text2);
      Console.WriteLine($"number1: {number1}");
      Console.WriteLine($"double1: {double1}");
    }
}
}
