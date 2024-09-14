using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserSystem.Application.Contracts.UserApp.Dtos
{
    public class UserCreateDto
    {
        [Required]
        public string UserNo { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(2)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "email不能为空")]
        public string Email { get; set; }
    }
}