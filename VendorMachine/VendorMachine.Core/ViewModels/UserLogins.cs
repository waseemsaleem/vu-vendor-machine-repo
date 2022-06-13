#nullable disable
using System.ComponentModel.DataAnnotations;

namespace VendorMachine.Core.ViewModels
{
    public class UserLogins
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
