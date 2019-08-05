namespace Zatoichi.EventSourcing.Queries
{
    using MediatR;

    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
        string Description { get; set; }
    }
}