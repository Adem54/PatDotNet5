namespace interfaceExample{
    public class Toyota : BaseOtomobil
    {
          public override Marka AracMarka()
        {
            return Marka.Toyota;
        }
        public override Renk StandartRenk()
        {
            return base.StandartRenk();
        }
    }
}