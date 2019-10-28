using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCourseWork.AuthorizationService.Entities
{
    public class EmployeeEntity
    {
        [Key]
        public int IdEmployee { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [DataType(DataType.Date)]
        public string Birthsday { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Education { get; set; }
        public string PlaceOfLiving { get; set; }
        public int IdLevel { get; set; }
        [ForeignKey("IdLevel")]
        public AcceptLevelEntity Level { get; set; }
    }
}
