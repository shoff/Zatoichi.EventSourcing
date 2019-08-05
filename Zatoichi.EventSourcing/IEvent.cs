namespace Zatoichi.EventSourcing
{
    using System;

    public interface IEvent
    {
        T Apply<T>(T t);
        int Revision { get; set; }
        string Expression { get; set; }
        DateTime CommitDate { get; set; }
    }
}