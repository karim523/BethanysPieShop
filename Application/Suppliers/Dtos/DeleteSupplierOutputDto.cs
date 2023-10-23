namespace Application.Suppliers.Dtos
{
    public class DeleteSupplierOutputDto
    {
        public int? Id { get; set; }
        public List<string>? Errors { get; set; }
    }
}
