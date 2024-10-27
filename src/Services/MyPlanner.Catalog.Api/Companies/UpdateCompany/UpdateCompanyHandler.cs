using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Companies.UpdateCompany
{
    public class UpdateCompanyCommand(string id, string Name) : AbstractCommand
    {
        public string Id { get; } = id;
        public string Name { get; } = Name;
    }

    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }


    public class UpdateCompanyCompanyHandler: AbstractCommandHandler<UpdateCompanyCommand, ResultSet>
    {
        private readonly IDocumentSession documentSession;

        public UpdateCompanyCompanyHandler(IDocumentSession documentSession, ILogger<UpdateCompanyCompanyHandler> logger): base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
            
        }
        public override async Task<ResultSet> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await documentSession.LoadAsync<Company>(request.Id, cancellationToken);

            if (company == null)
            {
               return ResultSet.Error($"Company {{CompanyId}} was not found", request.Id);
            }

            company.Name = request.Name;

            documentSession.Update(company);

            await documentSession.SaveChangesAsync(cancellationToken);

            return ResultSet.Success();
        }
    } 
}
