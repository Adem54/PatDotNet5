namespace TelRehberApp {

    public class GetPeople:BaseApplyManager{
        private ITelRehberService _telRehberService;

        public GetPeople(ITelRehberService telRehberService){
            _telRehberService=telRehberService;
            Text="Rehberi listelemek";//Enum da yapabiliriz.
            Number=4;
        }
     
        public override void Run()
        {
            foreach(var item in  _telRehberService.GetAll()){
                Console.WriteLine($"isim:{item.FirstName}");
                Console.WriteLine($"soyIsim:{item.LastName}");
                Console.WriteLine($"tel-no:{item.TelNumber}");
            }
           
        }
    }
}