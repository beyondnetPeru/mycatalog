namespace MyPlanner.Catalog.Api.Companies.UpdateCompany
{
    public class UpdateCompanyRequest(string Name) : AbstractCommand
    {
        public string Name { get; } = Name;
    }


    public class UpdateCompanyEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/companies/{id}", async (string id, [AsParameters] CompanyServices services, [FromBody] UpdateCompanyRequest request) =>
            {
                var command = new UpdateCompanyCommand(id, request.Name);

                var result = await services.Mediator.Send(command);

                return result.IsSuccess ? Results.Ok() : Results.NotFound();
            })
                .WithTags(ENDPOINT.Tag)
                .WithName(ENDPOINT.UPDATE.Name)
                .Produces<ResultSet>(StatusCodes.Status200OK)
                .Produces<ResultSet>(StatusCodes.Status404NotFound)
                .ProducesValidationProblem()
                .WithSummary(ENDPOINT.UPDATE.Summary)
                .WithDescription(ENDPOINT.UPDATE.Description);
        }
    }
}
