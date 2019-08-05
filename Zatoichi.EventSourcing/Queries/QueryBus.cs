namespace Zatoichi.EventSourcing.Queries
{
    using System.Threading.Tasks;
    using ChaosMonkey.Guards;
    using Common.Infrastructure.Resilience;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class QueryBus : IQueryBus
    {
        private readonly IMediator mediator;
        private readonly ILogger<QueryBus> logger;
        private readonly IExecutionPolicies executionPolicies;

        public QueryBus(
            IMediator mediator,
            ILogger<QueryBus> logger,
            IExecutionPolicies executionPolicies)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.executionPolicies = executionPolicies;
        }

        public virtual async Task<TResponse> Send<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
        {
            Guard.IsNotNull(query, nameof(query));
            var result = await this.executionPolicies.ApiExecutionPolicy.ExecuteAndCaptureAsync(()=>this.mediator.Send(query)).ConfigureAwait(false);

            if (result.Outcome != Polly.OutcomeType.Successful)
            {
                if (result.FinalException != null)
                {
                    this.logger?.LogError(result.FinalException.Message, result.FinalException);
                }
                else if (!string.IsNullOrWhiteSpace(query.Description))
                {
                    this.logger?.LogError(
                        $"Unknown exception occured trying to execute query: {query.Description}");
                }
                else
                {
                    this.logger?.LogError(
                        "Unknown exception occured trying to execute undocumented query.");
                }
            }

            return result.Result;
        }
    }
}