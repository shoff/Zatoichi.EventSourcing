namespace Zatoichi.EventSourcing
{
    using System;

    public abstract class Event : IEvent
    {
        public DateTime CommitDate { get; set; }
    }
}