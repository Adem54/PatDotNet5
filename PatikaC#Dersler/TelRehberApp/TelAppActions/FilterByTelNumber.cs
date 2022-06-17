

namespace TelRehberApp {
    public class FilterByTelNumber : IFilterManager
    {
        ITelRehberService _telRehberService;
        public string Text { get; set; }
        public int Number { get; set; }
     
        public FilterByTelNumber(ITelRehberService telRehberService){
            _telRehberService=telRehberService;
            Text="Telefon numarasina göre arama yapmak için:";
            Number=2;
        
        }
        public List<Person> ApplyFilter(object item)
        {
             var people=_telRehberService.GetAll();   
            var result = people.Where(p => p.TelNumber is not null && p.TelNumber.Contains((string)item)).ToList();
            return result;
        }
    }
}