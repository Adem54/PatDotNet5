

using System.Collections.Generic;
using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Entities;
using static WebApi.Aoplication.BookOperations.Queries.GetBooks.GetBooksQuery;
using static WebApi.Application.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;
using static WebApi.Application.AuthorOperations.Queries.GetAuthors.GetAuthorsQuery;
using static WebApi.Application.BookOperatins.Commands.CreateBook.CreateBookCommand;


using static WebApi.Application.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;
using static WebApi.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;
using static WebApi.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;


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
               //Enum kullanmiyoruz artik
            //   CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
                CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>src.Genre.Name));
                CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.AuthorName, opt=>opt.MapFrom(src=>src.Author.FirstName+" "+src.Author.LastName));
              

                //GetBookDetail deki map islemi direk birebir bir map degil yani
                //Book icersidne GenreId int iken, BookDetailViewModel icinde string
                //ayni sekilde tarih te oyle Book icinde Datetime, ama BookDetailViewModel
                //icinde bu, string seklindedir dolayisi ile burda config,bu sekidle farkli sekilde olan satirlari
                // nasil degistirecegini soylememiz gerekiyor
                //VE bu donusumu biz aslinda burda yapacagiz direk
                //src-source,sourcemiz Book, dest, destination ki o da BookDetailViewModel dir...
                CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=>src.Genre.Name))
                .ForMember(dest=>dest.AuthorName,opt=>opt.MapFrom(src=>src.Author.FirstName+" "+src.Author.LastName));
             

               // CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.AuthorName, opt=>opt.MapFrom(src=>src.Author.FirstName+src.Author.LastName));
                CreateMap<Genre, GenresViewModel>();//Oldugu gibi donustur demek, yani herhangi bir tip farkliligi falan  yok,
                //properties ler arasinda, gibi dusunebilirz
                CreateMap<Genre,GenreDetailViewModel>();

                CreateMap<Author,AuthorsViewModel>().ForMember(dest=>dest.BirthDate,opt=>opt.MapFrom(src=>src.BirthDate.ToString()));
                CreateMap<Author,AuthorDetailViewModel>().ForMember(dest=>dest.BirthDate,opt=>opt.MapFrom(src=>src.BirthDate.ToString()));
                CreateMap<CreateAuthorModel,Author>();
                
            }
            //Ilk parametre source, 2.parametre targettir
            //Burda demis oluyoruz ki CreateBookModel objesi Book entity objesine maplenebillir olsun diyoruz
            //Burda biz CreateBookModel objesi ile disardan datayi aliyoruz ve sonra Book entity objesine
             //datalari aktariyorduk,, iste kaynagimiz CreateBookModel, targetimiz ise Book olacak
             
        }


}