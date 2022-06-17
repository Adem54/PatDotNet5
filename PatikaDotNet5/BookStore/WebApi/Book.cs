

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi {

//Burda id nin uniq ve primarykey-auto increment olmasi icin attribute ediyoruz
//istersek computed olarak yani formattan gecirilerek getirilmesini saglayabiliriz
//Artik Biz veritabaninina initial olarak ekledigmiz data larin id kolonlarini 
//silebiliriz cunku otomatik olarak id yi kendisi atayacak
    public class Book {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Title {get; set;}
        public int GenreId {get; set;}
        public int PageCount {get; set;}
        public DateTime PublishDate {get; set;}
    }
}