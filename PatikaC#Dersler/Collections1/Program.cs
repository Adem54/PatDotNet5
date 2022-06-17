using System;
namespace Collectins1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //1 ve kucuk deger girerse, ayni degeri 2 kez girerse...
            Console.WriteLine("Hello, World!");
            // List<int> primeNumbers = new List<int>();
            // List<int> nonPrimeNumbers = new List<int>();
            // int count = 0;
            // while (true)
            // {

            //     count++;
            //     Console.WriteLine("Bir sayi giriniz");
            //     string? number = Console.ReadLine();
            //     if (int.TryParse(number, out int outNumber))
            //     {
            //         if (outNumber> 1)
            //         {
            //             Console.WriteLine($"You entered {count}. number ");
            //             bool isPrimeNumber = IsPrime(outNumber);
            //             // isPrimeNumber ? primeNumbers.Add(outNumber) : nonPrimeNumbers.Add(outNumber); 
            //             if (isPrimeNumber)
            //             {
            //                 primeNumbers.Add(outNumber);
            //             }
            //             else
            //             {
            //                 nonPrimeNumbers.Add(outNumber);
            //             }
            //         }else{
            //             Console.WriteLine($"Please enter the number which ise greater than 1");
            //              count--;
            //         }

            //     }
            //     else
            //     {
            //         Console.WriteLine("Please write int value");
            //         count--;
            //     }

            //     if (count == 20) break;
            // }
            // Console.WriteLine("Prime Numnbers");
            // primeNumbers.Sort();
            // Console.WriteLine("Count: "+primeNumbers.Count);
            // var result=FindAverage(primeNumbers);
            // Console.WriteLine("primeNumbersAverage: "+result);
            // primeNumbers.ForEach((item) => Console.WriteLine(item));
            // Console.WriteLine("NonPrime Numnbers");
            // nonPrimeNumbers.Sort();
            // Console.WriteLine("Count: "+nonPrimeNumbers.Count);
            // var result2=FindAverage(nonPrimeNumbers);
            // Console.WriteLine("NonPrimeNumbersAverage: "+result2);
            // nonPrimeNumbers.ForEach((item) => Console.WriteLine(item));

            // int[] arrays1={171,23,47,17,71,139};
            // int[] arrays2={982,24,49,18,72,138};
            // var result1=arrays1.GetFirstThreeMaxNumbers();
            // var result2=arrays2.GetFirstThreeMaxNumbers();
            // foreach (var item in result1)
            // {
            //     Console.WriteLine(item);
            // }
            //     Console.WriteLine("---------------------------");
            
            //   foreach (var item in result2)
            // {
            //     Console.WriteLine(item);
            // }

            string myText="Welcome to my class";
            string vowel="aeoui";
            string myTextVowel="";
            foreach(var item in myText){
                Console.WriteLine(item);
                if(vowel.Contains(item)){//Buraya son giris te bizim islemi yapmamiz gerekir...
                   
                    myTextVowel+=item == 'a'  ? item : item+" ";
                    Console.WriteLine("myTextVowel: "+myTextVowel);
                }
            }
            Console.WriteLine("-----------------"+myTextVowel);
            var vowelArr=myTextVowel.Split(" ");
            Array.Sort(vowelArr);
            foreach(var item in vowelArr){
                Console.WriteLine("itemmmmm: "+item);
            }

        }

        public static bool IsPrime(int number)
        {
            bool result = false;
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    break;
                }
                else if (i == number - 1)
                {
                    Console.WriteLine("i: " + i);
                    result = true;
                }
            }
            return result;
        }


        public static double FindAverage(List<int> list){
            int sum=0;
            for(int i=0; i<list.Count; i++){
                int item=list[i];
                sum+=item;
            }
            Console.WriteLine("sum: "+sum);
            double average=sum/list.Count;
            return average;
        }
            public static double FindAverage(int[] array){
            int sum=0;
            for(int i=0; i<array.Length; i++){
                int item=array[i];
                sum+=item;
            }
            Console.WriteLine("sum: "+sum);
            double average=sum/array.Length;
            return average;
        }
    }

    public static class Extension{

        public static int[] GetFirstThreeMaxNumbers(this  int[] array){
            //Bir int array listesini kucukten buyuge dogru siralamak icin sadece Array.Sort() metodu yeterlidir ancak buyukiten kucuge dogur sirlamak icin ise once Sort methodu sonra Reverse methodu kullanmamiz gerekir cunnku Reverse sadece listeyi tersine cevirir biz listeyi Sort ile kucukten buyuge siralar isek sonra da Reverse ile tersine cevirir isek o zaman listemiz buyukten kucuge siralanmis olacaktir...
                        Array.Sort(array);
                        Array.Reverse(array);
                        foreach (var item in array)
                        {
                            Console.WriteLine("item: "+item);
                        }
                        Console.WriteLine("-----------");
                        var results=array.Take(3).ToArray();
                        return results;
        }
    }
}

/*
Soru - 1: Klavyeden girilen 20 adet pozitif sayının asal ve asal olmayan olarak 2 ayrı listeye atın. 
(ArrayList sınıfını kullanara yazınız.)
Negatif ve numeric olmayan girişleri engelleyin.
Her bir dizinin elemanlarını büyükten küçüğe olacak şekilde ekrana yazdırın.
Her iki dizinin eleman sayısını ve ortalamasını ekrana yazdırın.
*/