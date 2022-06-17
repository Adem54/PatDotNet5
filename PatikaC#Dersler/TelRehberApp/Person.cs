namespace TelRehberApp
{
    public class Person
    {
        public int PersonId {get;  set;} 
        public string FirstName {get;set;}
        public string LastName {get; set;}
         private string? _telNumber;
        // public string TelNumber {get; set;}="";

        public string TelNumber
        {
            
            get
            {
                return _telNumber is not null ? _telNumber : "";
               
            }
            set
            {
                if (_telNumber is not null && _telNumber.Length == 8)
                {
                    _telNumber = value;
                }else {
                    Console.WriteLine("_telNumber: "+_telNumber);
                    Console.WriteLine("Lutfen tel numarasini 8 hane giriniz");
                }

            }
        }
        public Person(int personId,string firstName,string lastName, string telNumber){
             PersonId=personId;
             FirstName=firstName;
             LastName=lastName;
             TelNumber=telNumber;
             Console.WriteLine("telNumberrrr: "+telNumber);
        }

    }
}