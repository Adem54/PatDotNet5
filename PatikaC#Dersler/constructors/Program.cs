using System;
namespace constructors
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Person person = new Person("Adem","Erbas",34);
            Person person3 = new Person("Zehra","Erbas");
            Person person2=new Person();

            Console.WriteLine("firtstName:  " + person.FirstName == null);
            Console.WriteLine(" Age:  " + person.Age);
        }
    }

    public class Person
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public Person(string firstName, string lastName, int age)
        {
            Console.WriteLine("Constructor 3 paramtreli calisti");
            FirstName=firstName;
            LastName=lastName;
            Age=age;
        }

         public Person(string firstName, string lastName)
        {
            Console.WriteLine("Constructor 2 paramtreli calisti");
            FirstName=firstName;
            LastName=lastName;
          
        }

        public Person(){
            Console.WriteLine("Constructor default calisti");

        }

    }
}
