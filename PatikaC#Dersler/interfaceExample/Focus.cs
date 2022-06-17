namespace interfaceExample{
    public class Focus : BaseOtomobil
    {
        public override Marka AracMarka()
        {
            return Marka.Ford;
        }

        // public override Renk StandartRenk() 
        // {
        //     return base.StandartRenk();
        // }
        //varsayilan olarak yazilan icerigi kullanacagimiz icin hic yazmasak da olur ya da bu hali ile yazsakda default icerigi kullanmis oluruz
    }
}