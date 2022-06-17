namespace TelRehberApp
{

    public class ModifyPerson : BaseApplyManager
    {
        private ITelRehberService _telRehberService;
        public ModifyPerson(ITelRehberService telRehberService)
        {
            _telRehberService = telRehberService;
            Text = "Varolan Numarayi Güncelleme";
            Number = 3;
        }

        public override void Run()
        {
            while (true)
            {
                Console.WriteLine(" Lütfen numarasini silmek istediğiniz kişinin bilgilerini giriniz:");
                Console.WriteLine("Id:");
                var id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Name:");
                string? name = Console.ReadLine() ?? "";
                Console.WriteLine("LastName:");
                string? surname = Console.ReadLine() ?? "";
                Console.WriteLine("TelNo:");
                string? telNo = Console.ReadLine() ?? "";

                Person person = new Person(id, name, surname, telNo);
                if (name is "" && surname is "" && telNo is "")
                {
                    Console.WriteLine("Ad soyad ve telNo dan hicbirini girmediniz...");
                }
                else
                {
                    var result = _telRehberService.Modify(person);
                    if (result)
                    {
                        _telRehberService.GetAll().ForEach(p => Console.WriteLine(p.FirstName + "  " + p.LastName + " " + p.TelNumber)); break;
                    }
                    
                }
            }       
        }
    }
}