namespace MyPlanner.Catalog.Api.UseCases.Companies
{
    public class CompanyServices
    {
        public CompanyServices(IMediator mediator)
        {
            Mediator = mediator;
        }

        public IMediator Mediator { get; }
    }
}
