using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Companies.GetCompanyById
{
    public class GetCompanyByIdQuery(string companyId) : AbstractQuery
    {
        public string CompanyId { get; } = companyId;
    }

    public class GetCompanyByIdQueryHandler : AbstractQueryHandler<GetCompanyByIdQuery, ResultSet>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ILogger<GetCompanyByIdQueryHandler> logger;

        public GetCompanyByIdQueryHandler(IDocumentSession documentSession, ILogger<GetCompanyByIdQueryHandler> logger):base(logger)
        {
            _documentSession = documentSession;
            this.logger = logger;
        }

        public override async Task<ResultSet> HandleQuery(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _documentSession.Query<Company>().FirstOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);
            
            if (company == null) {
                string message = $"Company with id {request.Id} not found";
                logger.LogError(message);
                return ResultSet.Error(message);                
            }

            return ResultSet.Success(company);
        }
    }
}
