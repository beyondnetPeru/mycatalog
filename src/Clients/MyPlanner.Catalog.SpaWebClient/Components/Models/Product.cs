using System.ComponentModel.DataAnnotations;

namespace MyPlanner.Catalog.SpaWebClient.Components.Models
{
    public class Product
    {
        public string Id { get; set; } = default!;
        public string TenantId { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public eCurrency CurrencyValue { get; set; } = default!;
        public double CommercialValue { get; set; } = default!;
        public int Status { get; set; } = default!;
    }

    public enum eProductCategory
    {
        Food,
        Electronics,
        Software,
        Clothing,
        Books,
        Furniture,
        Other
    }

    public enum eCurrency
    {
        USD,
        EUR,
        GBP,
        JPY
    }
}
