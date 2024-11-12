using System.ComponentModel.DataAnnotations;

namespace MyPlanner.Catalog.SpaWebClient.Components.Models
{
    public class Product
    {
        public string Id { get; set; } = default!;
        public string TenantId { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = new();
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public CurrencyEnum CurrencyValue { get; set; } = default!;
        public double CommercialValue { get; set; } = default!;
        public int Status { get; set; } = default!;
    }

    public enum CurrencyEnum
    {
        USD,
        EUR,
        GBP,
        JPY
    }
}
