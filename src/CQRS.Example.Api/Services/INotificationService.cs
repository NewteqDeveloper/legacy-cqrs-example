using System;

namespace CQRS.Example.Api.Services
{
    public interface INotificationService
    {
        Action MarkAsStaleRequested { get; set; }
        void MarkAsStale();
    }
}