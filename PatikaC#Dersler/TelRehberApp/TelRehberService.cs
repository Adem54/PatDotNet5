
using System.Linq;
using System.Linq.Expressions;
namespace TelRehberApp
{
    public  class TelRehberService : ITelRehberService
    {
        List<Person> _people;
        public TelRehberService()
        {
            _people=PersonList.GetPeople();
        }
        public List<Person> GetAll()
        {
             return _people;
        }

        // public List<Person> GetPersonByNameOrLastName(string item)
        // {
        //     //Javascript teki filterin karsiligi Where dir
        //     var result = _people.Where(p => p.FirstName.Contains(item) || p.LastName.Contains(item)).ToList();
        //     return result;
        // }

        // public List<Person> GetPersonByTelNumber(int number)
        // {
        //     var result = _people.Where(p => p.TelNumber.ToString().Contains(number.ToString())).ToList();
        //     return result;
        // }

        public bool Modify(Person person)
        {
            while (true)
            {
                var findPerson = _people.Find(p=>p.PersonId==person.PersonId);
                if (findPerson is not null)
                {
                    var name=person.FirstName!=""? person.FirstName : findPerson.FirstName;
                    var surname=person.LastName!=""? person.LastName : findPerson.LastName;
                    var telNo=person.TelNumber!=""? person.TelNumber : findPerson.TelNumber;
                    findPerson.FirstName=name;
                    findPerson.LastName=surname;
                    findPerson.TelNumber=telNo;
                    return true;
                }
                else
                {
                   Console.WriteLine("Aradiginiz krtiterlere uygun veri rehberde bulunamadi. Lütfen bir seçim yapiniz.");
                    Console.WriteLine("Silmeyi sonlandirmak icin : (1)");
                    Console.WriteLine("Yeniden denemek icin : (2)");
                    Console.WriteLine("********************");
                    string? usersChoice=Console.ReadLine();
                    if(usersChoice=="2")return false;
                    else return true;
                }
            }

        }

        public bool Remove(string text)
        {
            //Not: Rehber uygun kriterlere uygun birden fazla kişi bulunursa ilk bulunan silinmeli.Find methodu bu isi yapiyor ilk buldugnu getiriyor
            while (true)
            {
                var findPerson = _people.Find(p => p.FirstName.Contains(text) || p.LastName.Contains(text));
                if (findPerson is not null)
                {
                
                    Console.WriteLine($"{findPerson.FirstName} ismili kisi rehberden silinmek uzere, onayliyor musununz (y/n)");
                    string? s = Console.ReadLine();
                    if (s == "y")
                    {
                        Console.WriteLine("Veriniz siliniyor");
                        _people.Remove(findPerson);return true; 
                    }
                  

                }
                else
                {
                    Console.WriteLine("Aradiginiz krtiterlere uygun veri rehberde bulunamadi. Lütfen bir seçim yapiniz.");
                    Console.WriteLine("Silmeyi sonlandirmak icin : (1)");
                    Console.WriteLine("Yeniden denemek icin : (2)");
                     Console.WriteLine("********************");
                    string? usersChoice=Console.ReadLine();
                    if(usersChoice=="2")return false;
                    else return true;
                }
            }
        }

        public void Save(Person person)
        {
            _people.Add(person);
        }
    }
}