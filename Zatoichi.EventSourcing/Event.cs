namespace Zatoichi.EventSourcing
{
    using System;

    public abstract class Event<T>
    {
        public int Revision { get; set; } // TODO this should be generated somehow
        public string Expression { get; set; }
        public DateTime CommitDate { get; set; }
    }
}