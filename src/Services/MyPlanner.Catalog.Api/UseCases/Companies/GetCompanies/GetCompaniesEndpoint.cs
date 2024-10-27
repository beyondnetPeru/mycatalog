using MyCatalog.Shared.Models.Pagination;
using MyPlanner.Catalog.Api.Models;
using MyPlanner.Catalog.Api.UseCases.Companies;

namespace MyPlanner.Catalog.Api.UseCases.Companies.GetCompanies
{
    public record GetCompanyResponse(IEnumerable<Company> Companies);

    public class GetCompaniesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/companies/", async ([FromBody] PaginationQuery paginationRequest, [AsParameters] CompanyServices services) =>
            {
                var query = paginationRequest.Adapt<GetCompaniesQuery>();

                var response = await services.Mediator.Send(query);

                return Results.Ok(response);
            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.LIST.Name)
                .Produces<IEnumerable<Company>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary(ENDPOINT.LIST.Summary)
                .WithDescription(ENDPOINT.LIST.Description);
        }
    }
}
