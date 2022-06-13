#nullable disable


namespace VendorMachine.Core.ViewModels
{
    public class UserVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Deposit { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
