namespace Magazine.Models
{
    public class UserInput
    {
        public int Id { get; set; }
        public int? DetailsId { get; set; }
        public string? Password { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
