using System;
namespace TelRehberApp{
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
//         int n=114424;
//         var result=Math.Floor(Math.Log10(n) + 1);

//         var result2=n.ToString().Length;
//         Console.WriteLine("result:{0}",result);
//         Console.WriteLine("result2:{0}",result2);

// //Boyle birsey girildiginde hata mesaji verecek tel numarasini 8 hane girmelisin diye
        Person person1=new Person(1,"Adem","Erbas","35438");
        Console.WriteLine(person1.TelNumber);
            // ManageConsole manageConsole=new ManageConsole();
            // manageConsole.ChooseYourTransact();
           
    }
}
}
