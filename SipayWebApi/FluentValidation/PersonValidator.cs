using FluentValidation;
using SipayWebApi.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SipayWebApi.FluentValidation
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {

            RuleFor(command => command.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .WithMessage("Name field must contain a minimum of 3 characters and a maximum of 100 characters.");



            RuleFor(command => command.Lastname)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100)
                .WithMessage("LastName field must contain a minimum of 5 characters and a maximum of 100 characters.");



            RuleFor(command => command.Phone)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(20)
                .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"))
                .WithMessage("PhoneNumber not valid. Enter XXX-XXX-XXXX format.");



            RuleFor(command => command.AccessLevel)
                .NotEmpty()
                .InclusiveBetween(1, 5)
                .WithMessage("You must enter a value between 1 and 5.");




            RuleFor(command => command.Salary)
                .NotEmpty()
                .InclusiveBetween(5000, 50000)
                .Custom((salary, context) =>
            {
                switch (context.InstanceToValidate.AccessLevel)
                {
                    case 1:
                        if (salary > 10000)
                            context.AddFailure("Salary cannot be greater then 10000");
                        break;

                    case 2:
                        if (salary > 20000)
                            context.AddFailure("Salary cannot be greater then 20000");
                        break;
                    case 3:
                        if (salary > 30000)
                            context.AddFailure("Salary cannot be greater then 30000");
                        break;
                    case 4:
                        if (salary > 40000)
                            context.AddFailure("Salary cannot be greater then 40000");
                        break;
                    default:
                        context.AddFailure("Invalid person.");
                        break;
                }
            });



            //Custom dışında Must ile de kullanılıyor. Örnek olsun diye bırakıyorum kod çalışıyor.

            /*RuleFor(person => person)
                .Must(person =>
                {
                    if (person.AccessLevel == 1)
                        return person.Salary <= 10000;

                    if (person.AccessLevel == 2)
                        return person.Salary <= 20000;

                    if (person.AccessLevel == 3)
                        return person.Salary <= 30000;

                    if (person.AccessLevel == 4)
                        return person.Salary <= 40000;

                    return false;
                })
                .WithMessage(person =>
                {
                    if (person.AccessLevel == 1)
                        return "Salary cannot be greater than 10000 for Access Level 1";

                    if (person.AccessLevel == 2)
                        return "Salary cannot be greater than 20000 for Access Level 2";

                    if (person.AccessLevel == 3)
                        return "Salary cannot be greater than 30000 for Access Level 3";

                    if (person.AccessLevel == 4)
                        return "Salary cannot be greater than 40000 for Access Level 4";

                    return "Invalid person.";
                });
            */



        }
    }
}
