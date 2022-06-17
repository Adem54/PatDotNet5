namespace TelRehberApp
{
    public interface IFilterManager
    {
        string Text {get; set;}
        int Number {get; set;}
        List<Person> ApplyFilter(object item);
    }
}