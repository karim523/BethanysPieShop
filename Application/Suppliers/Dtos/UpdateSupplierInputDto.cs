namespace Application.Suppliers.Dtos
{
    public class UpdateSupplierInputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public int Phone { get; set; }
        public string? Email { get; set; }
    }
}
