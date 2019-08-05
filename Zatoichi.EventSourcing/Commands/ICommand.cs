namespace Zatoichi.EventSourcing.Commands
{
    using MediatR;

    public interface ICommand : IRequest
    {
        string Description { get; set; }
    }
}