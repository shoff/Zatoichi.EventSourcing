namespace Zatoichi.EventSourcing
{
    using System;

    public interface IIntegrationEventId
    {
        Guid Id { get; }
    }
}