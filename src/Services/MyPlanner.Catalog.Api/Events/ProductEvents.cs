using BeyondNet.Ddd;
using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Events
{

    public record ProductCreatedEvent(
            string ProductId,
            string CompanyId,
            string Name,
            List<string> Category,
            string Description,
            string ImageFile,
            double Price) : DomainEvent;

    public record ProductUpdatedEvent(
        string ProductId,
        string CompanyId,
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        double Price) : DomainEvent;

    public record ProductStatusChangedEvent(string ProductId, ProductStatus Status) : DomainEvent;

    public record ProductDeletedEvent(string ProductId) : DomainEvent;
}

