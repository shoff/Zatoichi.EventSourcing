namespace Zatoichi.EventSourcing
{
    using System;

    public abstract class Event : IEvent
    {
        public abstract void Apply();

        public virtual void Apply(IEventEntity eventEntity)
        {
            throw new NotImplementedException();
        }
        public int Revision { get; set; } 
        public string Expression { get; set; }
        public DateTime CommitDate { get; set; }
    }
}