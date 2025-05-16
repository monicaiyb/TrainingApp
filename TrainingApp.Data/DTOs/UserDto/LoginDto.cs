using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Data.DTOs.UserDto
{
    public class LoginDto
    {
        public class LoginRequest
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public class AuthResponse
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }

        public class RefreshRequest
        {
            [Required]
            public string AccessToken { get; set; }
            [Required]
            public string RefreshToken { get; set; }
        }

    }
}
