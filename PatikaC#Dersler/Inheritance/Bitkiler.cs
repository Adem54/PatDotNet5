namespace Inheritance{
    public class Bitkiler:Bios{
      
      protected void FotosentezYapma(){
          Console.WriteLine("Vegetation photosynthesizing");
      }

      
    }

    public class TohumluBitkiler:Bitkiler{

//BU BILGI COK KRITIK DAHA ONCE DUYMADIGIIM BIR BILGI
//base benim bana miras veren,benim kalitim aldimiz yani base class imin methodlare erisim sansi verir.Gelip bu base uzerinden hangi methode erismek istersek o methoda base keywordu araciligi ile erisebiliriz:Bitkiler sinifinin FotosentezYapma methodunu protected yapinca TohumluBitkiler Bitikler i inherit etmesine ragman dogrudan bu methoda TohumluBitkiler uzerinden erisemedik ancak, gelip de TohumluBitkiler class inin constructor inda base.FotosentezYapma() methodunu invoke edince artik bu metoda TohumluBitkiler uzerinden olusturdugum instance veya nesneler uzerinden de erisebilme firsatini yakalamis oluyuroz..Aslinda biz base() yazmadan dogrudan da TohumluBitkiler constructor indan methodlarina veya proerptieslerine erisebiliyoruz....
//Ayrica baska bir nokta yine biz base uzerinden constructor icerisinde, TohumluBitkiler sinifinin kalitim aldigi yani base class i olan Bitkiler in base class i yani onun da kalitim aldigi inhjerit ettigi class veya siniflara ait methjodlara da yine base uzerinden erisebiliyoruz
//Bu bilgi coook onemli ve cook hayati...Bir class inherit ettigi class in,yani base class inin(super class) kendisi subclass, in da inherit ettigi ve inherit ettigi class in da inherit ettigi ne kadar class var ise onlarin icindeki methodlara ve proeprtylere de yine erisebiliyor....eger protected ise kendi constructorinda base keywordu araciligi ile asagidaki ornekte gibi yok public ise zaten dogrudan erismis olyor....
        public TohumluBitkiler(){
            base.FotosentezYapma();
            base.Nutrition();
            base.Respiration();
            base.Discharge();
            
        }
        public void TohumlaCogalma(){
            Console.WriteLine("Tohumlu bitkiler tohumla cogalirlar");
        }
    }

    public class TohumsuzBitkiler:Bitkiler {
        public void SporlaCogalma(){
            Console.WriteLine("Tohumsuz bitkiler sporla cogalirlar");
        }

        public TohumsuzBitkiler(){
            base.FotosentezYapma();
            base.Nutrition();
            base.Respiration();
            base.Discharge();
        }
    }


}