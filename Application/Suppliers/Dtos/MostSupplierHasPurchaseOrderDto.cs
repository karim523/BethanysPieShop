namespace Application.Suppliers.Dtos
{
    public class MostSupplierHasPurchaseOrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string? Email { get; set; }
        public int Phone { get; set; }
        public int NumberOfTimesUsed { get; set; }
        public List<string>? Errors { get; set; }
    }
}
