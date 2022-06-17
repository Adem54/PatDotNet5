

public class Person
{
        public string FirstName {get; set;}
    public Person(string firstName)
    {
        FirstName=firstName;
    }    public void Topla(ref int number1,int  number2){
        number1=24;
        number2=32;
        
    }

    public void CheckOut(ref int number3, out int number4){
            number3=45;
            number4=78;
    }
}

