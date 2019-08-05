namespace Zatoichi.EventSourcing.Queries
{
    using System.Threading.Tasks;

    public interface IQueryBus
    {
        Task<TResponse> Send<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
    }
}