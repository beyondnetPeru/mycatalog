using MyPlanner.Catalog.SpaWebClient.Components.Models;

public static class ProductsRepository
{
    private static List<Product> Products = new List<Product>()
    {
        new Product()
        {
            Id = Guid.NewGuid().ToString(),
            TenantId = "47495574-c357-4113-85bc-eb4c5c8d04aa",
            Name = "UMS",
            Description = "User Management System.",
            ImageFile = "product-1.png",
            CommercialValue = 950.00,
            CurrencyValue = CurrencyEnum.EUR,
            Category = new List<string> { "Common System" }
        },
        new Product()
        {
            Id = Guid.NewGuid().ToString(),
            TenantId = "47495574-c357-4113-85bc-eb4c5c8d04aa",
            Name = "MMS",
            Description = "Master Data Management System.",
            ImageFile = "product-2.png",
            CommercialValue = 950.00,
            CurrencyValue = CurrencyEnum.EUR,
            Category = new List<string> { "Common System" }
        },
        new Product()
        {
            Id = Guid.NewGuid().ToString(),
            TenantId = "47495574-c357-4113-85bc-eb4c5c8d04aa",
            Name = "TMS",
            Description = "Transportation Management System",
            ImageFile = "product-3.png",
            CurrencyValue = CurrencyEnum.USD,
            CommercialValue = 950.00,
            Category = new List<string> { "Supply Chain System" }
        },
        new Product()
        {
            Id = Guid.NewGuid().ToString(),
            TenantId = "c418de2d-da3a-4485-b3ee-584496a61a0d",
            Name = "WMS",
            Description = "Warehouse Management System.",
            ImageFile = "product-4.png",
            CommercialValue = 950.00,
            CurrencyValue = CurrencyEnum.USD,
            Category = new List<string> { "Supply Management System" }
        },
        new Product()
        {
            Id = Guid.NewGuid().ToString(),
            TenantId = "c418de2d-da3a-4485-b3ee-584496a61a0d",
            Name = "DMS",
            Description = "Document Management System.",
            ImageFile = "product-5.png",
            CommercialValue = 950.00,
            CurrencyValue = CurrencyEnum.USD,
            Category = new List<string> { "Common System" }
        },
        new Product()
        {
            Id = Guid.NewGuid().ToString(),
            TenantId = "c418de2d-da3a-4485-b3ee-584496a61a0d",
            Name = "FTMS",
            Description = "Foreign Trade Management System",
            ImageFile = "product-6.png",
            CommercialValue = 950.00,
            CurrencyValue = CurrencyEnum.USD,
            Category = new List<string> { "Supply Management System", "Common System" }
        }
    };

    public static void AddProduct(Product Product)
    {
        var maxId = Products.Max(s => s.Id);
        Product.Id = Guid.NewGuid().ToString();
        Product.TenantId = Guid.NewGuid().ToString();
        Products.Add(Product);
    }

    public static List<Product> GetProducts() => Products;


    public static Product? GetProductById(string id)
    {
        var Product = Products.FirstOrDefault(s => s.Id == id);
        if (Product != null)
        {
            return new Product
            {
                Id = Product.Id,
                TenantId = Product.TenantId,
                Name = Product.Name,
                Description = Product.Description,
                ImageFile = Product.ImageFile,
                CommercialValue = Product.CommercialValue,
                CurrencyValue = Product.CurrencyValue,
                Category = Product.Category
            };
        }
        return null;
    }

    public static void UpdateProduct(string ProductId, Product Product)
    {
        if (ProductId != Product.Id) return;

        var ProductToUpdate = Products.FirstOrDefault(s => s.Id == ProductId);
        if (ProductToUpdate != null)
        {
            ProductToUpdate.TenantId = Product.TenantId;
            ProductToUpdate.Name = Product.Name;
            ProductToUpdate.Description = Product.Description;
            ProductToUpdate.CurrencyValue = Product.CurrencyValue;
            ProductToUpdate.CommercialValue = Product.CommercialValue;
            ProductToUpdate.ImageFile = Product.ImageFile;
            ProductToUpdate.Category = Product.Category;
        }
    }

    public static void DeleteProduct(string ProductId)
    {
        var Product = Products.FirstOrDefault(s => s.Id == ProductId);
        if (Product != null)
        {
            Products.Remove(Product);
        }
    }

    public static List<Product> SearchProducts(string ProductFilter)
    {
        return Products.Where(s => s.Name.Contains(ProductFilter, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}