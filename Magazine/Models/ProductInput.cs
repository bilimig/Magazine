namespace Magazine.Models
{
    public class ProductInput
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? UomId { get; set; }
        public string? BaseUnit { get; set; }
        public int? Amount { get; set; }
    }
}
