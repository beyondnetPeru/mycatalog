using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.UseCases.Companies.GetCompanies
{
    public class GetCompaniesQuery(int? PageNumber, int? PageSize) : AbstractQuery
    {
        public int? PageNumber { get; } = PageNumber;
        public int? PageSize { get; } = PageSize;
    }

    public class GetCompaniesQueryHandler : AbstractQueryHandler<GetCompaniesQuery, ResultSet>
    {
        private readonly IDocumentSession documentSession;
        private readonly ILogger<GetCompaniesQueryHandler> logger;

        public GetCompaniesQueryHandler(IDocumentSession documentSession, ILogger<GetCompaniesQueryHandler> logger) : base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<ResultSet> HandleQuery(GetCompaniesQuery query, CancellationToken cancellationToken)
        {
            var companies = await documentSession.Query<Company>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

            return ResultSet.Success(companies);
        }
    }
}
