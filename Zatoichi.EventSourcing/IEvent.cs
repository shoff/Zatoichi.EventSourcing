namespace Zatoichi.EventSourcing
{
    using System;

    public interface IEvent
    {
        DateTime CommitDate { get; set; }
    }
}