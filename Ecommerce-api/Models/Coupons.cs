namespace Ecommerce_api.Models
{
    public class Coupons
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ValidUpto { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
