using System;
namespace RecursiveAndExtensionMethods
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Recursive Methodlar-Oz yinelemeli-Kendi kendini cagiran methodlar
            //-Bu methodlar foreach veya diger dongulerle yapabildigmiz bazi isleri bu tarz fonksiyonlar la da yapabiliriz, bazen belli bir sart saglanmama durumunda fonksyon bastan baslamasini isteyebiliriz....mesela
            //Coook onemlidiir...Faktoriyel alma, sayilarin ussunu alma islemlerini hesaplamayi recursive fonksiyon ile yapabiliriz...
            //Veya verilen bir sayinin 4.kuvvetinu bulan fonksiyon olusturalim
            Person person = new();//C#9 ile instance olusturmayi bu sekilde yapabiliyoruz
            Console.WriteLine("Expo methodunu test edelim....");
            var result1 = person.Expo(3, 4);//3 uzeri 4=>81
            var result2 = person.Expo(5);//faktoriyel aliyor...5 un 5*4*3*2*1=>120
            Console.WriteLine($"result1: {result1}");//result1: 81
            Console.WriteLine($"result2: {result2}");//result2: 120
            var result3 = person.Expo2(3, 4);
            Console.WriteLine($"result3: {result3}");//81
            var result4 = person.Expo2(5);
            Console.WriteLine($"result4: {result4}");//120


            Console.WriteLine("EXTENSION METHODS");
            //Extension methodlar cok onemlidir ve cok fazla ve cok kritik yerlerde cok isimize yariyor.Bazen mevcut olan fonksiyonu daha kolay kullanabilmek icin extension methoda donusturebiliriz ya da cok kullandgimiz bir islem var ise onu extension metoda donusturup hizlica ona erisiriz...
            string expression = "Adem Erbas";
            //Verdigimiz string text icersiinde bosluk olup olmadigini bana donen bir extension method yazmak istiyorum mesela
            //Asagida bir tane Extension class ve icerisine de extension metodumuz olusturacagim burda bilmemiz gereken en onemli seylerden bir tanesi extension class ve extension methodlarimiz static olmalidir.Extension lara biz instance uretmeden erisebiliyor olmamiz gerekiyor o yuzden static olmalidir
            //Extension method yapmaya calisirken once problemi normal yollarla cozen bir method yazalim ardindan o methodu extension method olarak yazalim...
            //Methodumuz extension olmasi icin parametreye en sola this vermemiz gerekir ve this ifadesini de verdikten sonra artik bir extension method olmus oluyor
            //Extension metodun parametresine verilen veri tipi ne ise extension metod sadece o veri tiplerinde uygulanabilir yani bizim yazdigimz CheckSpaces extension metodu parametreye biz this string param vermistik dolaysi ile sadece string lerde uygulanacak bir extension metod yazmis oldukk....
            bool result5 = expression.CheckSpaces();
            Console.WriteLine(result5);
            //Simdi de bosluk var ise boslugu silen veya baska bir karakter ile degistiren bir extension metod daha yazalim...
            if (result5)
            {
                Console.WriteLine(expression.RemoveWhiteSpaces());
            }
            //Verilen string ifadeyi buyuk harfe ceviren baska bir extension yazalim
            var result6 = expression.MakeUpperCase();
            Console.WriteLine("result6: " + result6);
            string text = "Zehra ERBAS";
            var result7 = text.MakeLowerCase();
            Console.WriteLine("result7: " + result7);

            //DIZIMIZI SIRALAYAN BIR EXTENSION YAZALIM
            //Ornegin bir tane integer diziyi siralayan bir extension metod yazalim biz dizi siralamasini Array.Sort ile yapiyorduk ama istiyoruz ki dogrudan dizi nin kendisi uzerinden yapalim, javascriptteki gibi iste o zaman extension metod yazariz...
            //Bir daha anlayalim bu mantigi biz extensionn metodu parametreye hangi tipi veriyorsak o tip lerdeki verilerimize datalarimiza uygulayabiliriz sadece...
            int[] numbers = { 12, 6, 34, 25, 19, 45 };
            var result8 = numbers.SortArray();
            result8.ShowArray();
            Console.WriteLine("ReverseArray");
            var result9 = numbers.ReverseArray();
            result9.ShowArray();
            //Verilen integer sayinin cift olup olmadigini kontrol eden bir extension method yazalim
            int testNumber = 9;
            int testNumber2 = 10;
            Console.WriteLine(testNumber.CheckNumberIfEven());
            Console.WriteLine(testNumber2.CheckNumberIfEven());
            //Verilen stringin ilk karakterini veren extension metod yazalim....
            string testText = "Adem";
            Console.WriteLine(testText.GetFirstChar());//'A'
            Console.WriteLine(testText.GetCharacter());//"A"

           var result10= FibonacciSerisi(6);
           Console.WriteLine("result10: "+ result10);
             static int FibonacciSerisi(int sayi)
            {
                if (sayi == 0)
                    return 0;
                else if(sayi == 1)
                  return 1;
                   else
                    return FibonacciSerisi(sayi - 1) + FibonacciSerisi(sayi - 2);
            }//FibonacciSerisi(6-1)+FibonacciSerisi(6-2)
            //FibonacciSerisi(5-1)+FibonacciSerisi(5-2) + //FibonacciSerisi(4-1)+FibonacciSerisi(4-2) 
            //FibonacciSerisi(5-1)+FibonacciSerisi(5-2) + //FibonacciSerisi(4-1)+FibonacciSerisi(4-2) Burda hata method lar icinde parametre 1 degil ve her bir metod invoke edilmesinden 2 tane metod daha invoke ediliyor ve 1 oluyor parametre artik ve de return 1 oluyor tabi ki dolayisi ile de 8 tane ayri metod invok ediliyor sonuc olarak ve return 1 old icinde 8 oluyor...

        }
    }


    public static class Extension
    {
        public static bool CheckSpaces(this string param)
        {
            return param.Contains(" ");
        }
        //Bosluklari degistirdigi string i donmesini istiyoruz
        public static string RemoveWhiteSpaces(this string param)
        {
            string[] array = param.Split(" ");
            //String i bosluklara gore ayir ve diziye at 
            //Adem Erbas 1 tane boyle bosluk oldugu icin 2 kelimeye ayirir Adem ve Erbas diye 2 elemanli bir dizi olusturur
            //["Adem","Erbas"] seklinde dizi yi de aralarinda hic bosluk olmayacak sekilde string olarak birlestirir string.Join("",array);
            return string.Join("", array);//
        }
        public static string MakeUpperCase(this string param)
        {
            return param.ToUpper();
        }

        public static string MakeLowerCase(this string param)
        {
            return param.ToLower();
        }

        public static int[] SortArray(this int[] array)
        {
            Array.Sort(array);
            return array;
        }

        public static int[] ReverseArray(this int[] array)
        {
            Array.Reverse(array);
            return array;
        }

        public static void ShowArray(this int[] param)
        {
            foreach (var item in param)
            {
                Console.WriteLine(item);
            }
        }

        //isNumberEven ismi de verebilirdik...
        public static bool CheckNumberIfEven(this int param)
        {
            return param % 2 == 0 ? true : false;
        }

        public static char GetFirstChar(this string param)
        {
            return param[0];
        }
        public static string GetCharacter(this string param)
        {
            return param.Substring(0, 1);//0.karakterden basla 1 karakter getir
        }
    }


    public class Person
    {

       
        public int Expo2(int number, int us)
        {
            int result = 1;
            for (int i = 1; i <= us; i++)
            {
                result *= number;
            }
            return result;
        }

        public int Expo2(int number)
        {
            int result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }
        public int Expo(int number, int us)
        {
            if (us < 2)
            {
                return number;
            }
            return Expo(number, us - 1) * number;
            //Expo(3,3)*3
            //Expo(3,2)*3*3
            //Expo(3,1)*3*3*3  Expo(3,1) us 1 oldugunda return number i donecek method direk yani 3 u donecek yine
            //3*3*3*3 olur sonuc....
        }

        public int Expo(int number)
        {
            if (number == 1)
            {
                return number;
            }
            return Expo(number - 1) * (number);
            //return Expo(4)*5
            //return Expo(3)*4*5
            //return Expo(2)*3*4*5
            //return Expo(1)*2*3*4*5 //Expo(1) parametesi 1 oldugunda da Expo(1) direk parametresindeki degeri donecek
            //Dolayisi ile   1*2*3*4*5 dir sonucu....
        }
    }

}
