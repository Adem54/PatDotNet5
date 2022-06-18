

using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.BookOperatins.CreateBook {

    public class CreateBookCommand {
            /*
            BURASI ONEMLI...
            Burda bir tane bizim model e ihtiyacimiz var, bu modeli disardan set leyecegiz cunku kullanicidan geliyor
              Geldigi nokta da CreateBookCommand.cs icinde de bir modeli setlememiz gerekiyor ki buraya dolu bir
              sekilde gelsin...
              Disardan bir model almis olduk, CreateBookModel tipinde bir model almis olduk...
            
            */
            public CreateBookModel Model {get; set;}
            //BURASI COOK ONEMLI TAM OLARAK ANLAYALIM....
             //Oncelikle bu CreateBookModel tipindeki Book Model i kullanicidan bize Controller
        //a gelecek ilk olarak, yani icinde kullanicinn gonderdigi data ile gelecek
        //Yani bu data dolu gelecek icinde data si var olarak gelecek ve biz de burda direk
        //Model e gelen data nin Title i acaba veritabanindaki Book Title lar ile ayni olan var mi
        //diye sorgulama yapabilecegiz...  Cunku gelen controller a kullanicidan gelen data
        // burdaki class imiz new lendikten sonra, CreateBookCommand new lendikten sonra 
        //Model properties ini atama yapilacak controller icerisinde ondan dolayi da biz Model.Title
        //deyince, kullanicini gondermis oldugu Title i alabilmmis olacagiz...
            private readonly BookStoreDbContext _dbContext;
            private readonly IMapper _mapper;
            public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
            {
                _dbContext=dbContext;
                _mapper=mapper;
            }
            
            /* 
            Disardan gelencek olan newBook artik yukarda olusturdgujmuz CreateBookModel dir
            
            
            */
            public void Handle(){
                var book=_dbContext.Books.SingleOrDefault(book=>book.Title==Model.Title);
                    if(book is not null)
                        //ayni kitap isminde bizde var ise o zaman anlamli bir exception firlatacgiz
                        throw new InvalidCastException("Kitap zaten mevcut!");
                          //Dikkat edelim Model ici dolu olarak geliyor buraya,Controller da parametre olarak geliyor
                        //Ardindan orda, var createCommand=new CreateBookCommand(_context);ve createCommand.Model=newBook
                        //,newBook paramtereden gelen CreateBookModel tipindeki kullanicidan gelen datadir
                        //  Controller icinde ici doldurulmus olacak
                        //Ve burda o model den alacagiz datayi ve de, Book entitysine yazdirarak veritabanina kaydetme
                        //islemin burda yapacagiz....
                        //Disardan bir model aldik biz, peki bize disardan ne geliyor kullanicidan bize
                        //bir Book entity si geliyor, bu gelen entity yi olusturup daha sonra, onun fieldlarini
                        //gelen Model icerisinden setliyor olmamiz gerekiyor...
                         //Simdi biz bize Controllerda atamasi yapilan Model icindeki datalardan Book entity mizden
                        //bir tane instance olusturup propertieslerini set edecegiz ve ondan sonra o set ettigmiz
                        //book entity instancesini veritabanina kaydedecegiz...
                       
                        //AUTOMAPPERIN KULLANACAGIMIZ YER TAM OLARAK BURDAKI 4 SATIR YERINE TEK SATIRDA BU ISI YAPMAK 
                        //CreatBookMode tipinde olan Model ile Book id haric ayni datalara sahiptirler.Dolayisi ile biz
                        //dicez ki CreateBookModel objesi Book objesine maplenebilir, donusturulebilir demem lazm
                        //Bunun iicinde bunu MappinProfile icinde belirtmemiz gerekir
                        //Burda biz CreateBookModel objesi ile disardan datayi aliyoruz ve sonra Book entity objesine
                        //datalari aktariyorduk,, iste kaynagimiz CreateBookModel, targetimiz ise Book olacak
                        //   book=new Book(); 
                        // book.Title=Model.Title;
                        // book.PublishDate=Model.PublishDate;
                        // book.PageCount=Model.PageCount;
                        // book.GenreId=Model.GenreId;
                        book=_mapper.Map<Book>(Model);
                        //Map<targetObje>() source uzerinden de tipi olacak, model ile gelen veriyi book objesi icerisine
                        // o verileri maple, convert et demektir nerden faydalaniyor MappingPfofile da yaptimgz o configurasyondan 
                        // faydalaniyor
                        //Endpointimizin calisip calismadigni test ettgimiz zaman end-pointimizin calistigni gorebiliriz....

                        //Buraya dikkat edelim, biz kullanicidan id haric tum datalari aliriz, id yi biz
                        //burda veritabaninda auto increment yontemi ile kendisinin otomatik olusturmasini sagliyourz
                        //ama baska bir yontemle de biz bu isi Guid type ile direk Book constructorinda
                        //Book class i new lendiginde otomatik kendisine bir id olusturmasini da saglayabiliriz
                        //VE burda biz veritabanina kaydettigmizde o durumda da id si ile birlikte veritabanina 
                        //kaydolmus olur....
                                //book entitymizden olusturdugmuz, instanceye Model uzerinden, kullanicidan gelen
                                //datalari propertieslere set ettikten sonra artik, veritabanina book instancesini 
                                //ekleyebiliriz
                    _dbContext.Books.Add(book);
                    _dbContext.SaveChanges();
            }

            /*
            Burda bir entity ile model i ayristirmaliyiz,biz Controller da Add islmeinde paramtreye direk entity aliyoruz
            bu yaklasim dogru degildir onun icin burda olustrdgumuz model i biz parametreye alacagiz,
             burda bir model olusturacagiz, ama bunu  view model olarak olusturmayacagiz
            Viewmodeller sadece UI a donmek icin kullaniriz
            Baktimiz zaman ozunde hepsi bir class ve burda da bir tane model class i olusturacagz..  
              Burda sunu dusunelim, bir kitap olusturacagimz zaman disardan hangi bilgilier almamiz gerekiyor, nelere
              kod icerisinde biz dolduracagiz bunu belirleyelim...
              Kullanici yeni kitap eklyecegi zaman kullanici bize id gondermeyecekki oylo birsey yok
              id bizim ismiz ondan dolayi,bizim kullanicidan gelecek datayi alacagimiz bir model olusturamamiz gerekyordu
              Ve de biz bu CreateModel tipinde bir data alacagiz kullanicidan...
              
              */
              public class CreateBookModel
              {
                
                public string Title {get; set;}
                public int GenreId {get; set;}//int olarak almam lazm, int value ye ihtiyacim var...
                public int PageCount {get; set;}
                public DateTime PublishDate {get; set;}
              }  
    }   
}