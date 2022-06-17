using System;
namespace Collections{
internal class Program
{
    private static void Main(string[] args)
    {
    //KOLLEKSIYONLAR
       //Kolleksiyonlarda farkli veri tipleri ile calisabiliyoruz
       //Kolleksiyonlar namespace olarak System.Collection altinda bulunuyor, dolayisi ile System.Collection i import edere kullaniriz
       //Kolleksiyonlar nesnelerden olusuyor
       //Kolleksiyonlar object veri tipi ile calisiyor
       //Kolleksiyonlari klasik arraylerin cok daha gelismis verisyonlari olarak dusunebiliriz...
       //Kolleksiyonlarin Dezavantajlari-
        //BOXING-UNBOXING
        //Bir deger tipinin referans tipine donusturulme isine boxing diyoruz, tam tersine de unboxing deniyor yani bir integer tipinde degeri collection icerisine attdigmizda, collectionlar object tipinde calisiyor ve object tipi bir referans tip tir dolayisi ile, integer i,char i ,bool gibi tipler i collection icerisine attgimizda aslinda ben boxing islemi yapmis oluyorum...Ayni zamanda o collection dan bir deger okumaya calistigmizda da unboxing islemi yapmmamiz gerekiyor...
        //Eleman sayisi arttikca boxing ve unboxing islemleri cokca artiyor ve bu da performansa cok olumsuz yansiyor...Bu da iste collection larin dezavantajlarindandir...
        /*
        Genel Amacli Kolleksiyonlar
        -List,ArrayList,Dictionary,HashTable,Quee,Stack Sorted List
        Ozel Amacli Kolleksiyonlar
        -Hybrit Dictionary, Name Value Dictionary, String Collection, String Dictionary
        Bit Tabanli Koleksiyonlar
        Bit Array
        */
       //Dizilerin 2 dezavantaji
       //1-Belirli veri tipleri ile calismak zorundayiz...
       //2-Bir arraye yeni veri eklemek istdgimz de boyutunu dogrudan yapamiyoruz bunu once boyutunu  buyutmemiz gerekiyor
       //Ve de bir array tanimlarken onun boyutunu bastan vermek gerekiyor

       //List<T> class
       //System.Collections.Generic namespace inden geliyor
       //List<T> class=>T bize bunun bir generic oldugunu soyluyor
       //List<T> ayni zamanda T object turundedir...
       //T=>Listenin icindeki nesnelerin tipini ifade ediyor, hangi tipten olusan bir nesne ekleyeceksek onu belirtmis oluyorz...
       //Her bir liste olusumunda biz bir instance veya bir nesne olusturuyoruz aslinda...
        
        //LIST SINIFI UZERINDEKI METODLAR
        //ADD METODU
         List<int> numbers=new(); 
        numbers.Add(23);//23 degerini ekliyoruz listemize   
        numbers.Add(10);
        numbers.Add(4);
        numbers.Add(92);
        numbers.Add(34);

        List<string> colorList=new();
        colorList.Add("red");
        colorList.Add("blue");
        colorList.Add("yellow");
        colorList.Add("green");
        colorList.Add("orange");

        //COUNT METHODU
        int numbersCount=numbers.Count;
        Console.WriteLine("numbersCount: "+numbersCount);
        int colorsCount=colorList.Count;
        Console.WriteLine($"colorsCount: {colorsCount}");

        //Liste elemanlarini gostermek
        //1-foreach ile gosterebiliriz
        //2-ForEach metoduunuz bize liste nesnesi sunuyor icineki elemanlari gosterebilmemiz icin, yani ForEach sadece Listelerde kulanabilyoruz...
        //ForEach methodunu biz arraylerde kullanamayiz...
        //ForEach cok kullanislidir tek satirda listemiz icersindeki elemenlteri yazdirabiliyoruz...
        numbers.ForEach(number=>Console.WriteLine(number));//ForEach icinde Action  onceden tanimli delege var yani, void birsey dondurmeyen bir delegedir dir....
        colorList.ForEach(color=>Console.WriteLine(color));
        Console.WriteLine("Remove-RemoveAt");
        //Listeden element cikarma
        numbers.Remove(92);//Remove ile ise icine verdiimgiz herhangi bir elementi sileriz...
        colorList.RemoveAt(2);//RemoveAt=>indexini verdigmiz elemnti sileriz
        numbers.ForEach(n=>Console.WriteLine(n));
        colorList.ForEach(c=>Console.WriteLine(c));

        //Liste iciinde arama yapma-Contains methodu
        bool isNumberIncluded=numbers.Contains(23);//liste icinde var ise true gelir...
        Console.WriteLine("isNumberIncluded: "+ isNumberIncluded);

        //Element yolu ile indexe erisme
       int indexOfGivenElement= numbers.IndexOf(4);//Paramtreye verdigmiz 4 elemani liste icerisinde 2.indexte bulunmaktadir
       Console.WriteLine($"indexOfGivenElement: {indexOfGivenElement}");

       int item=colorList.BinarySearch("green");//green elemnti 2.index te bulunmaktadir....
       Console.WriteLine("item:_ "+item);

       //Diziyi Listeye Cevirme..
       string[] animals= {"cat","dog","bird"};
       //animals dizisini myAnimals listesini ceviriyor
        List<string> myAnimals=new List<string>(animals);//Bu sekilde bir dizi icindeki elemanlari listemize aktarmis oluruz...
        myAnimals.ForEach(an=>Console.WriteLine(an));

        Console.WriteLine("Clean the list");
        //Listenin icini temizleme
        myAnimals.Clear();
        Console.WriteLine(myAnimals.Count);//=>0

        //Listeye nesne atamak, listeyi spesifik bir class tipinde olusturmak
        //Liste icinde nesne tutmak
        //Encapsule ettgimiz icin sadece public degiskene erisebiliriz private degiskene erisemeyiz...
        List<User> users=new List<User>{
            new User{FirstName="Adem",LastName="Erbas",Age=34},
            new User{FirstName="Zeynep",LastName="Erbas",Age=34},
            new User{FirstName="Zehra",LastName="Erbas",Age=8},
            new User{FirstName="Jale",LastName="Par",Age=30},
            };

            users.ForEach(user=>Console.WriteLine(user.FirstName+" "+user.LastName+ " "+ user.Age));
            users.Add(new User{FirstName="Sercan",LastName="Sari",Age=35});
            users.ForEach(user=>Console.WriteLine(user.FirstName+" "+user.LastName+ " "+ user.Age));
            User user1=new User();
            user1.FirstName="Kamil";
            user1.LastName="Kara";
            user1.Age=27;
            users.Add(user1);

            foreach(User user in users){
                Console.WriteLine("FirstName: "+user.FirstName);
                Console.WriteLine("LastName: "+user.LastName);
                Console.WriteLine("Age: "+user.Age);
            } 
            //Clear methodu ile Listemizi temizlemek
            users.Clear();
            Console.WriteLine("count: "+users.Count);
    }
}

public class User{
    private string _firstName;
    private string _lastName;
    private int _age;
//Encapsulation

//Disardan bizim field larimiza erisilemiyor ve biz bunlari proerties icerisinde kullanarak, aslinda bunlari korumus oluyyoruz ve de disardaki kullanicinin eger bu degerlere belli sinirlar icersinde veri girmesini vs istiyorsak iste onu da burda kontrol edebilirziz ornegin diyebiliriz ki set icinde if yazarak field su degerler arasinda atayacaksa atama yapsin yoksa atama yapamasin dersen disardan kullanici senin belirledigin degerler arasinda br deger verebilir ancak ve de sen bak ne yaptin iste encapsule etmis oldun...Ya da tum isimlerin basina Mr eklemek istersen ekkeyebilirsin ornegin iste bu da senin yine koymus oldugun sinirlr ve kurallar icerisinde bu degerlerin kullanmilamsini sagliyorsun aslindas ve bu nun adi encapsulation dir....
   public string FirstName {
        get {
            return _firstName;//get methodu her zaman return donmelidir...
        }
        set {
            _firstName=value;//set methounda da atama islemi set etme islemi olmaliddir...
        }
    }
        public string LastName { get => _lastName; set => _lastName = value; }
        public int Age { get => _age; set => _age = value; }
    }
}
