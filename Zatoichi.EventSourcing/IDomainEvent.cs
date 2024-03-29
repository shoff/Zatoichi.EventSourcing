﻿namespace Zatoichi.EventSourcing
{
    using System;

    public interface IDomainEvent
    {
        string Body { get; set; }
        string EventType { get; }
        EventVersion Version { get; }
        DateTime CommitDate { get; }
    }
}