using System;
using System.Linq;

namespace linqDemo
{
  static  class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
             //Datageneratoruin icindeki Initialize methdunu calistirarak tum veriyi inMemorydataya insert etmemiz gerekiyor
            DataGenerator.Initialize();
            LqDbContext context=new LqDbContext();
            var students=context.Students.ToList<Student>();
            //Burda ekledigmiz students lari alabiliyoruz, inmemory database de gorebiliyoruz...
            //Find-Verilen dbset icinde primary key olarak verilmis alani arama yapip o objeye erisilmesini sagliyor
            Console.WriteLine("***************** Find methodu");
            var student=context.Students.Where(student=>student.StudentId==2).SingleOrDefault<Student>();
            //Burasi normalde Where kullaninca liste icinde doner ama bunu sonrasinda SingleOrDefult
            // yaparsak o zaman aradigmiz veriyi ilk buldugu yerdekini getirir
            //Ama ayni seyi biz iste sadece Find kullanarak yapabiliriz
             var student2=context.Students.Where(student=>student.StudentId==2).ToList();
             //ToList dersem Studentten olusan generic collection list getirir
             //ToList vermez isem o zaman IQuarayble turunden getirir
             //Dikkat edelim Find icerisinde hazir delege bir fonksiyon yok,
             //predicate,func veya action hazir delegelerinden herhangi birisi yok
             //Find parametresine hangi id li datayi almak istersek onu veririz ve bize o id ye ait objeyi donduruyor
            student=context.Students.Find(2);
            Console.WriteLine($"student: {student.Name}");

            Console.WriteLine("***************** FirstOrDefault methodu");
            student=context.Students.Where(s=>s.Surname=="Erbas").FirstOrDefault<Student>();
            //Burda ilk buldugu datayi getirecek eger bu sart birden fazla data icin gecerli degil ise
            Console.WriteLine($"student: {student.Name}");
            //FirsOrDefault ile bu sekidle Where i kullanmadan da getirebiliriz...
            student=context.Students.FirstOrDefault(x=>x.Surname=="Erbas");
            Console.WriteLine($"student: {student.Name}");
            //Sadece first kullanirsan eger eger data var ise first ile firstOrDefault ayni sekilde calisir 
            //ama eger First kullanirken kendisine verilen
            //dizi de sarta uyacak hic data bulamaz ise o zaman hata firlatir, aama firstOrDefault da datayi 
            //bulamazsa null donduruyor....hata firlatmak yerine
            student=context.Students.SingleOrDefault(x=>x.Name=="Zehra");
            //SingleOrDefault un FirstOrDefaulttan farki sudur, SingleOrDefault verdigimiz sarti 
            //saglayan 1 tane data bekler, eger 1 den fazla datanin gelecegi bir sart olursa hata verir, iste farki budur....
         //   student=context.Students.SingleOrDefault(x=>x.Surname=="Erbas");//Ornegin burda hata donecektir..
            //Unhandled exception. System.InvalidOperationException: Sequence contains more than one element boyle bir hata aliriz
                var studentList=context.Students.Where(student=>student.classId==1).ToList<Student>();
                Console.WriteLine(studentList.Count);
            //Orderby ile sort islemleri yapabiliriz ve cok fazla ihtiyacimiz olacak...
            students=context.Students.OrderBy(x=>x.StudentId).ToList<Student>();
            //Default olarak siralama 1 den n ye , a dan z ye ascending dir...
            students=context.Students.OrderByDescending(x=>x.StudentId).ToList<Student>();
            //Tersten siralar, buyukten kucuge, z den a ya
            foreach (var std in students)
            {
                Console.WriteLine($"student: {std.Name},  studentId: {std.StudentId} ");
            }
              //Burasi coook onemli....Anonymus Object result....
              //Biz her zaman entity donduk yani veritabani tablasunun karsiligi olan obje tipinde 
              //ve o objenin icindeki degerler olarak donduk ama biz
              //bunu istemiyoruz onun yerine tamamen farkli datalar donmek istersek ne yapacagiz...  
              //Biz select ile anonim ve deger donerken icinde istedigmiz kolonlari donruecek sekilde anonm bir obje donebilirz
              var anonymusObj=context.Students.Where(x=>x.StudentId==2).Select(x=>new {Id=x.StudentId, FullName=x.Name+ " "+ x.Surname});

              foreach (var item in anonymusObj)
              {
                Console.WriteLine("id: {0}  fullname: {1}  ", item.Id,item.FullName);
              }
              //Gordgumz gibi Select sayesinde illaki direk bizim geri donen objemiz ile ayni
              // seyi donmek zorunda degiliz....tamaen icinde kend istedigmiz kolonlari oldugu yeni bir obje de donebilirz...
            
        }
    }
}
