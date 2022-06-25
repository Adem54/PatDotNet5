
using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre 
{
    public class UpdateGenreCommand {
        private readonly BookStoreDbContext _dbContext;
   
        public int GenreId {get; set;}
        public UpdateGenreModel Model {get; set;}

        public UpdateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public void Handle(){
            var genre=_dbContext.Genres.SingleOrDefault(genre=>genre.Id==GenreId);
            if(genre is null)throw new InvalidOperationException("Guncellenmek istenen kitap turu mevcut degil");
            //Disardan bana bende id ye gore olmayan bir kitap i update etmek icin geldi ama bana gonderdigi isim
            //halihazirda baska bir kitap turunun adi ise, yani baska bir id ye ait tur de bu isim var ise
            //bizim hata vermemiz gerekiyor....Yani kisacasasi ayni isimde genre var ama baska id de ise, eger
            //ya da soyle dusunelim ismi ve id si ayni olan bir genre gonderirse de update islemini yapsin diyecegiz
            //Sadece ayni genre ismi ile geip de onun id si farkli gelip de id sini degistirmesin diye yapiyoruz
           if(_dbContext.Genres.Any(x=>x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
           throw new InvalidOperationException("Ayni isimde kitap turu zaten mevcut");
           //Peki Name gondermez de sadece isActive bilgisinin gonderirse
            //Model.Name.Trim() sonunda bosluk var ise de silelim diyoruz.
           // genre.Name=Model.Name.Trim() == default ? genre.Name : Model.Name;
            genre.Name=string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            //Kullanici name i bos gonderip isActive i sadece true veya false gonderirse o zaman
            //da name e genre nin name ini vermesini bekliyoruz...
            //Aslinda amac su burda, eger kullanci sadece isActive e degistirmek istedi ve Name i ayni gonderdi
            //biz bunu engellemeyelim, is sadece isActive i de degistirebilsin, sen ayni isim ile gelirsen
            //update  yapamazsin dememis oluyoruz yani...
            //Yani Name i gondermezse kullanici, Name i Model den gelen deger e gore update etmeyecegiz o zaman
            //yine kendi Name ini atayacagiz, ancak default ise yani string bir degerin default string Empty ya da null
            //oluyor ondan dolayi updata etmemiz dogru olmaz cunku o zaman null yapar...o durumlarda kendi Name ini ver diyoruz
            //Ama yok  default degil ise o zaman tabi ki Model den gelen o deger ile guncelleme yapabiliriz...
            genre.IsActive=Model.IsActive;
            _dbContext.SaveChanges();
            //isActive false olarak degistirirse kullanici o zaman o kitap turu gozukmeyecektir...
        }

    }
       public class UpdateGenreModel {
               public string Name {get; set;} 
               public bool IsActive {get; set;}
        }
}