using System;

#nullable disable


namespace VendorMachine.Core.ViewModels
{
    public class UserTokens
    {
        public string Token { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
