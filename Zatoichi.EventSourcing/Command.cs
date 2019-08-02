namespace Zatoichi.EventSourcing
{
    using System;

    public abstract class Command<T>
    {
        public abstract void Apply(ref T t);
        public string Expression { get; set; }
        public DateTime CommitDate { get; set; }
    }
}