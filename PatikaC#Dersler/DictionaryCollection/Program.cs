using System;
namespace DictionaryCollection
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Dictionary System.Collection s lar altindadir...
            //key,value degerleri aliyorlar ve tipini belirtmemiz gerekiyor
            //Aslinda normal listelerde index vardir 0-1-2- diye giden burda bu index leri biz key gibi dusuneblirz sadece biz key i numara vermek zorunda degiliz istedgimiz tipte verebiliriz....
            //Key ler uniq olmak zorundadir bunu da unutmayalim...Compile time da hata almayiz ama runtime da hata aliriz...
            //Dcitionary System.Collection.Generic  namespace i altnda bulunyor
            Dictionary<int, string> users = new Dictionary<int, string>();
            //key ve value ni tiplerini ayri ayri belirtmemiz gerekir
            users.Add(10, "Adem Erbas");
            users.Add(12, "Ahmet Yilmaz");
            users.Add(18, "Deniz Arda");
            users.Add(20, "Ozcan Cosar");
            //Uniq olmayan bir key eklemeye calisalim-Run time hatasi alacagiz...
            //     users.Add(10, "Sezer Yeer");//=>Run time calisma zamani hatasi aliriz-Unhandled exception. System.ArgumentException: An item with the same key has already been added. Key: 10-Ama compiler time da hata vermemsi bizim icin tehlikeli birsey uygulama calsistirinca ancak farkedebilirz eger yanlisklikla boyle bir hata yaparsak
            //Dizi elemanlarina eriselim
            Console.WriteLine("Dizi elemanlarina eriselim");
            string item1 = users[10];
            string item2 = users[12];
            string item3 = users[18];
            string item4 = users[20];
            Console.WriteLine("item1: " + item1);
            Console.WriteLine("item2: " + item2);
            Console.WriteLine("item3: " + item3);
            Console.WriteLine("item4: " + item4);
            Console.WriteLine("foreach ile yazdiralim");

            //Key ve value ye ayri ayri erisme
            foreach (var item in users)
            {
                Console.WriteLine("item: " + item);
                Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);

            }
            //Count unu alalim...
            Console.WriteLine(users.Count);
            //Contains ile key uzerinden soruglayalim
            Console.WriteLine(users.ContainsKey(18));
            Console.WriteLine(users.ContainsValue("Adem Erbas"));

            //Bilmedigin bir key i dictionary collection listesinde aramak...
            string result;
            if (users.TryGetValue(12, out result))
            {
                Console.WriteLine("result: " + result);
            }

            //Update Dictionary items
            Console.WriteLine("Update Dictionary items");
            users[10] = "Selami Sarizeybek";
            users[12] = "John Robert";
            foreach (var item in users)
            {
                Console.WriteLine("item: " + item);
                Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);

            }
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Dictonary listemizden eleman cikarma");
            users.Remove(18);
            foreach (var item in users)
            {
                Console.WriteLine("item: " + item);
                Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);

            }
            //Dictionary listesindeki key ve value listesine ayri ayri erisebilme...
            Console.WriteLine("Keys.&&&&&&&&&&&&&&&&&&&&&&&");
            foreach (var item in users.Keys)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Keys.&&&&&&&&&&&&&&&&&&&&&&&");
             foreach (var item in users.Values)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Keys.&&&&&&&&&&&&&&&&&&&&&&&");

            //use ElementAt() to retrieve key-value pair using index
            Console.WriteLine("use ElementAt() to retrieve key-value pair using index");
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine("Key: {0}, Value: {1}",
                                                        users.ElementAt(i).Key,
                                                        users.ElementAt(i).Value);
            }


            //Value kismina birden fazla veri ekleyebiliriz ancak key kismi uniq ve 1 tane olmalidir....
            //creating a dictionary using collection-initializer syntax
            var cities = new Dictionary<string, string>(){
            {"UK", "London, Manchester, Birmingham"},
            {"USA", "Chicago, New York, Washington"},
            {"India", "Mumbai, New Delhi, Pune"}
            };
            /*
            Key: UK, Value: London, Manchester, Birmingham
                Key: USA, Value: Chicago, New York, Washington
            Key: India, Value: Mumbai, New Delhi, Pune
            */

            foreach (var kvp in cities)
                Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
        }
    }
}
