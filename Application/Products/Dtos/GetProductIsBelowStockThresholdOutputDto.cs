namespace Application.Products.Dtos
{
    public class GetProductIsBelowStockThresholdOutputDto
    {
        public ProductDetailDto? productDto {  get; set; }
        public List<string>? Errors { get; set; }
    }
    public class ProductDetailDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AmountInStock { get; set; }
        public int MaxAmountInStock { get; set; }
        public int MinAmountInStock { get; set; }
        public bool IsBelowStockThreshold {  get; set; }
    }
}
