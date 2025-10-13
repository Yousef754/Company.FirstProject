using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.Dto_Employee
{
    public class CreateEmployeeDto
    {

        [Required(ErrorMessage ="Name is Requiered")]
        public string Name { get; set; }

        [Range(22,60,ErrorMessage ="age must be between 22 and 60")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage ="Email not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "address is Requiered")]

        public string Address { get; set; }
        [Required( ErrorMessage = "Phone number must be 11 digits and start with 01")]
        public int Phone { get; set; }

        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        [DisplayName("Hiring date")]

        public DateTime HiringDate { get; set; }
        [DisplayName("Date  date")]

        public DateTime CreateAt { get; set; }





    }
}
