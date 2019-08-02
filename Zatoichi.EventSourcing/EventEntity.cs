namespace Zatoichi.EventSourcing
{
    using System.Threading.Tasks;

    public abstract class EventEntity
    {
        protected readonly IEventStore eventStore;

        protected EventEntity(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        protected virtual Task<T> ApplyAsync<T>()
            where T : EventEntity, new() // TODO remove new constraint
        {
            var snapShot = this.eventStore.GetLatestSnapShot<T>();
            
            // TODO 
            // do
            // {
            //
            //
            // } while()


            return Task.FromResult(new T());
        }
    }
}