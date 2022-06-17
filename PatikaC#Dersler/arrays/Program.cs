using System;
namespace arrays{
internal class Program
{
    private static void Main(string[] args)
    {
        string[] cities=new string[3]{"Skien","Ski","Bø"};
        string[] colors={"Blue","Red","Green"};
        string[] names=new string[2];
        names[0]="Adem";
        names[1]="Zehra";
        
        //Array sinifinin methodlari
        //Array bir class tir ve ya Array static class tir ya da methodlar static methodlardir burdan onu anlayabiliriz....
        int[] numberArray={23,12,45,76,37,49};
        Array.Sort(numberArray);//Array.Sort uzerine gelirsek Sort methodunun void metod oldugunu anlayabiliriz ve direk icine vedgimiz dizi de dogrudan degisiklik yaptingi gorebiliriz....ondan dolayi ezbere islem yapip da Array.Sort(numberArray) den gelen degeri baska bir degere atamaya calismayalim
        Console.WriteLine("numberArray sort: ");
        foreach (var number in numberArray)
        {
            Console.WriteLine(number);
        }
        Array.Reverse(numberArray);
        Console.WriteLine("numberArray reverse: ");
        foreach (var number in numberArray)
        {
            Console.WriteLine(number);
        }
        Console.WriteLine("Array Clear methodu");
        Array.Clear(numberArray,2,2);//2.index ten basla 2 elemenin yerine 0 yaziyor
         foreach (var number in numberArray)
        {
            Console.WriteLine(number);
        }
        //IndexOf methodu
       int index= Array.IndexOf(numberArray,23);
       //23 degerinin index numarasini ver demis oluyoruz ve bize index numarasini veriyor eger boyle bir eleman olmasa idi o zaman bize -1 verecekti....
       Console.WriteLine("index: "+ index);
        //Resize ile yeniden boyutlandirma 
        Array.Resize(ref numberArray,8);//numberArray dizimizin boyutunu 10 yap diyoruz
        numberArray[6]=82;
        numberArray[7]=39;
         foreach (var number in numberArray)
        {
            Console.WriteLine(number);
        }
    }
}
}
