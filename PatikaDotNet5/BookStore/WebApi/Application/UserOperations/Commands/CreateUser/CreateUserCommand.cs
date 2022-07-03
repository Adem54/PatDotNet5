
using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.CreateUser {
    public class CreateUserCommand
    { 
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserModel Model {get; set;}    
        public CreateUserCommand(IBookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext=dbContext;
            _mapper=mapper;
        }

        public void Handle(){
            //email bizim icin uniq olacagi icin onun uzerinden cek edebiliriz bu kullanici
            //daha onceden kayitli olarak var mi benim, database imde
            var user=_dbContext.Users.SingleOrDefault(u=>u.Email==Model.Email);
            if(user is  not null)throw new InvalidOperationException("Kullanici zaten mevcut.");
            user=_mapper.Map<User>(Model);//Bana bir user objesi ver ve bunu model den maple 
            //MappingProfile da    CreateMap<CreateUserModel,User>(); CreateUserModel i User a cevir diyoruz
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
//BESTPRACTISE BILGI
//Burda gelen password u geldigi gibi database e gonderdik ama bu o sekilde oldugu gibi database de tutulmaz normalde
//gercek projelerde password bir hashleme isleminde gecirildikten sonra sifrelenmis bir sekilde database de tutulur ve
//login islmeinde de kullanicinin gonderidig sifreyi, database de hashlanis halini cagirip onu eski haline dondurup
//o sekilde karsilastirip ona gore kullanicinin onceden register olup olmadigni anlariz....
        public class CreateUserModel 
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }   
            public string Password { get; set; } 
            /*Refresh token ile ilgili alanlar bir token soz konusu oldugunda belli olacak alanlardir
            Token in sonunda uretilecekler, dolayisi ile o zaman set edilecek, yani user i ilk defa 
            olustururken bizim 4 tane bilgiye ihtiyacimiz olacak....
             */   
            // public string RefreshToken {get; set;}
            // public DateTime? RefreshTokenExpireDate {get; set;}

        }
    }
}