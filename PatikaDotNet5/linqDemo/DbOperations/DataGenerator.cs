
using System.Linq;
namespace linqDemo {
    public class DataGenerator 
    {
            //Bir initialize static method olustracagiz class ismi ile direk erismek istiyrz
        public static void Initialize(){
            using(var context=new LqDbContext()){
                if(context.Students.Any())
                {
                    return;
                }
                context.Students.AddRange(
                    //StudentId leri kaldiracagiz AutoIncrementi Student entity class i icinde yaptigmiz zaman
                    new Student(){
                        StudentId=1,
                        Name="Adem",
                        Surname="Erbas",
                        classId=1
                    }, new Student(){
                        StudentId=2,
                        Name="Zeynep",
                        Surname="Erbas",
                        classId=1
                    }, new Student(){
                        StudentId=3,
                        Name="Zehra",
                        Surname="Erbas",
                        classId=2
                    }
                );

                context.SaveChanges();
                //Uygulama baslarken inmemory data da olmasini istedigmiz datayi da ekledik o zaman simdi de
                //Uygulama ilk ayaga kalkarken, burdaki datayi veritabanina ekleyerek initialize ederek baslasinki
                //Uygulamamiz ilk geldiginde bu datalarla gelsin....Yani her uygulama baslarken bu methodu calistirarak baslamali
                //Bunu da Program.cs de yapmaliyiz
            }
        }

    }
}