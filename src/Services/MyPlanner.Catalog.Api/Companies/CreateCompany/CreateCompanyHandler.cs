using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Companies.CreateCompany
{
    public class CreateCompanyCommad : AbstractCommand
    {
        public string Name { get; set; }
        public CreateCompanyCommad(string name)
        {
            Name = name;
        }
    }

    public class CreateCompanyCommandValidator: AbstractValidator<CreateCompanyCommad>
    {
        public CreateCompanyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public class CreateCompanyCommandHandler : AbstractCommandHandler<CreateCompanyCommad, ResultSet>
    {
        private readonly IDocumentSession documentSession;
        private readonly IValidator<CreateCompanyCommad> validator;

        public CreateCompanyCommandHandler(IDocumentSession documentSession,
                                               ILogger<CreateCompanyCommandHandler> logger,
                                               IValidator<CreateCompanyCommad> validator) : base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }


        public override async Task<ResultSet> Handle(CreateCompanyCommad command, CancellationToken cancellationToken)
        {
            var validationErrors = validator.Validate(command);

            if (validationErrors.Errors.Any())
            {
                string message = $"Error trying create a company. Errors:{validationErrors.Errors.ToString()}";
                logger.LogError(message);
                return ResultSet.Error(message);
            }

            var company = new Company
            {
                Name = command.Name
            };

            documentSession.Store(company);

            await documentSession.SaveChangesAsync(cancellationToken);

            return ResultSet.Success(company);
        }
    }
}
