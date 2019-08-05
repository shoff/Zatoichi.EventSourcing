namespace Zatoichi.EventSourcing
{
    using System;

    public interface IEvent
    {
        void Apply();
        int Revision { get; set; }
        string Expression { get; set; }
        DateTime CommitDate { get; set; }
    }
}