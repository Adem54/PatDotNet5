namespace TelRehberApp
{

    public class SearchPerson : BaseApplyManager
    {
        List<IFilterManager> _filterManagers;
        List<Person>? _searchResults;
        public SearchPerson(List<IFilterManager> filterManagers)
        {
            Text = "Rehberde arama yapmak";//Enum da yapabiliriz.
            Number = 5;
            _filterManagers = filterManagers;
        }
        public override void Run()
        {
            while (true)
            {
                Console.WriteLine("Arama yapmak istediğiniz tipi seçiniz.");
                Console.WriteLine("****************************************");
                _filterManagers.ForEach(f=>Console.WriteLine($"{f.Text} ({f.Number})"));
                int number = Convert.ToInt32(Console.ReadLine());
                var findFilterType = _filterManagers.Find(f => f.Number == number);
                if (findFilterType is not null)
                {
                   Console.WriteLine("Aramak istedginiz tel-no veya ism-soyisim giriniz");
                    var searchedItem=Console.ReadLine();
                   if(searchedItem is not null) _searchResults=findFilterType.ApplyFilter(searchedItem);
                    break;
                }
                else
                {
                    Console.WriteLine("Lutfen dogru gecerli islemlerden birini seciniz");
                    //Deyip tekrar secime dondurmesi gerekiiyor burda....
                }
            }
           if(_searchResults is not null) ShowFilterList(_searchResults);
        }
        private void ShowFilterList(List<Person> people){
            people.ForEach(p=>Console.WriteLine("Arama Sonuclariniz \n  ***********************************************  \n  isim:{0}\n soyisim:{1}\n Telefon Numarasi:{2}\n ",p.FirstName, p.LastName,p.TelNumber));
        }
    }
}