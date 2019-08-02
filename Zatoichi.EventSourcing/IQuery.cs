namespace Zatoichi.EventSourcing
{
    using MediatR;

    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}