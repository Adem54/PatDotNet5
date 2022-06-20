

using System.Collections.Generic;
using AutoMapper;
using static WebApi.BookOperatins.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBookDetail.GetBookDetailQuery;

using static WebApi.BookOperations.GetBooks.GetBooksQuery;

namespace WebApi.Common.MappingProfile 
{
    //MappingProfile i AutoMap olmasini istiyorsak Profile sinifindan kalitim almasi gerekir
    //ve artik MappingPrfoile bir automapperdir,tabi ki Profile Automapper dan geliyor
        public class MappingProfile:Profile {
            //Simdi burda ne neye donusebilir onun configlerini veriyor oalcagiz...
            //Controllerimizin icndeki, Create icin bakalim
            //Controollerdaki Create ,AddBook endpointinie bir tane mapper gonderelim injection yolu ile
            public MappingProfile(){
                CreateMap<CreateBookModel,Book>();
                CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
                //GetBookDetail deki map islemi direk birebir bir map degil yani
                //Book icersidne GenreId int iken, BookDetailViewModel icinde string
                //ayni sekilde tarih te oyle Book icinde Datetime, ama BookDetailViewModel
                //icinde bu, string seklindedir dolayisi ile burda config,bu sekidle farkli sekilde olan satirlari
                // nasil degistirecegini soylememiz gerekiyor
                //VE bu donusumu biz aslinda burda yapacagiz direk
                //src-source,sourcemiz Book, dest, destination ki o da BookDetailViewModel dir...
                CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
            }
            //Ilk parametre source, 2.parametre targettir
            //Burda demis oluyoruz ki CreateBookModel objesi Book entity objesine maplenebillir olsun diyoruz
            //Burda biz CreateBookModel objesi ile disardan datayi aliyoruz ve sonra Book entity objesine
             //datalari aktariyorduk,, iste kaynagimiz CreateBookModel, targetimiz ise Book olacak
             
        }


}