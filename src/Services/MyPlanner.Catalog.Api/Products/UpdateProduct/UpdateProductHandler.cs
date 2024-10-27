using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.Products.UpdateProduct
{
    public class UpdateProductCommand(string companyId, string producId, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : AbstractCommand
    {
        public string CompanyId { get; } = companyId;
        public string ProducId { get; } = producId;
        public string Name { get; } = Name;
        public List<string> Category { get; } = Category;
        public string Description { get; } = Description;
        public string ImageFile { get; } = ImageFile;
        public decimal Price { get; } = Price;
    }

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.CompanyId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ImageFile).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
    public class UpdateProductCommandHandler : AbstractCommandHandler<UpdateProductCommand, ResultSet>
    {
        private readonly IDocumentSession documentSession;

        public UpdateProductCommandHandler(IDocumentSession documentSession, ILogger<UpdateProductCommandHandler> logger) : base(logger)
        {
            this.documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
        }

        public override async Task<ResultSet> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await documentSession.LoadAsync<Product>(request.Id, cancellationToken);

            if (product == null)
            {
                return ResultSet.Error($"Product {request.Id} was not found");
            }

            product.CompanyId = request.CompanyId;
            product.Name = request.Name;
            product.Category = request.Category;
            product.Description = request.Description;
            product.ImageFile = request.ImageFile;
            product.Price = request.Price;

            documentSession.Update(product);

            await documentSession.SaveChangesAsync(cancellationToken);

            return ResultSet.Success("Product updated sucessfully");
        }
    }
}
