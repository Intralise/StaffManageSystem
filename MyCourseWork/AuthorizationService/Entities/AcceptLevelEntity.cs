using System.ComponentModel.DataAnnotations;

namespace MyCourseWork.AuthorizationService.Entities
{
    public class AcceptLevelEntity
    {
        [Key]
        public int IdLevel { get; set; }
        public string Level { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
