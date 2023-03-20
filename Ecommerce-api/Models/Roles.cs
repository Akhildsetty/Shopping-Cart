using Microsoft.VisualBasic;

namespace Ecommerce_api.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DateCreated { get; set; }
    }

    public class Role
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Customer = "Customer";

    }
}
