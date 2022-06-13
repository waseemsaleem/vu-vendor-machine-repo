#nullable disable

namespace VendorMachine.Core.Models
{
    public class ProductModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int AmountAvailable { get; set; }
        public int Cost { get; set; }

        public string SellerId { get; set; }
        public UserModel Seller { get; set; }
    }
}
