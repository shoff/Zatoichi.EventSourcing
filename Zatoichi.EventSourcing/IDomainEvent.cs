namespace Zatoichi.EventSourcing
{
    using System;

    public interface IDomainEvent
    {
        string EventType { get; }
        EventVersion Version { get; }
        DateTime CommitDate { get; }
    }
}