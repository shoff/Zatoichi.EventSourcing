namespace Zatoichi.EventSourcing
{
    using System;

    public interface IEvent
    {
        void Apply(IEventEntity eventEntity);
        int Revision { get; set; }
        string Expression { get; set; }
        DateTime CommitDate { get; set; }
    }
}