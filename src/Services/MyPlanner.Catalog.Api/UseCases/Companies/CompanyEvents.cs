using BeyondNet.Ddd;

namespace MyPlanner.Catalog.Api.UseCases.Companies
{
    public record CompanyCreatedEvent(string CompanyId, string Name) : DomainEvent;
    public record CompanyUpdatedEvent(string CompanyId, string Name) : DomainEvent;
    public record CompanyDeletedEvent(string CompanyId) : DomainEvent;
}
