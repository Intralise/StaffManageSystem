using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MyCourseWork.AuthorizationService.Entities
{
    public class AuthificationDateEntity
    {
        [Key]
        public int TableId { get; set; }
        public EmployeeEntity Employee { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
