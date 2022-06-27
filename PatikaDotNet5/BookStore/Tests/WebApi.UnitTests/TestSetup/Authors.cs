

using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup {
    public static class Authors {
        public static void AddAuthors(this BookStoreDbContext context)
        {
               context.Authors.AddRange(
                    new Author{
                    FirstName="Margaret",
                    LastName="Atwood",
                    BirthDate=new System.DateTime(1972,03,14)
                    },
                    new Author{
                    FirstName="Jokha",
                    LastName="Alharthi",
                    BirthDate=new System.DateTime(1977,07,04)
                    },
                    new Author{
                    FirstName=" Achuthan",
                    LastName="Namboodri",
                    BirthDate=new System.DateTime(1982,11,18)
                    }  

                        );
        }
    }
}