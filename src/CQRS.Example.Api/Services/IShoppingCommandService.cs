using CQRS.Example.Api.Domain;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Services
{
    public interface IShoppingCommandService
    {
        Task AddNewCustomer(Customer customer);
    }
}