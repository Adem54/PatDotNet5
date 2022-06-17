namespace TelRehberApp
{

    public class RemovePerson : BaseApplyManager
    {
                private ITelRehberService _telRehberService;
        public RemovePerson(ITelRehberService telRehberService)
        {
            _telRehberService = telRehberService;

            Text = "Varolan Numarayi Silmek";//Enum da yapabiliriz.
            Number = 2;
        }
        public override void Run()
        {
            while(true){
                Console.WriteLine(" Lütfen numarasini silmek istediğiniz kişinin adini ya da soyadini giriniz:");
            var text = Console.ReadLine();
            if (text is not null)
            {
                var result = _telRehberService.Remove(text);
                if(result){
                    _telRehberService.GetAll().ForEach(p => Console.WriteLine(p.FirstName + "  " + p.LastName + " " + p.TelNumber));break; 
                }
            }
            }  
        }
    }
}