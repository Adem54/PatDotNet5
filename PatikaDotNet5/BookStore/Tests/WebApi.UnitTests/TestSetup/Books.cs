using WebApi.DbOperations;
using WebApi.Entities;
using System;
namespace WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        //Aslinda bir exthension mehtod olusturuyoruz....
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
               //Id leri Book.cs entity frameworkunde auto-increment bir sekilde ayarladigmiz 
               //icin artik data eklerken id vermemize gerek yok kendisi yeni data geldiginde 
               //otomatik olarak artacak sekilde id atamasi yapacak
               new Book
               {
                   // Id=1,
                   Title = "Lean StartUp",
                   GenreId = 1,//Personal Growth,
                   AuthorId = 1,
                   PageCount = 200,
                   PublishDate = new DateTime(2010, 05, 23)
               },
               new Book
               {
                   // Id=2,
                   Title = "Herland",
                   GenreId = 2,//Science Fiction,
                   AuthorId = 2,
                   PageCount = 250,
                   PublishDate = new DateTime(2011, 07, 13)
               },
               new Book
               {
                   // Id=3,
                   Title = "Dune",
                   GenreId = 3,//Romance
                   AuthorId = 3,
                   PageCount = 540,
                   PublishDate = new DateTime(2004, 02, 6)
               }
               );
        }
    }
}