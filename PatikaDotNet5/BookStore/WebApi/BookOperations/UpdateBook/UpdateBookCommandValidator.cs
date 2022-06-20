
using System;
using FluentValidation;
using WebApi.BookOperations.UpdateBook;

namespace WebApi.BookOperations.UpdateBook {

    public class UpdateBookCommandValidator: AbstractValidator<UpdateBookCommand>
     { 
            public UpdateBookCommandValidator()
            {
                RuleFor(command=>command.BookId).GreaterThan(0);
                RuleFor(command=>command.Model.GenreId).GreaterThan(0);
                // PublisDate bos olmasin ve bugunden de kucuk olsun, yani gecmiste olmali
                RuleFor(command=>command.Model.Title).NotEmpty().MinimumLength(4);//En az 4 karakter olsun
            }
       
      }
}