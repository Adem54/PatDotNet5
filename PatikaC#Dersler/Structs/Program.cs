using System;
namespace Structs
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Struct lar deger tiplidir, class lar referans tiplerdir
            // Struct ta nesneyi biz new lemeden de onun propertylerine erisip islem yapabiliyoruz ama class ta bu mumkun degildir
            Coordinate point;
            //  Console.Write(point.x); // Compile time error  
            // A struct object can be created with or without the new operator, same as primitive type variables
            /*
            Above, an object of the Coordinate structure is created using the new keyword. It calls the default parameterless constructor of the struct, which initializes all the members to their default value of the specified data type.

            If you declare a variable of struct type without using new keyword, it does not call any constructor, so all the members remain unassigned. 
            Therefore, you must assign values to each member before accessing them, otherwise, it will give a compile-time error.
            */
            point.x = 10;
            point.y = 20;
            Console.Write(point.x); //output: 10  
            Console.Write(point.y); //output: 20  

/*
Normal bir class ta biz eger kisa kenar ve uzun kenar a deger atmasi yapmaz ve o degleri constructor uzerinden de almaz isek ve de Area methodu alan hesaplama methodunu cagirir isem, method class instancesi gider kisa kenar ve uzun kenar in default degerlernin alir(ornegin bool olsa idi false alirdi ki bu default degerleri atayan da yine default constrctor dir....)
Ama struct yapilarda iste ayni  mantikla gidip new leme yapmadan dogrudan methodu invoke edersek normal de new leme yapmadan kullanabiliyruz struct yapilari ama sinirli birsekilde kullanilabiliyor ve boyle bir senaryo da intial degerleri kendisi atayamiyor cunku default constructor calismiyor paramtresiz ondan dolayi de o zaamn new leme yapmadan, propertieslee initial deger atamadan, metodu invoke edemiyoruz...
*/
            /*
            A struct cannot contain a parameterless constructor. It can only contain parameterized constructors or a static constructor.
            */
            Coordinate2 point2 = new Coordinate2(10, 20);

            Console.WriteLine(point2.x); //output: 10  
            Console.WriteLine(point2.y); //output: 20  
            /*
            struct can include constructors, constants, fields, methods, properties, indexers, operators, events & nested types.
            struct cannot include a parameterless constructor or a destructor.
            struct can implement interfaces, same as class.
            struct cannot inherit another structure or class, and it cannot be the base of a class.
            struct members cannot be specified as abstract, sealed, virtual, or protected.
            */
            Numbers numbers;
            //New lemeden kullanirken property lere deger atamazsam methodu kullanirken hata alirim
            numbers.Number1=23;
            numbers.Number2=12;
            numbers.Sum();
            //Neden structlari kullaniriz...
            //Struct lar da performan acisindan biraz daha hiz sagliyor. stack bellek, ama class larin heap bellekte calisiyor ve ref turlu olmasi da referans tiplerini gucunu kullanmak  ve onlarn da saglamis oldugu performans avantajlarindan yararlanmak anlamina geliyor..
            //16byte a kadar olan verileri saklamak icin struct kullanim daha uygun iken, 16byte daha buyuk yapilar icin ise class kullanip heap da islem yapiyor olmak bizimn icin daha avantajlidir

        }
    }

    struct Coordinate
    {
        public int x;
        public int y;
    }

    //Struct yapilarda parametresiz contstructor tanimlayamiyoruz, ama parametreli constructor tanimlayabiliriz

    struct Coordinate2
    {
        public int x;
        public int y;

        public Coordinate2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
//struct yapilarda get ve set islemlerini yazmiyoruz.....yazarsak hata aliyoruz...ne zaman, new lemeden kullanmaya calistgimizda
struct Numbers{

    public int Number1;
    public int Number2;
    public int Sum(){
        return this.Number1+ Number2;
    }
}

}
/*
Struct'lar yani yapılar sınıflara çok benzerler. Struct ile yapıp sınıf ile yapamayacağız bir işlem yoktur diyebiliriz. 
Peki o halde struct yani yapılara neden ihtiyaç duyulur?
Class kullanmanızı gerektirecek kadar komplex olmayan yapılarınız varsa ve verileri kapsüllemek işinizi görecekse yapıları tercih edebilirsiniz.
Class lar referans tipli özellikler gösterir, Yapılar ise değer tipli özellikler gösterirler. En temel fark budur.
Diğer struct yada sınıflardan kalıtım almazlar.
Interface'lerden kalıtım alabilirler.
new anahtar sözcüğü ile nesneleri yaratılabilir.
Sınıflar gibi metot, property ve field'lardan oluşurlar.
Sınıf içerisinde struct, struct içerisinde de sınıf oluşturulabilir.
Static üye barındırabilirler.
*/

/*
Programlama dillerinde en mühim OOP(Object Oriented Programing) yapılarından olan class yapısına göre daha basit düzeyde işlemler gerçekleştirmemizi sağlayan ve belirli bir takım kısıtlamaları yanında barındıran struct yapısını C# diline özel ele alacağız.

Şimdi düşünün ki, bir yapı oluşturacaksınız ve bu yapı birbirleriyle ilişkili verileri barındıracaktır. Haliyle bunu bir class olarak belirleyebilir ve o classtan üretilen nesne üzerinde işlemlerimizi icra edebiliriz. Amma velakin bu yapımız class kadar kompleks işlemler için tasarlanmış bir yapı gerektirmiyorsa ve tutulacak verileri enkapsüle etmek yetiyorsa işte bu tarz durumlarda struct yapısını tercih edebiliriz.
Unutmayınız ki, classlar bir Referans Tipli(Reference Types) özellik gösterirken struct yapıları bir Değer Tipli(Value Types) değişken özelliği göstermektedir.

Yani anlayacağınız “int” gibi, “bool” gibi değer tipli bir değişken oluşturmak istiyorsanız struct yapısını tercih edebilirsiniz.

Bu demek oluyor ki, gerçekleştireceğimiz işlevselliğin yapısal olarak bir nesne yahut değer tipli bir değişken yapısında gerçekleştirilmesini tercih edebilir ve birazdan bahsedeceğim olumlu olumsuz yanlarıda hesaba katarak projenizde performansı daha maliyetli bir hale getirebilirsiniz.

Şimdi struct yapısını C# perspektifinden adım adım değerlendirelim;

Yazımızın yukarıdaki satırlarında da bahsettiğimiz gibi struct C#’ta value type yaratabileceğimiz yapıdır.
struct yapıları, interface yapılarını uygulayabilmektedirler.
struct yapılar new keywordüyle örneklendirilebilmektedir. Burada iki durum söz konusudur.
Eğer new operatörüyle örneklendirirsek ne olur?
Bildiğiniz gibi new operatörü classlarda kullanıldığı zaman ilgili classtan bir nesne talep edilmekte ve üretilen nesne belleğin Heap kısmında muhafaza edilmektedir. Ee söz gelimi struct yapısında da new operatörünü kullanırsak eğer evet ilgili yapıdan bir nesne üretilecektir ama struct bir değer tipli değişken yapısında olduğundan dolayı o nesne belleğin Stack kısmında muhafaza edilecektir.
Bu yapıya kadar oluşturduğumuz tüm nesnelerin Heap kısmında olduğunu söylemiştik. Halbuki Stack kısmında struct yapısında nesneleri tutabilmekteyiz.

Ayriyetten struct içerisindeki fieldlara default değerleri atanmış olacaktır.

Eğer new operatörü kullanmazsak ne olur?
Haliyle classlardaki gibi nesne mecburiyeti yoktur. Yani new operatörü ile struct yapıdan bir nesne üretmeden de o struct’ı kullanabilmekteyiz.
Yukarıda da gördüğünüz gibi nesnesiz structları kullanabilmekteyiz kullanmasına ama içerisindeki verilerin ilk değerleri atanmayacağından dolayı hata alınacaktır.
İşte böyle bir durumda ilk değerleri atayarak kullanmak gerekecektir.
Anlayacağınız class yapıları new keywordü ile belleğe fiziksel olarak çıkabilmekte ve kullanılabilmektedir. Aksi taktirde belleğe çıkamamaktadırlar. Struct’larda ise new ile belleğe çıkma zorunluluğu yoktur lakin içerisindeki elemanların ilk değerleri mecburi verilmek zorundadır.
struct yapılar içerilerinde metod, property veya field barındabilirler.
Haliyle bu elemanlardan metodları kullanabilmek için struct değişkenine new operatörüyle bir nesne bağlama zorunluluğumuz yoktur. Ama property yapılarını kullanabilmek için struct’tan bir nesneye ihtiyacımız vardır.
struct yapılar içlerinde static alanlar barındırabilirler
ve bu static yapılara normal bildiğiniz yapı ismi üzerinden .(nokta) diyerek ulaşabilmekteyiz.
Ve son olarak struct yapılar içerisinde class, class içerisinde de struct yapılar tanımlanabilir.

*/