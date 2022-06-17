namespace Inheritance{
    public class Animals:Bios{
       public void Adaptation(){
           Console.WriteLine("Animals adapt");
       }
    }

    public class Surungenler:Animals{
        public void SurunerekHareketEtmek(){
            Console.WriteLine("Surungenler surunerek hareket ederler");
        }
        public Surungenler(){
            base.Nutrition();
            base.Respiration();
            base.Discharge();
        }
    }

    public class Kuslar:Animals{
        public void Ucmak(){
            Console.WriteLine("Kuslar ucar");
        }
        public Kuslar(){
            base.Nutrition();
            base.Respiration();
            base.Discharge();
        }
    }
}