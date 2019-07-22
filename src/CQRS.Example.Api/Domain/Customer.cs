using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Domain
{
    public class Customer
    {
        public string CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime SignupDate { get; set; }

        public string Email { get; set; }

        public DateTime Birthdate { get; set; }

        public int Age
        {
            get
            {
                return new DateTime(DateTime.Now.Subtract(Birthdate).Ticks).Year - 1;
            }
        }
    }
}
