//using Interface;
namespace Inheritance{
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
       Bitkiler bitkiler=new Bitkiler();
       
       TohumluBitkiler tohumluBitkiler=new TohumluBitkiler();
       //tohumlu bitkiler de Bitkileri inherit etmeme ragmen Bitiler icindeki protected yaptigim FotosentezYapma metoduna erisemiorum
      // tohumluBitkiler.FotosentezYapma();

      //Eger bir class icerisindeki methodlar protected olarak tanimlanirse o zaman, o class i inherit eden sub class lar o class icindeki methodlara kendi constructor indan erisebilirler...Buu onemli bir bilgidir
      Person person=new Person();
      Person person2=new Student();
     // person.YemekYe();
      //Dikkat edelim Student ten olusturdugmuz instance veya nesne uzeirinden onun base class i icindeki protecteed metoda erisemedim ama o protected methodu Studen class i nin constructor inda eriserek cagirabiliriz eger cagirmak istersek...StudentConstructor i icerisnde ister 
      //Bu hali ile daha kontrollu bir yapi oluyor daha korumali, ve de daha az kod yazarak YemekYe methodunu invoke etmis olduk....
    }
}

class Person{
    protected int Id {get; set;}
    protected void YemekYe(){
        Console.WriteLine("Yemek ye");
    }
}
class Student:Person{
    public Student(){
        YemekYe();
    }
}
}
