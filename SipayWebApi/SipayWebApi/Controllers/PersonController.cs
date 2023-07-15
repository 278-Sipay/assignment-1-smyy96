using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SipayWebApi.Entities;
using SipayWebApi.FluentValidation;

namespace SipayWebApi.Controllers
{

    [ApiController]
    [Route("sipay/api/[controller]")]
    public class PersonController : ControllerBase
    {
        public PersonController() 
        { 

        }

        [HttpPost]
        public Person Post([FromBody] Person person)
        {
            PersonValidator validator = new PersonValidator();

            ValidationResult result = validator.Validate(person);


            return person;
        }
    }
}
