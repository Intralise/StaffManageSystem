using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCourseWork.AuthorizationService.Entities
{
    public class PhonesEntity
    {
        [Key]
        public int IdTable{ get; set; }
        public EmployeeEntity Employee { get; set; }
        public string StationPhone { get; set; }
        public string MobilePhone { get; set; }
    }
}
