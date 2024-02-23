using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace webapi_test211223.ViewModels
{
    public class LoginRequest
    {
        [Required]
        
        public string? sUserName { get; set; }
        [Required]
        public string? sPassword { get; set; }
        [DisplayName("Remember Me?")]
        public bool RememberMe { get; set; }
    }
}
