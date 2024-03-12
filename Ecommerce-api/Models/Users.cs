namespace Ecommerce_api.Models
{
    public class Users
    {
        public int? Id { get; set; }
        public string? AccountNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public int? District { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }
        public string? Pincode { get; set; }
        public bool isRemoved { get; set; }
    }
}
