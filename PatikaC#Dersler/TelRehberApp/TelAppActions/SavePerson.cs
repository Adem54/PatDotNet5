
namespace TelRehberApp
{

    public class SavePerson : BaseApplyManager
    {
        private ITelRehberService _telRehberService;

        public SavePerson(ITelRehberService telRehberService)
        {
            _telRehberService = telRehberService;

            Text = "Yeni Numara Kaydetmek";
            Number = 1;
        }

        public override void Run()
        {
            //Bu methodun icine girdikten sonra detayi burda yapacaksin yani kullaniciya gireecegi bilgileri burda sor....
            Console.WriteLine("Lutfen isim giriniz");
            var name = Console.ReadLine();
            Console.WriteLine("Lutfen soyisim giriniz");
            var surname = Console.ReadLine();
            Console.WriteLine("Lutfen tel-no giriniz");
            var telNo = Console.ReadLine();
            var id = _telRehberService.GetAll().Count + 1;
            if (name is not null && surname is not null && telNo is not null)
            {
                Person person = new Person(id, name, surname, telNo);
                _telRehberService.Save(person);
                _telRehberService.GetAll().ForEach(p=>Console.WriteLine(p.FirstName+ "  " +p.LastName+ " "+ p.TelNumber));
            }
              
        }


    }
}