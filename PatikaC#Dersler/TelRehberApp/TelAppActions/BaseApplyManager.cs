

namespace TelRehberApp{

    public abstract class BaseApplyManager : IApplyManager
    {
        public string? Text { get; set; }
        public int Number { get; set; }

        // public virtual void Run(Person person){
                
        // }
        // public virtual void Run(int number){
            
        // }
        // public virtual void Run(string text){
            
        // }
        // public virtual void Run(){
            
        // }
        // public virtual void Run(object item){
            
        // }
        public abstract void Run();
    }
}