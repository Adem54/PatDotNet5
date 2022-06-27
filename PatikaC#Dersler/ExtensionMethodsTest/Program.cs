// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string myText="Welcome to my home";
var result=myText.FindFirstLetter();
Console.WriteLine("result: "+result);
/*
Extension mehtodlarda bilmemiz gerekenler
1-Extension methodlar her tipe uygulanabilir
2-Extension methodlar i kullanirken bizim o extension metodunun icinde yazildigi
class ile isimiz yok, dogurdan hangi tip icin yapildi ise onun instancesi 
ile dot,nokta notasyonu ile kullanilir
3-Bize dotnet ten gelen kullandigmiz ozel tipler icin de 
extension method yazabiliriz ki zaten ileri seviye ve kreativ islemleri de
bu sekilde yapiyuoruz genelde,
4-Cok kullanilan ve cok ise yarayan bir konududr cok iyi bilmekte fayda var
cunku cok tikanildigi zaman bu kullanilarak cok pratik cozumler uretiliyor
*/

public static class MyString
{
    public static char FindFirstLetter(this string text){
        var result=text.Substring(0,1);
        return Convert.ToChar(result);
    }
}
