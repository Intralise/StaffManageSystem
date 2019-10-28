using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCourseWork.AuthorizationService.Dto
{
    public class EmployeeDto
    {
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Birthsday { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Education { get; set; }
        public string PlaceOfLiving { get; set; }
        public int IdLevel { get; set; }
    }
}
