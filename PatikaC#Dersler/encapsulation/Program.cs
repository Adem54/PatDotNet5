using System;
namespace encapsulation{
internal class Program
{
    private static void Main(string[] args)
    {
        //Encapusulation
        //Bir nesnesin bazi ozellikleri,methhodlari ni diger nesnelerden korumak, gizlemek anlamina geliyor
        //Biz bu isi erisim belrileyiciler uzerinden private olarak tanimlayarak yapiyoruz, ama
        //Pirvat durumunda fieldimzi okunamaz ve yazilmaz yani get edilmez ve dahi set edilemez
        //Ama fieldimizi disariya acmak istiyoruz ama bellii kosullar koyarak disariya acmak isteyebiliriz yani ornegin set methodu icin ornegin belli degerler girilmek sarti ile disari acmak isteyebiliriz, ya da belli default degerler atamasi yapariz ve kullanici ne girerse girsin default olarak bizim verdigmiz deger de kullanicin girdiigi deger ile gelsin orneign isimlerin basina Mr koymak gibi
        //Kullanicinin belli araliklarda degerler girmesini istyorsak ve de istemedigmiz deger girilirse hata firlatmak istiyorsak ornegin iste tam da bu kavram altina yapiuyoruz tum islemleri
        //Yani property ler kullanici ile bizim fieldlarimiz arasindaski araci olmus olacak, bir taraftan datalarimiz i kullandirmak okunmasini ve yazilmasini istiyoruz ama bir taraftan da 100% erisim de vermek istemiyoruz iste bu durumlarda property araciligi ile filedlarimiza erisim saglattiririz ve iste tam da property nin get ve set methodlari icinde, varsa istdegimiz kosullar vs orda yazarak ona gore kullanici kisitlayabiliriz, yonlendirebiliriz

        Student studen1=new Student("ERbas","Adem",12,1);//Burasi 2 olunca ve SinifDusur methodu calisirsa sadece 1 kez calisacak ve sinif 1 e dusecek sonra tekrar calisitrmak istediginde o zaman encapsulation devreye girecek ve console de yazdgimiz mesaj devreye girecekt...onemli....
        //1 .sinifdan daha asagi dusurulemez, en az 1 olmalidir..
        studen1.SinifDusur();
        studen1.SinifDusur();
    }
}

//Biz eger setter vermez isek veya get i normal public verir set e gelince onu private verir isek o zaman o datamiza disardan veri girilmesini engellemis oluruz...
public class Student {
    private string _firstName;
    private string _lastName;
    private int _studentNo;
    private int _class;

    public string FirstName {
        get{
            return _firstName;
        }
        set {
            _firstName=value;
        }
    }

        public string LastName { get => _lastName; set => _lastName = value; }
        public int StudentNo { get => _studentNo; set => _studentNo = value; }


//Eger belli sartlar var ve bu belli sartlar her harukarda boyle olmasi gerekiyor ve bu class in herhangib bir instancesi olusturulup da kullanilmak istendiginde bu sekilde calismasi gerekiyor ise o zaman, olmasi gereken bunu direk property uzerindeki getter icinde field ile logic yazarak yapmaktir...Yani biz islemi burdan yaparsak artik bu heryere acik olmamis oluuyor ve biz aslinda kapsullemis oluyoruz ve belli sartlar koymus oluyoruz
//ISTE BABA GIBI ENCAPSULATION ORNEGI....
//Bis iste bu logic yaptigmiz yerde hata firlatabiliriz, farkli logic ler yapabiliriz loglama yapabiliriz burda istedigmiz gibi 
//oyanama yabiliriiz...Property leri ve getter-setter i kullanarak iste encapsulatioin i yonetiyoruz
        public int Class { get{
            return _class;
        } set {
            if(value < 1){//Cok dikkat edelim value kullanicin girecegi degeri temsil ediyor.Buraya _class gelmez yoksa hata aliriz...Yani zaten biz _class i kiyaslamayacagiz class a verilmek istenen degeri sorgulayaip da onu kontrol altina almaya calisacagiz..
                Console.WriteLine("1 .sinifdan daha asagi dusurulemez, en az 1 olmalidir..");
                _class=1;
            }else{
                _class=value;
            }
        } }
        /*
public string LastName {
get {
return _lastName;
}
set {
_lastName=value;
}
}
*/
     public Student(string lastName, string firstName, int studentNo, int @class)
        {
            LastName = lastName;
            FirstName = firstName;
            StudentNo = studentNo;
            Class = @class;
        }
        public Student(){

        }
        public void ShowStudentInfo(){
            Console.WriteLine($"Student info: ");
            Console.WriteLine($"StudentName: {this.FirstName}");
            Console.WriteLine($"StudentLastName:{LastName} ");
            Console.WriteLine($"StudentNo:{StudentNo} ");
            Console.WriteLine($"StudentClass:{Class} ");
        }


        public void SinifAtlat(){
            Class+=1;
        }

        public void SinifDusur(){
            Class-=1;
        }

    }
}
