using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.UseCases.Companies.DeleteCompany
{
    public class DeleteCompanyCommand(string companyId) : AbstractCommand
    {
        public string CompanyId { get; } = companyId;
    }

    public class DeleteCompanyCommandHandler : AbstractCommandHandler<DeleteCompanyCommand, ResultSet>
    {
        private readonly IDocumentSession documentSession;

        public DeleteCompanyCommandHandler(IDocumentSession documentSession, ILogger<DeleteCompanyCommandHandler> logger) : base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
        }

        public override async Task<ResultSet> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await documentSession.LoadAsync<Company>(request.CompanyId, cancellationToken);

            if (company == null)
            {
                string message = $"Company with id {request.CompanyId} not found.";
                logger.LogError(message);
                return ResultSet.Error(message);
            }

            documentSession.Delete(company);

            await documentSession.SaveChangesAsync(cancellationToken);

            return ResultSet.Success("Company deleted successfully.");
        }
    }
}
