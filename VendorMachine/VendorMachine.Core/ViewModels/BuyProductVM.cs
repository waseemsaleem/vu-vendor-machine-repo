#nullable disable

namespace VendorMachine.Core.ViewModels
{
    public class BuyProductVM
    {
        public ProductVM Product { get; set; }
        public int TotalSpent { get; set; }
        public int Change { get; set; }
    }
}
