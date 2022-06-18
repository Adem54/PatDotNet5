
//Burda namespace proje adi,sonra BookOperations klasor adi ve sonra da GetBooks klasor adi
//GetBooksQuery.cs sirasi ile WebApi/BookOperations/GetBooks altindadir..
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks
{

    public class GetBooksQuery
    {
        //Sadece constructor icinden set edilsin disardan degistirilemesin baska bir yontemle
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper=mapper;
        }
/*
UI ya donecegim veri setini View model de korumak istiyorum, yani istedgim veri tipinin UI ya
dondugunden her zaman emin olmak istiyorum bunun icin ViewModel olusturacagiz, ve bunu burda 
tanimliyoruz 
*/
        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(book => book.Id).ToList<Book>();
            //BooksViewModel imide olustrduguma gore burda da artik BooksViewModel donmem gerekiyor
         
            List<BooksViewModel> vm=_mapper.Map<List<BooksViewModel>>(bookList);
         
            // List<BooksViewModel> vm=new List<BooksViewModel>();
            //veritabanindan gelen bookListesini foreach ile doneriz her bir book geldiginde 
            //book ile bilgileri, olusturdugumuz BookViewModel class inn icindeki proeprt ylere
            //atama yapariz
            // foreach (var book in bookList)
            // {

            //     vm.Add(new BooksViewModel(){
            //          Title=book.Title,
            //          Genre=((GenreEnum)book.GenreId).ToString(),   
                     //book dan gelen GenreId yi biz Genre ye verdigmiz zaman
                     //GenreEnum da gelen sayi kac ise onun icin enum olarak ne 
                     //yazdi isek onu karsilik olarak verir ama enum GenreEnum tipinde aliyorz
                     //dolayisi ile onu string e cast etmemiz gerekecektir..
            //          PublishDate=book.PublishDate.Date.ToString("dd/MM/yy"),
                     //PublishDate i biz saat ile beraber kaydettk ama biz sadece tarih kismini almak istiyoruz
                     //.Date dersek sadece tarih kismini getirir, geri kalan kismi 0000 getirir ve onu da formatlayacagiz
                     //Ve de geriye string donuyoruz...
            //          PageCount=book.PageCount
            //     });
                //Artik bizim geriye vm donmemiz gerekiyor
            // }
            return vm;
        }
        //Simdi burdaki isimizi bitirdikten sonra bookController icerisine gidip orda
        //GetBooks response action methodu icinde kullanalim ve UI a view model donelim

      /* 
       ViewModeli biz burda burda tanimliyoruz cunku disardan erisilmesine da acik hale
    getirmemis oluruz, ayrica biz belki GetById de daha baska ViewModel kullanacagiz, biz bunu
    kontrollu bir sekilde erisilsin diye buraya koyuyoruz
    ViewModel donmemizin mantigin surdan da anlayabilirz, ornegn GenreId var bunu biz listelerken
    kullanicinin ihtiyaci olmaz buyuk ihtimalle ki olacagi durumlar olabilir bunu da ayri degerlendiririz
    Yuksek bir ihtimalle GenreId yerine Genre ile ilgili diger datalara ihtiyac duyulur dolayisi iled de
    biz burda GenreId yerine Genre nin kendisini gondeririz
    Genre Id bize geldiginde biz GenreId nin 1, 2, 3 numaralarina gore ona belli string tip ler verecegiz
    Ornegin, sadece 1 id si bir tipi ifade eder, 2 sayisi baska bir tipi ifade eder gibi bundan dolayi da 
    Genre icin Enum olusturarak bu id lerin karsiligi olan string leri orda tutalim cunku burda belli
    type lar var sadece
    Tabi burda su konu uzerine dusunmek lazm, biz Genre isminde bir veritabani tablosu yapip
    onu Foreign key olarak kullanarak, sonra da GenreId icindeki diger bilgiler icin 
    Linq uzerinden join islemleri yapip sonra da donusunde Select ile bir tane BookDto donemez miyiz
    ki burda BookDto bizim BookDeatils i donereken, ki Book bilgileri ve Genre bilgilerinde
    kullaniciya gosterecegimiz datalardan olusuyor...
    */
       public class BooksViewModel
       {
        public string Title {get; set;}
        public int PageCount {get; set;}
        public string PublishDate {get; set;}
        public string Genre {get; set;}
       }

    }
}