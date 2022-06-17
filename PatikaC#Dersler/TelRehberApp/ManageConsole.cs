

namespace TelRehberApp {

    public  class ManageConsole {
        List<BaseApplyManager> _applyManagers;

        public ManageConsole(){
        _applyManagers=new List<BaseApplyManager>{
            new SavePerson(new TelRehberService()),
            new RemovePerson(new TelRehberService()),
            new ModifyPerson(new TelRehberService()),
            new GetPeople(new TelRehberService()),
            new SearchPerson(new List<IFilterManager>{
                new FilterByTelNumber(new TelRehberService()),
                new FilterByWholeName(new TelRehberService()),
            })
        };              
        }
        public  void ChooseYourTransact(){
                 Console.WriteLine("Yapmak istedginiz islemi seciniz");
                 foreach (var item in _applyManagers)
                 {
                            Console.WriteLine($"({item.Number})  {item.Text} ");
                 }
              var chosenTransactNumber=Console.ReadLine();
                var result=int.TryParse(chosenTransactNumber,out int outNumber);
                if(result){
                    var findTransactMethod=_applyManagers.Find(m=>m.Number==outNumber);
                   if(findTransactMethod is not null){
                       findTransactMethod.Run();
                   }
                }
              //   var choosenTransact=_applyManagers.
            
        }
    }
}