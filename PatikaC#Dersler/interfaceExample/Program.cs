using System;
namespace interfaceExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            IOtomobil focus = new Focus();
            //Enum degerini string olarak alabilmek icin ToSTring() methodunu kullaniriz...
            string focusMarka = focus.AracMarka().ToString();
            string focusRenk = focus.StandartRenk().ToString();
            int focusTekSayi = focus.TekerlekSayisi();

            Console.WriteLine("Marka:{0} - Renk:{1} - TakSayi:{2}\n", focusMarka, focusRenk, focusTekSayi);
            //Console.WriteLine("First Name:{0} - Last Name:{1} - Age:{2}\n", item.FirstName, item.LastName, item.Age);
        }
    }
}
