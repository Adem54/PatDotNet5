
namespace TelRehberApp
{
    public class FilterByWholeName : IFilterManager
    {
        ITelRehberService _telRehberService;
        public string Text { get; set; }
        public int Number { get; set; }
        public FilterByWholeName(ITelRehberService telRehberService)
        {
            _telRehberService = telRehberService;
            Text = "İsim veya soyisime göre arama yapmak için:";
            Number = 1;

        }
        public List<Person> ApplyFilter(object item)
        {
            var people=_telRehberService.GetAll();   
            var result = people.Where(p => p.FirstName.Contains((string)item) || p.LastName.Contains((string)item)).ToList();
            return result;
        }
    }
}