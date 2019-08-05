namespace Zatoichi.EventSourcing
{
    using System;

    public interface IAggregateId
    {
        Guid RootId { get; }
    }
}