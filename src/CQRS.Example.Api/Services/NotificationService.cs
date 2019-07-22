using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Example.Api.Services
{
    public class NotificationService : INotificationService
    {
        public Action MarkAsStaleRequested { get; set; }

        public void MarkAsStale()
        {
            if (MarkAsStaleRequested == null)
            {
                throw new ArgumentException("The action method has not been configured to handle this method", nameof(MarkAsStaleRequested));
            }
            MarkAsStaleRequested();
        }
    }
}
