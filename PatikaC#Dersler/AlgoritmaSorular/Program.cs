using System;

namespace AlgoritmaSorular
{
    internal class Program
    {
        private static void Main(string[] args)
        {
           //ConsoleApp.ShowTask1();
           //ConsoleApp.ShowTask2();
           //ConsoleApp.ShowTask3();
           ConsoleApp.ShowTask4();
        
        }
    }

    public class ConsoleApp{
        
        public static void ShowTask1(){
            Console.WriteLine("Pozitif bir sayi gir:");
            string? number = Console.ReadLine();
            bool isNumber = int.TryParse(number, out int outNumber);//number sayisini cevirebilirse onu outNumber a atar ve biz onu disarda alabiliriz...degilse outNumber 0 gelir 
            if (isNumber)
            {
                Console.WriteLine($"{outNumber} adet sayi giriniz");
                string allNumbers="";
                int count = 0;
                while (true)
                {
                    count++;
                    Console.WriteLine($"{count}.   sayiyi giriniz");
                    string? newNumber = Console.ReadLine();
                    allNumbers+=count==outNumber ? newNumber : newNumber+" ";//Son sayi nin da sagina bosluk birakacak ama biz onu istemiyoruz....cunku split ile dizi icine aralarindaki bosluga gore atacagiz...
                    Console.WriteLine(allNumbers);
                    if(count==outNumber)break;
                }
                //Stringi bosluklarina gore ayir ve dizi icine at-Split ile
                string[] numberArray=allNumbers.Split(" ");
                foreach (var item in numberArray)
                {
                   // Console.WriteLine("item: "+item);
                    int myNumber=Int32.Parse(item);
                    if(myNumber%2==0){
                      Console.WriteLine("myNumber: "+myNumber);
                    }
                }
            }
        }


        public static void ShowTask2(){
            int count=0;
            Console.WriteLine("Pozitif 2 sayi girin");
            int? m=null;
            int? n=null;
            while(true){
                count++;
                Console.WriteLine($"{count}. sayi giriniz");
               
                string? number=Console.ReadLine();
                Console.WriteLine("count: "+count);
                if(count==1){
                    Console.WriteLine("Count: "+count);
                    n=Convert.ToInt32(number);

                }else if(count==2){
                    Console.WriteLine("Count: "+count);
                    m=Convert.ToInt32(number);

                }
                if(count>=2) break;
               
            }


            Console.WriteLine("-----------------");
            int count2=0;
           Console.WriteLine($"{n} adet sayi giriniz");
            string myNumbers="";
            while(true){
                count2++;
                Console.WriteLine($"{count2}. sayiyi gir");
                string? myNewNumber=Console.ReadLine();
                myNumbers+=count2==n ? myNewNumber : myNewNumber+ " ";
                if(count2==n)break;
            }

            string[] myArray=myNumbers.Split(" ");
            foreach (var item in myArray)
            {
                int myItem=Convert.ToInt32(item);
                if(myItem % m == 0){
                    Console.WriteLine("myItem: "+myItem);
                }
            }
        }

        public static void ShowTask3(){

            Console.WriteLine("Pozitifi bir sayi girin?");
            string? number=Console.ReadLine();
              string allWords="";
            if(!string.IsNullOrEmpty(number)){
                   bool isNumber = int.TryParse(number, out int n); //Cevirme islemi dogruca yaplirsa yani stringe girilen deger hem dogru formatta hem de null degil ise ya da " " empty degil ise , int gelir ise returns true
                   if(isNumber){
                         int count3=0;
                       
                         Console.WriteLine($"{n} adet kelime gir");
                         while(true){
                             count3++;
                             string? word=Console.ReadLine();
                            allWords+=count3== n ? word : word + " "; 
                            if(count3==n)break;
                         }

                   } 
                   string[] arrayWord=allWords.Split(" ");
                   Array.Reverse(arrayWord);
                   foreach (var item in arrayWord)
                   {
                       Console.WriteLine("word: "+item);
                   }
            }
        }

        public static void ShowTask4(){
            Console.WriteLine("Bir cumle yaziniz");
            string? mySent=Console.ReadLine();
            if(!string.IsNullOrEmpty(mySent)){
                string[] arrayWord=mySent.Split(" ");//Aralarina bosluk lara gore ayiriyor ve diziye ceviriyor burda bu kelimelerin aralarinda bosluk oldugu icin
                //bu sekilde bolebiliyor,aralrinda virgul olsa idi virgule gore bolecekti
                Console.WriteLine("Word Number: "+ arrayWord.Length);
                string newSent=string.Join("",arrayWord);//Dizi icindeki string ifadeleri aralarinda hic bosluk birakmadan birlestiriyor, daha dogrusu aralarina "" boyle bir sey koyuyor ama bu "" hicbosluk olmasin demek oldugu icin hicbosluk birakmamis oluyor mesela biz "*" koysa idik o zaman da arlarinda * olacakti
                Console.WriteLine("newSent: "+ newSent);
                Console.WriteLine("Char Number: "+ newSent.Length);
     }   
        }

    }
}


/*
Bir konsol uygulamasında kullanıcıdan pozitif bir sayı girmesini isteyin(n). Sonrasında kullanıcıdan n adet pozitif sayı girmesini isteyin. Kullanıcının girmiş olduğu sayılardan çift olanlar console'a yazdırın.
Bir konsol uygulamasında kullanıcıdan pozitif iki sayı girmesini isteyin (n, m). Sonrasında kullanıcıdan n adet pozitif sayı girmesini isteyin. Kullanıcının girmiş olduğu sayılardan m'e eşit yada tam bölünenleri console'a yazdırın.
Bir konsol uygulamasında kullanıcıdan pozitif bir sayı girmesini isteyin (n). Sonrasında kullanıcıdan n adet kelime girmesi isteyin. Kullanıcının girişini yaptığı kelimeleri sondan başa doğru console'a yazdırın.
Bir konsol uygulamasında kullanıcıdan bir cümle yazması isteyin. Cümledeki toplam kelime ve harf sayısını console'a yazdırın.
*/