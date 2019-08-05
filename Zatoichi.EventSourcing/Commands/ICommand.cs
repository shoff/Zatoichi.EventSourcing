namespace Zatoichi.EventSourcing.Commands
{
    using MediatR;

    public interface ICommand : INotification
    {
        string Description { get; set; }
    }
}