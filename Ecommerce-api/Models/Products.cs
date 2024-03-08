namespace Ecommerce_api.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int CategeoryId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }   
        public float Price { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
