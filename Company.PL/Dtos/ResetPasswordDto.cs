using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class ResetPasswordDto
    {

        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm password doesn't match the password !!")]
        public string ConfirmPassword { get; set; }
    }
}
