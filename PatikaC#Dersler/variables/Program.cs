internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        //C# da bir degisken tanimladigmiz zaman mutlaka deger atamaliyiz, deger atamadan kullanmamaliyiz..
        //Zaten biz daha kodu yazarken o kodu calistirmamiza engel oluyor compiler, yani once build ediyor C# da kodu once bir build ediyor sonra calistiriyor zaten, yani kod once bir makine koduna donsturulur, sonra calistirilir dolayisi ile build yapilirken, kodumuzu compiler makine koduna
        //donusturmeye calisiyor, eger biz hatali kullanim yapmissak zaten daha compile edememis olacak, ondan dolayi da hata verecektir....
       // int myValue;
       // Console.WriteLine(myValue);//Use of unassigned local variable myValue[variables] uyarisi verecektir....
       int number=2;
      //int bir degere null atadigimiz zaman kendisi bunu implicit olarak ceviremeyecektir...dolayisi ile biz eger int bir degeri null tanimlayacak isek o zaman onu nulllable olmasi icin ? notasyonu kullaniriz...
       int? number2=null;
       string myName=null;//Ama string bir deger de boyle bir problem yoktur...direk null deger atayabiliriz cunku string degerler de bir referans tipli degerlerdir ve referans tipli degerler temel de ayni zaman da nullable dirlar zaten ondan dolayi onlara null atayinca sorun almayiz...
       int[] numbers=null;//referans tipli degerler ayni zamanda INullable dir ondan dolayi sorun almayiz...
       Person person=null;//Referans tipli degerler ayni zamanda INullable olduklari icin onlara null atayinca sorun olmaz ama primitve type li tiplerde null
       //atamasi yaparsak ? notasyonu ile onlari nullable olabilir diye ayarlamamiz gerekecektir
       bool? isTrue=null;
       int myNewValue=12;
       int MyNewValue=13;
       string @class="Customer";

       string myWord=" ";//Bu bo
       byte myNum=3;//Database de 1 byte lik yer kaplar-0-255 arasi deger alir
       sbyte myNum2=4;//-127-127 arasi deger alir
       short myNum3=5;//2 byte lik yer kaplar-\32768 den 32768 e kadar deger alacaktir
       ushort us=5;//short kadar yer kapliyor bellekte ama - deger almaz
       Int16 myNum4=12;//2 byte yer kaplar
       int myNumb5=14;//4byte yer kaplar- -2milyar kusurden 2milyar kusure kadar kullanilabilir
       Int32 myNumb6=16;//Bellekte 4byte yer kaplar
       Int64 myNumb7=18;//8byte lik yer kaplar...
       uint myNumb8=20;//4byte lik yer kapliyor, - deger alamaiyor sadece pozitif alir
       long myNumb9=21;//8 byte lik yer kaplar
       ulong myNumb10=25;//8byte lik ve - deger alamaz
       float f=4.3f;//4byte lik yer kaplar
       double d=5.4;//8byte lik yer kaplar
       decimal dcml=7.5m;//16 byte lik yer kaplar
       char chr='a';//2byte yer kaplarken string sinirsizdir..
       //String ler ile islem yapmak database de maliyetli islemlerdir
      string myName2="Adem";
      bool b1=true;
      bool b2=false;
      DateTime dt=DateTime.Now;//Bana run time daki zamani getirir yani calistirdgimz anda tarih ne ise onu getirir
      Console.WriteLine($"dt=  {dt}");
//26.05.2022 10:11:22
//Tarih uzerinde istedigmiz herseyi yapabilirz bundan 3-5 gun oncesi veya sonrasini alabiliyoruz ve de istersek sadece yil,ay, gun vs alabilirz

//Tum degisken tipleri aslinda birer objedir. Yani objeler tum tiplerin base idir yani tum tipler objeden inherit ediliyor, obje turunden turetilmistir...
// Bundan dolayi objeler tum degisken tiplerini tutabiliyor
object o1="x";
object o2='y';
object o3=4;
object o4=5.6;
object o5=false;
object o6=new Person("Adem");

//STRING IFADELER
string str1="";
string str2=null;
//Compiler diyor ki null olmayan birsey tanimadin ama null atamasi yaptin..Bu sen kodunu annotation yani koduna uygulayacagin ozellikler bitmemis demektir
string str3=string.Empty;//Bos bir stringi bu sekilde de atayabiliyoruz...""
str3="Adem Erbas";
string name1="Adem";
string name2="Erbas";
string WholeName=name1+ " "+name2;
Console.WriteLine("str3 type ise "+str3.GetType());
Console.WriteLine("str1 type ise "+str1.GetType());
Console.WriteLine("str2 type ise "+str2?.GetType());
//String ifadenin null veya empty oldugunu kontrol edelim
if(string.IsNullOrEmpty(str2)){
    Console.WriteLine("str2 null yada emptydir....");
}

//Integer tanimlama sekilleri
int int1=6;
int int2=3;
int ing3=int1*int2;

//boolean
bool bool1=10>2;
bool bool2=10<2;

//Degisken donusumleri
string str20="20";
int int5=20;
//int degerini stringe cevirdik...
string newValue=str20+int5.ToString();
Console.WriteLine(newValue);//2020

int int21=int5 +  Convert.ToInt32(str20);
Console.WriteLine(int21);//40
Console.WriteLine(int21.GetType());//System-Int32
//inte Parse ile cevirme...
//Parse i string den inte donusturme isleminde yapiyoruz...
int int22=int5 + int.Parse(str20);
Console.WriteLine(""+int22);//40

//DateTime
//Saatleri getirme, sadece gun,ay yil aralarinda nokta formati ile getir demis olyoruz...
string datetime=DateTime.Now.ToString("dd.MM.yyyy");
Console.WriteLine(datetime);//26.05.2022
string datetime2=DateTime.Now.ToString("dd/MM/yyyy");
Console.WriteLine(datetime2);//
string datetime3=DateTime.Now.ToString("HH:mm");
Console.WriteLine(datetime3);//

    }
}

public class Person{
    private string _name;
    public Person(string Name)
    {
        _name = Name;
    }
}