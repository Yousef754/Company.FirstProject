using AutoMapper;
using Company.DAL.Model;
using Company.PL.Dto_Employee;

namespace Company.PL.Mapping
{
    public class EmployeeProfile:Profile
    {

        public EmployeeProfile()
        {

            CreateMap<CreateEmployeeDto, Employee>();

        }


    }
}
