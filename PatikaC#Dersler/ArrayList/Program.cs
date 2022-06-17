using System;
using System.Collections;

internal class Program
{
    private static void Main(string[] args)
    {
        //ArrayList-System.Collections namespace 
        //Arraylist ler bir koleksiyon tipidir
        //ArrayList ler dinamik yapilardir birden fazla veri tipini bunyesinde barindirabilen yapilardir
        //ArrayList lerin icerisinde birden fazla veri tipini ayni anda saklayabiliriz 
        ArrayList myArrayList=new ArrayList();
        // myArrayList.Add("Adem");
        // myArrayList.Add(24);
        // myArrayList.Add(true);
        // myArrayList.Add(12.5);
        // myArrayList.Add('a');

        //Icindeki verilere eriselim...Indexer ile eriselim...
        //ArrayList icindeki elemanlari da ekrana yazdirabiliyoruz...
       // Console.WriteLine(myArrayList[3]);
       foreach (var item in myArrayList)
       {
           Console.WriteLine(item);
       }
       //AddRange ile birden fazla elemani toplu halde ekleyebiliriz..
       string[] colors={"red","green","yellow"};
       List<int> numbers=new List<int>{67,36,19,28,12,45};
      // myArrayList.AddRange(colors);//ICollection tipinde hersey ekleyebiliyruz, yani dizi ekleyebiliyoruz....
       myArrayList.AddRange(numbers);//icersine toplu bir liste de ekleyebiliriz...
        foreach (var item in myArrayList)
        {
            Console.WriteLine(item);
        }
        //Sort
        //Icine farkli tipler atabilmem bazi durumlarda tabi karisikliga yol acacak ornegin sortlama isleminde oldugu gibi
        Console.WriteLine("Sort");
        myArrayList.Sort();//Compile time da hata vermezken run time da patlayacaktir...
        //Sirlamak icin kendi icinde Compare etmeye calisiyor,ama compare edemiyor cunku farkli tipler string ile int i compare edemiyor...
        //Ama icerisindekiler sadece int olursa Sort isleminde hata almayiz...
        foreach (var item in myArrayList){
            Console.WriteLine("item: "+item);
        }
        //BinarySearch islemi
        //Kendi icinde siralammiz gerekiyor kendi icinde, yani once sort islemi yapmak gerekiyor binary search islemini yapabilmek icin...
        Console.WriteLine(myArrayList.BinarySearch(45));//Siralandiktan sonra 4. inddex te bulunuyor...
        Console.WriteLine("REverse");//Buyukten kucuge siralar, yani tersten siralama
        //Reverse
        myArrayList.Reverse();
        foreach(var item in myArrayList){
            Console.WriteLine(item);
        }
        //Clear ile de listeyi temizleme
        myArrayList.Clear();
        Console.WriteLine(myArrayList.Count);

    }
}

