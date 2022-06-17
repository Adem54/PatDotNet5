using System;
namespace StaticClass
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //Static Class lar
            //Static olmayan bir sinifin properties ve methjodlarina o siniftan olusturulan nesneler, instanceler araciligi ile erisiriz
            //Static olan elemanlara ise , proeprt method vs gibi bunlara ise instance, veya nesne olusturmadan static sinifin adi ile erisiyoruz...
            //SinifAdi.ErisemekIstenilenMethodVeyaProperty diyerek erisiyoruz ki zaten static class lar bir kere new lenebilir, yani biz istesek de instance olusturamayiz.   
            Console.WriteLine("Employee Count {0}", Employee.CountOfEmployee);
            Employee employee1 = new Employee("Adem", "Erbas", "web-development");//Static olmayan siniflari set etme islemini yapabilmek icin instance olusturmamiz gerekir
            Employee employee2 = new Employee("Zehra", "Erbas", "primary-school");
            Employee employee3 = new Employee("Zeynep", "Erbas", "math-teacher");
            Console.WriteLine("Employee Count {0}", Employee.CountOfEmployee);//3 olarak gelecektir...
            Employee employee4 = new Employee("Kemal", "Erdem", "physic");
            Employee employee5 = new Employee("Sevim", "Keskin", "medical");
            Console.WriteLine("Employee Count {0}", Employee.CountOfEmployee);//5 olarak gelecektir...

            //BUNLARI IYI BILEMIMIZ GEREKIR...BU COOK YAYGIN KULLANILAN BIR SEYDIR...
            //Ayrica surayi da cok iyi anlayalim,,biz firstName,lastName ve deparmtan a sadece private field da kullandik ve property olsturmadik ve de neyi sagladik bu degerlerin sadcece constructor uzerinden girilebilmesini saglamis olduk.....
            //COOOK ONEMLI BIR NOKTA....
            //Employee sinifinin hem normal constructor i var icerisinde firstName,lastNam, departmant parametresi alan hem de, static constructor i var peki hangisi daha once calisacak bunu nasil anlariz
            //Simdi  biz class imizin ilk instancesini olustururken ilk olarak static constructor calisacaktir ardindan ise normal public constructor calisacaktir ve bundan sonraki her instance olustugunda ise sadece public constructor calisacaktir cunku static constructor lar bir class in yasam dongusu boyunca 1 kez olusurlar..Ama biz Employee.CountOfEmployee dedigmiz zaman bu property ye erismeye calisacak ama buna erismeden once, gidip static onstructor i calistiracak cunku biz static elemana direk class uzerinden erisiyoruz ve class uzerinden calistirmaya calisinca da gidip static constructor calisacak....
            //Yani biz direk class uzerinden static bir property e erismeye calistigimiz da(Employee.CountOfEmployee) da ilk olarak gidecek static constructor i calistiracaktir ya da biz diyelim ki ilk olarak class tan bir instance olusturduk eger daha oncesinde static constructor hic calismamis sa bu durumda da ilk olarak static constructor calisacak daha sonra da normal constructor calisacaktir....

            //Static Sinif kullanimi
            //Transiction transiction=new Transiction();//Static class tan bir tane variable decaler edemezsin diyor
            //Static siniftan instance olusturamayiz ve static sinifin uyelerine staticsinifadi. diyerek erisebiliriz ancak....

            var result = Transiction.Sum(16, 24);
            Console.WriteLine("result: " + result);
            var result2=Transiction.Substract(56,12);
            Console.WriteLine("result2: " + result2);

            /*
            Siniflarini statik olmasi durumu-Neden static sinif kullaniriz...
           ONEMLI*** Buna neden ihtiyac duyariz. Cunku bir sinifi statik yaparsak o sinif icerisinde tum field,proeprty, methodlar static olmak zorundadir
           ****************Static sifin icerisinde static olmayan herhangi bir uye kullanamazsiniz...**************************
            Sinifi statik yaparak aslinda o static sinifi sadece amaci dogrultusunda kullanilmasini saglamis oluruz...icerisinde normal static olmayan uyeler atanmasina izin vermemiz oluruz...Ayrica da projemiz iceriisnde calisirken bizim o class i gorur gormez ne amacla kullandgimizi anlamizi saglar...
            Okunabilirlik ve kullanislilik acisindan faydalidir
            Static siniftan instance olusturamayiz, static sinif uyelerine static sinif iismi uzerinden erisebiliriz...
            Static siniflardan kalitim islemi uygulanamaz....****************bu da onemlidir...
            Daha cook tool, helper veya common gibi her kullanmamizda nesne olusturma gibi maliyetli bir islemden kacinmak icin de basvururuz bazen
            */
        }
    }

    public class Employee
    {
        private static int _countOfEmployee;
        //_countOfEmployee fieldini encapsule ettik property araciligi ile
        public static int CountOfEmployee { get => _countOfEmployee; private set => _countOfEmployee = value; }
        public string LastName { get => _lastName; set => _lastName = value; }

        //BESTPRACTISE...BIR CLASS IN STATIC ELEMANLARI HER ZAMAN O CLASS UZERINDEN YAPILAN EN SON ISLEM NE ISE ONU GETIRECEKTIR...BESTPRACTISE...
        //Static olmayan bir class icindeki elemanlardan static olmayan elemanlar o sinif icerisinde nesneye ozgu iken, static olan elemanlar ise proerpty,method vs bunlar sinif bazinda atanirlar, yani bu CountOfEmployee sinifa ait bir ozellik gibi dusunebiliriz, ne kadar nesne olusturursak olusturalim bu property ye sinif araciligi ile erisecegimiz icin her zaman degistirilen en son degeri getirecektir bize...
        //Bunlari encapsule etmiyoruz cunku bunlara ben disardan hic erisilmesin, proerpty araciligi ile de erisilmesin sadece constructor daan gonderilsin bu veriler istiyorum
        private string _firstName;

        private string _lastName;
        private string _departman;
        //Ve de _countofEmployee nin proeprt icindeki setter ini da private ya da kaldirmak istiyorum, yani artik disardan degistirilme imkanini da ortadan kaldirarak bunu sadece, constructor uzerinden degistirilmesini saglamis oluyoruz...
        //Her bir calisan eklendiginde calisan sayisi 1 artacaktir yani her bir calisan da her bir instance olusturuldugunda artacaktir cunku bu calisan class idir ve her bir instance bir calisan anlamina gelir dolayisi ile biz peki her bir instancenin olusturulmasini nerden kontrol ediyoruz tabi ki constructor da o zaman her bir new leme durumunda, bu calisan durumu bir attirilsin diyoruz, constructor icerisinde ve bu countOfEmployee static bir field oldugu icin ve dogrudan class a bagli olarak calistigi icin, instanceye degil class a bagli olarak, calistigi icin, en son olusturulan instance de biz en guncel degeri alabiliriz...
        //Yani her zaman degistirilen en son degeri alabiliriz....BUUU BESTPRACTISE...BUNU DAHA ONCE HIC GORMEMISTIM....SUPER TEKNIK....
        //Kisacasi normal constructor icerisine geliriz, ve static field olan _countOfEmployee yi _countOfEmployee++; her yeni nesne olusturuldugunda 1 arttiririz ve bu sekilde biz bu class tan kac tane nesne olusturuldu ise yani kac tane employee instance sin olusturuldu ise bunu her zaman kontrol edebiliriz....Iste bu muthis bestpractise dir......Disardan _countOfEmployee ye ulasmaya calisinca property uzerinden property de de return oalrak private field i getiriyor yani encapsulation o zaman kullanici her zaman en son _countOfEmployee kac ise o degeri alabilecektir....
        //Eger 1 kere static olan constructor calisti ise daha sonra yeni new leme veya instance olusturma ya da Employee.CountOfEmployee notasyonu ile static property nin getter islemi okuma islemi de olsa static constructor bir daha calismaz cunku yasam dongusu boyuunca bir kez calisacaktir....
        public Employee(string firstName, string lastName, string departman)
        {
            this._firstName = firstName;
            _lastName = lastName;
            _departman = departman;
            //burda biz this. deyince nelere erisiyoruz ya da Ctrl-space e basarak nelere ersiiyoruz gorebiliriz...
            _countOfEmployee++;
            Console.WriteLine("public Employee constructor i calisiyor");
        }
        //Simdi biz bir class olustrduk ve bu class in constructor ini da olusturduk ok.
        //Peki biz static constructor olusturamaz miyiz?
        //static constructor larin erisim belirtecleri yoktur,public,internal vs yoktur, normalde constructor larda publictir
        //static constructor icerisinde static olan field i set lemek istiyoruz
        //Calisan sinifi ilk olusturuldugunda calisan sayisi 0 olsun diyoruz...
        //Ve de _countofEmployee nin proeprt icindeki setter ini da private ya da kaldirmak istiyorum, yani artik disardan degistirilme imkanini da ortadan kaldirarak bunu sadece, constructor uzerinden degistirilmesini saglamis oluyoruz...
        static Employee()
        {
            _countOfEmployee = 0;
            Console.WriteLine("static Employee constructor i calisiyor");
        }
    }
    static class Transiction
    {
        public static long Sum(int number1, int number2)
        {
            return number1 + number2;
        }
        public static long Substract(int number1, int number2)
        {
            return number1 - number2;
        }
    }

}
