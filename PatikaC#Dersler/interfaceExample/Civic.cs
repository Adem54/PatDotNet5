namespace interfaceExample{
    public class Civic : BaseOtomobil
    {
        public override Marka AracMarka()
        {
            return Marka.Honda;
        }
        public override Renk StandartRenk()
        {
           return Renk.Beyaz;
        }
    }
}