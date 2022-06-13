#nullable disable

namespace VendorMachine.Core.ViewModels
{
    public class ProductVM
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int AmountAvailable { get; set; }
        public int Cost { get; set; }

        public string SellerId { get; set; }
    }
}
