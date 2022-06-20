
using System;
using FluentValidation;
using WebApi.BookOperatins.CreateBook;

namespace WebApi.BookOperations.CreateBook {

    public class CreateBookCommandValidator: AbstractValidator<CreateBookCommand>
     { 
            public CreateBookCommandValidator()
            {
                RuleFor(command=>command.Model.GenreId).NotEqual(0).GreaterThan(0);
                RuleFor(command=>command.Model.PageCount).GreaterThan(0);
                RuleFor(command=>command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
                // PublisDate bos olmasin ve bugunden de kucuk olsun, yani gecmiste olmali
                RuleFor(command=>command.Model.Title).NotEmpty().MinimumLength(4);//En az 4 karakter olsun
            }
       
      }
}