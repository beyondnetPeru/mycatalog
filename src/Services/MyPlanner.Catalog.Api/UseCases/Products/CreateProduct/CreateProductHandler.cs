﻿using MyPlanner.Catalog.Api.Models;

namespace MyPlanner.Catalog.Api.UseCases.Products.CreateProduct
{
    public class CreateProductCommand : AbstractCommand
    {
        public string TenantId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public int CurrencyValue { get; set; } = default!;
        public double Commertialvalue { get; set; } = default!;
        public int Status { get; set; } = default;
    }
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ImageFile).NotEmpty();
            RuleFor(x => x.Commertialvalue).GreaterThan(0);
        }
    }

    public class CreateProductCommandHandler : AbstractCommandHandler<CreateProductCommand, ResultSet>
    {
        private readonly IDocumentSession _documentSession;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IValidator<CreateProductCommand> _validator;

        public CreateProductCommandHandler(IDocumentSession documentSession,
                                           ILogger<CreateProductCommandHandler> logger,
                                           IValidator<CreateProductCommand> validator) : base(logger)
        {
            _documentSession = documentSession;
            _logger = logger;
            _validator = validator;
        }

        public override async Task<ResultSet> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            var validationErrors = _validator.Validate(command);

            if (validationErrors.Errors.Any())
                return ResultSet.Error($"Error trying create a product. Errors: {validationErrors.Errors.ToString()}");

            var product = command.Adapt<Product>();

            _documentSession.Store(product);

            await _documentSession.SaveChangesAsync(cancellationToken);

            return ResultSet.Success(product);
        }
    }
}
