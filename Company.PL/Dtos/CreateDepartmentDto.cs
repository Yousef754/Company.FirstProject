using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dtos
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage ="Code is Required")]
        public string code {  get; set; }

        [Required(ErrorMessage = "Name is Required")]

        public string name { get; set; }

        [Required(ErrorMessage = "Date is Required")]

        public DateTime CreateAt { get; set; }

    }
}
