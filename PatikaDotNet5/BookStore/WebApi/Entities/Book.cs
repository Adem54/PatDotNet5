

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities {

//Burda id nin uniq ve primarykey-auto increment olmasi icin attribute ediyoruz
//istersek computed olarak yani formattan gecirilerek getirilmesini saglayabiliriz
//Artik Biz veritabaninina initial olarak ekledigmiz data larin id kolonlarini 
//silebiliriz cunku otomatik olarak id yi kendisi atayacak
    public class Book {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Title {get; set;}
        public int GenreId {get; set;}
        public Genre Genre {get; set;}//Burda bunun foreign key oludgunu soylemis olduk yani
        public int AuthorId {get; set;}
        public Author Author {get; set;}//Foreign key
        public int PageCount {get; set;}
        public DateTime PublishDate {get; set;}
    }
}