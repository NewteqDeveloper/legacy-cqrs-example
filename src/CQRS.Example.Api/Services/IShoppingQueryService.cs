using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Example.Api.Domain;

namespace CQRS.Example.Api.Services
{
    public interface IShoppingQueryService
    {
        IList<Customer> GetAllCustomers();
    }
}