namespace Zatoichi.EventSourcing
{
    using System.Threading.Tasks;

    public abstract class EventEntity
    {
        protected virtual Task<T> ApplyAsync<T>(Event @event)
            where T : EventEntity, new() // TODO remove new constraint
        {

            return Task.FromResult(new T());
        }
    }
}