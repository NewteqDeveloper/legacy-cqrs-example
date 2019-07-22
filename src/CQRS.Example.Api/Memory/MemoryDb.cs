using CQRS.Example.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Memory
{
    public class MemoryDb
    {
        private MemoryDb()
        {

        }

        private static MemoryDb instance = null;

        private static readonly object padlock = new object();

        public static MemoryDb Database
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MemoryDb();
                    }

                    return instance;
                }
            }
        }

        public IList<Customer> Customers { get; set; } = new List<Customer>();

        public IList<ShoppingItem> ShoppingItems { get; set; } = new List<ShoppingItem>();

        public IList<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
    }
}
