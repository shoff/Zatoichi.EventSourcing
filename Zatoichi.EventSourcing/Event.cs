namespace Zatoichi.EventSourcing
{
    using System;

    public abstract class Event : IEvent
    {
        public abstract void Apply(IEventEntity eventEntity);
        public int Revision { get; set; } 
        public string Expression { get; set; }
        public DateTime CommitDate { get; set; }
    }
}