using BeyondNet.Ddd;

namespace MyPlanner.Catalog.Api.Events
{
    public record CompanyCreatedEvent(string CompanyId, string Name, int status) : DomainEvent;
    public record CompanyUpdatedEvent(string CompanyId, string Name, int status) : DomainEvent;
    public record CompanyDeletedEvent(string CompanyId) : DomainEvent;
}
