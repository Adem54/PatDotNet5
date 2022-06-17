using System;

namespace operators{
internal class Program
{
    private static void Main(string[] args)
    {
        int x=3;
        int y=3;
        y+=2;
        Console.WriteLine($"x: {x} - y: {y}");
        y*=2;
        x+=3;
        Console.WriteLine($"x: {x} - y: {y}");

        x/=2;
        Console.WriteLine($"x: {x} - y: {y}");

        bool isSuccess=true;
        bool isCompleted=false;

//Tek satir islemler icin curlybrackets kullanmamiza gerek yok!Suslu prantez kullanmaya gerek yok
        if(isSuccess)Console.WriteLine("isSuccess basarilidr");
    }
}
}
