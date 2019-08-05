namespace Zatoichi.EventSourcing
{
    using System;

    public abstract class Event : IEvent
    {
        public abstract T Apply<T>(T t);
        public int Revision { get; set; } 
        public string Expression { get; set; }
        public DateTime CommitDate { get; set; }
    }
}