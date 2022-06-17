
namespace TelRehberApp
{

    public static class PersonList
    {
        public static List<Person> GetPeople()
        {
            return new List<Person>{
                new Person(1,"Adem","Erbas","46241369"),
                new Person(2,"Zeynep","Erbas","34445671"),
                new Person(3,"Zehra","Erbas","12345677"),
                new Person(4,"Sercan","Sari","87654321"),
                new Person(5,"Adnan","Olga","46296345"),
                new Person(6,"Terje","Olav","99112275")
            };
        }
    }
}