using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace VariousTests.WEB.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Подтвердить пароль")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
    }
}