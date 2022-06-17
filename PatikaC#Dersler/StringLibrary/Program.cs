using System;
namespace StringLibrary{
internal class Program
{
    private static void Main(string[] args)
    {
        string myValue="Dersimiz CSharp hosgeldiniz";
        string myValue2="dersimiz CSharp hosgeldiniz";
        //karaktre sayisi-Length
        Console.WriteLine(myValue.Length);
        Console.WriteLine(myValue.ToUpper());
        Console.WriteLine(myValue.ToLower());
        //Concat birlestirmek-Iki stringi birlestirmek
        Console.WriteLine(string.Concat(myValue," Merhaba"));
       //Dizilerde 2 diziyi birlestirirken bu sekilde yapariz
        // int[] myNum={3,6,8};
        // int[] myNum2={5,12,9};
        // myNum=myNum.Concat(myNum2).ToArray();
        // foreach (var item in myNum)
        // {
        // Console.WriteLine("item: "+item);
        //  }
        //Compare, CompareTo- Karsilastirma yapmak
        //1.degisken 2.degiskene esit olursa 0, 1.degisken 2.degisken den buyuk ise 1, kucuk ise -1 donduruyor
        Console.WriteLine(myValue.CompareTo(myValue2));
        //Compare- string ile kullaniliyor...
        Console.WriteLine(String.Compare(myValue,myValue2,true));//ignore case durumu, karsilastirmayi yaparken buyuk kucuk harf duyarsiz hale gelir eger 3.parametreyi true yaprsak yok false yaparsak o zaman da buyuk kucuk harf duyarli hale gelecektir..Buyuk kucuk harf duyarliligini kaldirinca esit olduklari icin 0 verir sonucu
         Console.WriteLine(String.Compare(myValue,myValue2,false));
         string myText="CSharp";
         //Contains
         Console.WriteLine(myValue.Contains(myText));//Bir text icerisinde baska bir text var mi onu check eder
         Console.WriteLine(myValue.EndsWith("hosgeldiniz"));//text in sonu icine verdgimiz text ile bitiyorsa o zaman true verir degilse false verir   
         Console.WriteLine(myValue.StartsWith("CSharp"));//False

         //IndexOf-Icerisine text icinden verilen text veya char akteri arar ve nerde kacinci index te bulursa onu getirir,bulamaz ise -1 doner
         Console.WriteLine(myValue.IndexOf('a'));//a myValue stringinin kacinci karakteri ise o degeri verir...
         Console.WriteLine(myValue.IndexOf("Qw"));//a myValue stringinin kacinci karakteri ise o degeri verir...

         //Insert-string icinde istedgimiz yere yeni string ifadeler ekleyebiliriz
         Console.WriteLine(myValue.Insert(9,"burda"));
         Console.WriteLine(myValue.LastIndexOf('i'));//i den ornegin birden fazla var ise en son kacinci index te ise onu getirir

         //PadLeft-PadRight
         Console.WriteLine(myValue+myText.PadLeft(30));//myValue2 nin sonuna icerisine verdgimiz sayiya tamamlayacak kadar bosluk ekler...
         //myValue2 nin karakter sayisi ve arti bosluk saysi beraber 30 olmasi gerekiyor..............Total de 30 karakterlik yer ayiriyor ve myText in karakter sayisindan geri kalan boslugu sola ekliyor
         Console.WriteLine(myValue+myText.PadLeft(30, '*'));//myText in sol tarafinda myText in karakter sayisindan 30 a kac karakter kaldi ise o kadar karakteri sol tarafa * olarak ekleyecektir....
         Console.WriteLine(myValue.PadRight(50)+myValue2);//myValue2 nin sonuna icerisine verdgimiz sayiya tamamlayacak kadar bosluk ekler...
         //Simdide myText in karakter sayisini dusunce 30 sayisina kac tane kaldi ise o kadar karakter boslugunu sag tarafa ekler...
        Console.WriteLine(myValue.PadRight(50,'-')+myValue2);//myValue karakter sayisindan 50 ye kadar olan sag taraftaki bosluga - cizgi karakteri ile dolduracaktir
         //Remove fonksiyonu
         Console.WriteLine(myValue.Remove(16));//icine verdgimiz index ten sonraki kismi siler
        //Donus olarak da biz e girdigmiz index degerinden oncesini getiriyor
        Console.WriteLine(myValue.Remove(5,3));//5.karakterden basla 3 karakter sil
        Console.WriteLine(myValue.Remove(0,1));//En bastakli karakteri siler
        //Replace-fonksiyonu
        Console.WriteLine(myValue.Replace("CSharp","C#"));//CSharp yerine C# yazar
        Console.WriteLine(myValue.Replace(" ","*"));//Bosluklari * a cevirecektir...
        //Split-String imizi icine verdigimz kritere gore parcalayip dizi icine atar...
        Console.WriteLine(myValue.Split(' ')[1]);//Bosluklara gore parcala ve diziye at ve bana da bu dizinin 1.elemanini getir dersek
       //Substring
    Console.WriteLine(myValue.Substring(4));//1 tane sayi verirsek o indexten baslayip sonuna kadar getirir
     Console.WriteLine(myValue.Substring(4,6));//4.indexten baslayarak 6 karakteri bize getirecektir
        //String fonksiyonlari ile biz yapacagimiz bir cok islemi cok kisa ve kolay birsekilde yapabiliyoruz onda dolayi bir problemle karsilasinca hemen onu for dongusu vs kullanarak cozmeye calismayalim da, onun yerine, once string methodlarina basvurmamiz gerekir cunku bizim isimizi cok ciddi kolaylastiriyorlar
    }
}
}
