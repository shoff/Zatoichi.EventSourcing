namespace Zatoichi.EventSourcing
{
    using System;

    public interface IDomainEvent
    {
        string Body { get; }
        string EventType { get; }
        EventVersion Version { get; }
        DateTime CommitDate { get; }
    }
}