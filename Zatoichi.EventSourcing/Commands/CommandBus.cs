namespace Zatoichi.EventSourcing.Commands
{
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Common.Infrastructure.Resilience;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class CommandBus : ICommandBus
    {
        private readonly IMediator mediator;
        private readonly ILogger<CommandBus> logger;
        private readonly IExecutionPolicies executionPolicies;

        public CommandBus(
            IMediator mediator,
            ILogger<CommandBus> logger,
            IExecutionPolicies executionPolicies)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.executionPolicies = executionPolicies;
            this.logger?.LogTrace("CommandBus created");
        }

        public virtual async Task Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            Guard.IsNotNull(command, nameof(command));
            var result = await this.executionPolicies.ApiExecutionPolicy.ExecuteAndCaptureAsync(()=>this.mediator.Publish(command)).ConfigureAwait(false);
            if (result.Outcome != Polly.OutcomeType.Successful)
            {
                if (result.FinalException != null)
                {
                    this.logger?.LogError(result.FinalException.Message, result.FinalException);
                }
                else if(!string.IsNullOrWhiteSpace(command.Description))
                {
                    this.logger?.LogError(
                        $"Unknown exception occured trying to execute command: {command.Description}");
                }
                else
                {
                    this.logger?.LogError(
                        "Unknown exception occured trying to execute undocumented command.");
                }
            }
        }
    }
}