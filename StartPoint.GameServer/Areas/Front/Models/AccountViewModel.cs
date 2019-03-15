using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartPoint.GameServer.Areas.Front.Models
{
    public class LoginViewModel
    {
        public string LoginRule { get; set; }
        public bool LoginWithFacbook { get; set; }
        public bool LoginWithGoogle { get; set; }
        public bool LoginWithTwitter { get; set; }
    }

    public class InternalLoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class CustomLoginViewModel
    {
        [Required]
        public string PhoneNo { get; set; }
    }
}
