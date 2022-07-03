

using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.UserOperations.Commands.CreateUser {
    public class CreateUserCommand {
        private readonly BookSellerDbContext _dbContext;
        public CreateUserModel Model {get; set;}
        public CreateUserCommand(BookSellerDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public void Handle()
        {
             var user=_dbContext.Users.SingleOrDefault(user=>user.Email==Model.Email);
            if (user is not null)
            {
                throw new InvalidOperationException("Kullanici zaten mevcut");
            }
            else
            {
                User newUser = new User();
                newUser.FirstName = Model.FirstName;
                newUser.LastName = Model.LastName;
                newUser.Email = Model.Email;
                newUser.Password = Model.Password;
                _dbContext.Add(newUser);
                _dbContext.SaveChanges();
            }
        }

       public class  CreateUserModel
       {
            public string FirstName { get; set; }   
            public string LastName { get; set; }
            public string Email  {get; set;}
            public string Password {get; set;} 
       }
    }
}